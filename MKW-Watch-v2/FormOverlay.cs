using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MKW_Watch_v2.Utils;
using System.Linq;
using System.IO;
using System.Text;

namespace MKW_Watch_v2
{
       
    public partial class FormOverlay : Form
    {

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public FormOverlay()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            TransparencyKey = BackColor;

        }


        ProcessMemoryReader mreader = new ProcessMemoryReader();

        int bytesOut = 0;

        public void FormOverlay_Load_1(object sender, EventArgs e)
        {
            TopMost = true;

            Timer timer1 = new Timer();
            timer1.Interval = (1); // 1ms
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();

            FormBorderStyle = FormBorderStyle.None;

            Process process = Process.GetProcessesByName("Dolphin").ToList().FirstOrDefault();
            if (process != null)
            {
                mreader.ReadProcess = process;
                mreader.OpenProcess();
            }
            else
            {
                throw new ArgumentNullException("Dolphin is not running!");
            }

        }


        private void FormOverlay_Paint(object sender, PaintEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            uint PlayerBase;

            // Game ID
            long GameID = BitConverter.ToInt64(mreader.ReadMemory((IntPtr)(0x7FFF0000), 8, out bytesOut), 0);
            byte[] bytes = BitConverter.GetBytes(GameID);

            string GameIDString = Encoding.ASCII.GetString(bytes);

            // Automatic version selector

            if (GameIDString == "RMCE01\0\0")
            {
                PlayerBase = 0x9BD110;
            }
            else if (GameIDString == "RMCP01\0\0")
            {
                PlayerBase = 0x9C18F8;
            }
            else if (GameIDString == "RMCJ01\0\0")
            {
                PlayerBase = 0x9C0958;
            }
            else if (GameIDString == "RMCK01\0\0")
            {
                PlayerBase = 0x9AFF38;
            }
            else
            {
                PlayerBase = 0;
            }

            int screenX = Screen.PrimaryScreen.Bounds.Width;
            int screenY = Screen.PrimaryScreen.Bounds.Height;

            bool checkIfInRace;
            // PlayerBase
            uint PlayerBaseOffset = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(0x7FFF0000 + PlayerBase), 4, out bytesOut), 0);
            byte[] offsetbytes = BitConverter.GetBytes(PlayerBaseOffset);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            PlayerBaseOffset = BitConverter.ToUInt32(offsetbytes, 0);

            if (PlayerBaseOffset != 0)
            {
                checkIfInRace = true;
            }

            else
            {
                checkIfInRace = false;
            }

            // offset 1
            PlayerBaseOffset = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x20), 4, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(PlayerBaseOffset);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            PlayerBaseOffset = BitConverter.ToUInt32(offsetbytes, 0);

            // offset 2        
            PlayerBaseOffset = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0), 4, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(PlayerBaseOffset);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            PlayerBaseOffset = BitConverter.ToUInt32(offsetbytes, 0);

            // offset 3
            PlayerBaseOffset = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x10), 4, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(PlayerBaseOffset);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            PlayerBaseOffset = BitConverter.ToUInt32(offsetbytes, 0);

            // offset 4
            PlayerBaseOffset = BitConverter.ToUInt32(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x10), 4, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(PlayerBaseOffset);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            PlayerBaseOffset = BitConverter.ToUInt32(offsetbytes, 0);

            // Airtime
            short air = BitConverter.ToInt16(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x21A), 4, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(air);
            Array.Reverse(offsetbytes);
            air = BitConverter.ToInt16(offsetbytes, 0);

            char[] arrayAir = air.ToString().ToCharArray();
            int Air = arrayAir.Length;

            // MT Charge
            short MT_Charge = BitConverter.ToInt16(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0xFE), 2, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(MT_Charge);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            MT_Charge = BitConverter.ToInt16(offsetbytes, 0);

            // MT Charge
            short Kart_MT_Charge = BitConverter.ToInt16(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x100), 2, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(Kart_MT_Charge);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            Kart_MT_Charge = BitConverter.ToInt16(offsetbytes, 0);

            // SSMT Charge
            short SSMT_Charge = BitConverter.ToInt16(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x14C), 2, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(SSMT_Charge);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            SSMT_Charge = BitConverter.ToInt16(offsetbytes, 0);

            int All_MT = Math.Max((MT_Charge + Kart_MT_Charge), SSMT_Charge);
            All_MT = Math.Max(All_MT, SSMT_Charge);

            char[] arrayAll_MT = All_MT.ToString().ToCharArray();
            int MT = arrayAll_MT.Length;


            // Boost
            short Trick_Boost = BitConverter.ToInt16(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x114), 4, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(Trick_Boost);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            Trick_Boost = BitConverter.ToInt16(offsetbytes, 0);

            short Mushroom_Boost = BitConverter.ToInt16(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x110), 4, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(Mushroom_Boost);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            Mushroom_Boost = BitConverter.ToInt16(offsetbytes, 0);

            short MT_Boost = BitConverter.ToInt16(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x10C), 4, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(MT_Boost);
            Array.Reverse(offsetbytes, 0, offsetbytes.Length);
            MT_Boost = BitConverter.ToInt16(offsetbytes, 0);

            short All_Boost = Math.Max(Trick_Boost, Mushroom_Boost);
            All_Boost = Math.Max(All_Boost, MT_Boost);

            char[] arrayAll_Boost = All_Boost.ToString().ToCharArray();
            int BoostArrayLength = arrayAll_Boost.Length;

            // Speed
            float num = BitConverter.ToSingle(mreader.ReadMemory((IntPtr)(0x7FFF0000 + (PlayerBaseOffset - 0x80000000) + 0x20), 4, out bytesOut), 0);
            offsetbytes = BitConverter.GetBytes(num);
            Array.Reverse(offsetbytes);
            num = BitConverter.ToSingle(offsetbytes, 0);
            char[] FLarray = num.ToString().ToCharArray();
            int numfloat = FLarray.Length;            

            int num3 = (int)num;
            char[] array = num3.ToString().ToCharArray();
            int num4 = array.Length;

            int initialStyle = GetWindowLong(Handle, -20);
            SetWindowLong(Handle, -20, initialStyle | 0x80000 | 0x20);

            numBox3.Visible = true;
            numBox2.Visible = true;
            numBox1.Visible = true;

            int screenX4 = (int)Math.Round(screenX * 0.87);
            int screenY4 = (int)Math.Round(screenY * 0.869);


            kmhBox1.Location = new Point(screenX4, screenY4);
            kmhBox1.ImageLocation = @"Icons\KMH.png";            
            kmhBox1.SendToBack();

            kmhBox1.Width = (int)Math.Round(screenX * 0.0744791666666667);
            kmhBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);
            kmhBox1.SizeMode = PictureBoxSizeMode.Zoom;

            int screenY6 = (int)Math.Round(screenY * 0.4694444444444444);

            // Air Meter Location            

            airTextureBox1.ImageLocation = @"Icons\Air.png";            
            airTextureBox1.SendToBack();

            airTextureBox1.Location = new Point((int)Math.Round(screenX * 0.8098958333333333), screenY6);
            airBox3.Location = new Point((int)Math.Round(screenX * 0.854999998), screenY6);
            airBox2.Location = new Point((int)Math.Round(screenX * 0.879999999), screenY6);
            airBox1.Location = new Point((int)Math.Round(screenX * 0.905), screenY6);

            airBox3.Width = (int)Math.Round(screenX * 0.0260416666666667);
            airBox2.Width = (int)Math.Round(screenX * 0.0260416666666667);
            airBox1.Width = (int)Math.Round(screenX * 0.0260416666666667);
            airTextureBox1.Width = (int)Math.Round(screenX * 0.0333333333333333);

            airBox3.Height = (int)Math.Round(screenY * 0.0592592592592593);
            airBox2.Height = (int)Math.Round(screenY * 0.0592592592592593);
            airBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);
            airTextureBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);

            airBox3.SizeMode = PictureBoxSizeMode.Zoom;
            airBox2.SizeMode = PictureBoxSizeMode.Zoom;
            airBox1.SizeMode = PictureBoxSizeMode.Zoom;
            airTextureBox1.SizeMode = PictureBoxSizeMode.Zoom;


            int screenY5 = (int)Math.Round(screenY * 0.5342592592592593);

            // MT Meter Location            

            mtTextureBox1.ImageLocation = @"Icons\MT.png";
            
            mtTextureBox1.SendToBack();

            mtTextureBox1.Location = new Point((int)Math.Round(screenX * 0.8098958333333333), screenY5);
            mtBox3.Location = new Point((int)Math.Round(screenX * 0.854999998), screenY5);
            mtBox2.Location = new Point((int)Math.Round(screenX * 0.879999999), screenY5);
            mtBox1.Location = new Point((int)Math.Round(screenX * 0.905), screenY5);

            mtBox3.Width = (int)Math.Round(screenX * 0.0260416666666667);
            mtBox2.Width = (int)Math.Round(screenX * 0.0260416666666667);
            mtBox1.Width = (int)Math.Round(screenX * 0.0260416666666667);
            mtTextureBox1.Width = (int)Math.Round(screenX * 0.0333333333333333);

            mtBox3.Height = (int)Math.Round(screenY * 0.0592592592592593);
            mtBox2.Height = (int)Math.Round(screenY * 0.0592592592592593);
            mtBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);
            mtTextureBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);

            mtBox3.SizeMode = PictureBoxSizeMode.Zoom;
            mtBox2.SizeMode = PictureBoxSizeMode.Zoom;
            mtBox1.SizeMode = PictureBoxSizeMode.Zoom;
            mtTextureBox1.SizeMode = PictureBoxSizeMode.Zoom;


            int screenY3 = (int)Math.Round(screenY * 0.5990740740740741);

            // Boost Meter Location

            boostTextureBox1.ImageLocation = @"Icons\Boost.png";            
            boostTextureBox1.SendToBack();

            boostTextureBox1.Location = new Point((int)Math.Round(screenX * 0.8098958333333333), screenY3);
            boostBox3.Location = new Point((int)Math.Round(screenX * 0.854999998), screenY3);
            boostBox2.Location = new Point((int)Math.Round(screenX * 0.879999999), screenY3);
            boostBox1.Location = new Point((int)Math.Round(screenX * 0.905), screenY3);

            boostBox3.Width = (int)Math.Round(screenX * 0.0260416666666667);
            boostBox2.Width = (int)Math.Round(screenX * 0.0260416666666667);
            boostBox1.Width = (int)Math.Round(screenX * 0.0260416666666667);
            boostTextureBox1.Width = (int)Math.Round(screenX * 0.0333333333333333);

            boostBox3.Height = (int)Math.Round(screenY * 0.0592592592592593);
            boostBox2.Height = (int)Math.Round(screenY * 0.0592592592592593);
            boostBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);
            boostTextureBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);

            boostBox3.SizeMode = PictureBoxSizeMode.Zoom;
            boostBox2.SizeMode = PictureBoxSizeMode.Zoom;
            boostBox1.SizeMode = PictureBoxSizeMode.Zoom;
            boostTextureBox1.SizeMode = PictureBoxSizeMode.Zoom;


            int screenY2 = (int)Math.Round(screenY * 0.82);

            // Speedometer Location

            coronBox1.Location = new Point((int)Math.Round(screenX * 0.9197916666666667), screenY2);
            coronBox1.Width = (int)Math.Round(screenX * 0.0333333333333333);
            coronBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);
            coronBox1.SizeMode = PictureBoxSizeMode.Zoom;

            numBox3.Location = new Point((int)Math.Round(screenX * 0.854999998), screenY2);
            numBox2.Location = new Point((int)Math.Round(screenX * 0.879999999), screenY2);
            numBox1.Location = new Point((int)Math.Round(screenX * 0.905), screenY2);
            numFloatBox1.Location = new Point((int)Math.Round(screenX * 0.9432291666666667), screenY2);

            numBox3.Width = (int)Math.Round(screenX * 0.0260416666666667);
            numBox2.Width = (int)Math.Round(screenX * 0.0260416666666667);
            numBox1.Width = (int)Math.Round(screenX * 0.0260416666666667);
            numFloatBox1.Width = (int)Math.Round(screenX * 0.0260416666666667);

            numBox3.Height = (int)Math.Round(screenY * 0.0592592592592593);
            numBox2.Height = (int)Math.Round(screenY * 0.0592592592592593);
            numBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);
            numFloatBox1.Height = (int)Math.Round(screenY * 0.0592592592592593);
            
            numBox3.SizeMode = PictureBoxSizeMode.Zoom;
            numBox2.SizeMode = PictureBoxSizeMode.Zoom;
            numBox1.SizeMode = PictureBoxSizeMode.Zoom;
            numFloatBox1.SizeMode = PictureBoxSizeMode.Zoom;

            if (checkIfInRace == true)
            {
                airTextureBox1.Visible = true;
                boostTextureBox1.Visible = true;
                mtTextureBox1.Visible = true;
                kmhBox1.Visible = true;

                if (Air >= 3)
                {
                    airBox3.Visible = true;
                    airBox2.Visible = true;
                    airBox1.Visible = true;
                    airBox3.Image = Image.FromFile(numFolderPath + "\\" + arrayAir[0].ToString() + ".png");
                    airBox2.Image = Image.FromFile(numFolderPath + "\\" + arrayAir[1].ToString() + ".png");
                    airBox1.Image = Image.FromFile(numFolderPath + "\\" + arrayAir[2].ToString() + ".png");
                }
                else if (Air >= 2)
                {
                    airBox3.Visible = false;
                    airBox2.Visible = true;
                    airBox1.Visible = true;
                    airBox2.Image = Image.FromFile(numFolderPath + "\\" + arrayAir[0].ToString() + ".png");
                    airBox1.Image = Image.FromFile(numFolderPath + "\\" + arrayAir[1].ToString() + ".png");
                }
                else if (Air >= 1)
                {
                    airBox3.Visible = false;
                    airBox2.Visible = false;
                    airBox1.Visible = true;
                    airBox1.Image = Image.FromFile(this.numFolderPath + "\\" + arrayAir[0].ToString() + ".png");
                }

                if (MT >= 3)
                {
                    mtBox3.Visible = true;
                    mtBox2.Visible = true;
                    mtBox1.Visible = true;
                    mtBox3.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_MT[0].ToString() + ".png");
                    mtBox2.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_MT[1].ToString() + ".png");
                    mtBox1.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_MT[2].ToString() + ".png");
                }
                else if (MT >= 2)
                {
                    mtBox3.Visible = false;
                    mtBox2.Visible = true;
                    mtBox1.Visible = true;
                    mtBox2.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_MT[0].ToString() + ".png");
                    mtBox1.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_MT[1].ToString() + ".png");
                }
                else if (MT >= 1)
                {
                    mtBox3.Visible = false;
                    mtBox2.Visible = false;
                    mtBox1.Visible = true;
                    mtBox1.Image = Image.FromFile(this.numFolderPath + "\\" + arrayAll_MT[0].ToString() + ".png");
                }

                if (BoostArrayLength >= 3)
                {
                    boostBox3.Visible = true;
                    boostBox2.Visible = true;
                    boostBox1.Visible = true;
                    boostBox3.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_Boost[0].ToString() + ".png");
                    boostBox2.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_Boost[1].ToString() + ".png");
                    boostBox1.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_Boost[2].ToString() + ".png");
                }
                else if (BoostArrayLength >= 2)
                {
                    boostBox3.Visible = false;
                    boostBox2.Visible = true;
                    boostBox1.Visible = true;
                    boostBox2.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_Boost[0].ToString() + ".png");
                    boostBox1.Image = Image.FromFile(numFolderPath + "\\" + arrayAll_Boost[1].ToString() + ".png");
                }
                else if (BoostArrayLength >= 1)
                {
                    boostBox3.Visible = false;
                    boostBox2.Visible = false;
                    boostBox1.Visible = true;
                    boostBox1.Image = Image.FromFile(this.numFolderPath + "\\" + arrayAll_Boost[0].ToString() + ".png");
                }
                if (num4 >= 3)
                {
                    numBox3.Visible = true;
                    numBox2.Visible = true;
                    numBox3.Image = Image.FromFile(numFolderPath + "\\" + array[0].ToString() + ".png");
                    numBox2.Image = Image.FromFile(numFolderPath + "\\" + array[1].ToString() + ".png");
                    numBox1.Image = Image.FromFile(numFolderPath + "\\" + array[2].ToString() + ".png");

                }
                else if (num4 >= 2)
                {
                    numBox3.Visible = false;
                    numBox2.Visible = true;
                    numBox2.Image = Image.FromFile(numFolderPath + "\\" + array[0].ToString() + ".png");
                    numBox1.Image = Image.FromFile(numFolderPath + "\\" + array[1].ToString() + ".png");
                }
                else if (num4 >= 1)
                {
                    numBox3.Visible = false;
                    numBox2.Visible = false;
                    numBox1.Image = Image.FromFile(this.numFolderPath + "\\" + array[0].ToString() + ".png");
                }

                if ((num4 >= 3) && (numfloat > 3))
                {
                    if (FLarray[3].ToString() == ",")
                    {
                        numFloatBox1.Visible = true;
                        coronBox1.Visible = true;
                        coronBox1.Image = Image.FromFile(numFolderPath + "\\coron.png");
                        numFloatBox1.Image = Image.FromFile(numFolderPath + "\\" + FLarray[4].ToString() + ".png");
                    }
                    else
                    {
                        numFloatBox1.Visible = false;
                        coronBox1.Visible = false;
                    }
                }

                else if ((num4 >= 2) && (numfloat > 2))
                {
                    if (FLarray[2].ToString() == ",")
                    {
                        numFloatBox1.Visible = true;
                        coronBox1.Visible = true;
                        coronBox1.Image = Image.FromFile(numFolderPath + "\\coron.png");
                        numFloatBox1.Image = Image.FromFile(numFolderPath + "\\" + FLarray[3].ToString() + ".png");
                    }
                    else
                    {
                        numFloatBox1.Visible = false;
                        coronBox1.Visible = false;
                    }
                }

                else if ((num4 >= 1) && (numfloat > 1))
                {
                    if (FLarray[1].ToString() == ",")
                    {
                        numFloatBox1.Visible = true;
                        coronBox1.Visible = true;
                        coronBox1.Image = Image.FromFile(numFolderPath + "\\coron.png");
                        numFloatBox1.Image = Image.FromFile(numFolderPath + "\\" + FLarray[2].ToString() + ".png");
                    }
                    else
                    {
                        numFloatBox1.Visible = false;
                        coronBox1.Visible = false;
                    }
                }

                else
                {
                    numFloatBox1.Visible = false;
                    coronBox1.Visible = false;
                }
            }
            else
            {
                airBox3.Visible = false;
                airBox2.Visible = false;
                airBox1.Visible = false;
                mtBox3.Visible = false;
                mtBox2.Visible = false;
                mtBox1.Visible = false;
                boostBox3.Visible = false;
                boostBox2.Visible = false;
                boostBox1.Visible = false;
                numBox3.Visible = false;
                numBox2.Visible = false;
                numBox1.Visible = false;
                numFloatBox1.Visible = false;
                coronBox1.Visible = false;
                airTextureBox1.Visible = false;
                boostTextureBox1.Visible = false;
                mtTextureBox1.Visible = false;
                kmhBox1.Visible = false;
            }
        }
        private string numFolderPath = Directory.GetCurrentDirectory() + "\\Font";
    }
}
