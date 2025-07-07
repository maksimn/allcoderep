import random
import numpy as np
import matplotlib.pyplot as plt
import json

with open("boston_subset.json", "r") as iofile:
    dataset = json.load(iofile)
feature_matrix = np.array(dataset["data"])
targets = np.array(dataset["target"])
