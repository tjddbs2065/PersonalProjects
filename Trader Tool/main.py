import urllib.request as ul
import json
import sys
import io

url = "https://opendart.fss.or.kr/api/list.json?crtfc_key=c20aa3003972bf429b8c63f2a43c111fb2d62a45&pblntf_ty=B"

request = ul.Request(url)
response = ul.urlopen(request)

rescode = response.getcode()

if(rescode == 200):
    responseData = response.read()
    rDD = json.loads(responseData)
    print(rDD)


for i in rDD["list"]:
    print(i)
print(rDD["list"])