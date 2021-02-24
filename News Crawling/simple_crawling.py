import requests
from bs4 import BeautifulSoup

raw = requests.get("https://news.naver.com/main/list.nhn?mode=LSD&mid=sec&sid1=101&listType=title&date=20210225",
                   headers={'User-Agent':'Mozilla/5.0'})
html = BeautifulSoup(raw.text, "html.parser")

main_components = html.select(".type02 > li > a")

for a in main_components:
    print(a.text)
