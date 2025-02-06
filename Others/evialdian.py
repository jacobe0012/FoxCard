from pynput.mouse import Button, Controller, Listener
from pynput.keyboard import Controller as KeyboardController, Listener as KeyboardListener
import threading
import random
import time

# 创建鼠标控制对象和键盘控制对象
mouse_controller = Controller()
keyboard_controller = KeyboardController()

# 控制按下数字 "1" 的标志
keep_pressing = False

def start_typing1():
    while keep_pressing:
        keyboard_controller.press('t')  # 按下 T 键
        #time.sleep(random.uniform(0.02, 0.08))  # 给时间按键反应
        keyboard_controller.release('t')  # 松开数字 1
        time.sleep(random.uniform(0.3, 0.3))  # 随机间隔 2-3 秒

# 模拟按下 "T" 键和开始按数字 "1"
def start_typing():
    # 持续按数字 1
    while keep_pressing:
        keyboard_controller.press('1')  # 按下数字 1
        #time.sleep(random.uniform(0.08, 0.15))  # 给时间按键反应
        keyboard_controller.release('1')  # 松开数字 1
        time.sleep(random.uniform(2, 2))  # 随机间隔 2-3 秒
        


# 停止按数字 "1"
def stop_typing():
    global keep_pressing
    keep_pressing = False  # 停止按数字 1
    keyboard_controller.release('t')  # 按下 T 键

# 当按下鼠标侧键时，启动或停止按数字 "1"
def on_click(x, y, button, pressed):
    global keep_pressing
    if button == Button.x1 and pressed:  # 按下侧键1
        print("侧键1按下，启动按T键并按数字1")
        keep_pressing = True
        threading.Thread(target=start_typing, daemon=True).start()  # 启动一个新的线程开始按键
        threading.Thread(target=start_typing1, daemon=True).start()  # 启动一个新的线程开始按键
    if button == Button.x2 and pressed:  # 按下侧键2
        print("侧键2按下，停止按数字1")
        stop_typing()  # 停止按数字1

# 启动鼠标事件监听器
with Listener(on_click=on_click) as listener:
    listener.join()
