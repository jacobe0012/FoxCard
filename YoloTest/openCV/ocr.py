import cv2
import numpy as np
from paddleocr import PaddleOCR

# Step 1: 使用OpenCV模板匹配
def template_matching(image_path, template_path):
    # 加载图片和模板
    img = cv2.imread(image_path)
    template = cv2.imread(template_path)
    
    # 获取模板的尺寸（动态获取）
    w, h = template.shape[1], template.shape[0]
    
    # 使用模板匹配方法
    res = cv2.matchTemplate(img, template, cv2.TM_CCOEFF_NORMED)
    
    # 获取匹配结果中的位置
    threshold = 0.8  # 匹配的阈值
    loc = np.where(res >= threshold)
    
    # 绘制矩形框标记模板的位置
    for pt in zip(*loc[::-1]):
        cv2.rectangle(img, pt, (pt[0] + w, pt[1] + h), (0, 255, 0), 2)
        
    # 显示结果
    cv2.imshow('Detected', img)
    cv2.waitKey(0)
    cv2.destroyAllWindows()
    
    # 返回匹配位置的坐标和模板大小
    return loc, w, h

# Step 2: 使用PaddleOCR识别文字
def recognize_text_in_region(image_path, loc, template_width, template_height):
    # 加载图片
    img = cv2.imread(image_path)
    
    # 假设loc为模板匹配位置的坐标，切割区域
    x, y = loc[0][0], loc[1][0]
    region = img[y:y + template_height, x:x + template_width]
    
    # 使用PaddleOCR识别该区域的文字
    ocr = PaddleOCR(use_angle_cls=True, lang='ch')  # 设置中文识别
    result = ocr.ocr(region, cls=True)
    
    # 打印识别结果
    for line in result[0]:
        print(f"Detected text: {line[1]}")

# 使用示例
image_path = 'D:\gitProject\FoxCard\YoloTest\openCV\invitepic.png'  # 输入图片路径
template_path = 'D:\gitProject\FoxCard\YoloTest\openCV\invite.png'  # 输入模板路径

# 1. 模板匹配
loc, template_width, template_height = template_matching(image_path, template_path)

# 2. OCR识别
if loc[0].size > 0:  # 确保有匹配区域
    recognize_text_in_region(image_path, loc, template_width, template_height)

