using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerApp
{
    public partial class Form1 : Form
    {
        private Socket serverSocket;
        private Socket clientSocket;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                // Sunucu soketi oluştur
                IPAddress localAddress = IPAddress.Parse("192.168.0.103");
                IPEndPoint localEndPoint = new IPEndPoint(localAddress, 12345);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // İstemciden gelen bağlantıları dinle
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(10);

                // İstemciden gelen bağlantıyı kabul et
                DisplayMessage("Bağlantı bekleniyor...");
                clientSocket = serverSocket.Accept();
                DisplayMessage("İstemci bağlandı.");

                // Veriyi al
                byte[] data = new byte[1024];
                int receivedBytes = clientSocket.Receive(data);
                string message = Encoding.ASCII.GetString(data, 0, receivedBytes);
                DisplayMessage("Gelen mesaj: " + message);

                // Soketi kapat
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                serverSocket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void DisplayMessage(string message)
        {
            // TextBox kontrolüne mesajı ekleyerek ekranda göster
            txtLog.AppendText(message + Environment.NewLine);
        }
    }
}
