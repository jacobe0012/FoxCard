import pygetwindow as gw
import pyautogui
import numpy as np
import cv2
from PIL import ImageGrab
import time
import os

# 获取PoE窗口
def get_poe_window(title="Path of Exile 2"):
    # 获取所有窗口，查找包含指定标题的窗口
    windows = gw.getWindowsWithTitle(title)
    if windows:
        return windows[0]
    else:
        print(f"未找到标题为 '{title}' 的窗口")
        return None

# 截取指定窗口的图像并保存
def capture_window_and_save(window, save_path="poe_screenshot.png"):
    left, top, right, bottom = window.left, window.top, window.right, window.bottom
    screenshot = ImageGrab.grab(bbox=(left, top, right, bottom))  # 截取窗口区域
    screenshot.save(save_path)  # 保存为图像文件
    print(f"截图已保存: {save_path}")

# 每3秒截取一次PoE窗口并保存
def capture_poe_every_3_seconds():
    poe_window = get_poe_window("Path of Exile 2")
    
    if not poe_window:
        return

    # 创建保存截图的文件夹
    if not os.path.exists("screenshots"):
        os.makedirs("screenshots")

    # 每3秒截取一次窗口并保存
    counter = 1
    while True:
        # 生成保存的文件名，例如 poe_screenshot_1.png
        save_path = f"screenshots/poe_screenshot_{counter}.png"
        capture_window_and_save(poe_window, save_path)

        # 等待3秒
        time.sleep(3)
        counter += 1

if __name__ == "__main__":
    capture_poe_every_3_seconds()
