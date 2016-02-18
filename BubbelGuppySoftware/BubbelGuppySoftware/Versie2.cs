using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BubbelGuppySoftware
{
    public partial class Versie2 : Form
    {
        //Settings
        private const Keys vooruit = Keys.W;
        private const Keys links = Keys.A;
        private const Keys rechts = Keys.D;
        private const Keys achteruit = Keys.S;

        private const String ip = "192.168.4.1";
        private const int fromPort = 8888;
        private const int destPort = 8888;

        //Vars
        private byte[] buffer;

        public Versie2()
        {
            InitializeComponent();
            buffer = new byte[2];
        }

        private void SendUdp(byte[] data)
        {
            using (UdpClient c = new UdpClient(fromPort))
                c.Send(data, data.Length, ip, destPort);
        }

        private void Versie2_KeyDown(object sender, KeyEventArgs e)
        {
            KeyHandeler(e, false);
        }

        private void Versie2_KeyUp(object sender, KeyEventArgs e)
        {
            KeyHandeler(e, true);
        }

        private void KeyHandeler(KeyEventArgs e, bool up)
        {
            if (up)
            {
                switch (e.KeyCode)
                {
                    case vooruit:
                    case achteruit:
                        if (buffer[0] == 0)
                        {
                            e.Handled = true;
                            return;
                        }
                        buffer[0] = 0;
                        SendUdp(new byte[2] { 0, 255 });
                        e.Handled = true;
                        break;
                    case links:
                    case rechts:
                        if (buffer[1] == 0)
                        {
                            e.Handled = true;
                            return;
                        }
                        buffer[1] = 0;
                        SendUdp(new byte[2] { 255, 0 });
                        e.Handled = true;
                        break;
                }
            }
            else
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
                        SendUdp(new byte[2] { 1, 255 });
                        e.Handled = true;
                        break;
                    case achteruit:
                        if (buffer[0] == 2)
                        {
                            e.Handled = true;
                            return;
                        }
                        buffer[0] = 2;
                        SendUdp(new byte[2] { 2, 255 });
                        e.Handled = true;
                        break;
                    case links:
                        if (buffer[1] == 3)
                        {
                            e.Handled = true;
                            return;
                        }
                        buffer[1] = 3;
                        SendUdp(new byte[2] { 255, 1 });
                        e.Handled = true;
                        break;
                    case rechts:
                        if (buffer[1] == 4)
                        {
                            e.Handled = true;
                            return;
                        }
                        buffer[1] = 4;
                        SendUdp(new byte[2] { 255, 2 });
                        e.Handled = true;
                        break;
                }
            }
        }
    }
}
