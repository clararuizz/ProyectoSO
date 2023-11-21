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

        bool MCon = false;
        int conectado = 0;
        int logged = 0;
        int ID;
        int inv = 0;
        string userName;
        string[] trozos;
        string[] Conectados;

        Register R;
        Consultas C;
        RecoverPassword Pass;
        Monopoly M;
        List<Partida> Partidas = new List<Partida>();

        delegate void DelegadoParaEscribirEnLabel(string mensaje);
        delegate void DelegadoParaEscribirEnDataGrid(string[] mensaje);
        delegate void DelegadoParaNumPartida(int num);
        delegate void DelegadoParaMostrarInvitaciones(string[] invitaciones);
        delegate void DelegadoParaChat(string[] cachos);

        private void PonerEnMarchaPartida(int nPartida, string[] trozos) //abre nuevo formulario de partida
        {
            Partida P=new Partida(this.server,nPartida,this.userName, trozos);
            Partidas.Add(P);
            P.Text = "Partida "+nPartida.ToString();
            P.ShowDialog();
        }
        public Home()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            checkConnected.Checked = false;
            userLabel.Text = "Not Logged";
            usernameTextBox.Text = "Marcel";
            passwordTextBox.Text = "123456";
            conectadosGridView.ColumnHeadersVisible = false;
            conectadosGridView.RowHeadersVisible = false;
        }
        private void AtenderServidor() //thread para atender respuestas del servidor
        {
            while (true)
            {

                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                trozos = (Encoding.ASCII.GetString(msg2).Split('\0')[0]).Split('/');
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
                            DelegadoParaEscribirEnLabel delegado = new DelegadoParaEscribirEnLabel(PonUserEnLabel);
                            userLabel.Invoke(delegado, new object[] {userName});
                        }
                        else
                        {
                            MessageBox.Show("Error Al Logearse");
                        }
                        break;
                    case 2:
                        mensaje = trozos[1];
                        Pass.DameAns(mensaje);
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
                        mensaje = trozos[1];
                        R.TomaAns(Convert.ToInt32(mensaje));

                        break;
                    case 7:
                        Conectados = trozos;
                        DelegadoParaEscribirEnDataGrid delegado_2 = new DelegadoParaEscribirEnDataGrid(updateDataGridView);
                        conectadosGridView.Invoke(delegado_2, new object[] { Conectados });
                        if (MCon == true)
                        {
                            DelegadoParaEscribirEnDataGrid delegado_5 = new DelegadoParaEscribirEnDataGrid(M.DameConectados);
                            M.Invoke(delegado_5, new object[] { Conectados });
                        }
                        break;
                    case 8:
                        MessageBox.Show(trozos[1]+" te ha invitado a la partida: " + trozos[2]);
                        break;

                    case 10:
                        DelegadoParaMostrarInvitaciones delegado_4 = new DelegadoParaMostrarInvitaciones(M.DameInvitaciones);
                        M.Invoke(delegado_4, new object[] { trozos });
                        break;

                    case 11: //partida creada
                        int numPar = Convert.ToInt32(trozos[1]);
                        DelegadoParaNumPartida delegado_3 = new DelegadoParaNumPartida(M.setLabelNum);
                        M.Invoke(delegado_3, new object[] {numPar});
                        break;
                    case 12:
                        MessageBox.Show("La partida " + trozos[1] + " puede empezar");
                        ThreadStart ts = delegate { PonerEnMarchaPartida(Convert.ToInt32(trozos[1]),trozos); };
                        Thread T = new Thread(ts);
                        T.Start();
                        break;
                    case 13:
                        bool encontrado = false;
                        int j = 0;
                        int partida = 0;
                        while ((encontrado == false) && (j < Partidas.Count()))
                        {
                            partida = Partidas[j].DameNumPartida();
                            if (partida == Convert.ToInt32(trozos[1]))
                            {
                                encontrado = true;
                            }
                            else
                                j++;
                        }
                        DelegadoParaChat delegado_6 = new DelegadoParaChat(Partidas[j].addMessage);
                        Partidas[j].Invoke(delegado_6, new object[] { trozos });
                        break;
                    case 14:
                        if (Convert.ToInt32(trozos[2]) == -1)
                            MessageBox.Show("la partida numero " + trozos[1] + " ha sido cancelada");
                        break;
                    case 15:
                        if (Convert.ToInt32(trozos[1]) != -1)
                        {
                            encontrado = false;
                            j = 0;
                            partida = 0;
                            while ((encontrado == false) && (j < Partidas.Count()))
                            {
                                partida = Partidas[j].DameNumPartida();
                                if (partida == Convert.ToInt32(trozos[1]))
                                {
                                    encontrado = true;
                                }
                                else
                                    j++;
                            }
                            if (encontrado == true)
                            {
                                DelegadoParaEscribirEnDataGrid delegado_7 = new DelegadoParaEscribirEnDataGrid(Partidas[j].UpdatePartidaDGV);
                                Partidas[j].Invoke(delegado_7, new object[] { trozos });
                            }
                        }
                        else
                            MessageBox.Show("Ha ocurrido una tirada erronea");
                        break;
                }
            }
        }

        private void connectButton_Click(object sender, EventArgs e)

        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("XXXXXX");
            IPEndPoint ipep = new IPEndPoint(direc, 9051);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                MessageBox.Show("Conectado");
                conectado = 1;
                checkConnected.Checked = true;
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();
                ////update Conectados
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
        private void disconnectButton_Click(object sender, EventArgs e) //desconectarse del servidor
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

        private void enterButton_Click(object sender, EventArgs e) //funcion para hacer el login
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
        private void logOutButton_Click(object sender, EventArgs e) //cerrar sesion en el usuario
        {
            if ((logged == 0) || (conectado == 0))
            {
                MessageBox.Show("Not Logged Yet");
            }
            else
            {
                string mensaje = "9/"+userName;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                userName = null;
                ID = 0;
                logged = 0;
                userLabel.Text = "notLogged";
            }
        }

        private void forgotLink_Click(object sender, EventArgs e) //abrir formulario de recover password
        {
            if (conectado == 0)
            {
                MessageBox.Show("Client Not Yet Conected");
            }
            else
            {
                Pass = new RecoverPassword(server);
                Pass.ShowDialog();
            }
        }

        private void registerButton_Click(object sender, EventArgs e)//abrir formulario para registrarse
        {
            if (conectado == 0)
            {
                MessageBox.Show("Client Not Yet Conected");
            }
            else
            {
                R = new Register(server);
                R.ShowDialog();
            }
        }

        private void consultasButton_Click(object sender, EventArgs e)//abrir formulario de consultas
        {
            if((logged == 0) ||(conectado == 0))
            {
                MessageBox.Show("Not logged");
            }
            else
            {
                C = new Consultas(server, ID);
                C.ShowDialog();
            }
        }

        private void playButton_Click(object sender, EventArgs e)//entrar al formulario de gestion de monopoly
        {
            if ((logged == 0) || (conectado == 0))
            {
                MessageBox.Show("Not logged");
            }
            else
            {
                MCon = true;
                M = new Monopoly(server, userName, Conectados);
                M.ShowDialog();
                MCon = false;
            }
        }
        private void updateDataGridView(string[] partes)//actualizar grid tabla de conectados
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


        public void PonUserEnLabel(string userName)
        {
            userLabel.Text = userName;
        }
        private void Home_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            string mensaje = "9/"+userName;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            //Mensaje de desconexión
            mensaje = "0/";

            msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}
