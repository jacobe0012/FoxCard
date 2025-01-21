import os
import time
import pygetwindow as gw
import pyautogui  # 使用 pyautogui 截图
from pynput.keyboard import Listener, Key

# 获取应用程序的窗口句柄
def get_window_handle(window_title):
    windows = gw.getWindowsWithTitle(window_title)
    if windows:
        return windows[0]
    else:
        print(f"没有找到标题为 '{window_title}' 的窗口。")
        return None

# 截取指定窗口的截图
def capture_window(window):
    left, top, right, bottom = window.left, window.top, window.right, window.bottom
    screenshot = pyautogui.screenshot(region=(left, top, right-left, bottom-top))  # 使用 pyautogui
    return screenshot

# 按键监听函数
capturing = False
def on_press(key):
    global capturing
    if key == Key.f7:  # 当按下 F7 键时
        capturing = not capturing  # 切换截屏状态
        print("截图已" + ("暂停" if not capturing else "开始"))

# 指定保存截图的文件夹
def ensure_folder_exists(folder_path):
    if not os.path.exists(folder_path):
        os.makedirs(folder_path)

# 主逻辑：每 10 秒截取一次画面，支持 F7 按键控制暂停和恢复
def capture_game_every_10_seconds(window_title, save_folder):
    window = get_window_handle(window_title)
    if window is None:
        return
    
    # 确保文件夹存在
    ensure_folder_exists(save_folder)
    
    # 启动键盘监听
    listener = Listener(on_press=on_press)
    listener.start()
    
    while True:
        print(f"截图状态: {capturing}")  # 输出当前状态
        if capturing:
            # 执行截图操作
            screenshot = capture_window(window)
            # 保存截图，命名为 timestamp.png
            timestamp = int(time.time())
            screenshot_path = os.path.join(save_folder, f"screenshot_{timestamp}.png")
            print(f"保存路径：{screenshot_path}")  # 输出保存路径
            screenshot.save(screenshot_path)
            print(f"已保存截图: {screenshot_path}")
        
        # 每 10 秒检查一次
        time.sleep(5)

# 运行脚本并指定游戏窗口标题及保存文件夹
if __name__ == "__main__":
    # 替换为你游戏或应用程序的窗口标题
    game_window_title = "Path of Exile 2"  # 例如 "Path of Exile"
    # 替换为你想保存截图的文件夹路径
    save_folder = r".\data\rawimg"  # 例如 "C:\\Users\\YourUsername\\Desktop\\screenshots"
    capture_game_every_10_seconds(game_window_title, save_folder)
