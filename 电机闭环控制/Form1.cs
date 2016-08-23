using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 电机闭环控制
{
    public partial class Form1 : Form
    {
        wifipact wifi = new wifipact(System.Enum.GetNames(typeof(SetCmd)).Length,System.Enum.GetNames(typeof(GetCmd)).Length);

        enum GetCmd
        {
            All=0,
        }
        enum SetCmd
        {
            Pid=0,
            SPEED,
            Stand,
        }
        void GetAllEvent(wifipact e)
        {
            float angle,gyro;
            angle = e.ReadInt32();
            gyro = e.ReadInt32();

            Invoke(new MethodInvoker(() =>
            {
                angleLab.Text = angle+"";
                gyroLab.Text = gyro+"";

            }));

        }
        public Form1()
        {
            InitializeComponent();
        }
        Timer t = new Timer();
        private void Form1_Load(object sender, EventArgs e)
        {

            wifi.getComRegistered((byte)GetCmd.All, GetAllEvent);
            wifi.timeOutCount = 50;
            t.Interval = 3000;
            t.Tick += new EventHandler((object s, EventArgs ex) =>
              {

              });
            //t.Start();
        }
        void scann_ok()
        {
            Invoke(new MethodInvoker(() =>
            {
                scannbut.Text = "扫描";
                scannbut.Enabled = true;
                foreach (wifipact.EquiPmentData bufe in wifi.Equi)
                {
                    listBox1.Items.Add(bufe.num.ToString() + "  " + bufe.ip.ToString() + "  " + bufe.id.ToString());
                }
            }));
        }
        private void scannbut_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            wifi.scann(scann_ok);
            scannbut.Text = "扫描中";
            scannbut.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            wifi.SelectEqui(listBox1.SelectedIndex);
        }

        bool gbut = false;
        private void getdata_Click(object sender, EventArgs e)
        {
            gbut = !gbut;
            if (gbut)
            {
                wifi.conGatCmd((byte)GetCmd.All);
                getdata.Text = "获取中";
            }
            else
            {
                wifi.stopConGatCmd((byte)GetCmd.All);
                getdata.Text = "获取数据";
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            KPlab.Text = KPtra.Value.ToString();
            KDlab.Text = ((float)KDtra.Value/10).ToString();
            EPlab.Text = ((float)EPtra.Value / 10).ToString();
            EIlab.Text = ((float)EItra.Value / 10).ToString();
            wifi.setData((byte)SetCmd.Pid,(short)KPtra.Value,(short)KDtra.Value,(short)EPtra.Value,(short)EItra.Value);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            Lspeedlab.Text = Lseedtra.Value.ToString();
            Rspeedlab.Text = Rspeedtra.Value.ToString();
            wifi.setData((byte)SetCmd.SPEED,(short)Lseedtra.Value,(short)Rspeedtra.Value);
        }

        private void Standcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (Standcheck.Checked == true)
            {
                wifi.setData((byte)SetCmd.Stand, (byte)1);
            }
            else {
                wifi.setData((byte)SetCmd.Stand, (byte)0);
            }
        }
    }
}
