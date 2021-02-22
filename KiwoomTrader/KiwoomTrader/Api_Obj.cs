using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwoomTrader
{
    public partial class Api
    {

        /// <summary>
        /// 로그인 정보를 저장할 구조체이다.
        /// </summary>
        public struct 구조체_로그인정보
        {
            public int 계좌수;
            public int 서버종류;
            public int 키보드보안;
            public int 방화벽보안;
            public string[] 계좌리스트;
            public string 계좌번호;
            public string 사용자ID;
            public string 사용자이름;
        }
        public struct 구조체_예수금정보
        {
            public int 예수금;
            public int 출금가능금액;
        }
        public struct 구조체_계좌평가잔고정보_싱글
        {
            public int 총매입금액;
            public int 총평가금액;
            public int 총평가손익금액;
            public double 총수익률;
            public int 추정예탁자산;
            public int 조회건수;
        }
        //public struct 구조체_계좌평가잔고_멀티
        //{
        //    public string 종목번호;
        //    public string 종목명;
        //    public int 평가손익;
        //    public double 수익률;
        //    public int 매입가; 
        //    public int 보유수량;
        //    public int 매매가능수량;
        //    public int 현재가;
        //    public int 매입금액;
        //    public int 평가금액;
        //    public int 보유비중;
        //}

        //public struct 구조체_미체결정보
        //{

        //}



        public const int Failed = 1;
        public const int Success = 0;
        public const int Connected = 1;
        public const int Disconnected = 0;


        public const string KOSPI = "0";
        public const string KOSDAQ = "10";
    }
}
