import numpy as np
from tqdm.auto import tqdm
from matplotlib import pyplot as plt

from sklearn.tree import DecisionTreeRegressor
from sklearn.linear_model import LinearRegression
from sklearn.datasets import make_regression
from boosting import SimplifiedBoostingRegressor

def overfitting_tests():
    for _ in tqdm(range(10)):
        X = np.random.randn(200, 10)
        y = np.random.normal(0, 1, X.shape[0])
        boosting_regressor = SimplifiedBoostingRegressor()    
        boosting_regressor.fit(DecisionTreeRegressor, X, y, 100, 10, 10)
        assert boosting_regressor.loss_log[-1] < 1e-6, 'Boosting should overfit with many deep trees on simple data!'
        assert boosting_regressor.loss_log[0] > 1e-2, 'First tree loos should be not too low!'    
    print('Overfitting tests done!')

def zero_lr_tests():
    for _ in tqdm(range(10)):
        X = np.random.randn(200, 10)
        y = np.random.normal(0, 1, X.shape[0])
        boosting_regressor = SimplifiedBoostingRegressor()    
        boosting_regressor.fit(DecisionTreeRegressor, X, y, 10, 0., 10)
        predictions = boosting_regressor.predict(X)
        assert all(predictions == 0), 'With zero weight model should predict constant values!'
        assert boosting_regressor.loss_log[-1] == boosting_regressor.loss_log[0], 'With zero weight model should not learn anything new!'
    print('Zero lr tests done!')

def fitting_tests():
    for _ in tqdm(range(10)):
        data, targets = make_regression(1000, 10)
        indices = np.arange(len(data))
        np.random.shuffle(indices)
        data_train, targets_train = data[indices[:700]], targets[indices[:700]]
        data_val, targets_val = data[indices[700:]], targets[indices[700:]]

        train_loss_log = []
        val_loss_log = []

        for depth in range(1, 25):
            boosting_regressor = SimplifiedBoostingRegressor()    

            boosting_regressor.fit(DecisionTreeRegressor, data_train, targets_train, depth, 0.2, 5)
            predictions_train = boosting_regressor.predict(data_train)
            predictions_val = boosting_regressor.predict(data_val)
            train_loss_log.append(np.mean((predictions_train-targets_train)**2))
            val_loss_log.append(np.mean((predictions_val-targets_val)**2))
        
        assert train_loss_log[-2] > train_loss_log[-1] and abs(train_loss_log[-2]/train_loss_log[-1]) < 2, '{}, {}'.format(train_loss_log[-2], train_loss_log[-1])

    plt.plot(range(1, len(train_loss_log)+1), train_loss_log, label='train')
    plt.plot(range(1, len(val_loss_log)+1), val_loss_log, label='val')
    plt.xlabel('Ensemble size')
    plt.ylabel('Error')
    plt.legend()

def first_test():
    x = []
    y = []

    for i in range(11):
        p = i * 0.5
        x.append(p)
        y.append(np.cos(p))

    # plt.plot(x, y)
    # plt.savefig('plt.png')

    boosting_regressor = SimplifiedBoostingRegressor()    
    boosting_regressor.fit(DecisionTreeRegressor, np.array(x).reshape(-1, 1), np.array(y).reshape(-1, 1), 17, 2.0, 10)
    print(boosting_regressor.loss_log)
    # assert boosting_regressor.loss_log[-1] < 1e-6, 'Boosting should overfit with many deep trees on simple data!'
    # assert boosting_regressor.loss_log[0] > 1e-2, 'First tree loos should be not too low!'   

def iteration_test():
    X = np.random.randn(200, 10)
    y = np.random.normal(0, 1, X.shape[0])
    boosting_regressor = SimplifiedBoostingRegressor()    
    boosting_regressor.fit(DecisionTreeRegressor, X, y, 100, 10, 10)
    print(boosting_regressor.loss_log)

# first_test()
# iteration_test()
# overfitting_tests()
zero_lr_tests()
# fitting_tests()
