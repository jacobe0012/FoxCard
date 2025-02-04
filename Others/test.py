from pynput.mouse import Button, Controller, Listener

# 创建鼠标控制对象
mouse_controller = Controller()

# 当按下鼠标侧键时，按下鼠标左键
def on_click(x, y, button, pressed):
    # 检测到鼠标侧键按下
    if button == Button.x1 and pressed:
        print("侧键按下，模拟左键点击")
        mouse_controller.press(Button.left)  # 模拟鼠标左键点击

# 启动监听器
with Listener(on_click=on_click) as listener:
    listener.join()
