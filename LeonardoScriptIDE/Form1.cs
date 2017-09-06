using Autofac;
using LeonardoScriptCore.Serial;
using LeonardoScriptIDE.WINApi;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LeonardoScriptIDE.WINApi.WindowsOP;

namespace LeonardoScriptIDE
{
    public partial class Form1 : Form
    {
        IntPtr hwdFinded = IntPtr.Zero;
        IntPtr hwdApp = IntPtr.Zero;
        IntPtr hwdTemp = IntPtr.Zero;

        Image imagePre;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pic_wndFinder.Image = Image.FromFile(Application.StartupPath + "\\Source\\FindWndHome.bmp");
            Module.Initial();
            GetComList();
        }
        /// <summary>
        /// 获取本机可用串口
        /// </summary>
        public void GetComList()
        {
            string[] PortNames = LeonardoScriptCore.Serial.SerialPortClient.GetAvailableComs();
            this.comboBox1.Items.Clear();
            foreach (string sName in PortNames)
            {
                string sValue = sName;
                this.comboBox1.Items.Add(sValue);
            }
            if (PortNames.Length > 0)
            {
                comboBox1.Text = "";
                comboBox1.SelectedText = PortNames[0];
            }
        }

        private void btn_ref_Click(object sender, EventArgs e)
        {
            GetComList();
        }

        private void btn_opn_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                var client = Module.Container.Resolve<ISerialPortClient>();
                client.Start(comboBox1.Text, 9600);
                if (client.IsOpen)
                {
                    label_sta.Text = comboBox1.Text + " 已打开";
                }
            }
        }

        private void pic_wndFinder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                txt_GetAppName.Text = "";
                txt_GetHwnd.Text = "";

                //设置查找图标光标
                Cursor.Current = new Cursor(Application.StartupPath + "\\Source\\findwnd.cur");
                //变更背景图片
                imagePre = pic_wndFinder.Image;
                pic_wndFinder.Image = Image.FromFile(Application.StartupPath + "\\Source\\FindWndGone.bmp");
                //设置本控件捕获鼠标，处理相应的鼠标事件
                pic_wndFinder.Capture = true;

                hwdFinded = IntPtr.Zero;
            }
        }

        private void pic_wndFinder_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.Bounds.Contains(Cursor.Position))
            {
                hwdFinded = WindowFromPoint(Cursor.Position);
                if (hwdFinded != IntPtr.Zero)
                {
                    //输出句柄
                    txt_GetHwnd.Text = hwdFinded.ToString("X").PadLeft(8, '0');
                    //输出标题
                    StringBuilder strTemp = new StringBuilder(256);
                    //GetWindowText(hwdFinded, strTemp, strTemp.Capacity);
                    SendMessage(hwdFinded, 0x000D, 256, strTemp);

                    txt_Title.Text = strTemp.ToString();

                    //输出类名
                    RealGetWindowClass(hwdFinded, strTemp, 256);
                    txt_Class.Text = strTemp.ToString();

                    //向上查找Windows窗体,应用程序的主窗体的父窗体句柄为IntPtr.Zero
                    IntPtr hWdParent = GetParent(hwdFinded);

                    while (hWdParent != IntPtr.Zero)
                    {
                        hwdTemp = hWdParent;
                        hWdParent = GetParent(hwdTemp);
                    }

                    StringBuilder title = new StringBuilder(256);
                    GetWindowText(hwdTemp, title, title.Capacity);
                    txt_GetAppName.Text = title.ToString();
                    if (hwdTemp != hwdFinded)
                    {
                        hwdApp = hwdTemp;
                    }
                    else
                    {
                        hwdApp = hwdFinded;
                    }

                    //输出相对位置
                    Rect rect = new Rect();
                    GetWindowRect(hwdTemp, ref rect);
                    txt_RevPos.Text = "{X=" + (Cursor.Position.X - rect.left).ToString() + ",Y=" + (Cursor.Position.Y - rect.top).ToString() + "}";

                    //输出屏幕位置
                    txt_SrcPos.Text = Cursor.Position.ToString();
                }
            }
        }

        private void pic_wndFinder_MouseUp(object sender, MouseEventArgs e)
        {
            //恢复初始状态
            Cursor.Current = Cursors.Default;
            pic_wndFinder.Image = imagePre;
            pic_wndFinder.Capture = false;
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
            WindowsOP.SetWindowPos(Marshal.StringToHGlobalAnsi(txt_GetHwnd.Text), -1, 0, 0, 0, 0, 1 | 2);
        }
    }
}
