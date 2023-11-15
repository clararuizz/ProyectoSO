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
        }


        private void partidasJugadasButton_Click(object sender, EventArgs e)
        {
            string mensaje = "4/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
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
            }
        }
        public void TomaRespuesta1(string mensaje)
        {
            MessageBox.Show("Respuesta: " + mensaje);
        }
        public void TomaRespuesta2(string[] trozos)
        {
            if (trozos[1] == "0")
                MessageBox.Show("Respuesta: " + trozos[2]);
            else
                MessageBox.Show("No hay datos");
        }
        private void Consultas_Load(object sender, EventArgs e)
        {
            usuarioLabel.Text = Convert.ToString(this.ID);
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
