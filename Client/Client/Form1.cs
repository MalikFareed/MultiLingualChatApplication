using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using DarrenLee.Translator;

namespace Client
{
    public partial class Form1 : Form
    {        
        string lang = string.Empty;
        NetworkStream ns;
        StreamWriter sw;
        public Form1(string _lang)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            this.lang = _lang;
            
        }

        byte[] b = new byte[1024];
        public void Main()
        {
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Loopback,11000));
            ns = client.GetStream();

            sw = new StreamWriter(ns); 
           
            
            ns.BeginRead(b, 0, b.Length, new AsyncCallback(Read), ns);
        }

        private void Read(IAsyncResult ar)
        {
            NetworkStream ns = (NetworkStream)ar.AsyncState;
            int count = ns.EndRead(ar);
            var received = Encoding.ASCII.GetString(b, 0, count);
            txtMessages.Text += Translator.Translate(received, "en", lang)+"\n";
            
            ns.BeginRead(b, 0, b.Length, new AsyncCallback(Read), ns);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(Translator.Translate(txtEnterMsg.Text, lang, "en"));
                sw.Flush();
                txtEnterMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
