using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        int nForm;
        Socket server;
        public Form2(int nForm, Socket server)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            numForm.Text = nForm.ToString();
        
        }

        public void TomaRespuesta3(string mensaje)
        {
            MessageBox.Show(mensaje);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string mensaje = "1/Quien Tiene Mas Puntos";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            msg = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(nForm, server);
            form3.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string mensaje = "3/Cuantas partidas se han jugado";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void MoneyButton_Click(object sender, EventArgs e)
        {
            string name = nombre.Text;
            string mensaje = "4/Quiero Saber Money/"+name;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }
}
