using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace KiwoomTrader
{
    public partial class Api : UserControl
    {
        /// <summary>
        /// 현재 서버와의 연결 상태를 알려준다.
        /// </summary>
        public int CurrentState { get { return m_axKHOpenAPI.GetConnectState(); } }

        public Api()
        {
            InitializeComponent();

            //구조체 변수 초기화
            변수_로그인 = new 구조체_로그인정보();
            변수_예수금 = new 구조체_예수금정보();
            변수_계좌평가잔고_싱글 = new 구조체_계좌평가잔고정보_싱글();

            //사전 초기화
            사전_계좌평가잔고 = new Dictionary<string, Dictionary<string, string>>();
            사전_미체결 = new Dictionary<string, Dictionary<string, string>>();

            //배역 초기화
            배열_코스닥 = null;
            배열_코스닥 = null;

            //리스트 초기화
            리스트_종목일봉 = new List<List<string>>();


            // 이벤트 등록
            m_axKHOpenAPI.OnEventConnect += onEventConnect;
            m_axKHOpenAPI.OnReceiveTrData += onReceiveTrData;

            시장종목초기화();
        }



        //public void 계산함수()
        //{
        //    string[] code_list = 시장종목코드리스트("10");
        //    Console.WriteLine(code_list.Length.ToString());



        //    for (int i = 0; i < code_list.Length; i++)
        //    {
        //        calcEvent.WaitOne();
        //        m_axKHOpenAPI.DisconnectRealData(scr2);
        //        Console.WriteLine("{0} / {1} : KOSDAQ 종목 코드 : {2} 업데이트중...", i, code_list.Length, code_list[i]);
        //        주식일봉차트_조회(stock_code: code_list[i]);
        //    }
        //}
    }

}
