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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApplication1
{
    public partial class Register : Form
    {
        Socket server;
        public Register(Socket s)
        {
            InitializeComponent();
            this.server = s;
        }

        private void registerButon_Click(object sender, EventArgs e)
        {
            string user = (usernameTextBox.Text).Trim();
            string psw = (passwordTextBox.Text).Trim();
            string email =(emailTextBox.Text).Trim();
            if ((user.Length==0)||(psw.Length==0)||(email.Length==0))
            {
                MessageBox.Show("Introduce Datos");
            }
            else
            {
                string mensaje = "6/" + user + "/" + psw+"/"+email;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string result = mensaje.Split('/')[0];
                int res = Convert.ToInt32(result);
                if (res == 0)
                {
                    MessageBox.Show("Register Correcto");
                }
                else
                    MessageBox.Show("Usuario ya existente");
            }
        }
    }
}
