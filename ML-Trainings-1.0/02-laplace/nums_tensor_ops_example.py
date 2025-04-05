import numpy as np

nums_1d = np.array([1, 3, 2])
nums_2d = np.array([[1.0, 3.0, 2.0], [0.0, 3.0, 1.2]])
nums_3d = np.array([
    [[1.0, 3.0, 2.0], [0.0, 3.0, 1.2]],
    [[1.1, 3.2, 2.3], [0.1, 3.2, 1.5]]
])

result_1d = np.exp(nums_1d)
result_2d = np.exp(nums_2d)
result_3d = np.exp(nums_3d)

print(result_1d)
