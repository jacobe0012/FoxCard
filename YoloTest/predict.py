import cv2
from PIL import Image

from ultralytics import YOLO

model = YOLO(r".\models\best.pt")
savepath = r".\runs\predict"
# accepts all formats - image/dir/Path/URL/video/PIL/ndarray. 0 for webcam
results = model.predict(source=r".\assets\屏幕录制 2025-01-21 024927.mp4", show=True,save =True,line_width =1,line_thickness=1)


# Process results list
for result in results:
    boxes = result.boxes  # Boxes object for bounding box outputs
    masks = result.masks  # Masks object for segmentation masks outputs
    keypoints = result.keypoints  # Keypoints object for pose outputs
    probs = result.probs  # Probs object for classification outputs
    obb = result.obb  # Oriented boxes object for OBB outputs
    #result.show()  # display to screen
    #result.save(filename=savepath+ result.path)  # save to disk
