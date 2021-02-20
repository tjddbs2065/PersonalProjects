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

namespace KiwoomTrader
{
    public partial class Api : UserControl
    {
        private int account_cnt;
        private int server_type;
        private int keyboard_sec;
        private int firewall_sec;
        private string[] account_list;
        private string account_num;
        private string user_id;
        private string user_name;

        private Dictionary<string, Hashtable> 계좌평가잔고내역사전;
        private Dictionary<string, Hashtable> 미체결사전;
        //Hashtable 계좌평가잔고내역사전;

        /// <summary>
        /// 현재 서버와의 연결 상태를 알려준다.
        /// </summary>
        public int CurrentState { get { return m_axKHOpenAPI.GetConnectState(); } }

        /// <summary>
        /// 스레드 동기화를 위해 사용할 스레드 제어
        /// </summary>
        //true일 경우 차단기가 올라갔다고 생각
        static AutoResetEvent autoEvent = new AutoResetEvent(true);

        public Api()
        {
            InitializeComponent();

            //변수 초기화
            계좌평가잔고내역사전 = new Dictionary<string, Hashtable>();
            미체결사전 = new Dictionary<string, Hashtable>();

            // 이벤트 등록
            m_axKHOpenAPI.OnEventConnect += onEventConnect;
            m_axKHOpenAPI.OnReceiveTrData += onReceiveTrData;
        }

        public int LogIn()
        {
            return m_axKHOpenAPI.CommConnect();
        }
        public void EnterPassword()
        {
            m_axKHOpenAPI.KOA_Functions("ShowAccountWindow", "");
        }
        public void InitLoginInfo()
        {
            account_cnt = Int32.Parse(m_axKHOpenAPI.GetLoginInfo("ACCOUNT_CNT"));

            string tmpAcct = m_axKHOpenAPI.GetLoginInfo("ACCLIST");
            account_list = tmpAcct.Split(';');
            account_num = account_list[0];

            user_id = m_axKHOpenAPI.GetLoginInfo("USER_ID");
            user_name = m_axKHOpenAPI.GetLoginInfo("USER_NAME");
            server_type = Int32.Parse(m_axKHOpenAPI.GetLoginInfo("GetServerGubun"));
            keyboard_sec = Int32.Parse(m_axKHOpenAPI.GetLoginInfo("KEY_BSECGB"));
            firewall_sec = Int32.Parse(m_axKHOpenAPI.GetLoginInfo("FIREW_SECGB"));
        }
        /// <summary>
        /// 예수금 조회 함수
        /// </summary>
        /// <param name="view_type">2.일반조회, 3.추정조회 </param>
        public void 예수금_조회(string view_type = "2")
        {
            m_axKHOpenAPI.SetInputValue("계좌번호", account_num);
            m_axKHOpenAPI.SetInputValue("비밀번호", "");
            m_axKHOpenAPI.SetInputValue("비밀번호입력매체구분", "00");
            m_axKHOpenAPI.SetInputValue("조회구분", view_type);

            m_axKHOpenAPI.CommRqData("정보_예수금상세현황요청", "opw00001", 0, KiwoomConst.scr1); 
            
            //차단기의 상태를 확인해 통과시킬지 여부를 결정한다.
            autoEvent.WaitOne();
        }
        /// <summary>
        /// 계좌평가잔고내역요청 조회 함수
        /// </summary>
        /// <param name="password"></param>
        /// <param name="view_type">2.일반 조회, 3.추정조회</param>
        public void 계좌평가잔고내역요청_조회(string sPrevNext = "0", string view_type = "2")
        {
            m_axKHOpenAPI.SetInputValue("계좌번호", account_num);
            m_axKHOpenAPI.SetInputValue("비밀번호", "");
            m_axKHOpenAPI.SetInputValue("비밀번호입력매체구분", "00");
            m_axKHOpenAPI.SetInputValue("조회구분", view_type);

            m_axKHOpenAPI.CommRqData("정보_계좌평가잔고내역요청", "opw00018", 0, KiwoomConst.scr1);

            //차단기의 상태를 확인해 통과시킬지 여부를 결정한다.
            autoEvent.WaitOne();
        }
        /// <summary>
        /// 미체결 종목 조회 함수
        /// </summary>
        /// <param name="sPrevNext"></param>
        public void 미체결_조회(string sPrevNext = "0")
        {
            m_axKHOpenAPI.SetInputValue("계좌번호", account_num);
            m_axKHOpenAPI.SetInputValue("전체종목구분", "0");
            m_axKHOpenAPI.SetInputValue("매매구분", "0");
            m_axKHOpenAPI.SetInputValue("종목코드", "");
            m_axKHOpenAPI.SetInputValue("체결구분", "1");

            m_axKHOpenAPI.CommRqData("정보_미체결요청", "opt10075", 0, KiwoomConst.scr1);

            //차단기의 상태를 확인해 통과시킬지 여부를 결정한다.
            autoEvent.WaitOne();
        }

        /// <summary>
        /// 로그인 시 호출되는 이벤트
        /// </summary>
        private void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            //로그인 시 호출
            if(e.nErrCode == 0)
            {
                //성공
                Console.WriteLine("로그인 성공");
                InitLoginInfo();

                //계좌 비밀번호 입력
                EnterPassword();
            }
            else
            { 
                Console.WriteLine("로그인 실패");
                Application.Exit();
            }
        }
        /// <summary>
        /// 서버로부터의 메시지를 받는 이벤트
        /// </summary>
        private void onEventReceiveMsg(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveMsgEvent e)
        {
            Console.WriteLine("==== 서버 메시지 ====");
            Console.WriteLine(e.sScrNo);
            Console.WriteLine(e.sRQName);
            Console.WriteLine(e.sTrCode);
            Console.WriteLine(e.sMsg);
            Console.WriteLine("====================");
        }

