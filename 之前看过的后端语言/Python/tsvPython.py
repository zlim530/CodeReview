tsvName = r'D:\Environment\SCOPESDK\CosmosSamples\VCROOT\local\users\alias\output.tsv'
f = open(tsvName, 'r')
nameList = []
for line in f:
    nameList.append(line.split('\t')[4])
# print(len(list(set(nameList))))
# 以 \t 作为分隔符，查看 tsv 文件中某一列不重复的数据
print(nameList)