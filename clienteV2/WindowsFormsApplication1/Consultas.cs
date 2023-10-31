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

namespace WindowsFormsApplication1
{
    public partial class Consultas : Form
    {
        int ID;
        Socket server;
        public Consultas(Socket S,int iD)
        {
            InitializeComponent();
            this.ID = iD;
            this.server = S;
        }

        private void masPuntosButton_Click(object sender, EventArgs e)
        {
            string mensaje = "3/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            //Recibimos mensaje del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }

        private void partidasJugadasButton_Click(object sender, EventArgs e)
        {
            string mensaje = "4/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            //Recibimos mensaje del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show(mensaje);
        }

        private void MoneyButton_Click(object sender, EventArgs e)
        {
            string name = (nombreTextBox.Text).Trim();
            if(name.Length==0)
            {
                MessageBox.Show("Introduce Nombre");
            }
            else
            {
                string mensaje = "5/" + name;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] res = mensaje.Split('/');
                if (res[0] == "1")
                {
                    MessageBox.Show("Query Not Found");
                }
                else
                    MessageBox.Show("Dinero: " + res[1]);
            }

        }

        private void Consultas_Load(object sender, EventArgs e)
        {
            usuarioLabel.Text = Convert.ToString(this.ID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mensaje = "7/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            //Recibimos mensaje del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            string[] partes;
            partes = mensaje.Split('/');
            string lista = string.Join(" ",partes);
            MessageBox.Show("Lost jugadores conectados son: " + lista);

        }
    }
}
