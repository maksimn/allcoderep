import numpy as np
import pandas as pd
import matplotlib
import matplotlib.pyplot as plt
import seaborn as sns
from sklearn import datasets

matplotlib.rcParams['font.size'] = 11

dataset = datasets.load_iris()

# print(dataset.DESCR)

features = dataset.data
target = dataset.target

features.shape, target.shape

class GaussianDistribution:
    def __init__(self, feature):
        '''
        Args:
            feature: column of design matrix, represents all available values
                of feature to model.
                axis=0 stays for samples.
        '''
        self.mean = feature.mean(axis=0)
        self.std = feature.std(axis=0)

    def logpdf(self, value):
        '''Logarithm of probability density at value'''
        return - 0.5 * np.log(2. * np.pi * self.std**2) - (value - self.mean)**2 / (2 * self.std**2) # <YOUR CODE HERE>
    
    def pdf(self, value):
        return np.exp(self.logpdf(value)) # <YOUR CODE HERE>

import scipy

_test = scipy.stats.norm(loc=features[:, :2].mean(axis=0), scale=features[:, :2].std(axis=0))
assert np.allclose(
    GaussianDistribution(features[:, :2]).logpdf(features[:5, :2]),
    _test.logpdf(features[:5, :2])
)
print('Seems fine!')
