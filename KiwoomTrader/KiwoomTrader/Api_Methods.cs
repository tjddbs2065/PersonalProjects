using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KiwoomTrader
{
    public partial class Api
    {
        //=========== 로그인 관련 =================================
        /// <summary>
        /// 로그인 창을 호출한다.
        /// </summary>
        /// <returns>0:성공, 1:실패</returns>
        public int 로그인()
        {
            Console.WriteLine("로그인 함수 호출");
            return m_axKHOpenAPI.CommConnect();
        }
        /// <summary>
        /// 계좌 패스워드 입력 창을 호출한다.
        /// </summary>
        public void 패스워드()
        {
            Console.WriteLine("패스워드 함수 호출");
            m_axKHOpenAPI.KOA_Functions("ShowAccountWindow", "");
        }
        /// <summary>
        /// 로그인정보 구조체에 정보를 초기화한다.
        /// </summary>
        public void 로그인정보이벤트()
        {
            변수_로그인.계좌수 = Int32.Parse(m_axKHOpenAPI.GetLoginInfo("ACCOUNT_CNT"));

            string tmpAcct = m_axKHOpenAPI.GetLoginInfo("ACCLIST");
            변수_로그인.계좌리스트 = tmpAcct.Split(';');
            변수_로그인.계좌번호 = 변수_로그인.계좌리스트[0];

            변수_로그인.사용자ID = m_axKHOpenAPI.GetLoginInfo("USER_ID");
            변수_로그인.사용자이름 = m_axKHOpenAPI.GetLoginInfo("USER_NAME");
            변수_로그인.서버종류 = Int32.Parse(m_axKHOpenAPI.GetLoginInfo("GetServerGubun"));
            변수_로그인.키보드보안 = Int32.Parse(m_axKHOpenAPI.GetLoginInfo("KEY_BSECGB"));
            변수_로그인.방화벽보안 = Int32.Parse(m_axKHOpenAPI.GetLoginInfo("FIREW_SECGB"));
        }


        //=========== 계좌 조회 관련 ==============================
        /// <summary>
        /// 예수금 정보에 대해 요청한다.
        /// </summary>
        /// <param name="view_type"> 2.일반조회, 3.추정조회 </param>
        public int 예수금상세현황요청(string 조회종류 = "2")
        {
            Console.WriteLine("예수금요청 함수 호출");
            if (!변수_로그인.Equals(default(구조체_로그인정보)))
            {
                //차단기의 상태를 확인해 통과시킬지 여부를 결정한다.
                autoEvent.WaitOne();

                m_axKHOpenAPI.SetInputValue("계좌번호", 변수_로그인.계좌번호);
                m_axKHOpenAPI.SetInputValue("비밀번호", "");
                m_axKHOpenAPI.SetInputValue("비밀번호입력매체구분", "00");
                m_axKHOpenAPI.SetInputValue("조회구분", 조회종류);
                //예수금상세현황요청
                m_axKHOpenAPI.CommRqData("정보_예수금상세현황요청", "opw00001", 0, 화면번호_잔고조회);

                return Success;
            }
            else
            {
                Console.WriteLine("로그인정보초기화 함수를 호출해 로그인 정보를 초기화 해주세요.");
                return Failed;
            }
        }
        /// <summary>
        /// Event 에서 예수금 정보를 초기화 하기 위해 사용한다.
        /// </summary>
        private void 예수금상세이벤트(ref AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            변수_예수금.예수금 = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "예수금"));
            변수_예수금.출금가능금액 = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "출금가능금액"));

            autoEvent.Set();
        }

        /// <summary>
        /// 계좌평가잔고 정보에 대해 요청한다.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="view_type">1.합산, 2.개별</param>
        public int 계좌평가잔고요청(string sPrevNext = "0", string 조회종류 = "2")
        {
            Console.WriteLine("계좌 잔고 요청 함수 호출");
            if (!변수_로그인.Equals(default(구조체_로그인정보)))
            {
                //차단기의 상태를 확인해 통과시킬지 여부를 결정한다.
                autoEvent.WaitOne();

                m_axKHOpenAPI.SetInputValue("계좌번호", 변수_로그인.계좌번호);
                m_axKHOpenAPI.SetInputValue("비밀번호", "");
                m_axKHOpenAPI.SetInputValue("비밀번호입력매체구분", "00");
                m_axKHOpenAPI.SetInputValue("조회구분", 조회종류);

                m_axKHOpenAPI.CommRqData("정보_계좌평가잔고내역요청", "opw00018", Int32.Parse(sPrevNext), 화면번호_잔고조회);

                return Success;
            }
            else
            {
                Console.WriteLine("로그인정보초기화 함수를 호출해 로그인 정보를 초기화 해주세요.");
                return Failed;
            }
        }
        /// <summary>
        /// Event 에서 구조체_계좌평가잔고 정보 구조체에 계좌 종합 정보를 초기화 하고
        /// 계좌평가잔고 사전에 각 종목의 정보를 초기화 한다.
        /// </summary>
        private void 계좌평가잔고이벤트(ref AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            func_count += 1;
            Console.WriteLine("계좌평가잔고 함수 호출");
            //계좌 종합 정보 초기화
            변수_계좌평가잔고_싱글.총매입금액 = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "총매입금액"));
            변수_계좌평가잔고_싱글.총평가금액 = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "총평가금액"));
            변수_계좌평가잔고_싱글.총평가손익금액 = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "총평가손익금액"));
            변수_계좌평가잔고_싱글.총수익률 = Double.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "총수익률(%)"));
            변수_계좌평가잔고_싱글.추정예탁자산 = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "추정예탁자산"));
            변수_계좌평가잔고_싱글.조회건수 = Int32.Parse(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "조회건수"));


            //행 개수(종목 개수라 봐도 무방)//멀티데이터의 갯수를 반환
            int 종목수 = m_axKHOpenAPI.GetRepeatCnt(e.sTrCode, e.sRQName);


            //계좌 각 종목 정보 초기화
            for (int i = 0; i < 종목수; i++)
            {
                string 종목코드 = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목번호").Trim().Substring(1);
                Console.WriteLine("{0} ({1})", 종목코드, func_count);

                if (!사전_계좌평가잔고.ContainsKey(종목코드))
                {
                    //계좌평가잔고내역사전.Add(stock_code, new Hashtable());
                    Dictionary<string, string> 임시사전 = new Dictionary<string, string>();

                    임시사전.Add("종목번호", 종목코드);
                    임시사전.Add("종목명", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim());
                    임시사전.Add("평가손익", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "평가손익"));
                    임시사전.Add("수익률(%)", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "수익률(%)"));
                    임시사전.Add("매입가", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매입가"));
                    임시사전.Add("보유수량", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "보유수량"));
                    임시사전.Add("매매가능수량", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매매가능수량"));
                    임시사전.Add("현재가", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "현재가"));
                    임시사전.Add("매입금액", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매입금액"));
                    임시사전.Add("평가금액", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "평가금액"));
                    임시사전.Add("보유비중", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "보유비중"));

                    사전_계좌평가잔고.Add(종목코드, 임시사전);
                }
                else
                {
                    사전_계좌평가잔고[종목코드]["평가손익"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "평가손익");
                    사전_계좌평가잔고[종목코드]["수익률(%)"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "수익률(%)");
                    사전_계좌평가잔고[종목코드]["매입가"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매입가");
                    사전_계좌평가잔고[종목코드]["보유수량"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "보유수량");
                    사전_계좌평가잔고[종목코드]["매매가능수량"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매매가능수량");
                    사전_계좌평가잔고[종목코드]["현재가"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "현재가");
                    사전_계좌평가잔고[종목코드]["매입금액"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "매입금액");
                    사전_계좌평가잔고[종목코드]["평가금액"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "평가금액");
                    사전_계좌평가잔고[종목코드]["보유비중"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "보유비중");
                }

            }

            autoEvent.Set();
            if (e.sPrevNext == "2")
            {
                계좌평가잔고요청("2");
            }
            else
            {
                func_count = 0;
            }
        }

        /// <summary>
        /// 미체결 정보에 대해 요청한다.
        /// </summary>
        /// <param name="sPrevNext"></param>
        public int 미체결요청(string sPrevNext = "0")
        {
            Console.WriteLine("미체결 요청 함수 호출");
            if (!변수_로그인.Equals(default(구조체_로그인정보)))
            {
                //차단기의 상태를 확인해 통과시킬지 여부를 결정한다.
                autoEvent.WaitOne();

                m_axKHOpenAPI.SetInputValue("계좌번호", 변수_로그인.계좌번호);
                m_axKHOpenAPI.SetInputValue("전체종목구분", "0");//0:전체 1:종목
                m_axKHOpenAPI.SetInputValue("매매구분", "0"); //0:전체, 1:매도, 2:매수
                m_axKHOpenAPI.SetInputValue("종목코드", "");
                m_axKHOpenAPI.SetInputValue("체결구분", "1"); //0:전체, 1:미체결, 2:체결

                m_axKHOpenAPI.CommRqData("정보_미체결요청", "opt10075", 0, 화면번호_주문조회);
                return Success;
            }
            else
            {
                Console.WriteLine("로그인정보초기화 함수를 호출해 로그인 정보를 초기화 해주세요.");
                return Failed;
            }
        }
        /// <summary>
        /// 미체결 사전에 각 종목의 정보를 초기화 한다.
        /// </summary>
        /// <param name="e"></param>
        public void 미체결이벤트(ref AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            int 종목수 = m_axKHOpenAPI.GetRepeatCnt(e.sTrCode, e.sRQName);
            for (int i = 0; i < 종목수; i++)
            {
                string 주문번호 = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문번호").Trim();
                if (!사전_미체결.ContainsKey(주문번호))
                {
                    //사전_미체결.Add(order_no, new Dictionary<string, string>());
                    Dictionary<string, string> 임시사전 = new Dictionary<string, string>();

                    임시사전.Add("주문번호", 주문번호);
                    임시사전.Add("종목코드", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목코드"));
                    임시사전.Add("주문상태", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문상태"));
                    임시사전.Add("종목명", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim());
                    임시사전.Add("보유수량", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문수량"));
                    임시사전.Add("매입가", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문가격"));
                    임시사전.Add("현재가", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "미체결수량"));
                    임시사전.Add("수익률(%)", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문구분"));
                    임시사전.Add("매입금액", m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "체결량"));

                    사전_미체결.Add(주문번호, 임시사전);
                }
                else
                {
                    사전_미체결[주문번호]["종목코드"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목코드");
                    사전_미체결[주문번호]["주문상태"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문상태");
                    사전_미체결[주문번호]["종목명"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                    사전_미체결[주문번호]["주문수량"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문수량");
                    사전_미체결[주문번호]["주문가격"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문가격");
                    사전_미체결[주문번호]["미체결수량"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "미체결수량");
                    사전_미체결[주문번호]["주문구분"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "주문구분");
                    사전_미체결[주문번호]["체결량"] = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "체결량");

                }
            }

            autoEvent.Set();
        }


        //=========== 종목 정보 조회 관련 ========================
        /// <summary>
        /// 시장 종목들의 정보를 요청한다.
        /// </summary>
        /// <param name="market_code">0:코스피, 10:코스닥, 8:ETF, 50:KONEX</param>
        /// <returns></returns>
        public string[] 시장종목요청(string market_code = "0")
        {
            Console.WriteLine("시장종목요청 함수 호출");
            return m_axKHOpenAPI.GetCodeListByMarket(market_code).Split(';');
        }
        /// <summary>
        /// 시장 종목 배열에 각 시장의 종목들을 초기화한다.
        /// </summary>
        public void 시장종목초기화()
        {
            배열_코스피 = 시장종목요청("0");
            배열_코스닥 = 시장종목요청("10");
        }

        /// <summary>
        /// 입력된 종목의 일봉 정보를 요청한다.
        /// </summary>
        /// <param name="stock_code"></param>
        /// <param name="date"></param>
        /// <param name="sPrevNext"></param>
        public void 주식일봉차트요청(string stock_code, string date = "", string sPrevNext = "0")
        {
            if (m_axKHOpenAPI.GetMasterCodeName(stock_code).Equals(""))
            {
                Console.WriteLine("잘못된 종목 코드입니다. 다시 입력해 주세요.");
                return;
            }
            Console.WriteLine("일봉차트 요청 함수 호출");
            //차단기의 상태를 확인해 통과시킬지 여부를 결정한다.
            autoEvent.WaitOne();

            //Delay(3600);
            Delay(800);

            if (date == "") date = DateTime.Now.ToString("yyyyMMdd");
            m_axKHOpenAPI.SetInputValue("종목코드", stock_code.Trim());
            m_axKHOpenAPI.SetInputValue("기준일자", date);
            m_axKHOpenAPI.SetInputValue("수정주가구분", "1");

            m_axKHOpenAPI.CommRqData("정보_주식일봉차트요청", "opt10081", Int32.Parse(sPrevNext), 화면번호_차트조회);
        }
        /// <summary>
        /// 종목 일봉 리스트에 각 일봉의 정보를 초기화 한다.
        /// </summary>
        /// <param name="e"></param>
        public void 주식일봉차트이벤트(ref AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            func_count += 1;
            string 종목코드 = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim();
            int 종목일수 = m_axKHOpenAPI.GetRepeatCnt(e.sTrCode, e.sRQName);

            for (int i = 0; i < 종목일수; i++)
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

                리스트_종목일봉.Add(data);
            }
            Console.WriteLine("{0} : {1}일 ({2})", 종목코드, 종목일수, func_count);

            if(!e.sPrevNext.Equals("2"))
            {
                //임시로 일봉 받아온 종목들의 정보를 파일로 저장
                string 종목이름 = m_axKHOpenAPI.GetMasterCodeName(종목코드);
                string path = @".\";
                path += 종목이름 + ".txt";

                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        foreach (List<string> data in 리스트_종목일봉)
                        {
                            sw.WriteLine("{0};{1};{2}", data[4], 종목코드, data[1]);
                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        foreach (List<string> data in 리스트_종목일봉)
                        {
                            sw.WriteLine("{0};{1};{2}", data[4], 종목코드, data[1]);
                        }
                    }
                }

                func_count = 0;

                //기존 종목의 일봉 정보 제거
                리스트_종목일봉.Clear();
                Console.WriteLine("일봉 데이터 저장 완료");
            }

            autoEvent.Set();
            if (e.sPrevNext.Equals("2"))
            {
                주식일봉차트요청(stock_code: 종목코드, sPrevNext: e.sPrevNext);
            }


        }


        //=========== 실시간 종목 조회 =========================
        /// <summary>
        /// 실시간으로 받을 데이터 목록을 등록
        /// </summary>
        public void 실시간요청등록()
        {
            Console.WriteLine("실시간 요청 함수 호출");
            m_axKHOpenAPI.SetRealReg(화면번호_실시간조회, "", RealType.REALTYPE.장시작시간.장운영구분.ToString(), "0");

            Console.WriteLine(RealType.REALTYPE.주식체결.체결시간);
            m_axKHOpenAPI.SetRealReg(화면번호_실시간조회, "096040", RealType.REALTYPE.주식체결.체결시간.ToString(), "1");


        }

        //=========== 차트 분석 ===============================
        /// <summary>
        /// 입력된 종목의 분봉 정보를 요청한다.
        /// </summary>
        /// <param name="stock_code"></param>
        /// <param name="tick"></param>
        /// <param name="sPrevNext"></param>
        public void 주식분봉차트요청(string stock_code, string tick = "1", string sPrevNext = "0")
        {
            if (m_axKHOpenAPI.GetMasterCodeName(stock_code).Equals(""))
            {
                Console.WriteLine("잘못된 종목 코드입니다. 다시 입력해 주세요.");
                return;
            }

            Console.WriteLine("분봉차트 요청 함수 호출");

            //Delay(3600);
            //Delay(800);

            m_axKHOpenAPI.SetInputValue("종목코드", stock_code.Trim());
            m_axKHOpenAPI.SetInputValue("틱범위", tick);
            m_axKHOpenAPI.SetInputValue("수정주가구분", "1");

            m_axKHOpenAPI.CommRqData("정보_주식분봉차트요청", "opt10080", Int32.Parse(sPrevNext), 화면번호_차트조회);

            //차단기의 상태를 확인해 통과시킬지 여부를 결정한다.
            autoEvent.Reset();
            autoEvent.WaitOne();
        }
        /// <summary>
        /// 종목 분봉 리스트에 각 분봉의 정보를 초기화 한다.
        /// </summary>
        /// <param name="e"></param>
        public void 주식분봉차트이벤트(ref AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) //최대 900개 조회
        {
            //기존 종목의 일봉 정보 제거
            리스트_종목분봉.Clear();

            func_count += 1;
            string 종목코드 = m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim();
            int 종목분봉수 = m_axKHOpenAPI.GetRepeatCnt(e.sTrCode, e.sRQName);

            for (int i = 0; i < 종목분봉수; i++)
            {
                List<string> data = new List<string>();
                data.Add("");
                data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim().Replace("+","").Replace("-",""));
                data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "거래량").Trim());
                data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "거래대금").Trim());
                data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "체결시간").Trim());
                data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "시가").Trim().Replace("+", "").Replace("-", ""));
                data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "고가").Trim().Replace("+", "").Replace("-", ""));
                data.Add(m_axKHOpenAPI.GetCommData(e.sTrCode, e.sRQName, i, "저가").Trim().Replace("+", "").Replace("-", ""));
                data.Add("");

                리스트_종목분봉.Add(data);
            }
            Console.WriteLine("{0} : {1}일 ({2})", 종목코드, 종목분봉수, func_count);

                //임시로 일봉 받아온 종목들의 정보를 파일로 저장
                string 종목이름 = m_axKHOpenAPI.GetMasterCodeName(종목코드);
                string path = @".\";
                path += 종목이름 + ".txt";

                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        foreach (List<string> data in 리스트_종목분봉)
                        {
                            sw.WriteLine("{0};{1};{2}", data[4], 종목코드, data[1]);
                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        foreach (List<string> data in 리스트_종목분봉)
                        {
                            sw.WriteLine("{0};{1};{2}", data[4], 종목코드, data[1]);
                        }
                    }
                }
                func_count = 0;

                Console.WriteLine("분봉 데이터 저장 완료");
            

            autoEvent.Set();
        }



        //=========== API 유틸 =================================
        //
        public List<List<string>> Get_종목분봉()
        {
            return 리스트_종목분봉;
        }

        //함수 다시 호출할 때 사자린 데이터도 맞춰서 연동할 수 있게(가능할까?)
        //나중에 딕셔너리 최적화 <= 꼭 하자!!
        public void 종목파일읽기()
        {
            string line;
            using(StreamReader sr = new StreamReader(@".\stock_list.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] tmp_line = line.Split(';');
                    if (!사전_종목파일.ContainsKey(tmp_line[0]))
                    {
                        Dictionary<string, string> 임시사전 = new Dictionary<string, string>();
                        임시사전.Add("종목명", tmp_line[1]);
                        임시사전.Add("현재가", tmp_line[2]);

                        사전_종목파일.Add(tmp_line[0], 임시사전);
                    }
                    else
                    {
                        사전_종목파일[tmp_line[0]]["종목명"] = tmp_line[1];
                        사전_종목파일[tmp_line[0]]["현재가"] = tmp_line[2];
                    }
                }
            }
            Console.WriteLine("종목 파일 읽기 완료");
        }
        //나중에 딕셔너리 최적화 <= 꼭 하자!!
        /// <summary>
        /// 각 사전에 저장되어 있는 종목을 중복을 제외하여 스크린 번호를 부여하고 스크린번호 사전에 보관한다.
        /// </summary>
        public void 종목화면번호정리()
        {
            //사전_종목스크린번호.Clear();

            Dictionary<string, Dictionary<string, string>> 임시사전 = new Dictionary<string, Dictionary<string, string>>();

            //각 사전에 있는 종목을 불러와 종목의 중복을 제거하고 사전에 추가(이미 종목 코드가 들어있으면 넘기는 방식으로)
            foreach(string 종목코드 in 사전_계좌평가잔고.Keys)
            {
                if (!임시사전.ContainsKey(종목코드))
                    임시사전.Add(종목코드, null);
            }
            foreach(var 주문번호 in 사전_미체결.Keys)
            {
                if (!임시사전.ContainsKey(사전_미체결[주문번호]["종목코드"]))
                    임시사전.Add(사전_미체결[주문번호]["종목코드"], null);
            }
            //로드된 종목 검사
            foreach(var 종목코드 in 사전_종목파일.Keys)
            {
                if (!임시사전.ContainsKey(종목코드))
                    임시사전.Add(종목코드, null);
            }
           

            //스크린 번호 할당
            int count_per_scr= 0;
            
            foreach (string 종목코드 in 임시사전.Keys)
            {
                int 잔고스크린번호 = int.Parse(화면번호_잔고조회);
                int 주문스크린번호 = int.Parse(화면번호_주문조회);

                //한 스크린 번호 당 80개의 종목의 정보를 가진다.
                if (count_per_scr % 80 == 0)
                {
                    잔고스크린번호 += 1;
                    주문스크린번호 += 1;
                    //기본 화면번호를 변경해준다.
                    화면번호_잔고조회 = 잔고스크린번호.ToString();
                    화면번호_주문조회 = 주문스크린번호.ToString();
                }

                Dictionary<string, string> 스크린번호사전 = new Dictionary<string, string>();
                스크린번호사전.Add("잔고스크린번호", 잔고스크린번호.ToString());
                스크린번호사전.Add("주문스크린번호", 주문스크린번호.ToString());

                if (사전_종목스크린번호.ContainsKey(종목코드))
                {
                    사전_종목스크린번호[종목코드]["잔고스크린번호"] = 화면번호_잔고조회.ToString();
                    사전_종목스크린번호[종목코드]["주문스크린번호"] = 화면번호_주문조회.ToString();
                }
                else
                {
                    사전_종목스크린번호.Add(종목코드, 스크린번호사전);
                }
                count_per_scr += 1;
            }
            foreach(var scrno in 사전_종목스크린번호)
            {
                Console.WriteLine("{0} : {1} : {2}", scrno.Key, scrno.Value["잔고스크린번호"], scrno.Value["주문스크린번호"]);
            }
            Console.WriteLine("종목 스크린번호 할당 완료");
            
        }




        /// <summary>
        /// 조회 시 줄 딜레이 함수(3.6초 권장)
        /// </summary>
        private static void Delay(int ms = 3600)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, ms);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
        }
        

        //public void Calc()
        //{
        //    else
        //    {
        //        int total_price;
        //        bool pass_success = false;
        //        Console.WriteLine("총 일수 : {0}", 계산데이터.Count);

        //        //120일 이평선을 그릴만큼의 데이터가 있는지 체크
        //        if (계산데이터 == null || 계산데이터.Count < 120)
        //        {
        //            pass_success = false;
        //        }
        //        else // 120일 이상 되면
        //        {

        //            total_price = 0;
        //            for (int i = 0; i < 120; i++)
        //            {
        //                total_price += Int32.Parse(계산데이터[i][1]); // 현재가 합
        //            }
        //            int 오늘이평선 = total_price / 120; // 오늘자 120일 이평선

        //            bool bottom_stock_price = false;
        //            int check_price = 0;
        //            if (Int32.Parse(계산데이터[0][7]) <= 오늘이평선 && 오늘이평선 <= Int32.Parse(계산데이터[0][6]))
        //            {
        //                Console.WriteLine("오늘 주가 120일 이평선에 걸쳐있는 것 확인");
        //                bottom_stock_price = true;
        //                check_price = Int32.Parse(계산데이터[0][6]);
        //            }
        //            //과거 일봉들이 120일 이평선보다 밑에 있는지 확인
        //            //그렇게 확인을 하다가 일봉이 120일 이평선보다 위에 있으면 계산 진행
        //            int prev_price = 0;
        //            if (bottom_stock_price == true)
        //            {
        //                int 오늘이평선_전 = 0;
        //                bool price_top_moving = false;
        //                int idx = 1;
        //                while (true)
        //                {
        //                    //120일치가 있는지 계속 확인
        //                    if (계산데이터.Count - idx < 120)
        //                    {
        //                        Console.WriteLine("120일치가 없음!");
        //                        break;
        //                    }
        //                    total_price = 0;
        //                    for (int i = idx; i < 120 + idx; i++)
        //                    {
        //                        total_price += Int32.Parse(계산데이터[i][1]);
        //                    }
        //                    오늘이평선_전 = total_price / 120;

        //                    if (오늘이평선_전 <= Int32.Parse(계산데이터[idx][6]) && idx <= 20)
        //                    {
        //                        Console.WriteLine("20일 동안 주가가 120일 이평선과 같거나 위에 있으면 조건 통과 못함");
        //                        price_top_moving = false;
        //                        break;
        //                    }
        //                    else if (Int32.Parse(계산데이터[idx][7]) > 오늘이평선_전 && idx > 20)
        //                    {
        //                        Console.WriteLine("120일 이평선 위에 있는 일봉 확인됨");
        //                        price_top_moving = true;
        //                        prev_price = Int32.Parse(계산데이터[idx][7]);
        //                        break;
        //                    }

        //                    idx += 1;
        //                }
        //                //해당 부분 이평선이 가장 최근 일자의 이평선 가격보다 낮은지 확인
        //                if (price_top_moving == true)
        //                {
        //                    if (오늘이평선 > 오늘이평선_전 && check_price > prev_price)
        //                    {
        //                        Console.WriteLine("포착된 이평선의 가격이 오늘자(최근일자) 이평선 가격보다 낮은것 확인됨");
        //                        Console.WriteLine("포착된 부분의 일봉 저가가 오늘자 일봉의 고가보다 낮은지 확인됨");
        //                        pass_success = true;
        //                    }
        //                }
        //            }
        //        }

        //        if (pass_success == true)
        //        {
        //            Console.WriteLine("조건부 통과됨");

        //        }
        //        else if (pass_success == false)
        //        {
        //            Console.WriteLine("조건부 통과 못함");

        //            string code_nm = m_axKHOpenAPI.GetMasterCodeName(code);
        //            string path = @"C:\Users\SeongYun\Desktop\GitHub_ToyProject\KiwoomTrader\test.txt";
        //            if (!File.Exists(path))
        //            {
        //                using (File.Create(path)) { }
        //                using (StreamWriter sw = File.AppendText(path))
        //                {
        //                    sw.WriteLine("{0};{1};{2}", code, code_nm, 계산데이터[0][1]);
        //                }
        //            }
        //            else
        //            {
        //                using (StreamWriter sw = File.AppendText(path))
        //                {
        //                    sw.WriteLine("{0};{1};{2}", code, code_nm, 계산데이터[0][1]);
        //                }
        //            }
        //        }
        //        계산데이터.Clear();
        //        calcEvent.Set();

        //    }

    }
}
