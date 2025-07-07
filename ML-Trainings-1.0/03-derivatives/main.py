import random
import numpy as np
import matplotlib.pyplot as plt
import json

from derivatives import LossAndDerivatives

with open("boston_subset.json", "r") as iofile:
    dataset = json.load(iofile)
feature_matrix = np.array(dataset["data"])
targets = np.array(dataset["target"])

w = np.array([1.0, 1.0])
x_n, y_n = feature_matrix, targets

# Repeating data to make everything multi-dimentional
w = np.vstack(
    [w[None, :] + 0.27, w[None, :] + 0.22, w[None, :] + 0.45, w[None, :] + 0.1]
).T
y_n = np.hstack([y_n[:, None], 2 * y_n[:, None], 3 * y_n[:, None], 4 * y_n[:, None]])

reference_mse_derivative = np.array(
    [
        [7.32890068, 12.88731311, 18.82128365, 23.97731238],
        [9.55674399, 17.05397661, 24.98807528, 32.01723714],
    ]
)
reference_l2_reg_derivative = np.array([[2.54, 2.44, 2.9, 2.2], [2.54, 2.44, 2.9, 2.2]])

assert np.allclose(
    reference_mse_derivative, LossAndDerivatives.mse_derivative(x_n, y_n, w), rtol=1e-3
), "Something wrong with MSE derivative"

assert np.allclose(
    reference_l2_reg_derivative, LossAndDerivatives.l2_reg_derivative(w), rtol=1e-3
), "Something wrong with L2 reg derivative"

print(
    "MSE derivative:\n{} \n\nL2 reg derivative:\n{}".format(
        LossAndDerivatives.mse_derivative(x_n, y_n, w),
        LossAndDerivatives.l2_reg_derivative(w),
    )
)

reference_mae_derivative = np.array(
    [
        [0.19708867, 0.19621798, 0.19621798, 0.19572906],
        [0.25574138, 0.25524507, 0.25524507, 0.25406404],
    ]
)
reference_l1_reg_derivative = np.array([[1.0, 1.0, 1.0, 1.0], [1.0, 1.0, 1.0, 1.0]])

assert np.allclose(
    reference_mae_derivative, LossAndDerivatives.mae_derivative(x_n, y_n, w), rtol=1e-3
), "Something wrong with MAE derivative"

assert np.allclose(
    reference_l1_reg_derivative, LossAndDerivatives.l1_reg_derivative(w), rtol=1e-3
), "Something wrong with L1 reg derivative"

print(
    "MAE derivative:\n{} \n\nL1 reg derivative:\n{}".format(
        LossAndDerivatives.mae_derivative(x_n, y_n, w),
        LossAndDerivatives.l1_reg_derivative(w),
    )
)

def get_w_by_grad(
    X, Y, w_0, loss_mode="mse", reg_mode=None, lr=0.05, n_steps=100, reg_coeff=0.05
):
    if loss_mode == "mse":
        loss_function = LossAndDerivatives.mse
        loss_derivative = LossAndDerivatives.mse_derivative
    elif loss_mode == "mae":
        loss_function = LossAndDerivatives.mae
        loss_derivative = LossAndDerivatives.mae_derivative
    else:
        raise ValueError(
            "Unknown loss function. Available loss functions: `mse`, `mae`"
        )

    if reg_mode is None:
        reg_function = LossAndDerivatives.no_reg
        reg_derivative = (
            LossAndDerivatives.no_reg_derivative
        )  # lambda w: np.zeros_like(w)
    elif reg_mode == "l2":
        reg_function = LossAndDerivatives.l2_reg
        reg_derivative = LossAndDerivatives.l2_reg_derivative
    elif reg_mode == "l1":
        reg_function = LossAndDerivatives.l1_reg
        reg_derivative = LossAndDerivatives.l1_reg_derivative
    else:
        raise ValueError(
            "Unknown regularization mode. Available modes: `l1`, `l2`, None"
        )

    w = w_0.copy()

    for i in range(n_steps):
        empirical_risk = loss_function(X, Y, w) + reg_coeff * reg_function(w)
        gradient = loss_derivative(X, Y, w) + reg_coeff * reg_derivative(w)
        gradient_norm = np.linalg.norm(gradient)
        if gradient_norm > 5.0:
            gradient = gradient / gradient_norm * 5.0
        w -= lr * gradient

        if i % 25 == 0:
            print(
                "Step={}, loss={},\ngradient values={}\n".format(
                    i, empirical_risk, gradient
                )
            )
    return w

# Initial weight matrix
w = np.ones((2, 1), dtype=float)
y_n = targets[:, None]

w_grad = get_w_by_grad(x_n, y_n, w, loss_mode="mse", reg_mode="l2", n_steps=250)

from sklearn.linear_model import Ridge

lr = Ridge(alpha=0.05)
lr.fit(x_n, y_n)
print(
    "sklearn linear regression implementation delivers MSE = {}".format(
        np.mean((lr.predict(x_n) - y_n) ** 2)
    )
)

plt.scatter(x_n[:, -1], y_n[:, -1])
plt.scatter(
    x_n[:, -1],
    x_n.dot(w_grad)[:, -1],
    color="orange",
    label="Handwritten linear regression",
    linewidth=5,
)
plt.scatter(x_n[:, -1], lr.predict(x_n), color="cyan", label="sklearn Ridge")
plt.legend()
plt.savefig('img1.png')

print('Handwritten MSE =', LossAndDerivatives.mse(x_n, y_n, w))
