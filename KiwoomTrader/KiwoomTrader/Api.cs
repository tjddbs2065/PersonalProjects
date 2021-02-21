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
        private int account_cnt;
        private int server_type;
        private int keyboard_sec;
        private int firewall_sec;
        private string[] account_list;
        private string account_num;
        private string user_id;
        private string user_name;

        //
        private Dictionary<string, Hashtable> 계좌평가잔고내역사전;
        private Dictionary<string, Hashtable> 미체결사전;
        //Hashtable 계좌평가잔고내역사전;

        //종목 분석용 리스트
        List<List<string>> 계산데이터;

        /// <summary>
        /// 스레드 동기화를 위해 사용할 스레드 제어
        /// </summary>
        //true일 경우 차단기가 올라갔다고 생각
        static AutoResetEvent autoEvent = new AutoResetEvent(true);
        static AutoResetEvent calcEvent = new AutoResetEvent(true);


        /// <summary>
        /// 현재 서버와의 연결 상태를 알려준다.
        /// </summary>
        public int CurrentState { get { return m_axKHOpenAPI.GetConnectState(); } }


        public Api()
        {
            InitializeComponent();

            //변수 초기화
            계좌평가잔고내역사전 = new Dictionary<string, Hashtable>();
            미체결사전 = new Dictionary<string, Hashtable>();
            계산데이터 = new List<List<string>>();

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

            m_axKHOpenAPI.CommRqData("정보_계좌평가잔고내역요청", "opw00018", Int32.Parse(sPrevNext), KiwoomConst.scr1);

            //차단기의 상태를 확인해 통과시킬지 여부를 결정한다.
            autoEvent.Set();
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
        public string[] 시장종목코드리스트(string market_code = "0")
        {
            string code_string = m_axKHOpenAPI.GetCodeListByMarket(market_code);
            string[] tmp_list = code_string.Split(';');
            //마지막에 빈 값이 들어가기 때문에 빼준다.
            string[] code_list = new string[tmp_list.Length - 1];
            Array.Copy(tmp_list, 0, code_list, 0, tmp_list.Length - 1);

            return code_list;
        }
        public void 계산함수()
        {
            string[] code_list = 시장종목코드리스트("10");
            Console.WriteLine(code_list.Length.ToString());



            for (int i = 0; i < code_list.Length; i++)
            {
                calcEvent.WaitOne();
                m_axKHOpenAPI.DisconnectRealData(KiwoomConst.scr2);
                Console.WriteLine("{0} / {1} : KOSDAQ 종목 코드 : {2} 업데이트중...", i, code_list.Length, code_list[i]);
                주식일봉차트_조회(stock_code: code_list[i]);
            }
        }
        public void 주식일봉차트_조회(string stock_code="", string date = "", string sPrevNext = "0")
        {
            autoEvent.WaitOne();
            Delay(3600);
            if (date == "") date = DateTime.Now.ToString("yyyyMMdd");

            m_axKHOpenAPI.SetInputValue("종목코드", stock_code);
            m_axKHOpenAPI.SetInputValue("기준일자", date);
            m_axKHOpenAPI.SetInputValue("수정주가구분", "1");

            m_axKHOpenAPI.CommRqData("정보_주식일봉차트", "opt10081", Int32.Parse(sPrevNext), KiwoomConst.scr2);

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
            else if(e.sRQName == "정보_주식일봉차트")
            {
                string code = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim();
                int days = m_axKHOpenAPI.GetRepeatCnt(e.sTrCode, e.sRQName);
                Console.WriteLine("{0} 일봉데이터 요청({1}일)", code, days);
                for(int i = 0; i < days; i++)
                {
                    List<string> data = new List<string>();
                    data.Add("");
                    data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim());
                    data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "거래량").Trim());
                    data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "거래대금").Trim());
                    data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "일자").Trim());
                    data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "시가").Trim());
                    data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "고가").Trim());
                    data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "저가").Trim());
                    data.Add("");

                    계산데이터.Add(data);
                }


                if (e.sPrevNext == "2")
                {
                    autoEvent.Set();
                    주식일봉차트_조회(stock_code: code, sPrevNext: e.sPrevNext);
                }
                else
                {
                    int total_price;
                    bool pass_success = false;
                    Console.WriteLine("총 일수 : {0}", 계산데이터.Count);

                    //120일 이평선을 그릴만큼의 데이터가 있는지 체크
                    if (계산데이터 == null || 계산데이터.Count < 120)
                    {
                        pass_success = false;
                    }
                    else // 120일 이상 되면
                    {

                        total_price = 0;
                        for(int i =0; i < 120; i++)
                        {
                            total_price += Int32.Parse(계산데이터[i][1]); // 현재가 합
                        }
                        int 오늘이평선 = total_price / 120; // 오늘자 120일 이평선

                        bool bottom_stock_price = false;
                        int check_price = 0;
                        if (Int32.Parse(계산데이터[0][7]) <= 오늘이평선 && 오늘이평선 <= Int32.Parse(계산데이터[0][6]))
                        {
                            Console.WriteLine("오늘 주가 120일 이평선에 걸쳐있는 것 확인");
                            bottom_stock_price = true;
                            check_price = Int32.Parse(계산데이터[0][6]);
                        }
                        //과거 일봉들이 120일 이평선보다 밑에 있는지 확인
                        //그렇게 확인을 하다가 일봉이 120일 이평선보다 위에 있으면 계산 진행
                        int prev_price = 0;
                        if(bottom_stock_price == true)
                        {
                            int 오늘이평선_전 = 0;
                            bool price_top_moving = false;
                            int idx = 1;
                            while (true)
                            {
                                //120일치가 있는지 계속 확인
                                if(계산데이터.Count - idx < 120)
                                {
                                    Console.WriteLine("120일치가 없음!");
                                    break;
                                }
                                total_price = 0;
                                for (int i = idx; i < 120 + idx; i++)
                                {
                                    total_price += Int32.Parse(계산데이터[i][1]);
                                }
                                오늘이평선_전 = total_price / 120;

                                if(오늘이평선_전 <= Int32.Parse(계산데이터[idx][6]) && idx <= 20)
                                {
                                    Console.WriteLine("20일 동안 주가가 120일 이평선과 같거나 위에 있으면 조건 통과 못함");
                                    price_top_moving = false;
                                    break;
                                }
                                else if(Int32.Parse(계산데이터[idx][7]) > 오늘이평선_전 && idx > 20)
                                {
                                    Console.WriteLine("120일 이평선 위에 있는 일봉 확인됨");
                                    price_top_moving = true;
                                    prev_price = Int32.Parse(계산데이터[idx][7]);
                                    break;
                                }

                                idx += 1;
                            }
                            //해당 부분 이평선이 가장 최근 일자의 이평선 가격보다 낮은지 확인
                            if(price_top_moving == true)
                            {
                                if(오늘이평선 > 오늘이평선_전 && check_price > prev_price)
                                {
                                    Console.WriteLine("포착된 이평선의 가격이 오늘자(최근일자) 이평선 가격보다 낮은것 확인됨");
                                    Console.WriteLine("포착된 부분의 일봉 저가가 오늘자 일봉의 고가보다 낮은지 확인됨");
                                    pass_success = true;
                                }
                            }
                        }
                    }

                    if(pass_success == true)
                    {
                        Console.WriteLine("조건부 통과됨");
                        string code_nm = m_axKHOpenAPI.GetMasterCodeName(code);
                        string path = @"C:\Users\SeongYun\Desktop\GitHub_ToyProject\KiwoomTrader\test.txt";
                        if (!File.Exists(path))
                        {
                            using (File.Create(path)) { }
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("{0};{1};{2}", code, code_nm, 계산데이터[0][1]);
                            }
                        }
                        else
                        {
                            using(StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine("{0};{1};{2}", code, code_nm, 계산데이터[0][1]);
                            }
                        }
                    }
                    else if(pass_success == false)
                    {
                        Console.WriteLine("조건부 통과 못함");
                    }
                    계산데이터.Clear();
                    calcEvent.Set();
                    autoEvent.Set();
                }

            }

        }


        /// <summary>
        /// 조회 시 사용할 딜레이 함수(3.6초 권장)
        /// </summary>
        private static void Delay(int ms)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime AfterWards = ThisMoment.Add(duration);

            while(AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
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
        public const string scr2 = "2001";
        public const string scr3 = "2002";
    }
}
