import os
import json

script_path = os.path.abspath(__file__)
dir_path = os.path.dirname(script_path)
parent_dir = os.path.dirname(dir_path)

with open(parent_dir + r"/trunk/Dev/Assets/ApesGang/ConfigJsonData/config_tbskill_effect.json", 'r') as f:
    data = json.load(f)



for i, value in enumerate(data):
    res = [[]]
    db = []

    index = "buff_entity_para"
    attribute = "buff_entity_attr"
    idd = "buff_entity_id"
    target = "buff_entity_target"
    time = "buff_entity_duration"

    if value[idd] != 0:
        if value[time] != 0:
            value[index].insert(0, value[time])
        db.append({'x': value[idd], 'y': len(value[index]), 'z': value[target]})
    else:
        db.append({'x': 0, 'y': 0, 'z': 0})

    for indexNum in range(1, 8):
        ## 处理每一个buff
        index = "buff_" + str(indexNum) + "_para"
        attribute = "buff_" + str(indexNum) + "_attr"
        idd = "buff_" + str(indexNum) + "_id"
        target = "buff_" + str(indexNum) + "_target"
        time = "buff_" + str(indexNum) + "_duration"
        if idd in value:
            if value[time] != 0:
                value[index].insert(0, value[time])
            db.append({'x': value[idd], 'y': len(value[index]), 'z': value[target]})
        else:
            db.append({'x': 0, 'y': 0, 'z': 0})

    dataDb = [[], [], [], [], [], [], [], []];

    index = "buff_entity_para"
    attribute = "buff_entity_attr"
    idd = "buff_entity_id"
    if index in value:
        for vv in value[index]:
            dataDb[0].append({'x': vv, 'y': 0, 'z': 0})
    if attribute in value:
        for vv in value[attribute]:
            if len(dataDb[0]) > vv['x'] :
                dataDb[0][vv['x'] ]['y'] = vv['y']
                dataDb[0][vv['x'] ]['z'] = vv['z']

    for indexNum in range(1, 6):
        index = "buff_" + str(indexNum) + "_para"
        attribute = "buff_" + str(indexNum) + "_attr"
        idd = "buff_" + str(indexNum) + "_id"
        if index in value:
            for vv in value[index]:
                dataDb[indexNum].append({'x': vv, 'y': 0, 'z': 0})
        if attribute in value:
            for vv in value[attribute]:
                if len(dataDb[indexNum]) > vv['x'] :
                    dataDb[indexNum][vv['x'] ]['y'] = vv['y']
                    dataDb[indexNum][vv['x'] ]['z'] = vv['z']
    maxIndexCount = 0
    for iii,vvv in enumerate(db):
        if len(res[-1]) > 3:
            res.append([])
        res[-1].append(vvv)
        maxIndexCount = vvv['y'] if maxIndexCount < vvv['y'] else maxIndexCount;

    if maxIndexCount > 0 :
        for indexNum2 in range(0,maxIndexCount):
            res.append([{'x':0,'y':0,'z':0},{'x':0,'y':0,'z':0},{'x':0,'y':0,'z':0},{'x':0,'y':0,'z':0}])
            res.append([{'x':0,'y':0,'z':0},{'x':0,'y':0,'z':0},{'x':0,'y':0,'z':0},{'x':0,'y':0,'z':0}])
        for indexNum in range(0, 6):
            for iiii,vvvv in enumerate(dataDb[indexNum]):
                res[(indexNum)//4+ (iiii+1)*2 ][indexNum%4] = vvvv
    data[i]['skill_effect_buff_new'] = res

f.close()
##print(data)

json_data = json.dumps(data, indent=2)
with open(parent_dir + r"/trunk/Dev/Assets/ApesGang/ConfigJsonData/config_tbskill_effect.json", 'w') as f:
    f.write(json_data)
f.close()
