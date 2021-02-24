using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KiwoomTrader
{
    public partial class Form1 : Form
    {
        private Api api;

        public Form1()
        {
            InitializeComponent();
            cht_분봉.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;

            api = new Api();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (api.로그인() == Api.Success)
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
            api.예수금상세현황요청();
        }

        private void btn_계좌평가잔고내역_Click(object sender, EventArgs e)
        {
            api.계좌평가잔고요청();
        }

        private void btn_미체결_Click(object sender, EventArgs e)
        {
            api.미체결요청();
        }

        private void btn_일봉_Click(object sender, EventArgs e)
        {
            if (!tb_종목코드.Text.Equals(""))
            {
                api.주식일봉차트요청(tb_종목코드.Text);
                tb_종목코드.Clear();
            }
        }

        private void btn_종목파일읽기_Click(object sender, EventArgs e)
        {
            api.종목파일읽기();
        }

        private void btn_종목스크린번호할당_Click(object sender, EventArgs e)
        {
            api.종목화면번호정리();
        }

        private void btn_실시간_시작_Click(object sender, EventArgs e)
        {
            api.실시간요청등록();
        }

        private void btn_분봉버튼_Click(object sender, EventArgs e)
        {
            if (!tb_종목코드.Text.Equals(""))
            {
                api.주식분봉차트요청(tb_종목코드.Text);
                tb_종목코드.Clear();

                foreach(var i in cht_분봉.Series)
                {
                    i.Points.Clear();
                }

                List<List<string>> 분봉리스트 = api.Get_종목분봉();
                분봉리스트.Reverse();

                for (int idx = 510; idx < 900; idx++)
                {
                    Console.WriteLine(분봉리스트[idx][4]);
                    cht_분봉.Series[0].Points.Add(Int32.Parse(분봉리스트[idx][1])); //현재가 출력

                    bool pass_success = true;
                    if (분봉리스트 == null || 분봉리스트.Count < 120)
                    {
                        pass_success = false;
                    }
                    else // 120일 이상 되면
                    {
                        int total_price = 0;
                        for (int i = idx - 60; i < idx; i++)
                        {
                            total_price += Int32.Parse(분봉리스트[i][1]); // 현재가 합
                        }
                        int 이평선60 = total_price / 60;
                        cht_분봉.Series[1].Points.Add(이평선60);

                        total_price = 0;
                        for (int i = idx-120; i < idx; i++)
                        {
                            total_price += Int32.Parse(분봉리스트[i][1]); // 현재가 합
                        }
                        int 이평선120 = total_price / 120; // 오늘자 120일 이평선
                        cht_분봉.Series[2].Points.Add(이평선120);

                    }
                }
                분봉리스트.Clear();
            }
        }

        private void cht_분봉_AxisViewChanged(object sender, System.Windows.Forms.DataVisualization.Charting.ViewEventArgs e)
        {
            if (sender.Equals(cht_분봉))
            {
                int start_position = (int)e.Axis.ScaleView.ViewMinimum;
                int end_position = (int)e.Axis.ScaleView.ViewMaximum;

                //double yMinValue = double.MaxValue;
                double yMaxValue = double.MinValue;

                for (int i = start_position; i < end_position; i++)
                {
                    Series s = cht_분봉.Series[0];

                    if (i < s.Points.Count)
                    {
                        yMaxValue = Math.Max(yMaxValue, s.Points[i].YValues[0]);
                        //yMinValue = Math.Min(yMinValue, s.Points[i].YValues[1]);
                    }
                }
                
                cht_분봉.ChartAreas[0].AxisY.Maximum = yMaxValue + (yMaxValue*0.2);
                cht_분봉.ChartAreas[0].AxisY.Minimum = yMaxValue - (yMaxValue * 0.2);
            }
        }
    }
}
