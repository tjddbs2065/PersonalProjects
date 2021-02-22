using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace KiwoomTrader
{
    public partial class Api
    {
        /// <summary>
        /// 싱글 데이터를 담는 구조체이다.
        /// </summary>
        private 구조체_로그인정보 변수_로그인;
        private 구조체_예수금정보 변수_예수금;
        private 구조체_계좌평가잔고정보_싱글 변수_계좌평가잔고_싱글;

        /// <summary>
        /// 멀티 데이터를 담기 위한 사전 자료형들이다.
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> 사전_계좌평가잔고;
        private Dictionary<string, Dictionary<string, string>> 사전_미체결;
        private Dictionary<string, Dictionary<string, string>> 사전_종목파일;

        /// <summary>
        /// 종목 코드들의 스크린 번호를 정리해서 저장하는 사전이다.
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> 사전_종목스크린번호;

        /// <summary>
        /// 종목들의 나열을 위해 사용하는 문자열 배열이다.
        /// </summary>
        private string[] 배열_코스닥;
        private string[] 배열_코스피;

        /// <summary>
        /// 한 종목의 정보를 담기 위한 리스트이다. <= 딕셔너리로 바꿀까?
        /// </summary>
        List<List<string>> 리스트_종목일봉;

        /// <summary>
        /// 함수의 호출 횟수를 구분하기 위한 변수이다.
        /// </summary>
        private static uint func_count;


        /// <summary>
        /// 스레드 동기화를 위해 사용할 스레드 제어
        /// </summary>
        //true일 경우 차단기가 올라갔다고 생각
        private static AutoResetEvent autoEvent = new AutoResetEvent(true);
        //static AutoResetEvent calcEvent = new AutoResetEvent(true);
        //static AutoResetEvent funcEvent = new AutoResetEvent(true);


        private string 화면번호_잔고조회 = "2000";
        private string 화면번호_주문조회 = "3000";

        private string 화면번호_일봉조회 = "4000";
        private string 화면번호_실시간조회 = "5000";
    }
}
