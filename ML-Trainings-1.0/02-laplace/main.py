import numpy as np
import pandas as pd
import matplotlib
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn import datasets

matplotlib.rcParams['font.size'] = 11

dataset = datasets.load_iris()

features = dataset.data
target = dataset.target

features.shape, target.shape

# Run some setup code for this notebook.
import numpy as np

from distribution import LaplaceDistribution

import scipy

loc0, scale0 = scipy.stats.laplace.fit(features[:, 0])
loc1, scale1 = scipy.stats.laplace.fit(features[:, 1])

# 1d case
values1 = features[:, 0]

# print(values1)

my_distr_1 = LaplaceDistribution(values1)

# check the 1d median (loc parameter)
assert np.allclose(my_distr_1.loc, loc0), '1d distribution median error'
# check the 1d scale (loc parameter)
assert np.allclose(my_distr_1.scale, scale0), '1d distribution scale error'

# 2d case
value2 = features[:, :2]
my_distr_2 = LaplaceDistribution(value2)

# check the 2d median (loc parameter)
assert np.allclose(my_distr_2.loc, np.array([loc0, loc1])), '2d distribution median error'
# check the 2d median (loc parameter)
assert np.allclose(my_distr_2.scale, np.array([scale0, scale1])), '2d distribution scale error'

print('Seems fine!')

_test = scipy.stats.laplace(loc=[loc0, loc1], scale=[scale0, scale1])

result = my_distr_2.logpdf(features[:5, :2])
excepted = _test.logpdf(features[:5, :2])
    
assert np.allclose(result, excepted), 'Logpdfs do not match scipy results!'

print('Seems fine!')
