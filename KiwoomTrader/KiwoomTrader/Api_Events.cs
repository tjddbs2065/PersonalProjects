using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KiwoomTrader
{
    public partial class Api
    {        
        /// <summary>
        /// 로그인 시 호출되는 이벤트
        /// </summary>
        private void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                //성공
                Console.WriteLine("로그인 성공");
                로그인정보이벤트();

                //계좌 비밀번호 입력
                패스워드();
            }
            else
            {
                //실패
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
        /// TR 요청(주로 조회)이 오면 호출되는 이벤트
        /// </summary>
        private void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sRQName.Equals("정보_예수금상세현황요청"))
            {
                //예수금상세현황요청 시 발생하는 이벤트 처리
                예수금상세이벤트(ref e);
            }
            else if (e.sRQName.Equals("정보_계좌평가잔고내역요청"))
            {
                //계좌평가잔고요청 시 발생하는 이벤트 처리
                계좌평가잔고이벤트(ref e);
            }
            else if (e.sRQName == "정보_미체결요청")
            {
                //미체결요청시 발생하는 이벤트 처리
                미체결이벤트(ref e);
            }
            else if (e.sRQName == "정보_주식일봉차트요청")
            {
                주식일봉차트이벤트(ref e);
            }
            else if (e.sRQName.Equals("정보_주식분봉차트요청"))
            {
                주식분봉차트이벤트(ref e);
            }

        }

        /// <summary>
        /// 실시간 요청을 하면 실시간 데이터를 받을 때마다 호출되는 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            Console.WriteLine(e.sRealType);
            if (e.sRealType == "장시작시간")
            {
                int fid = RealType.REALTYPE.장시작시간.장운영구분;
                string value = m_axKHOpenAPI.GetCommRealData(e.sRealKey, fid);
                if (value == "0")
                    Console.WriteLine("장 시작 전");
                else if (value == "3")
                    Console.WriteLine("장 시작");
                else if (value == "2")
                    Console.WriteLine("장 종료, 동시호가로 넘어감");
                else if (value == "4")
                    Console.WriteLine("3시30분 장 종료");
            }
            else if(e.sRealType == "주식체결")
            {
                Console.WriteLine(m_axKHOpenAPI.GetCommRealData(e.sRealKey, RealType.REALTYPE.주식체결.현재가));
                Console.WriteLine(m_axKHOpenAPI.GetCommRealData(e.sRealKey, RealType.REALTYPE.주식체결.등락율));
                Console.WriteLine(m_axKHOpenAPI.GetCommRealData(e.sRealKey, RealType.REALTYPE.주식체결.거래량));
                Console.WriteLine(m_axKHOpenAPI.GetCommRealData(e.sRealKey, RealType.REALTYPE.주식체결.체결시간));

            }
        }
    }
}
