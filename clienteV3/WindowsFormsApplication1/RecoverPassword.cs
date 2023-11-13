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
            string recoverN = IDTextBox.Text.Trim();
            int j;
            if ((recoverN.Length == 0))
            {
                MessageBox.Show("Rellena el campo");
            }
            else if (int.TryParse(recoverN, out j))
            {
                ID = (IDTextBox.Text).Trim();
                string mensaje = "2/" + ID;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("Numeros!!");
            }
        }
    }
}
