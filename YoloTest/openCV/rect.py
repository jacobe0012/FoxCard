import cv2
import numpy as np

# 用于存储矩形区域的起始点和结束点
start_point = None
end_point = None
rectangles = []  # 存储所有标注的区域
scale_factor=0.5
# 鼠标回调函数，用于拖动并绘制矩形
def draw_rectangle(event, x, y, flags, param):
    global start_point, end_point, rectangles

    if event == cv2.EVENT_LBUTTONDOWN:
        # 鼠标左键按下，记录起始点
        start_point = (x, y)
    elif event == cv2.EVENT_LBUTTONUP:
        # 鼠标左键松开，记录结束点并绘制矩形
        end_point = (x, y)
        rectangles.append((start_point, end_point))
        start_point = None
        end_point = None

        print(f"标注区域：{rectangles[-1]}")  # 输出当前标注的区域坐标

# 载入图像，选择感兴趣的区域
def select_area(image_path, scale_factor=1.0):
    global start_point, end_point, rectangles

    # 载入图像
    image = cv2.imread(image_path)

    # 缩放图像，避免显示不完全
    height, width = image.shape[:2]
    new_width = int(width * scale_factor)
    new_height = int(height * scale_factor)
    image_resized = cv2.resize(image, (new_width, new_height))

    # 创建一个窗口并设置鼠标回调
    cv2.namedWindow("Select Region")
    cv2.setMouseCallback("Select Region", draw_rectangle)

    while True:
        # 显示图像
        img_copy = image_resized.copy()
        
        # 如果存在标注区域，绘制矩形框
        for (start, end) in rectangles:
            cv2.rectangle(img_copy, start, end, (0, 255, 0), 2)

        # 显示当前图像
        cv2.imshow("Select Region", img_copy)

        key = cv2.waitKey(1) & 0xFF
        if key == 27:  # 按Esc键退出
            break

    # 关闭窗口
    cv2.destroyAllWindows()

    # 返回所有标注的区域
    return rectangles

if __name__ == "__main__":
    image_path = "D:\\gitProject\\FoxCard\\YoloTest\\openCV\\rect.png"  # 替换为你想选择区域的图片路径
    regions = select_area(image_path, scale_factor)  # 例如缩放图像为原始尺寸的50%

    if regions:
        print("选择的区域坐标:")
        for idx, region in enumerate(regions):
            print(f"区域 {idx + 1}: {region}")
    else:
        print("未选择任何区域")
