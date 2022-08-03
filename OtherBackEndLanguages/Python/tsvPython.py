# tsvName = r'D:\Environment\SCOPESDK\CosmosSamples\VCROOT\local\users\alias\output.tsv'
tsvName = r'C:\Users\v-zijiagu\Downloads\Main_ie_googlequery_2022_07_30.ss'
f = open(tsvName, 'r', encoding='utf-8')
nameList = []
for line in f:
    nameList.append(line.split('\t')[40])
# print(len(list(set(nameList))))
# 以 \t 作为分隔符，查看 tsv 文件中某一列不重复的数据
print(nameList)