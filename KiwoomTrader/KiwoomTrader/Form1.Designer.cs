namespace KiwoomTrader
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btn_예수금 = new System.Windows.Forms.Button();
            this.btn_계좌평가잔고내역 = new System.Windows.Forms.Button();
            this.btn_미체결 = new System.Windows.Forms.Button();
            this.btn_일봉 = new System.Windows.Forms.Button();
            this.tb_종목코드 = new System.Windows.Forms.TextBox();
            this.btn_종목파일읽기 = new System.Windows.Forms.Button();
            this.btn_종목스크린번호할당 = new System.Windows.Forms.Button();
            this.btn_실시간_시작 = new System.Windows.Forms.Button();
            this.btn_분봉버튼 = new System.Windows.Forms.Button();
            this.cht_분봉 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.cht_분봉)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_예수금
            // 
            this.btn_예수금.Location = new System.Drawing.Point(12, 12);
            this.btn_예수금.Name = "btn_예수금";
            this.btn_예수금.Size = new System.Drawing.Size(50, 47);
            this.btn_예수금.TabIndex = 0;
            this.btn_예수금.Text = "예수금 조회";
            this.btn_예수금.UseVisualStyleBackColor = true;
            this.btn_예수금.Click += new System.EventHandler(this.btn_예수금_Click);
            // 
            // btn_계좌평가잔고내역
            // 
            this.btn_계좌평가잔고내역.Location = new System.Drawing.Point(68, 12);
            this.btn_계좌평가잔고내역.Name = "btn_계좌평가잔고내역";
            this.btn_계좌평가잔고내역.Size = new System.Drawing.Size(75, 47);
            this.btn_계좌평가잔고내역.TabIndex = 1;
            this.btn_계좌평가잔고내역.Text = "계좌평가 잔고내역";
            this.btn_계좌평가잔고내역.UseVisualStyleBackColor = true;
            this.btn_계좌평가잔고내역.Click += new System.EventHandler(this.btn_계좌평가잔고내역_Click);
            // 
            // btn_미체결
            // 
            this.btn_미체결.Location = new System.Drawing.Point(149, 12);
            this.btn_미체결.Name = "btn_미체결";
            this.btn_미체결.Size = new System.Drawing.Size(55, 47);
            this.btn_미체결.TabIndex = 2;
            this.btn_미체결.Text = "미체결 조회";
            this.btn_미체결.UseVisualStyleBackColor = true;
            this.btn_미체결.Click += new System.EventHandler(this.btn_미체결_Click);
            // 
            // btn_일봉
            // 
            this.btn_일봉.Location = new System.Drawing.Point(137, 77);
            this.btn_일봉.Name = "btn_일봉";
            this.btn_일봉.Size = new System.Drawing.Size(67, 47);
            this.btn_일봉.TabIndex = 3;
            this.btn_일봉.Text = "종목 일봉 조회";
            this.btn_일봉.UseVisualStyleBackColor = true;
            this.btn_일봉.Click += new System.EventHandler(this.btn_일봉_Click);
            // 
            // tb_종목코드
            // 
            this.tb_종목코드.Location = new System.Drawing.Point(12, 91);
            this.tb_종목코드.Name = "tb_종목코드";
            this.tb_종목코드.Size = new System.Drawing.Size(119, 21);
            this.tb_종목코드.TabIndex = 4;
            // 
            // btn_종목파일읽기
            // 
            this.btn_종목파일읽기.Location = new System.Drawing.Point(724, 12);
            this.btn_종목파일읽기.Name = "btn_종목파일읽기";
            this.btn_종목파일읽기.Size = new System.Drawing.Size(75, 47);
            this.btn_종목파일읽기.TabIndex = 5;
            this.btn_종목파일읽기.Text = "파일읽기";
            this.btn_종목파일읽기.UseVisualStyleBackColor = true;
            this.btn_종목파일읽기.Click += new System.EventHandler(this.btn_종목파일읽기_Click);
            // 
            // btn_종목스크린번호할당
            // 
            this.btn_종목스크린번호할당.Location = new System.Drawing.Point(643, 12);
            this.btn_종목스크린번호할당.Name = "btn_종목스크린번호할당";
            this.btn_종목스크린번호할당.Size = new System.Drawing.Size(75, 47);
            this.btn_종목스크린번호할당.TabIndex = 6;
            this.btn_종목스크린번호할당.Text = "종목스크린번호할당";
            this.btn_종목스크린번호할당.UseVisualStyleBackColor = true;
            this.btn_종목스크린번호할당.Click += new System.EventHandler(this.btn_종목스크린번호할당_Click);
            // 
            // btn_실시간_시작
            // 
            this.btn_실시간_시작.Location = new System.Drawing.Point(643, 65);
            this.btn_실시간_시작.Name = "btn_실시간_시작";
            this.btn_실시간_시작.Size = new System.Drawing.Size(75, 47);
            this.btn_실시간_시작.TabIndex = 7;
            this.btn_실시간_시작.Text = "실시간조회 시작";
            this.btn_실시간_시작.UseVisualStyleBackColor = true;
            this.btn_실시간_시작.Click += new System.EventHandler(this.btn_실시간_시작_Click);
            // 
            // btn_분봉버튼
            // 
            this.btn_분봉버튼.Location = new System.Drawing.Point(12, 138);
            this.btn_분봉버튼.Name = "btn_분봉버튼";
            this.btn_분봉버튼.Size = new System.Drawing.Size(75, 45);
            this.btn_분봉버튼.TabIndex = 8;
            this.btn_분봉버튼.Text = "분봉 버튼";
            this.btn_분봉버튼.UseVisualStyleBackColor = true;
            this.btn_분봉버튼.Click += new System.EventHandler(this.btn_분봉버튼_Click);
            // 
            // cht_분봉
            // 
            this.cht_분봉.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            chartArea1.AxisX.InterlacedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.AxisX.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX2.InterlacedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.AxisX2.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.InterlacedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.AxisY.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY2.InterlacedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.AxisY2.LineColor = System.Drawing.Color.Silver;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.cht_분봉.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.cht_분봉.Legends.Add(legend1);
            this.cht_분봉.Location = new System.Drawing.Point(93, 138);
            this.cht_분봉.Name = "cht_분봉";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = System.Drawing.Color.Red;
            series1.CustomProperties = "PriceDownColor=Blue, PriceUpColor=Red";
            series1.Legend = "Legend1";
            series1.Name = "현재가";
            series1.YValuesPerPoint = 4;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series2.Legend = "Legend1";
            series2.Name = "60일선";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.DimGray;
            series3.Legend = "Legend1";
            series3.Name = "120일선";
            this.cht_분봉.Series.Add(series1);
            this.cht_분봉.Series.Add(series2);
            this.cht_분봉.Series.Add(series3);
            this.cht_분봉.Size = new System.Drawing.Size(695, 300);
            this.cht_분봉.TabIndex = 9;
            this.cht_분봉.Text = "cht_분봉";
            this.cht_분봉.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.cht_분봉_AxisViewChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cht_분봉);
            this.Controls.Add(this.btn_분봉버튼);
            this.Controls.Add(this.btn_실시간_시작);
            this.Controls.Add(this.btn_종목스크린번호할당);
            this.Controls.Add(this.btn_종목파일읽기);
            this.Controls.Add(this.tb_종목코드);
            this.Controls.Add(this.btn_일봉);
            this.Controls.Add(this.btn_미체결);
            this.Controls.Add(this.btn_계좌평가잔고내역);
            this.Controls.Add(this.btn_예수금);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cht_분봉)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_예수금;
        private System.Windows.Forms.Button btn_계좌평가잔고내역;
        private System.Windows.Forms.Button btn_미체결;
        private System.Windows.Forms.Button btn_일봉;
        private System.Windows.Forms.TextBox tb_종목코드;
        private System.Windows.Forms.Button btn_종목파일읽기;
        private System.Windows.Forms.Button btn_종목스크린번호할당;
        private System.Windows.Forms.Button btn_실시간_시작;
        private System.Windows.Forms.Button btn_분봉버튼;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht_분봉;
    }
}

