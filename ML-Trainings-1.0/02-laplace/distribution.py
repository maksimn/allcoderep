import numpy as np

class LaplaceDistribution:    
    @staticmethod
    def mean_abs_deviation_from_median(x: np.ndarray):
        '''
        Args:
        - x: A numpy array of shape (n_objects, n_features) containing the data
          consisting of num_train samples each of dimension D.
        '''
        ####
        # Do not change the class outside of this block
        # Your code here
        n = len(x)
        mu = np.median(x)
        mae = 0.0

        for num in x:
            mae += abs(num - mu)

        return mae / n
        ####

    def __init__(self, features):
        '''
        Args:
            feature: A numpy array of shape (n_objects, n_features). Every column represents all available values for the selected feature.
        '''
        ####
        # Do not change the class outside of this block
        if len(features.shape) == 1:
            self.loc = np.median(features)
            self.scale = LaplaceDistribution.mean_abs_deviation_from_median(features)
        elif len(features.shape) == 2:
            columns = features.shape[1]
            medians = []
            maes = []

            for i in range(columns):
                arr = features[:, i]
                median = np.median(arr)
                mae = LaplaceDistribution.mean_abs_deviation_from_median(arr)
                medians.append(median)
                maes.append(mae)

            self.loc = np.array(medians)
            self.scale = np.array(maes)

        else:
            self.loc = None
            self.scale = None
        ####


    def logpdf(self, values):
        '''
        Returns logarithm of probability density at every input value.
        Args:
            values: A numpy array of shape (n_objects, n_features). Every column represents all available values for the selected feature.
        '''
        ####
        # Do not change the class outside of this block
        rows = values.shape[0]
        columns = values.shape[1]
        arr = np.eye(rows, columns)

        for i in range(rows):
            for j in range(columns):
                arr[i][j] = (-1 * np.log(2 * self.scale[j])) - (abs(values[i][j] - self.loc[j]) / self.scale[j])

        return arr
        ####
        
    
    def pdf(self, values):
        '''
        Returns probability density at every input value.
        Args:
            values: A numpy array of shape (n_objects, n_features). Every column represents all available values for the selected feature.
        '''
        return np.exp(self.logpdf(values))
