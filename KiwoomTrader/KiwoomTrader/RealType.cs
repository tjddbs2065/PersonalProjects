using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KiwoomTrader
{
    public static class RealType
    {
        public static class SENDTYPE
        {
            public static class  거래구분
            {
                public static string 지정가 = "00";
                public static string 시장가 = "03";
                public static string 조건부지정가 = "05";
                public static string 최유리지정가 = "06";
                public static string 최우선지정가 = "07";
                public static string 지정가IOC = "10";
                public static string 시장가IOC = "13";
                public static string 최유리IOC = "16";
                public static string 지정가FOK = "20";
                public static string 시장가FOK = "23";
                public static string 최유리FOK = "26";
                public static string 장전시간외종가 = "61";
                public static string 시간외단일가매매 = "62";
                public static string 장후시간외종가 = "81";
            }
        }
        public static class REALTYPE
        {
            public static class 주식체결
            {
                public static int 체결시간 = 20;
                public static int 현재가 = 10;
                public static int 전일대비= 11;
                public static int 등락율 = 12;
                public static int 매돠호가 = 27;
                public static int 매수호가 = 28;
                public static int 거래량 = 15;
                public static int 누적거래량 = 13;
                public static int 누적거래대금 = 14;
                public static int 시가 = 16;
                public static int 고가 = 17;
                public static int 저가= 18;
                public static int 전일대비기호= 25;
                public static int 전일거래량대비_주 = 26;
                public static int 거래대금증감= 29;
                public static int 전일거래량대비_비율 = 30;
                public static int 거래회전율 = 31;
                public static int 거래비용 = 32;
                public static int 체결강도 = 228;
                public static int 시가총액 = 311;
                public static int 장구분 = 290;
                public static int KO접근도= 691;
                public static int 상한가발생시간 = 567;
                public static int 하한가발생시간 = 568;
            }
            public static class 장시작시간
            {
                public static int 장운영구분 = 215;
                public static int 시간 = 20; //(HHMMSS)
                public static int 장시작예상잔여시간 = 214;
            }
            public static class 주문체결
            {
                public static int 계좌번호 = 9201;
                public static int 주문번호 = 9203; 
                public static int 관리자사번 = 9205;
                public static int 종목코드 = 9001;
                public static int 주문업무분류 = 912;
                public static int 주문상태 = 913;
                public static int 종목명 = 302;
                public static int 주문수량= 900;
                public static int 주문가격= 901;
                public static int 미체결수량= 902;
                public static int 체결누계금액 = 903;
                public static int 원주문번호 = 904;
                public static int 주문구분 = 905;
                public static int 매매구분= 906;
                public static int 매도수구분= 907;
                public static int 주문체결시간 = 908;
                public static int 체결번호 = 909;
                public static int 체결가 = 910;
                public static int 체결량 = 911;
                public static int 현재가 = 10;
                public static int 최우선매도호가 = 27;
                public static int 최우선매수호가 = 28;
                public static int 단위체결가 = 914;
                public static int 단위체결량 = 915;
                public static int 당일매매수수료 = 938;
                public static int 당일매매세금 = 939;
                public static int 거부사유 = 919;
                public static int 화면번호 = 920;
                public static int 터미널번호 = 921;
                public static int 신용구분 = 922;
                public static int 대출일 = 923;
            }
            public static class 매도수구분
            {
                public static string 매도 = "1";
                public static string 매수 = "2";
            }
            public static class 잔고
            {
                public static string 계좌번호 = "9201";
                public static string 종목코드 = "9001";
                public static string 종목명 = "302";
                public static string 현재가 = "10";
                public static string 보유수량 = "930";
                public static string 매입단가 = "931";
                public static string 총매입가 = "932";
                public static string 주문가능수량 = "933";
                public static string 당일순매수량 = "945";
                public static string 매도매수구분 = "946";
                public static string 당일총매도손익 = "950";
                public static string 예수금 = "951";
                public static string 매도호가 = "27";
                public static string 매수호가 = "28";
                public static string 기준가 = "307";
                public static string 손익률 = "8019";
            }

        }
    }
}
