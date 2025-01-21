
from ultralytics import YOLO
import os

script_path = os.path.abspath(__file__)
dir_path = os.path.dirname(script_path)
data_yaml_path =dir_path + '/datasets/mycoco.yaml'

if __name__ == '__main__':
    model = YOLO("yolo11n.pt")

    model.train(data=data_yaml_path,
                imgsz=640,
                epochs=100,
                device= 0
                )
 
