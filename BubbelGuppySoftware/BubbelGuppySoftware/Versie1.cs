using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BubbelGuppySoftware
{
    public partial class Versie1 : Form
    {
        //Settings
        private const Keys vooruit = Keys.W;
        private const Keys links = Keys.A;
        private const Keys rechts = Keys.D;
        private const Keys achteruit = Keys.S;

        private const String ip = "";
        private const int fromPort = 55000;
        private const int destPort = 55000;

        //Vars
        private byte[] buffer;

        public Versie1()
        {
            InitializeComponent();
            buffer = new byte[2];
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(unused => KeyHandeler(e));
        }

        private void KeyHandeler(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case vooruit:
                    if (buffer[0] == 1)
                    {
                        e.Handled = true;
                        return;
                    }
                    buffer[0] = 1;
                    e.Handled = true;
                    break;
                case achteruit:
                    if (buffer[0] == 2)
                    {
                        e.Handled = true;
                        return;
                    }
                    buffer[0] = 2;
                    e.Handled = true;
                    break;
                case links:
                    if (buffer[1] == 3)
                    {
                        e.Handled = true;
                        return;
                    }
                    buffer[1] = 3;
                    e.Handled = true;
                    break;
                case rechts:
                    if (buffer[1] == 4)
                    {
                        e.Handled = true;
                        return;
                    }
                    buffer[1] = 4;
                    e.Handled = true;
                    break;
            }

        }

        private void SendUdp(byte[] data)
        {
            using (UdpClient c = new UdpClient(fromPort))
                c.Send(data, data.Length, ip, destPort);
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            SendUdp(buffer.ToArray());
            buffer[0] = buffer[1] = 0;
        }
    }
}
