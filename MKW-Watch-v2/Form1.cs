using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MKW_Watch_v2
{
    
    public partial class Form1 : Form
    {
        
        FormOverlay frm = new FormOverlay();
        public Form1()
        {
            InitializeComponent();


            checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
            chkSpdFloat1.CheckedChanged += new EventHandler(CheckedSpdFloatChanged);
            chkAir.CheckedChanged += new EventHandler(chkAir_CheckedChanged);
            chkMT.CheckedChanged += new EventHandler(chkBst_CheckedChanged);
            chkBst.CheckedChanged += new EventHandler(chkMT_CheckedChanged);
        }

        private bool hooked;

        private void button1_Click(object sender, EventArgs e)
        {
            if (hooked)
            {
                return;
            }

            if (hooked == false)
            {
                btnConnect.Text = "Connect To Dolphin";
                frm.Hide();
                checkBox1.Hide();
                chkSpdFloat1.Hide();
                chkAir.Hide();
                chkMT.Hide();
                chkBst.Hide();
                lblStatus.Text = "Not Connected";
                lblStatus.ForeColor = Color.Red;
            }

            Process[] processesByName = Process.GetProcessesByName("Dolphin");
            if (processesByName.Length == 0)
            {
                return;
            }

            foreach (object obj in processesByName[0].Modules)
            {
                ProcessModule processModule = (ProcessModule)obj;
                if (processModule.ModuleName == "Dolphin.exe")
                {
                    baseAddress = (int)processModule.BaseAddress;
                    break;
                }
            }
            frm.Show();
            checkBox1.Show();
            chkAir.Show();
            chkMT.Show();
            chkBst.Show();
            lblStatus.Text = "Connected";
            lblStatus.ForeColor = Color.Green;
        }


        public static bool CheckBoxSpdFloatStatus = false;
        public static bool CheckBox1Status = false;
        public static bool CheckedAirChangedStatus = false;
        public static bool CheckedMTChangedStatus = false;
        public static bool CheckedBstChangedStatus = false;


        private int baseAddress;


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox1Status = checkBox1.Checked;

            if (checkBox1.Checked == true)
            {                
                chkSpdFloat1.Show();
            }
            else
            {                
                chkSpdFloat1.Hide();
            }
        }

        private void CheckedSpdFloatChanged(object sender, EventArgs e)
        {
            CheckBoxSpdFloatStatus = chkSpdFloat1.Checked;
        }

        private void chkAir_CheckedChanged(object sender, EventArgs e)
        {
            CheckedAirChangedStatus = chkAir.Checked;
        }

        private void chkBst_CheckedChanged(object sender, EventArgs e)
        {
            CheckedBstChangedStatus = chkBst.Checked;
        }

        private void chkMT_CheckedChanged(object sender, EventArgs e)
        {
            CheckedMTChangedStatus = chkMT.Checked;
        }
    }
}
