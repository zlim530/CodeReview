import math

days_in_month = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31]
# 列表
jie_qi = ["小寒","大寒","立春","雨水","惊蛰","春分",
          "清明","谷雨","立夏","小满","芒种","夏至",
          "小暑","大暑","立秋","处暑","白露","秋分",
          "寒露","霜降","立冬","小雪","大雪","冬至"]
# 字典
jie_qi_to_month = {
    "大寒":"腊月", "雨水":"正月", "春分":"二月", "谷雨":"三月",
    "小满":"四月", "夏至":"五月", "大暑":"六月", "处暑":"七月",
    "秋分":"八月", "霜降":"九月", "小雪":"十月", "冬至":"冬月",
}
chinese_day = ['',"初一","初二","初三","初四","初五","初六","初七",
                  "初八","初九","初十","十一","十二","十三","十四",
                  "十五","十六","十七","十八","十九","二十","廿一",
                  "廿二","廿三","廿四","廿五","廿六","廿七","廿八",
                  "廿九","三十",]

""" 找到每个月的节气 ：经验公式"""
def F(y, x):
    return 365.242 * (y - 1900) + 6.2 + 15.22 * x - 1.9 * math.sin(0.262 * x)

""" 找到每个月的初一（朔）：经验公式 """
def M(m):
    return 1.6 + 29.5306 * m + 0.4 * math.sin(1 - 0.45058 * m)

def is_leap_year(year):
    if year % 400 == 0:
        return True
    elif year % 100 == 0:
        return False
    elif year % 4 == 0:
        return True
    else:
        return False

def get_days_in_month(year, month):
    if is_leap_year(year) and month == 1:
        return days_in_month[month] + 1
    else:
        return days_in_month[month]

""" 将天数转换成公历的年月日 """
def days_to_gregorian_date(n):
    """ 
    :param n: num days from 1900-01-00.
    """
    y = 1900
    m = 0
    while n > get_days_in_month(y, m):
        n = n - get_days_in_month(y, m)
        m = (m + 1) % 12
        if m == 0:
            y = y + 1
    year  = y
    month = m + 1
    day   = n
    return year, month, day

def gregorian_date_to_days(year, month, day):
    """ 
    Count the total number of days from 1900-01-00 to the current greporian date.
    """
    days = 0
    for y in range(1900, year):
        if is_leap_year(y):
            days = days + 366
        else:
            days = days + 365

    for m in range(0, month - 1):
        days = days + get_days_in_month(year, m)
    
    days = days + day
    return days

print(gregorian_date_to_days(2001, 1, 1))

def get_nearest_shou_day(day):
    L = 0
    while day >= int(M(L)):
        L = L + 1
    return L - 1, M(L - 1)

days = gregorian_date_to_days(2019, 4, 10)
# print(days)
_, shou_day = get_nearest_shou_day(days)
shou_day = int(shou_day)
y, m, d = days_to_gregorian_date(shou_day)
# print("{}-{}-{}".format(y, m, d))
print(chinese_day[days - shou_day+ 1] )

def create_jie_qi_list(year):
    dong_zhi_day = F(year - 1, jie_qi.index("冬至"))
    result = [{
                "day": int(dong_zhi_day),
                "month": jie_qi_to_month["冬至"],
                "jieqi":"冬至"
            }]
    for i in range(1, len(jie_qi), 2):
        jieqi_name = jie_qi[i]
        day = F(year, i)
        result.append({
            "day":   int(day),
            "month": jie_qi_to_month[jieqi_name],
            "jieqi": jieqi_name
        })
    return result

jie_qi_list = create_jie_qi_list(2019)
for item in jie_qi_list:
    print(item)

def create_month_list(year):
    days = int(F(year - 1, jie_qi.index("冬至")))
    m, shou_day = get_nearest_shou_day(days)
    result = []
    for i in range(m, m + 13):
        result.append({
            "month": None,
            "first_day":int(M(i)),
            "last_day": int(M(i + 1) - 1)
        })
    return result

month_list = create_month_list(2019)
for item in month_list:
    print(item)

# for i in range(0,4):
    # days = int( F(1900, i))
    # days = int( F(2015, i))
    # days = int( M(i))
    # y, m, d = days_to_gregorian_date(days)
    # print("{}年{}月{}日".format(y, m, d))