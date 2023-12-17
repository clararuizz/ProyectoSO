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
        #region Inicializaciones
        Socket server;
        public Register(Socket s)
        {
            InitializeComponent();
            this.server = s;
        }
        #endregion

        #region Register
        public void TomaAns(int codigo)
        {
            if (codigo == 0)
                MessageBox.Show("Usuer correctly registered");
            else
                MessageBox.Show("Already exists");
        }
        private void registerButon_Click(object sender, EventArgs e)
        {
            // El usuario pide registrarse.
            string user = (usernameTextBox.Text).Trim();
            string psw = (passwordTextBox.Text).Trim();
            string email =(emailTextBox.Text).Trim();
            if ((user.Length==0)||(psw.Length==0)||(email.Length==0))
            {
                MessageBox.Show("Introduce Data");
            }
            else
            {
                string mensaje = "6/" + user + "/" + psw+"/"+email;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }
        public void ans(string mensaje)
        {
            string res_1 = mensaje.Split('/')[0];
            int res_2 = Convert.ToInt32(res_1);
            if (res_2 == 0)
            {
                MessageBox.Show("Correctly registered");
            }
            else
                MessageBox.Show("User already exists");
        }
        #endregion
    }
}
