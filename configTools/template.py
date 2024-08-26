import os
import shutil

def delete_files(folder_path):
    # 删除文件夹下的所有文件
    for filename in os.listdir(folder_path):
        file_path = os.path.join(folder_path, filename)
        if os.path.isfile(file_path):
            os.remove(file_path)

def copy_files(source_folder, destination_folder):
    # 复制文件到目标文件夹
    for filename in os.listdir(source_folder):
        source_file = os.path.join(source_folder, filename)
        destination_file = os.path.join(destination_folder, filename)
        shutil.copy2(source_file, destination_file)

script_path = os.path.abspath(__file__)
dir_path = os.path.dirname(script_path)
parent_dir = os.path.dirname(dir_path)


# 指定文件夹路径
folder_path =dir_path + r"\Luban\Luban.ClientServer\Templates\config\cs_unity_json"
source_folder =dir_path + r"\CustomTem\class"
destination_folder =dir_path + r"\Luban\Luban.ClientServer\Templates\config\cs_unity_json"

# 删除文件夹下的所有文件
delete_files(folder_path)

# 复制文件到目标文件夹
copy_files(source_folder, folder_path)
