
'''
tick: 몇분봉 데이터인지
avg_moving: 몇일선 데이터를 원하는지
list: 데이터
'''
def get_moving_avg_line(avg_moving, list):
    tmp_list = []
    for position, data in enumerate(list):
        if position < (avg_moving-1):
            tmp_list.append(0)
        else:
            total = 0
            for idx in range(avg_moving):
                total += list[position - idx]
            avg_value = int(total / avg_moving)
            if list[position] < 1000:
                tmp_list.append(avg_value - (avg_value % 1))
            elif list[position] < 5000:
                tmp_list.append(avg_value - (avg_value % 5))
            elif list[position] < 10000:
                tmp_list.append(avg_value - (avg_value % 10))
            elif list[position] < 50000:
                tmp_list.append(avg_value - (avg_value % 50))

    return tmp_list

def get_gradient_list(list):
    tmp_list = []
    for position, data in enumerate(list):
        if position < 1:
            tmp_list.append(0)
        else:
            if list[position-1] < 1000:
                tmp_list.append(int((list[position] - list[position-1]) / 1))
            elif list[position-1] < 5000:
                tmp_list.append(int((list[position] - list[position-1])/ 5))
            elif list[position-1] < 10000:
                tmp_list.append(int((list[position] - list[position-1]) / 10))
            elif list[position-1] < 50000:
                tmp_list.append(int((list[position] - list[position-1]) / 50))
            else:
                tmp_list.append(0)
    return tmp_list

raw_data = []
f = open("C:/Users/SeongYun/Desktop/GitHub_ToyProject/KiwoomTrader/KiwoomTrader/bin/Debug/케이사인.txt")
for line in f.readlines()[::-1]:
    data = line.split(';')
    raw_data.append(data[2])
f.close()
raw_data.reverse()

isToday = False
price_today = []
for date in raw_data:
    if date == "20210226090000":
        isToday = True
    elif date == "20210226153000":
        isToday = False
    if isToday == True:
        price_today.append(raw_data[date])
#print(price_today)


avg_moving_list = get_moving_avg_line(3, price_today)
#print(avg_moving_list)
gradient_moving_list = get_gradient_list(avg_moving_list)
print(gradient_moving_list)
print(len(gradient_moving_list))
#for i in gradient_moving_list:
#    if i < 0:
#        print("하락")
#    elif i == 0:
#        print("보합")
#    elif i > 0:
#        print("상승")