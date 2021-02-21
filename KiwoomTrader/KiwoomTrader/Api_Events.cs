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


        }

    }
}
