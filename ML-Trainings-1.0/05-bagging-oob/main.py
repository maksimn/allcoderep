import numpy as np
from bagging import SimplifiedBaggingRegressor
from sklearn.linear_model import LinearRegression
from tqdm.auto import tqdm

def simple_tests():
    for _ in tqdm(range(100)):
        X = np.random.randn(2000, 10)
        y = np.mean(X, axis=1)
        bagging_regressor = SimplifiedBaggingRegressor(num_bags=10, oob=True)
        bagging_regressor.fit(LinearRegression, X, y)
        predictions = bagging_regressor.predict(X)
        assert np.mean((predictions - y)**2) < 1e-6, 'Linear dependency should be fitted with almost zero error!'
        assert bagging_regressor.oob, 'OOB feature must be turned on'
        oob_score = bagging_regressor.OOB_score()
        assert oob_score < 1e-6, 'OOB error for linear dependency should be also close to zero!'

simple_tests()
