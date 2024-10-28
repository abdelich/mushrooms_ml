import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.tree import DecisionTreeClassifier
from sklearn.preprocessing import LabelEncoder
from sklearn.metrics import accuracy_score
import pickle
import os

data_file_path = os.path.join('..', 'Data', 'mushrooms.csv')

data = pd.read_csv(data_file_path, usecols=lambda column: column != 'Unnamed: 0')
print(data.head())

label_encoder = LabelEncoder()
for column in data.columns:
    data[column] = label_encoder.fit_transform(data[column])

X = data.drop('poisonous', axis=1)
y = data['poisonous']

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

model = DecisionTreeClassifier()
model.fit(X_train, y_train)

y_pred = model.predict(X_test)
accuracy = accuracy_score(y_test, y_pred)
print(accuracy)

with open('mushroom_classifier.pkl', 'wb') as model_file:
    pickle.dump(model, model_file)
