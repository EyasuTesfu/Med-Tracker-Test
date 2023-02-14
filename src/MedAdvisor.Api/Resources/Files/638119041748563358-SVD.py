# Authors: Gemechis, Zeaman, Geach
# Date: 10/10/2019
# Description: This program performs SVD on the iris dataset.
import matplotlib.pyplot as plt
from sklearn.preprocessing import StandardScaler
import numpy as np
import pandas as pd
from sklearn import datasets
#  Load the iris dataset
iris = datasets.load_iris()
# Create a dataframe from the iris dataset
iris_df = pd.DataFrame(data=np.c_[
                       iris['data'], iris['target']], columns=iris['feature_names'] + ['target'])

# Print the first 5 rows of the dataframe
print(iris_df.head())
# Print the shape of the dataframe
print(iris_df.shape)
# Print the column names of the dataframe
print(iris_df.columns)
# Print the data types of the dataframe
print(iris_df.dtypes)
# Print the summary statistics of the dataframe
print(iris_df.describe())

# Create a StandardScaler object
scaler = StandardScaler()
# Fit the scaler to the dataframe
scaled_data = scaler.fit_transform(iris_df.drop('target', axis=1))

# Perform SVD on the scaled data
U, sigma, V = np.linalg.svd(scaled_data)
# Print the U matrix, sigma matrix and V matrix
print("U matrix:", U)
print("Sigma matrix:", sigma)
print("V matrix:", V)

# Plot the first two principal components
# Create a matrix of the first two principal components
u_pca = U[:, :2]
sigma_pca = np.diag(sigma[:2])
x_pca = u_pca.dot(sigma_pca)

# Plot the transformed data
plt.scatter(x_pca[:, 0], x_pca[:, 1])
plt.xlabel('First Principal Component')
plt.ylabel('Second Principal Component')
plt.show()
