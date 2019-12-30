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

namespace Server
{
    public partial class Form1 : Form
    {
        NetworkStream ns;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Main();
        }
        void Main()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback,11000);
            listener.Start(10);
            listener.BeginAcceptTcpClient(new AsyncCallback(ClientConnected),listener);

        }

        byte[] b = new byte[1024];
        int i = 0;
        List<TcpClient> clients = new List<TcpClient>();
        private void ClientConnected(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            TcpClient client = listener.EndAcceptTcpClient(ar);

            lbClients.Items.Add("Client "+ ++i);
            clients.Add(client);

            b = new byte[1024];
            NetworkStream ns = client.GetStream();
            ns.BeginRead(b,0,b.Length,new AsyncCallback(Read),ns);
            
            listener.BeginAcceptTcpClient(new AsyncCallback(ClientConnected), listener);
        }

        private void Read(IAsyncResult ar)
        {           
            ns = (NetworkStream)ar.AsyncState;
            int count = ns.EndRead(ar);
            var received = Encoding.ASCII.GetString(b, 0, count);
           
            
            txtMessages.Text += received+ "\n";

            ns.BeginRead(b, 0, b.Length, new AsyncCallback(Read), ns);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var index = lbClients.SelectedIndex;
                TcpClient client = (TcpClient)clients[index];
                ns = client.GetStream();

                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(txtWriteMsg.Text);
                sw.Flush();
                txtWriteMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            
        }
    }
}
