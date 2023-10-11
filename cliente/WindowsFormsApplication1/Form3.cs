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
    public partial class Form3 : Form
    {
        int nForm;
        Socket server;
        string ID;
        public Form3(int nForm, Socket server)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mensaje = "2/Quiero recuperar mi contraseña/" + ID;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            msg = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ID = Convert.ToString(textBox1.Text);
        }
    }
}
