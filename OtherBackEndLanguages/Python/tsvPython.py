import csv

# tsvName = r'D:\Environment\SCOPESDK\CosmosSamples\VCROOT\local\users\alias\output.tsv'
# f = open(tsvName, 'r', encoding='utf-8')
# nameList = []
# for line in f:
#     nameList.append(line.split('\t')[40])
# # print(len(list(set(nameList))))
# # 以 \t 作为分隔符，查看 tsv 文件中某一列不重复的数据
# print(nameList)

countryList = [];
csvName = r'C:\Users\v-zijiagu\Downloads\Main_edge_fromsearchtraffic_2022_07_31.ss_TOP_1000.csv'
with open(csvName, newline='', encoding='utf-8') as csvfile:
        reader = csv.DictReader(csvfile)
        for row in reader:
            # print(row['Country'])
            countryList.append(row['Country'])

# print(tuple(countryList))
print(countryList)