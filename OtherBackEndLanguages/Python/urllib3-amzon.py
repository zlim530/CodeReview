""" import urllib.request
req = urllib.request.urlopen("https://www.baidu.com")
# req = urllib.request.urlopen("https://www.amazon.com")
print(req.code) """

# name = input("pls enter your name:")
# print("hello",name)

""" a = 100
if a >= 0:
    print(a)
else:
    print(-a) """

# 简单爬虫
import requests
from lxml import etree

def main():
    """ url = "https://movie.douban.com/chart"
    header = {"User-Agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36"}
    html = requests.get(url, headers=header).text
    Music = etree.HTML(html)
    MusicTitle = Music.xpath("/html/head/title/text()")[0]
    print(MusicTitle) # 豆瓣电影排行榜 """
    
    crawl_urls = [
        "https://book.douban.com/subject/25862578",
        "https://book.douban.com/subject/26698660",
        "https://book.douban.com/subject/2230208"
    ]
    parse_rule = "//div[@id='wrapper']/h1/span/text()"

    for url in crawl_urls:
        header = {"User-Agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36"}
        response = requests.get(url, headers = header)
        result = etree.HTML(response.text).xpath(parse_rule)[0]
        print(result) 
        """ 
        解忧杂货店
        巨人的陨落
        我的前半生
        """

if __name__ == '__main__':
    main()