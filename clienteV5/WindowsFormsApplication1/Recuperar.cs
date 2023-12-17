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
    public partial class Recuperar : Form
    {
        Socket server;
        int ID;
        public Recuperar(Socket S)
        {
            InitializeComponent();
            this.server = S;
        }

        private void recoverButton_Click(object sender, EventArgs e)
        {
            string mensaje = "19/" + GameBox.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void Recuperar_Load(object sender, EventArgs e)
        {
            label1.Visible=true;
        }
    }
}
