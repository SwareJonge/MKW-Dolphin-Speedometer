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
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                frm.Show();
            }
            else
            {
                frm.Hide();
            }            
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
                checkBox1.Hide();
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
            checkBox1.Show();
            lblStatus.Text = "Connected";
            lblStatus.ForeColor = Color.Green;
        }

        
        private int baseAddress;
    }
}
