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
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace WindowsFormsApplication1
{
    public partial class Home : Form
    {
        Socket server;
        Thread atender;
        int conectado = 0;
        int logged = 0;
        int ID;
        string userName;

        Register R;
        Monopoly M;
        Consultas C;
        RecoverPassword Pass;

        delegate void DelegadoParaPonerTexto(string texto);
        private void PonerEnMarchaMonoPoly()
        {
            M = new Monopoly(server, userName);
            M.ShowDialog();
        }
        private void PonerEnMarchaConsultas()
        {
            C = new Consultas(server, ID);
            C.ShowDialog();
        }
        private void PonerEnMarchaRecover()
        {
            Pass = new RecoverPassword(server);
            Pass.ShowDialog();
        }
        private void PonerEnMarchaRegister()
        {
            R = new Register(server);
            R.ShowDialog();
        }
        public Home()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            checkConnected.Checked = false;
            userLabel.Text = "Not Logged";
            conectadosGridView.ColumnHeadersVisible = false;
            conectadosGridView.RowHeadersVisible = false;
            //conectadosGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        public void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string[] trozos = (Encoding.ASCII.GetString(msg2).Split('\0')[0]).Split('/');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje;

                switch (codigo)
                {
                    case 1:
                        int result = Convert.ToInt16(trozos[1]);
                        if (result == 1)
                        {
                            ID = Convert.ToInt16(trozos[2]);
                            userName = usernameTextBox.Text.Trim();
                            logged = 1;
                            userLabel.Text = userName;
                        }
                        else
                        {
                            MessageBox.Show("Error Al Logearse");
                        }
                        break;
                    case 2:
                        //mensaje = trozos[1]; AIXO ES PER PASS
                        //MessageBox.Show("El Password es" + mensaje);
                        break;
                    case 3:
                        mensaje = trozos[1];
                        MessageBox.Show(mensaje);
                        break;
                    case 4:
                        mensaje = trozos[1];
                        C.TomaRespuesta1(mensaje);
                        break;
                    case 5:
                        C.TomaRespuesta2(trozos);
                        break;
                    case 6:
                        //mensaje = trozos[1]; AIXO ES PER REG
                        //C.TomaRespuesta1(mensaje);
                        break;
                    case 7:
                        updateDataGridView(trozos);
                        break;
                    //case 8 seria invitar?
                }
            }
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.101");
            IPEndPoint ipep = new IPEndPoint(direc, 9042);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                MessageBox.Show("Conectado");
                conectado = 1;
                checkConnected.Checked = true;
                ThreadStart ts = delegate {AtenderServidor();};
                atender = new Thread(ts);
                atender.Start();
                //update Conectados
                string mensaje = "7/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

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
                atender.Abort();
                conectado = 0;
                checkConnected.Checked = false;
                server.Shutdown(SocketShutdown.Both);
                server.Close();

                conectadosGridView.DataSource = null;
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
            else if(logged==1)
            {
                MessageBox.Show("Client Already Logged");
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
            }
        }
        private void logOutButton_Click(object sender, EventArgs e)
        {
            if ((logged == 0) || (conectado == 0))
            {
                MessageBox.Show("Not Logged Yet");
            }
            else
            {
                string mensaje = "9/";
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                userName = null;
                ID = 0;
                logged = 0;
                userLabel.Text = "notLogged";
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
                ThreadStart ts = delegate { PonerEnMarchaRecover(); };
                Thread T1 = new Thread(ts);
                T1.Start();
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
                ThreadStart ts = delegate { PonerEnMarchaRegister(); };
                Thread T2 = new Thread(ts);
                T2.Start();
            }
        }


        private void consultasButton_Click(object sender, EventArgs e)
        {
            if((logged == 0) ||(conectado == 0))
            {
                MessageBox.Show("Not logged");
            }
            else
            {
                ThreadStart ts = delegate { PonerEnMarchaConsultas(); };
                Thread T3 = new Thread(ts);
                T3.Start();
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if ((logged == 0) || (conectado == 0))
            {
                MessageBox.Show("Not logged");
            }
            else
            {
                ThreadStart ts = delegate { PonerEnMarchaMonoPoly(); };
                Thread T4 = new Thread(ts);
                T4.Start();
            }
        }
        private void updateDataGridView(string[] partes)
        {
            conectadosGridView.DataSource = null;
            conectadosGridView.ColumnCount = 2;
            conectadosGridView.RowCount = partes.Length-1;
            conectadosGridView[0, 0].Value = "Total Users";
            conectadosGridView[1, 0].Value = partes[1];
            if (partes[1] != "0")
            {
                for (int i = 1; i < partes.Length-1; i++)
                {
                    conectadosGridView[1, i].Value = partes[i+1];
                    conectadosGridView[0, i].Value = Convert.ToString(i);
                }
            }
        }
    }
}
