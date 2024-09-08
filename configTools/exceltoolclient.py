from openpyxl import Workbook, load_workbook
import os

# 打开Excel文件
def genfixedexcel(index,excel_path,output_path):
    script_path = os.path.abspath(__file__)
    dir_path = os.path.dirname(script_path)
    parent_dir = os.path.dirname(dir_path)

    index += 4
    folder_name = os.path.basename(os.path.dirname(excel_path))
    wb = load_workbook(excel_path, data_only=True)
    # 选择第一个工作表
    #ws = wb['_data']
    sheet_names = wb.sheetnames
    # 遍历工作表名称，找到包含"data"的工作表
    matching_sheets = [sheet_name for sheet_name in sheet_names if 'data' in sheet_name.lower()]

    matching_sheetself = [sheet_name for sheet_name in sheet_names if 'self' in sheet_name.lower()]
    
   
    if len(matching_sheets) == 0:
        print(excel_path+"：找不到data工作表")
        wb.close()
        return None

    
    # 处理自用的excel表
    if len(matching_sheetself) > 0:
        
        # 保存修改后的Excel文件
        file_name = os.path.basename(excel_path)
        file_name_without_extension = os.path.splitext(file_name)[0]
        new_file_name_without_extension = file_name_without_extension + "_Fixed.xlsx"
        myname = output_path + "/"  + file_name_without_extension + "_Fixed.xlsx"

        # 将此次处理的excel文件写入luban的__tables__.xlsx文件中
        
        tablespath = output_path + r"\__tables__.xlsx"
        workbook = load_workbook(tablespath)  # 替换为实际的文件名
        worksheet = workbook['Sheet1']  # 替换为实际的工作表名

        # TODO这里可能有单例表 映射表 列表 默认映射表(键为第一个)
        cell0 = worksheet.cell(row=index, column=2)
        cell0.value ="config.Tb" + file_name_without_extension   
                        
        cell1 = worksheet.cell(row=index, column=3)
        cell1.value = file_name_without_extension  
                                
        cell2 = worksheet.cell(row=index, column=4)
        cell2.value = "TRUE"
                            
        cell3 = worksheet.cell(row=index, column=5)
        cell3.value = new_file_name_without_extension
       
        # 保存luban.__tables__和兼容后的配置表
        workbook.save(tablespath)
        workbook.close()
        wb.save(myname)
        wb.close()
        return None
   
    #sheet_name = wb.sheetnames[0]
    ws =wb[matching_sheets[0]] 
    found_server = False
    rowserver = ws[3]
    # 检查单元格中是否有包含字符 "server"
    for cell in rowserver:
        if cell.value and ("client" or "all") in str(cell.value):
            found_server = True
            break  # 一旦找到了，退出循环
    if not found_server:
        print(excel_path+"：找不到client")
        wb.close()
        return None

    # 删除除目标表之外的其他表
    for sheet_name in sheet_names:
        if sheet_name != matching_sheets[0]:
            sheet = wb[sheet_name]
            wb.remove(sheet)

    # 保存修改后的 Excel 文件
    #wb.save('updated_excel_file.xlsx')

    #ws = wb[sheet_name]

    # 插入空白列

    ws.insert_cols(1)
 
    # 向A1单元格写入数据
    ws['A1'] = '##var'
    ws['A2'] = '##type'
    ws['A3'] = '##group'
    ws['A4'] = '##'

    #给多语言表新增一个当前语言字段 current
    if excel_path ==parent_dir + r"\config\language.xlsx":
        last_column = ws.max_column   
        different_strings = ["current", "string", "client", "当前语言"]  
        for idx, string in enumerate(different_strings, start=1):  # Start from row 2
            cell = ws.cell(row=idx, column=last_column)  # Select the cell in the last column and current row
            cell.value = string
    
    if excel_path ==parent_dir + r"\config\battle\monster_template.xlsx":
        last_column = ws.max_column   
        different_strings = ["monster_id", "int", "client", "怪物id"]  
        for idx, string in enumerate(different_strings, start=1):  # Start from row 2
            cell = ws.cell(row=idx, column=last_column)  # Select the cell in the last column and current row
            cell.value = string        
    # 修改第一行避免关键字冲突
    row1 = ws[1]
    for cell in row1[1:]:
        # 将单元格的值转换为全小写
        cell.value = str(cell.value).lower()
        #cell.value = str(cell.value).replace(" ", "")
        # 如果与对比字符串相等，则替换为新字符串
        if cell.value == "base":
            cell.value = "base_0"
        if cell.value == "event":
            cell.value = "event_0"
        if cell.value == "default":
            cell.value = "default_0"
        if cell.value == "goto":
            cell.value = "goto_0"    
        if cell.value == "lock":
            cell.value = "lock_0"

    # 获取总行数
    total_rows = ws.max_row
    # 创建一个列表来存储需要删除的行号
    rows_to_delete = []
    # 遍历每一行，检查第一列是否为空
    for row in range(1, total_rows + 1):
        cell_value = ws.cell(row=row, column=2).value
        if cell_value is None or cell_value == "":
            rows_to_delete.append(row)
    # 倒序删除空行，以避免删除后索引混乱

    for row in reversed(rows_to_delete):
        ws.delete_rows(row)

    # 修改第三行读取权限为luban兼容
    row3 = ws[3]
    for cell in row3[1:]:
        # 将单元格的值转换为全小写
        cell.value = str(cell.value).lower()
        #cell.value = str(cell.value).replace(" ", "")
        # 如果与对比字符串相等，则替换为新字符串
        if cell.value == "client":
            cell.value = "c"
        elif cell.value == "all":
            cell.value = "c,s"
        elif cell.value == "server": 
            cell.value = "s"
        else:
            cell.value = "none"        

    # 修改第二行数据类型为luban兼容
    row2 = ws[2]
    for cell in row2[1:]:
     # 将单元格的值转换为全小写
        cell.value = str(cell.value).lower()
        #cell.value = str(cell.value).replace(" ", "")
        # 如果与对比字符串相等，则替换为新字符串
        if cell.value == "array_string":
            cell.value = "(list#sep=;),string"
        elif cell.value == "array1_int" or cell.value == "array_int":
            cell.value = "(list#sep=;),int"
        elif cell.value == "array2_int":
            cell.value = "(list#sep=|),vector2"            
        elif cell.value == "array3_int":
            cell.value = "(list#sep=|),vector3"

        elif cell.value == "none":
            ws.delete_cols(cell.column)         
        # TODO:补充类型处
    # 获取第二行的数据


    # 保存修改后的Excel文件
    file_name = os.path.basename(excel_path)
    file_name_without_extension = os.path.splitext(file_name)[0]
    if file_name_without_extension == "event":
        file_name_without_extension = "event_0"
    new_file_name_without_extension = file_name_without_extension + "_Fixed.xlsx"
    myname = output_path + "/"  + file_name_without_extension + "_Fixed.xlsx"
    
    # 将此次处理的excel文件写入luban的__tables__.xlsx文件中
    
    tablespath = output_path + r"\__tables__.xlsx"
    workbook = load_workbook(tablespath)  # 替换为实际的文件名
    worksheet = workbook['Sheet1']  # 替换为实际的工作表名

   # TODO这里可能有单例表 映射表 列表 默认映射表(键为第一个)
    cell0 = worksheet.cell(row=index, column=2)
    cell0.value ="config.Tb" + file_name_without_extension   
                    
    cell1 = worksheet.cell(row=index, column=3)
    cell1.value = file_name_without_extension  
                               
    cell2 = worksheet.cell(row=index, column=4)
    cell2.value = "TRUE"
                        
    cell3 = worksheet.cell(row=index, column=5)
    cell3.value = new_file_name_without_extension

    # 将equip_data表转为多键映射表
    if excel_path ==parent_dir + r"\config\item\equip_data.xlsx":
        cell4 = worksheet.cell(row=index, column=6)
        cell4.value = "id+quality"

    if excel_path ==parent_dir + r"\config\battle\map_refresh.xlsx":
        cell4 = worksheet.cell(row=index, column=6)
        cell4.value = "id+rule_id"    

    
    # 将若干表转为单列表表
    listHandleArr = []
    # 在这里添加List的表路径

    listHandleArr.append(parent_dir+r"\config\battle\battle_drop.xlsx")
    listHandleArr.append(parent_dir+r"\config\battle\monster_template.xlsx")
    listHandleArr.append(parent_dir+r"\config\item\drop.xlsx")
    listHandleArr.append(parent_dir+r"\config\item\draw_banner.xlsx")
    listHandleArr.append(parent_dir+r"\config\battle\battleshop_drop.xlsx")
    listHandleArr.append(parent_dir+r"\config\battle\monster_trigger.xlsx")
    listHandleArr.append(parent_dir+r"\config\item\battlepass_reward.xlsx")
    listHandleArr.append(parent_dir+r"\config\battle\element_effect.xlsx")
    listHandleArr.append(parent_dir+r"\config\battle\module_refresh.xlsx")
    listHandleArr.append(parent_dir+r"\config\battle\battletech_drop.xlsx")
    listHandleArr.append(parent_dir+r"\config\item\fund_reward.xlsx")
    
    if excel_path in listHandleArr:
        handleList(ws,worksheet,index)
    # 保存luban.__tables__和兼容后的配置表
    workbook.save(tablespath)
    workbook.close()
    wb.save(myname)
    wb.close()

def handleList(ws,worksheet,index):
    cell4 = worksheet.cell(row=index, column=7)
    cell4.value = "list"
        # 计算实际的行数
    actual_max_row = ws.calculate_dimension().split(':')[1]
    # 从实际行数字符串中提取行号
    total_rows = int(actual_max_row[1:])
        # 在第二列之后插入一个新列
    ws.insert_cols(2)
    # 将值写入第二列的1-4行
    values = ['assist', 'int', 'c,s','辅助主键id']
    for i, value in enumerate(values, start=1):
        ws.cell(row=i, column=2, value=value)        
    # 从第5行开始，按从1到n的值填充新列
    start_row = 5
    n = total_rows - start_row + 1
    for i in range(1, n + 1):
        ws.cell(row=start_row + i - 1, column=2, value=i)        

