import pygetwindow as gw
import pyautogui
import numpy as np
import cv2
from PIL import ImageGrab
import time
import random
from pynput.mouse import Button, Controller



click_threshold=0.8
title="Path of Exile 2"

def get_window_position(window_title):
    # 获取窗口对象
    window = gw.getWindowsWithTitle(window_title)
    if window:
        # 获取窗口的左上角坐标和窗口尺寸
        window = window[0]
        return window.left, window.top, window.width, window.height
    else:
        print("未找到窗口:", window_title)
        return None

# 获取PoE窗口
def get_poe_window(title="Path of Exile 2"):
    windows = gw.getWindowsWithTitle(title)
    if windows:
        return windows[0]
    else:
        print(f"未找到标题为 '{title}' 的窗口")
        return None

# 截取PoE窗口的图像
def capture_window(window):
    left, top, right, bottom = window.left, window.top, window.right, window.bottom
    screenshot = ImageGrab.grab(bbox=(left, top, right, bottom))  # 截取窗口区域
    return np.array(screenshot)

# 模板匹配并点击“确定”按钮
def check_and_click_invite(window, template_path, click_threshold=0.6):
    # 截取窗口
    screenshot = capture_window(window)
    # 获取游戏窗口位置
    window_position = get_window_position("Path of Exile 2")
    if not window_position:
        return False

    window_left, window_top, window_width, window_height = window_position
    # 加载模板图像（比如邀请弹窗的“确定”按钮）
    template = cv2.imread(template_path, cv2.IMREAD_GRAYSCALE)
    
    # 获取截图和模板的尺寸
    screenshot_gray = cv2.cvtColor(screenshot, cv2.COLOR_RGB2GRAY)
    h, w = template.shape[:2]
    screenshot_h, screenshot_w = screenshot_gray.shape

    # 如果截图的大小小于模板的大小，则继续循环
    if screenshot_h < h or screenshot_w < w:
        print("截图太小，跳过匹配")
        return False

    # 使用OpenCV模板匹配
    result = cv2.matchTemplate(screenshot_gray, template, cv2.TM_CCOEFF_NORMED)
    min_val, max_val, min_loc, max_loc = cv2.minMaxLoc(result)

    # 如果匹配度超过阈值，模拟点击
    if max_val >= click_threshold:
        print("邀请弹窗检测到，点击确定按钮")
        # 计算按钮的中心位置
        button_center = (max_loc[0] + w // 2, max_loc[1] + h // 2)
        # 计算目标点击位置并添加随机偏移
        target_x = window_left + button_center[0] 
        target_y = window_top + button_center[1] 

        # 加入随机偏移量（模拟人类点击不完全准确）
        random_offset_x = random.randint(-5, 5)  # 随机偏移范围：-5 到 5
        random_offset_y = random.randint(-5, 5)
        target_x += random_offset_x
        target_y += random_offset_y

        # 在屏幕上显示截图并绘制矩形框
        # cv2.rectangle(screenshot, max_loc, (max_loc[0] + w, max_loc[1] + h), (0, 255, 0), 2)
        # cv2.imshow("Detected Invite Button", screenshot)
        # 移动鼠标到目标位置
        mouse = Controller()
        mouse.position = target_x, target_y
        print("点击坐标:",button_center[0], button_center[1])
        # 添加随机延迟，模拟人类操作
        random_delay = random.uniform(0.3, 1.0)  # 随机延迟时间（0.3到1.0秒之间）
        time.sleep(random_delay)

        # 执行左键点击
        mouse.click(Button.left, 1)

        # 等待一段随机时间，模拟人类点击后的反应时间
        time.sleep(random.uniform(0.3, 1.0))

        return True

    return False

# 每3秒检查一次PoE窗口并自动点击“确定”按钮
def monitor_poe_invite(template_path):
    poe_window = get_poe_window(title)
    
    if not poe_window:
        return

    while True:
        # 检测并点击邀请弹窗
        if check_and_click_invite(poe_window, template_path,click_threshold):
            print("邀请弹窗已点击")
        else:
            print("未检测到邀请弹窗或匹配失败")

        time.sleep(3)  # 每3秒检查一次

if __name__ == "__main__":
    # 模板图片路径（请提供邀请弹窗“确定”按钮的截图）
    template_path = "D:\\gitProject\\FoxCard\\YoloTest\\openCV\\pics\\comehome.png"
    monitor_poe_invite(template_path)
