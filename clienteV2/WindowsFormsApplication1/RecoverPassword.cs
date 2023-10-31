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
    public partial class RecoverPassword : Form
    {
        Socket server;
        string ID;
        public RecoverPassword(Socket server)
        {
            InitializeComponent();
            this.server = server;
        }

        private void recoverButton_Click(object sender, EventArgs e)
        {
            ID=(IDTextBox.Text).Trim();
            string mensaje = "2/"+ID;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos mensaje del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show("El Password es"+mensaje);
        }
    }
}
