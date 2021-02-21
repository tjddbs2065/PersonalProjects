using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiwoomTrader
{
    public partial class Form1 : Form
    {
        private Api api;

        public Form1()
        {
            InitializeComponent();

            api = new Api();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (api.LogIn() == KiwoomConst.Success)
            {
                Console.WriteLine("로그인 창 띄우기 성공.");
            }
            else
            {
                Console.WriteLine("로그인 창 띄우기 실패.");
                Application.Exit();   
            }

        }

        private void btn_예수금_Click(object sender, EventArgs e)
        {
            api.예수금_조회();
        }

        private void btn_계좌평가잔고내역_Click(object sender, EventArgs e)
        {
            api.계좌평가잔고내역요청_조회();
        }

        private void btn_미체결_Click(object sender, EventArgs e)
        {
            api.미체결_조회();
        }

        private void btn_tmp_Click(object sender, EventArgs e)
        {
            api.계산함수();
        }
    }
}
