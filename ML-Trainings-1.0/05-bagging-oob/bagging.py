import numpy as np

class SimplifiedBaggingRegressor:
    def __init__(self, num_bags, oob=False):
        self.num_bags = num_bags
        self.oob = oob
        
    def _generate_splits(self, data: np.ndarray):
        '''
        Generate indices for every bag and store in self.indices_list list
        '''
        data_length = len(data)
        self.indices_list = np.empty((self.num_bags, data_length))
        
        for bag in range(self.num_bags):
            self.indices_list[bag] = np.random.randint(0, data_length, data_length)
        
    def fit(self, model_constructor, data, target):
        '''
        Fit model on every bag.
        Model constructor with no parameters (and with no ()) is passed to this function.
        
        example:
        
        bagging_regressor = SimplifiedBaggingRegressor(num_bags=10, oob=True)
        bagging_regressor.fit(LinearRegression, X, y)
        '''
        self.data = None
        self.target = None
        self._generate_splits(data)
        assert len(set(list(map(len, self.indices_list)))) == 1, 'All bags should be of the same length!'
        assert list(map(len, self.indices_list))[0] == len(data), 'All bags should contain `len(data)` number of elements!'
        self.models_list = []
        for bag in range(self.num_bags):
            model = model_constructor()
            data_bag = [data[int(ind)] for ind in self.indices_list[bag]]
            target_bag = [target[int(ind)] for ind in self.indices_list[bag]]
            self.models_list.append(model.fit(data_bag, target_bag)) # store fitted models here
        if self.oob:
            self.data = data
            self.target = target
        
    def predict(self, data):
        '''
        Get average prediction for every object from passed dataset
        '''
        # Your code here
        return [self._predictValue(value) for value in data]

    def _predictValue(self, value):
        '''
        Get average prediction for a single object
        '''
        values = [model.predict(value.reshape(1, -1)) for model in self.models_list]

        return np.mean(values)
    
    def _get_oob_predictions_from_every_model(self):
        '''
        Generates list of lists, where list i contains predictions for self.data[i] object
        from all models, which have not seen this object during training phase
        '''
        list_of_predictions_lists = [[] for _ in range(len(self.data))]
        # Your Code Here
        indices_sets = [{*indices} for indices in self.indices_list]

        for i in range(len(self.data)):
            obj = np.array(self.data[i]).reshape(1, -1)

            for j in range(self.num_bags):
                if not i in indices_sets[j]:
                    prediction = self.models_list[j].predict(obj)
                    list_of_predictions_lists.append(prediction)
        
        self.list_of_predictions_lists = np.array(list_of_predictions_lists, dtype=object)
    
    def _get_averaged_oob_predictions(self):
        '''
        Compute average prediction for every object from training set.
        If object has been used in all bags on training phase, return None instead of prediction
        '''
        self._get_oob_predictions_from_every_model()
        self.oob_predictions = [None for _ in range(len(self.data))]
        
        for i in range(len(self.data)):
            predictions = self.list_of_predictions_lists[i]
            self.oob_predictions[i] = None if not predictions else np.mean(predictions)
        
    def OOB_score(self):
        '''
        Compute mean square error for all objects, which have at least one prediction
        '''
        self._get_averaged_oob_predictions()

        OOB = 0.0

        for i in range(len(self.data)):
            prediction = self.oob_predictions[i]
            y = self.target[i]

            if not (prediction is None):
                OOB += (y - prediction) ** 2

        return OOB
