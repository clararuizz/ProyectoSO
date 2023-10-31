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
    public partial class Home : Form
    {
        Socket server;
        int conectado = 0;
        public Home()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            checkConnected.Checked = false;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9030);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                MessageBox.Show("Conectado");
                conectado = 1;
                checkConnected.Checked = true;
                updateDataGridView();

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
        }
        private void disconnectButton_Click(object sender, EventArgs e)
        {
            if (conectado==0)
            {
                MessageBox.Show("Client Not Yet Conected");
            }
            else
            {
                //Mensaje de desconexión
                string mensaje = "0/";

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                // Nos desconectamos
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                conectado = 0;
                checkConnected.Checked = false;
            }
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            string userN = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            if (conectado == 0)
            {
                MessageBox.Show("Cliente No Conectado Al Servidor!");
            }
            else if ((userN.Length==0 )||(password.Length==0))
            {
                MessageBox.Show("Rellena el campo");
            }
            else
            {
                string mensaje = "1/" + userN + "/" + password + "/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Recibimos la respuesta del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0]; //retorna 1 si es pot loguejar amb el ID
                string[] cachos = mensaje.Split('/');
                int result = Convert.ToInt16(cachos[0]);
                if (result == 1)
                {
                    int ID = Convert.ToInt16(cachos[1]);
                    Consultas C = new Consultas(this.server, ID);
                    C.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error Al Logearse");
                }
            }
        }

        private void forgotLink_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
            {
                MessageBox.Show("Client Not Yet Conected");
            }
            else
            {
                RecoverPassword form3 = new RecoverPassword(server);
                form3.ShowDialog();
            }

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
            {
                MessageBox.Show("Client Not Yet Conected");
            }
            else
            {
                Register f = new Register(this.server);
                f.ShowDialog();
            }
        }

        private void updateDataGridView()
        {
            conectadosGridView.DataSource = null;
            string mensaje = "7/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            //Recibimos mensaje del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            string[] partes= mensaje.Split('/');
            conectadosGridView.ColumnCount = 2;
            conectadosGridView.RowCount = partes.Length;
            conectadosGridView.ColumnHeadersVisible = false;
            conectadosGridView.RowHeadersVisible = false;
            conectadosGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            conectadosGridView[0, 0].Value = "Total Users";
            conectadosGridView[1, 0].Value = partes[0];
            for (int i=1; i<partes.Length; i++)
            {
                conectadosGridView[1,i].Value= partes[i];
                conectadosGridView[0, i].Value = Convert.ToString(i);
            }

        }
    }
}
