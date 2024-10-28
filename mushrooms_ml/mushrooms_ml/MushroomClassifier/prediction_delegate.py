import pandas as pd
import joblib
from sklearn.preprocessing import LabelEncoder
import sys
import os

script_dir = os.path.dirname(__file__)
model_path = os.path.join(script_dir, 'mushroom_classifier.pkl')

model = joblib.load(model_path)

columns = [
    "cap-shape", "cap-surface", "cap-color", "bruises", "odor", "gill-attachment",
    "gill-spacing", "gill-size", "gill-color", "stalk-shape", "stalk-root",
    "stalk-surface-above-ring", "stalk-surface-below-ring", "stalk-color-above-ring",
    "stalk-color-below-ring", "veil-type", "veil-color", "ring-number",
    "ring-type", "spore-print-color", "population", "habitat"
]


def encode_data(data):
    label_encoders = {}
    categories = {
        "cap-shape": ['b', 'c', 'x', 'f', 'k', 's'],
        "cap-surface": ['f', 'g', 'y', 's'],
        "cap-color": ['n', 'b', 'c', 'g', 'r', 'p', 'u', 'e', 'w', 'y'],
        "bruises": ['t', 'f'],
        "odor": ['a', 'l', 'c', 'y', 'f', 'm', 'n', 'p', 's'],
        "gill-attachment": ['a', 'd', 'f', 'n'],
        "gill-spacing": ['c', 'w', 'd'],
        "gill-size": ['b', 'n'],
        "gill-color": ['k', 'n', 'b', 'h', 'g', 'r', 'o', 'p', 'u', 'e', 'w', 'y'],
        "stalk-shape": ['e', 't'],
        "stalk-root": ['b', 'c', 'u', 'e', 'z', 'r', '?'],
        "stalk-surface-above-ring": ['f', 'y', 'k', 's'],
        "stalk-surface-below-ring": ['f', 'y', 'k', 's'],
        "stalk-color-above-ring": ['n', 'b', 'c', 'g', 'o', 'p', 'e', 'w', 'y'],
        "stalk-color-below-ring": ['n', 'b', 'c', 'g', 'o', 'p', 'e', 'w', 'y'],
        "veil-type": ['p', 'u'],
        "veil-color": ['n', 'o', 'w', 'y'],
        "ring-number": ['n', 'o', 't'],
        "ring-type": ['c', 'e', 'f', 'l', 'n', 'p', 's', 'z'],
        "spore-print-color": ['k', 'n', 'b', 'h', 'r', 'o', 'u', 'w', 'y'],
        "population": ['a', 'c', 'n', 's', 'v', 'y'],
        "habitat": ['g', 'l', 'm', 'p', 'u', 'w', 'd']
    }

    encoded_data = []
    for i, column in enumerate(columns):
        le = LabelEncoder()
        le.fit(categories[column])
        label_encoders[column] = le
        encoded_value = le.transform([data[i]])[0]
        encoded_data.append(encoded_value)

    return pd.DataFrame([encoded_data], columns=columns)


data = sys.argv[1].split(",")

encoded_data = encode_data(data)

prediction = model.predict(encoded_data)
if prediction[0] == 1:
    print('Poisonous')
else:
    print('Edible')
