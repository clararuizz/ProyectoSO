using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Partida : Form
    {
        Socket server;
        int numPartida;
        string user;
        string player1;//seria linvitador
        string player2;
        string player3;
        string player4;
        Random rnd;
        int counter;
        int duration;
        public Partida(Socket s,int nPartida,string user, string[] trozos)
        {
            InitializeComponent();
            rnd=new Random();
            this.server = s;
            this.user=user;
            this.numPartida = nPartida;
            this.player1 = trozos[2];
            player1Label.Visible = true;
            player1Label.Text= player1;
            if(trozos.Length>=4 ) {
                this.player2 = trozos[3];
                player2Label.Visible = true;
                player2Label.Text = player2;
            }
            if (trozos.Length >= 5)
            {
                this.player3 = trozos[4];
                player3Label.Visible = true;
                player3Label.Text = player3;
            }
            if (trozos.Length >= 6)
            {
                this.player4 = trozos[5];
                player4Label.Visible = true;
                player4Label.Text = player4;
            }
            chatTextBox.AppendText("Started game "+Convert.ToString(this.numPartida) + "\r\n");
        }
        private void Partida_Load(object sender, EventArgs e)
        {
            timer.Interval = 150;
            PartidaDGV.ColumnHeadersVisible = false;
            PartidaDGV.RowHeadersVisible = false;
            string mensaje = "14/" + numPartida.ToString() + "/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        public int DameNumPartida() //devuelve el numero de la partida de este formulario
        {
            return numPartida;
        }

        private void sendButton_Click(object sender, EventArgs e) //envia mensaje al servidor para el chat
        {
            string text = writeTextBox.Text.Trim();
            if (text.Length!=0)
            {
                string mensaje = "13/" + Convert.ToString(this.numPartida) + "/" + this.user + "/"+text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }
        public void addMessage(string[] trozos) //añade mensaje recibido en el chat
        {
            string sender = trozos[2];
            string text = trozos[3];
            if(sender==user)
            {
                sender = "Me";
            }
            chatTextBox.AppendText(sender+": "+text +"\r\n");
            writeTextBox.Text = "";
        }

        private void tirarButton_Click(object sender, EventArgs e) //pone en marcha el dado
        {
            duration = rnd.Next(15, 30);
            timer.Start();
        }
        private void UpdateDado(int num) //actualiza foto dado
        {
            switch (num)
            {
                case 1:
                    dadoPicture.Image = Image.FromFile("one.png");
                    break;
                case 2:
                    dadoPicture.Image = Image.FromFile("two.png");
                    break;
                case 3:
                    dadoPicture.Image = Image.FromFile("three.png");
                    break;
                case 4:
                    dadoPicture.Image = Image.FromFile("four.png");
                    break;
                case 5:
                    dadoPicture.Image = Image.FromFile("five.png");
                    break;
                case 6:
                    dadoPicture.Image = Image.FromFile("six.png");
                    break;
            }
        }
        public void UpdatePartidaDGV(string[] trozos) //actualiza tablero de info partida
        {
            PartidaDGV.DataSource = null;
            PartidaDGV.ColumnCount = 3;
            PartidaDGV.RowCount = Convert.ToInt32(trozos[2]) + 1;
            PartidaDGV[0, 0].Value = "Jugadores";
            PartidaDGV[1, 0].Value = "Dinero";
            PartidaDGV[2, 0].Value = "Posicion";
            int l = 3;
            for(int k = 1; k< Convert.ToInt32(trozos[2])+1; k++)
            {
                PartidaDGV[0, k].Value = trozos[l];
                PartidaDGV[1, k].Value = trozos[l + 1];
                PartidaDGV[2, k].Value = trozos[l + 2];
                l = l + 3;
            }
        }

        private void timer_Tick(object sender, EventArgs e) //en cada tick cambia el valor hasta que se para y envia el valor final
        {
            int value=rnd.Next(1, 6);
            UpdateDado(value);
            if (counter>duration)
            {
                timer.Stop();
                counter = 0;
                string mensaje = "15/" + value.ToString() + "/" + user + "/" + numPartida.ToString() + "/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                counter++;
            }
        }
    }
}
