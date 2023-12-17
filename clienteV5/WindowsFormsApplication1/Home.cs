using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Home : Form
    {
        #region Inicializaciones
        // Inicializaciones.
        Socket server;
        Thread atender;

        bool MCon = false;
        int conectado = 0;
        int logged = 0;
        int ID;
        string userName;
        string[] trozos;
        string[] Conectados;

        Register R;
        Consultas C;
        RecoverPassword Pass;
        Monopoly M;
        Comprar Comp;
        List<Partida> Partidas = new List<Partida>();

        delegate void DelegadoParaEscribirEnLabel(string mensaje);
        delegate void DelegadoParaEscribirEnDataGrid(string[] mensaje);
        delegate void DelegadoParaEscribirEnDataGrid2(string[] mensaje, string header);
        delegate void DelegadoParaNumPartida(int num);
        delegate void DelegadoParaMostrarInvitaciones(string[] invitaciones);
        delegate void DelegadoParaChat(string[] cachos);
        delegate void DelegadoParaTurno(int jugador);
        delegate void DelegadoParaCerrarPartida(int i);
        delegate void DelegadoParaAnunciarPerdedor(string n);
        delegate void DelegadoParaPonerPropiedad(Color color, int posicion);

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
            this.conectadosGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        #endregion

        #region Conexión/Desconexión
        private void connectButton_Click(object sender, EventArgs e)

        {
            // Se crea un IPEndPoint con el ip del servidor y puerto del servidor al que deseamos conectarnos
            string ipS = "10.4.119.5";
            string ipVB = "192.168.56.102";
            int sockS = 50053;
            int sockVB = 9021;
            IPAddress direc = IPAddress.Parse(ipVB);
            IPEndPoint ipep = new IPEndPoint(direc,sockVB);

            // Se crea el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
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
                MessageBox.Show("Error connecting with the server");
                return;
            }
        }
        private void disconnectButton_Click(object sender, EventArgs e) 
        {
            // Desconexión del servidor
            if (conectado == 0)
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
        #endregion

        #region AtenderServidor
        private void AtenderServidor() 
        {
            // Se atienden las peticiones del servidor.
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
                            userLabel.Invoke(delegado, new object[] { userName });
                        }
                        else if (result == 3)
                        {
                            MessageBox.Show("Account already logged in a session");
                        }
                        else
                        {
                            MessageBox.Show("LogIn Error");
                        }
                        break;
                    case 2:
                        mensaje = trozos[1];
                        Pass.DameAns(mensaje);
                        break;
                    case 3:
                        DelegadoParaEscribirEnDataGrid delegado_12 = new DelegadoParaEscribirEnDataGrid(C.UpdateRespuestasDGV1);
                        C.Invoke(delegado_12, new object[] { trozos });
                        break;
                    case 4:
                        DelegadoParaEscribirEnDataGrid2 delegado_13 = new DelegadoParaEscribirEnDataGrid2(C.UpdateRespuestasDGV2);
                        C.Invoke(delegado_13, new object[] { trozos,"Winner" });
                        break;
                    case 5:
                        DelegadoParaEscribirEnDataGrid2 delegado_14 = new DelegadoParaEscribirEnDataGrid2(C.UpdateRespuestasDGV2);
                        C.Invoke(delegado_14, new object[] { trozos,"Date" });
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
                        MessageBox.Show(trozos[1] + " has invited you to game: " + trozos[2]);
                        break;

                    case 10:
                        DelegadoParaMostrarInvitaciones delegado_4 = new DelegadoParaMostrarInvitaciones(M.DameInvitaciones);
                        M.Invoke(delegado_4, new object[] { trozos });
                        break;

                    case 11: //partida creada
                        int numPar = Convert.ToInt32(trozos[1]);
                        DelegadoParaNumPartida delegado_3 = new DelegadoParaNumPartida(M.setLabelNum);
                        M.Invoke(delegado_3, new object[] { numPar });
                        break;
                    case 12:
                        ThreadStart ts = delegate { PonerEnMarchaPartida(Convert.ToInt32(trozos[1]), trozos); };
                        Thread T = new Thread(ts);
                        T.Start();
                        break;
                    case 13:
                        bool encontrado = false;
                        int j = 0;
                        int partida = 0;
                        while ((encontrado == false) && (j < Partidas.Count()))
                        {
                            while ((encontrado == false) && (j < Partidas.Count()))
                            {
                                if (Partidas[j] != null)
                                {
                                    partida = Partidas[j].DameNumPartida();
                                    if (partida == Convert.ToInt32(trozos[1]))
                                    {
                                        encontrado = true;
                                    }
                                    else
                                        j++;
                                }
                                else
                                    j++;
                            }
                        }
                        DelegadoParaChat delegado_6 = new DelegadoParaChat(Partidas[j].addMessage);
                        Partidas[j].Invoke(delegado_6, new object[] { trozos });
                        break;
                    case 14:
                        if (Convert.ToInt32(trozos[2]) == -1)
                            MessageBox.Show("game number " + trozos[1] + " has been cancelled");
                        break;
                    case 15:
                        if (Convert.ToInt32(trozos[1]) != -1)
                        {
                            encontrado = false;
                            j = 0;
                            partida = 0;
                            while ((encontrado == false) && (j < Partidas.Count()))
                            {
                                if (Partidas[j] != null)
                                {
                                    partida = Partidas[j].DameNumPartida();
                                    if (partida == Convert.ToInt32(trozos[1]))
                                    {
                                        encontrado = true;
                                    }
                                    else
                                        j++;
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
                            MessageBox.Show("Wrong throw");
                        break;
                    case 16:
                        if (trozos[1] == "0")
                        {
                            encontrado = false;
                            j = 0;
                            partida = 0;
                            while ((encontrado == false) && (j < Partidas.Count()))
                            {
                                if (Partidas[j] != null)
                                {
                                    partida = Partidas[j].DameNumPartida();
                                    if (partida == Convert.ToInt32(trozos[2]))
                                    {
                                        MessageBox.Show("The property " + trozos[3] + " has a rent of " + trozos[4] + ", and it is from " + trozos[6] + ",you have to pay him!");
                                        mensaje = "17/" + trozos[2] + "/" + userName + "/" + trozos[4] + "/" + trozos[6];
                                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                                        server.Send(msg);
                                        encontrado = true;
                                    }
                                    else
                                        j++;
                                }
                                else
                                    j++;
                            }
                        }
                        else
                        {
                            if (trozos[2] == "1")
                            {
                                Comp = new Comprar(server, userName);
                                Comp.DameInfo(trozos);
                                Comp.ShowDialog();
                                

                            }
                            else
                                MessageBox.Show("The property " + trozos[4] + " is on sale for " + trozos[5] + " but you can not afford it");
                        }
                        break;
                    case 17:
                        encontrado = false;
                        j = 0;
                        partida = 0;
                        while ((encontrado == false) && (j < Partidas.Count()))
                        {
                            if (Partidas[j] != null)
                            {
                                partida = Partidas[j].DameNumPartida();
                                if (partida == Convert.ToInt32(trozos[1]))
                                {
                                    encontrado = true;
                                }
                                else
                                    j++;
                            }
                            else
                                j++;
                        }
                        if (encontrado)
                        {
                            string texto = "The player " + trozos[2] + " has bought " + trozos[3];
                            DelegadoParaEscribirEnLabel delegado_9 = new DelegadoParaEscribirEnLabel(Partidas[j].PonNoti);
                            Partidas[j].Invoke(delegado_9, new object[] { texto });
                            string[] players = new string[4];
                            Color[] colores = new Color[4];
                            players = Partidas[j].DameJugadores();
                            colores = Partidas[j].DameColor();
                            if (trozos[2] == players[0])
                            {
                                Color miColor = colores[0];
                                int pos = Convert.ToInt32(trozos[4]);
                                DelegadoParaPonerPropiedad delegado = new DelegadoParaPonerPropiedad(Partidas[j].PonPropiedad);
                                Partidas[j].Invoke(delegado, new object[] { miColor, pos });
                            }
                            if (trozos[2] == players[1])
                            {
                                Color miColor = colores[1];
                                int pos = Convert.ToInt32(trozos[4]);
                                DelegadoParaPonerPropiedad delegado = new DelegadoParaPonerPropiedad(Partidas[j].PonPropiedad);
                                Partidas[j].Invoke(delegado, new object[] { miColor, pos });
                            }
                            if (trozos[2] == players[2])
                            {
                                Color miColor = colores[2];
                                int pos = Convert.ToInt32(trozos[4]);
                                DelegadoParaPonerPropiedad delegado = new DelegadoParaPonerPropiedad(Partidas[j].PonPropiedad);
                                Partidas[j].Invoke(delegado, new object[] { miColor, pos });
                            }
                            if (trozos[2] == players[3])
                            {
                                Color miColor = colores[3];
                                int pos = Convert.ToInt32(trozos[4]);
                                DelegadoParaPonerPropiedad delegado = new DelegadoParaPonerPropiedad(Partidas[j].PonPropiedad);
                                Partidas[j].Invoke(delegado, new object[] { miColor, pos });
                            }
                        }
                        break;
                    case 18:
                        encontrado = false;
                        j = 0;
                        partida = 0;
                        //MessageBox.Show(trozos[1]+" "+trozos[2]);
                        while ((encontrado == false) && (j < Partidas.Count()))
                        {
                            if (Partidas[j] != null)
                            {
                                partida = Partidas[j].DameNumPartida();
                                if (partida == Convert.ToInt32(trozos[1]))
                                {
                                    encontrado = true;
                                }
                                else
                                    j++;
                            }
                            else
                                j++;
                        }
                        if (encontrado)
                        {
                            DelegadoParaTurno delegado_8 = new DelegadoParaTurno(Partidas[j].DameTurno);
                            Partidas[j].Invoke(delegado_8, new object[] { Convert.ToInt16(trozos[2]) });
                        }
                        break;
                    case 19:
                        encontrado = false;
                        j = 0;
                        partida = 0;
                        while ((encontrado == false) && (j < Partidas.Count()))
                        {
                            if (Partidas[j] != null)
                            {
                                partida = Partidas[j].DameNumPartida();
                                if (partida == Convert.ToInt32(trozos[1]))
                                {
                                    encontrado = true;
                                }
                                else
                                    j++;
                            }
                            else
                                j++;
                        }
                        if (encontrado)
                        {
                            DelegadoParaEscribirEnLabel delegado_9 = new DelegadoParaEscribirEnLabel(Partidas[j].PonNoti);
                            Partidas[j].Invoke(delegado_9, new object[] { trozos[2] });
                        }
                        break;
                    case 20:
                        encontrado = false;
                        j = 0;
                        partida = 0;
                        while ((encontrado == false) && (j < Partidas.Count()))
                        {
                            if (Partidas[j] != null)
                            {
                                partida = Partidas[j].DameNumPartida();
                                if (partida == Convert.ToInt32(trozos[1]))
                                {
                                    encontrado = true;
                                }
                                else
                                    j++;
                            }
                            else
                                j++;
                        }
                        if (encontrado)
                        {
                            Partidas[j].PonCarcel();
                            DelegadoParaEscribirEnLabel delegado_9 = new DelegadoParaEscribirEnLabel(Partidas[j].PonNoti);
                            Partidas[j].Invoke(delegado_9, new object[] { "¡You are going to jail!, wait 2 throws" });
                        }
                        break;
                    case 21:
                        encontrado = false;
                        j = 0;
                        partida = 0;
                        while ((encontrado == false) && (j < Partidas.Count()))
                        {
                            if (Partidas[j] != null)
                            {
                                partida = Partidas[j].DameNumPartida();
                                if (partida == Convert.ToInt32(trozos[1]))
                                {
                                    encontrado = true;
                                }
                                else
                                    j++;
                            }
                            else
                                j++;
                        }
                        if (encontrado)
                        {
                            if (trozos[2] == "empate")
                            {
                                MessageBox.Show("All players must abandon the game.");
                                DelegadoParaCerrarPartida delegat = new DelegadoParaCerrarPartida(Partidas[j].Cerrar);
                                Partidas[j].Invoke(delegat, new object[] { j });
                            }
                            else
                            {
                                MessageBox.Show("The winner of this game is: " + trozos[2].ToString() + ". All players must abandon the game.");
                                DelegadoParaCerrarPartida delegat = new DelegadoParaCerrarPartida(Partidas[j].Cerrar);
                                Partidas[j].Invoke(delegat, new object[] { j });
                            }
                            Partidas[j] = null;
                        }
                        break;
                    case 22:
                        encontrado = false;
                        j = 0;
                        partida = 0;
                        while ((encontrado == false) && (j < Partidas.Count()))
                        {
                            if (Partidas[j] != null)
                            {
                                partida = Partidas[j].DameNumPartida();
                                if (partida == Convert.ToInt32(trozos[1]))
                                {
                                    encontrado = true;
                                }
                                else
                                    j++;
                            }
                            else
                                j++;
                        }
                        if (encontrado)
                        {
                            if (trozos[2] == userName)
                            {
                                MessageBox.Show("You've run out of money, the game ends for you.");
                                //DelegadoParaCerrarPartida delegat = new DelegadoParaCerrarPartida(Partidas[j].Cerrar);
                                //Partidas[j].Invoke(delegat, new object[] { j });
                            }
                        }
                        break;
                    case 23:
                        if (Convert.ToInt32(trozos[1]) == 0)
                        {
                            MessageBox.Show("The user has been deleted correctly.");
                        }
                        break;
                    case 24:
                        encontrado = false;
                        j = 0;
                        partida = 0;
                        while ((encontrado == false) && (j < Partidas.Count()))
                        {
                            if (Partidas[j] != null)
                            {
                                partida = Partidas[j].DameNumPartida();
                                if (partida == Convert.ToInt32(trozos[1]))
                                {
                                    encontrado = true;
                                }
                                else
                                    j++;
                            }
                            else
                                j++;
                        }
                        if (encontrado)
                        {
                            //MessageBox.Show("You've run out of money, the game ends for you.");
                            DelegadoParaAnunciarPerdedor delegat = new DelegadoParaAnunciarPerdedor(Partidas[j].NuevoPerdedor);
                            Partidas[j].Invoke(delegat, new object[] { trozos[2] });
                        }
                        break;
                    case 25:
                        MessageBox.Show("Error, the game can not restart, someone is missing");
                        break;
                
                }
            }
        }
        #endregion

        #region LogIn/LogOut
        private void enterButton_Click(object sender, EventArgs e) 
        {
            // El usuario hace LogIn [Codigo: 1].
            string userN = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            if (conectado == 0)
            {
                MessageBox.Show("Client not connected to server!");
            }
            else if (logged == 1)
            {
                MessageBox.Show("Client Already Logged");
            }
            else if ((userN.Length == 0) || (password.Length == 0))
            {
                MessageBox.Show("Fill the box");
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
            // El usuario hace LogOut [Código: 9].
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
        private void Home_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            string mensaje = "9/" + userName;
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
        private void showPassword_Click_1(object sender, EventArgs e)
        {
            // Para que el usuario vea o no su contraseña.
            Bitmap show = new Bitmap("openeye.jpg");
            Bitmap hide = new Bitmap("closedeye.jpg");
            if (passwordTextBox.UseSystemPasswordChar)
            {
                passwordTextBox.UseSystemPasswordChar = false;
                showPassword.BackgroundImage = (Image)hide;
            }
            else
            {
                passwordTextBox.UseSystemPasswordChar = true;
                showPassword.BackgroundImage = (Image)show;
            }
        }
        #endregion

        #region Botones
        private void forgotLink_Click(object sender, EventArgs e) 
        {
            // El usuario quiere recuperar su contraseña, se abre el formulario RecoverPassword.
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

        private void registerButton_Click(object sender, EventArgs e)
        {
            // El usuario quiere registrarse, se abre el formulario Register.
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

        private void consultasButton_Click(object sender, EventArgs e)
        {
            // El usuario quiere hacer consultas, se abre el formulario de consultas.
            if((logged == 0) ||(conectado == 0))
            {
                MessageBox.Show("Not logged");
            }
            else
            {
                C = new Consultas(server, ID,this.userName);
                C.ShowDialog();
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // El usuario quiere empezar a jugar, se abre el formulario Monopoly.
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
        #endregion
        private void PonerEnMarchaPartida(int nPartida, string[] trozos) 
        {
            // El usuario quiere empezar una partida, se abre el formulario Partida.
            Partida P = new Partida(this.server, nPartida, this.userName, trozos);
            Partidas.Add(P);
            P.Text = "Game " + nPartida.ToString();
            P.ShowDialog();
        }
        private void updateDataGridView(string[] partes)
        {
            // Actualización de el DataGrid de conectados.
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

        private void EliminaButton_Click(object sender, EventArgs e)
        {
            string mensaje = "23/" + eliminaTextBox.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void conectadosGridView_SelectionChanged(object sender, EventArgs e)
        {
            conectadosGridView.ClearSelection();
        }
    }
}