        /// <summary>
        /// TR 요청이 오면 호출되는 이벤트
        /// </summary>
        private void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName == "정보_예수금상세현황요청")
            {
                string 예수금 = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "예수금");
                Console.WriteLine(Int32.Parse(예수금));
                string 출금가능금액 = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "출금가능금액");
                Console.WriteLine(Int32.Parse(출금가능금액));

                autoEvent.Set();
            }
            else if (e.sRQName == "정보_계좌평가잔고내역요청")
            {
                string 총매입금액 = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "총매입금액");
                Console.WriteLine(Int32.Parse(총매입금액));
                string 총수익률 = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "총수익률(%)");
                Console.WriteLine(Double.Parse(총수익률));

                //행 개수(종목 개수라 봐도 무방)
                int rows = m_axKHOpenAPI.GetRepeatCnt(e.sTrCode, e.sRQName);
                
                for(int i = 0; i < rows; i++)
                {
                    string stock_code = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목번호").Trim().Substring(1);

                    if (!계좌평가잔고내역사전.ContainsKey(stock_code)) 
                    {
                        //계좌평가잔고내역사전.Add(stock_code, new Dictionary<string, string>()); 
                        계좌평가잔고내역사전.Add(stock_code, new Hashtable());

                        계좌평가잔고내역사전[stock_code].Add("종목번호", stock_code);
                        계좌평가잔고내역사전[stock_code].Add("종목명", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim());
                        계좌평가잔고내역사전[stock_code].Add("보유수량", Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "보유수량")));
                        계좌평가잔고내역사전[stock_code].Add("매입가", Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매입가")));
                        계좌평가잔고내역사전[stock_code].Add("수익률(%)", Double.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "수익률(%)")));
                        계좌평가잔고내역사전[stock_code].Add("현재가", Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "현재가")));
                        계좌평가잔고내역사전[stock_code].Add("매입금액", Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매입금액")));
                        계좌평가잔고내역사전[stock_code].Add("매매가능수량", Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매매가능수량")));
                    }
                    else
                    {
                        계좌평가잔고내역사전[stock_code]["종목명"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                        계좌평가잔고내역사전[stock_code]["보유수량"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "보유수량"));
                        계좌평가잔고내역사전[stock_code]["매입가"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매입가"));
                        계좌평가잔고내역사전[stock_code]["수익률(%)"] = Double.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "수익률(%)"));
                        계좌평가잔고내역사전[stock_code]["현재가"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "현재가"));
                        계좌평가잔고내역사전[stock_code]["매입금액"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매입금액"));
                        계좌평가잔고내역사전[stock_code]["매매가능수량"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매매가능수량"));
                    }

                }
                if(e.sPrevNext == "2")
                {
                    계좌평가잔고내역요청_조회("2");
                    autoEvent.Set();
                    return;
                }

                Console.WriteLine("종목 수 : " + rows);
                foreach (KeyValuePair<string, Hashtable> items in 계좌평가잔고내역사전)
                {
                    Console.WriteLine("======= " + items.Key + " =======");
                    foreach (var item in items.Value.Keys)
                    {
                        Console.WriteLine("{0} : {1}", item, items.Value[item]);
                    }
                }

                autoEvent.Set();
            }
            else if(e.sRQName == "정보_미체결요청")
            {
                int rows = m_axKHOpenAPI.GetRepeatCnt(e.sTrCode, e.sRQName);
                for(int i = 0; i < rows; i++)
                {

                    string order_no = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문번호").Trim();
                    if (!미체결사전.ContainsKey(order_no))
                    {
                        미체결사전.Add(order_no, new Hashtable());

                        미체결사전[order_no].Add("주문번호", order_no);
                        미체결사전[order_no].Add("종목코드", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목코드"));
                        미체결사전[order_no].Add("종목명", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim());
                        계좌평가잔고내역사전[order_no].Add("주문상태", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문상태"));
                        계좌평가잔고내역사전[order_no].Add("보유수량", Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문수량")));
                        계좌평가잔고내역사전[order_no].Add("매입가", Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문가격")));
                        계좌평가잔고내역사전[order_no].Add("수익률(%)", Double.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문구분")));
                        계좌평가잔고내역사전[order_no].Add("현재가", Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "미체결수량")));
                        계좌평가잔고내역사전[order_no].Add("매입금액", Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "체결량")));
                    }
                    else
                    {
                        미체결사전[order_no].Add("주문번호", order_no);
                        미체결사전[order_no]["종목코드"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목코드");
                        미체결사전[order_no]["종목명"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                        계좌평가잔고내역사전[order_no]["주문상태"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문상태");
                        계좌평가잔고내역사전[order_no]["주문수량"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문수량"));
                        계좌평가잔고내역사전[order_no]["주문가격"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문가격"));
                        계좌평가잔고내역사전[order_no]["주문구분"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문구분"));
                        계좌평가잔고내역사전[order_no]["미체결수량"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "미체결수량"));
                        계좌평가잔고내역사전[order_no]["체결량"] = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "체결량"));

                    }
                }

                Console.WriteLine("종목 수 : " + rows);
                foreach (KeyValuePair<string, Hashtable> items in 미체결사전)
                {
                    Console.WriteLine("======= " + items.Key + " =======");
                    foreach (var item in items.Value.Keys)
                    {
                        Console.WriteLine("{0} : {1}", item, items.Value[item]);
                    }
                }

                autoEvent.Set();
            }


        }
    }
    class KiwoomConst
    {
        public const int Failed = 1;
        public const int Success = 0;
        public const int Connected = 1;
        public const int Disconnected = 0;

        public const string scr1 = "2000";
    }
}
