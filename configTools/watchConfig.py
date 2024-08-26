import time
import subprocess
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

# 定义文件夹路径和要运行的.bat文件路径
folder_path = r"D:\JiYu\config"  # 替换为您的目标文件夹的实际路径
bat_file_path = r"D:\ConfigTools/gen.bat"  # 替换为您要运行的.bat文件的实际路径

class MyHandler(FileSystemEventHandler):
    def on_any_event(self, event):
        if event.is_directory:
            return
        elif event.event_type in ['created', 'modified', 'deleted']:
            print(f"文件 {event.src_path} 发生了 {event.event_type} 事件.")
            run_bat_file()

def run_bat_file():
    try:
        subprocess.run(bat_file_path, shell=True, check=True)
    except subprocess.CalledProcessError as e:
        print(f"运行 .bat 文件时发生错误: {e}")
    except FileNotFoundError:
        print("指定的 .bat 文件未找到.")

if __name__ == "__main__":
    event_handler = MyHandler()
    observer = Observer()
    observer.schedule(event_handler, path=folder_path, recursive=True)
    observer.start()

    print(f"正在监视文件夹 {folder_path} 变化...")

    try:
        while True:
            time.sleep(20)
    except KeyboardInterrupt:
        observer.stop()

    observer.join()