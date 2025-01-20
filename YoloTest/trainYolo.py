
# 模型配置文件
#model_yaml_path = r"C:\Users\93585\AppData\Local\Programs\Python\Python312\Lib\site-packages\ultralytics\cfg\models\11\yolo11.yaml"
#数据集配置文件
data_yaml_path = r'D:/YoloTest/datasets/mycoco.yaml'
#预训练模型
#pre_model_name = 'yolo11n.pt'


import warnings
warnings.filterwarnings('ignore')
from ultralytics import YOLO
 
if __name__ == '__main__':
    model = YOLO("yolo11n.pt")
    # 如何切换模型版本, 上面的ymal文件可以改为 yolo11s.yaml就是使用的11s,
    # 类似某个改进的yaml文件名称为yolov11-XXX.yaml那么如果想使用其它版本就把上面的名称改为yolov11l-XXX.yaml即可（改的是上面YOLO中间的名字不是配置文件的）！
    model.train(data=data_yaml_path,
                imgsz=640,
                epochs=100,
                device= 0
                )
 
