using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double dt = 0.01;
        const double g = 9.81;

        double a;
        double v0;
        double y0;
        int time;

        double t;
        double x;
        double y;
        private void btStart_Click(object sender, EventArgs e)
        {
            a = (double)edAngle.Value * Math.PI / 180;
            v0 = (double)edSpeed.Value;
            y0 = (double)edHeight.Value;
            time = 0;

            t = 0;
            x = 0;
            y = y0;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);

            double h = v0 * v0 * Math.Sin(a) * Math.Sin(a) / (2 * g);
            double l = v0 * v0 * Math.Sin(2 * a) / g;
            if (y0 != 0)
            {
                l += y0 / Math.Tan(a);
            }

            chart1.ChartAreas[0].AxisY.Maximum = h + y0;
            chart1.ChartAreas[0].AxisX.Maximum = l;

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            label5.Text = time.ToString();
            t += dt;
            x = v0 * Math.Cos(a) * t;
            y = y0 + v0 * Math.Sin(a) * t - g * t * t / 2;
            chart1.Series[0].Points.AddXY(x, y);
            if (y <= 0) timer1.Stop();
        }

        private void Stop_btn_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
