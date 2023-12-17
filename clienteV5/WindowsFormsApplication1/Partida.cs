using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApplication1
{
    public partial class Partida : Form
    {
        #region Inicializaciones
        // Inicializaciones.
        Socket server;
        int numPartida;
        string user;
        string player1;//seria l'invitador
        string player2;
        string player3;
        string player4;
        Random rnd;
        int counter;
        int duration;
        int miNumTurno;
        int turno = 0;
        int numDado;
        PictureBox[] pbs = new PictureBox[4];
        TableroPoints[] tablero = new TableroPoints[40];
        PictureBox[] propiedades = new PictureBox[40];
        Color[] colores = new Color[4];
        string[] jugadores = new string[4];
        int encendido;
        public Partida(Socket s,int nPartida,string user, string[] trozos)
        {
            InitializeComponent();
            pbs[0] = p1;
            pbs[1] = p2;
            pbs[2] = p3;
            pbs[3] = p4;
            Point p = new Point(100, 900);
            pbs[0].Location = p;
            pbs[1].Location = p;
            pbs[2].Location = p;
            pbs[3].Location = p;
            panel1.SendToBack();

            propiedades[0] = null;
            propiedades[1] = pb1;
            propiedades[2] = null;
            propiedades[3] = pb3;
            propiedades[4] = null;
            propiedades[5] = pb5;
            propiedades[6] = pb6;
            propiedades[7] = null;
            propiedades[8] = pb8;
            propiedades[9] = pb9;
            propiedades[10] = null;
            propiedades[11] = pb11;
            propiedades[12] = pb12;
            propiedades[13] = pb13;
            propiedades[14] = pb14;
            propiedades[15] = pb15;
            propiedades[16] = pb16;
            propiedades[17] = null;
            propiedades[18] = pb18;
            propiedades[19] = pb19;
            propiedades[20] = null;
            propiedades[21] = pb21;
            propiedades[22] = null;
            propiedades[23] = pb23;
            propiedades[24] = pb24;
            propiedades[25] = pb25;
            propiedades[26] = pb26;
            propiedades[27] = pb27;
            propiedades[28] = pb28;
            propiedades[29] = pb29;
            propiedades[30] = null;
            propiedades[31] = pb31;
            propiedades[32] = pb32;
            propiedades[33] = null;
            propiedades[34] = pb34;
            propiedades[35] = pb35;
            propiedades[36] = null;
            propiedades[37] = pb37;
            propiedades[38] = null;
            propiedades[39] = pb39;

            colores[0] = Color.Green;
            colores[1] = Color.Red;
            colores[2] = Color.Orange;
            colores[3] = Color.Magenta;

            TableroPoints t = new TableroPoints(0, 770, 525);
            tablero[0] = t;
            TableroPoints t1 = new TableroPoints(1, 680, 525);
            tablero[1] = t1;
            TableroPoints t2 = new TableroPoints(2, 617, 525);
            tablero[2] = t2;
            TableroPoints t3 = new TableroPoints(3, 554, 525);
            tablero[3] = t3;
            TableroPoints t4 = new TableroPoints(4, 491, 525);
            tablero[4] = t4;
            TableroPoints t5 = new TableroPoints(5, 428, 525);
            tablero[5] = t5;
            TableroPoints t6 = new TableroPoints(6, 365, 525);
            tablero[6] = t6;
            TableroPoints t7 = new TableroPoints(7, 302, 525);
            tablero[7] = t7;
            TableroPoints t8 = new TableroPoints(8, 240, 525);
            tablero[8] = t8;
            TableroPoints t9 = new TableroPoints(9, 172, 525);
            tablero[9] = t9;
            TableroPoints t10 = new TableroPoints(10, 80, 525);
            tablero[10] = t10;
            TableroPoints t11 = new TableroPoints(11, 80, 460);
            tablero[11] = t11;
            TableroPoints t12 = new TableroPoints(12, 80, 418);
            tablero[12] = t12;
            TableroPoints t13 = new TableroPoints(13, 80, 376);
            tablero[13] = t13;
            TableroPoints t14 = new TableroPoints(14, 80, 334);
            tablero[14] = t14;
            TableroPoints t15 = new TableroPoints(15, 80, 292);
            tablero[15] = t15;
            TableroPoints t16 = new TableroPoints(16, 80, 250);
            tablero[16] = t16;
            TableroPoints t17 = new TableroPoints(17, 80, 208);
            tablero[17] = t17;
            TableroPoints t18 = new TableroPoints(18, 80, 166);
            tablero[18] = t18;
            TableroPoints t19 = new TableroPoints(19, 80, 124);
            tablero[19] = t19;
            TableroPoints t20 = new TableroPoints(20, 80, 82);
            tablero[20] = t20;
            TableroPoints t21 = new TableroPoints(21, 172, 61);
            tablero[21] = t21;
            TableroPoints t22 = new TableroPoints(22, 240, 61);
            tablero[22] = t22;
            TableroPoints t23 = new TableroPoints(23, 302, 61);
            tablero[23] = t23;
            TableroPoints t24 = new TableroPoints(24, 365, 61);
            tablero[24] = t24;
            TableroPoints t25 = new TableroPoints(25, 428, 61);
            tablero[25] = t25;
            TableroPoints t26 = new TableroPoints(26, 491, 61);
            tablero[26] = t26; ;
            TableroPoints t27 = new TableroPoints(27, 554, 61);
            tablero[27] = t27;
            TableroPoints t28 = new TableroPoints(28, 617, 61);
            tablero[28] = t28;
            TableroPoints t29 = new TableroPoints(29, 680, 61);
            tablero[29] = t29;
            TableroPoints t30 = new TableroPoints(30, 780, 61);
            tablero[30] = t30;
            TableroPoints t31 = new TableroPoints(31, 780, 124);
            tablero[31] = t31;
            TableroPoints t32 = new TableroPoints(32, 780, 166);
            tablero[32] = t32;
            TableroPoints t33 = new TableroPoints(33, 780, 208);
            tablero[33] = t33;
            TableroPoints t34 = new TableroPoints(34, 780, 250);
            tablero[34] = t34;
            TableroPoints t35 = new TableroPoints(35, 780, 292);
            tablero[35] = t35;
            TableroPoints t36 = new TableroPoints(36, 780, 334);
            tablero[36] = t36;
            TableroPoints t37 = new TableroPoints(37, 780, 376);
            tablero[37] = t37;
            TableroPoints t38 = new TableroPoints(38, 780, 418);
            tablero[38] = t38;
            TableroPoints t39 = new TableroPoints(39, 780, 460);
            tablero[39] = t39;

            jugadores[0] = player1Label.Text;
            jugadores[1] = player2Label.Text;
            jugadores[2] = player3Label.Text;
            jugadores[3] = player4Label.Text;

            this.labelnombre.Text = "UserName: " + user;
            rnd=new Random();
            this.server = s;
            this.user=user;
            this.numPartida = nPartida;
            this.player1 = trozos[2];
            if (this.player1 == this.user)
            {
                turno = 1;
            }
            player1Label.BackColor = Color.LightGreen;
            player1Label.Visible = true;
            player1Label.Text = player1;
            jugador1.BackColor = colores[0];
            jugador1.Visible = true;
            pbs[0].BackColor = colores[0];
            pbs[0].Visible = true;
            pbs[0].BringToFront();
            if (trozos.Length>=4 ) {
                this.player2 = trozos[3];
                player2Label.Visible = true;
                player2Label.Text = player2;
                jugador2.BackColor = colores[1];
                jugador2.Visible = true;
                pbs[1].BackColor = colores[1];
                pbs[1].Visible = true;
                pbs[1].BringToFront();
            }
            if (trozos.Length >= 5)
            {
                this.player3 = trozos[4];
                player3Label.Visible = true;
                player3Label.Text = player3;
                jugador3.BackColor = colores[2];
                jugador3.Visible = true;
                pbs[2].BackColor = colores[2];
                pbs[2].Visible = true;
                pbs[2].BringToFront();
            }
            if (trozos.Length >= 6)
            {
                this.player4 = trozos[5];
                player4Label.Visible = true;
                player4Label.Text = player4;
                jugador4.BackColor = colores[3];
                jugador4.Visible = true;
                pbs[3].BackColor = colores[3];
                pbs[3].Visible = true;
                pbs[3].BringToFront();
            }
            chatTextBox.AppendText("Started game "+ Convert.ToString(this.numPartida) + "\r\n");
            if (this.player1 == this.user)
                miNumTurno = 1;
            else if(this.player2 == this.user)
                miNumTurno= 2;
            else if (this.player3 == this.user)
                miNumTurno = 3;
            else if (this.player4 == this.user)
                miNumTurno = 4;
        }
        public Color[] DameColor()
        {
            Color player1 = Color.Green;
            Color player2 = Color.Red;
            Color player3 = Color.Orange;
            Color player4 = Color.Magenta;;
            colores[0] = player1;
            colores[1] = player2;
            colores[2] = player3;
            colores[3] = player4;
            return colores;
        }
        public string[] DameJugadores()
        {
            string[] jugadores = new string[4];
            jugadores[0] = player1;
            jugadores[1] = player2;
            jugadores[2] = player3;
            jugadores[3] = player4;
            return jugadores;
        }
        public void PonPropiedad(Color color, int posicion)
        {
            propiedades[posicion].BackColor = color;
            propiedades[posicion].Visible = true;
        }
        private void Partida_Load(object sender, EventArgs e)
        {
            timer.Interval = 150;
            timer1.Interval = 700;
            PartidaDGV.ColumnHeadersVisible = false;
            PartidaDGV.RowHeadersVisible = false;
            timer1.Start();
            DameColor();
        }
        public int DameNumPartida() //devuelve el numero de la partida de este formulario
        {
            return numPartida;
        }
        public void DameTurno(int turnoActual)
        {
            if (this.miNumTurno == turnoActual)
                turno = 1;
            else
                turno = 0;
            player1Label.BackColor = Color.White;
            player2Label.BackColor = Color.White;
            player3Label.BackColor = Color.White;
            player4Label.BackColor = Color.White;
            NotiLbl.BackColor = Color.White;
            if (turnoActual == 1)
                player1Label.BackColor = Color.LightGreen;
            else if (turnoActual == 2)
                player2Label.BackColor = Color.LightGreen;
            else if (turnoActual == 3)
                player3Label.BackColor = Color.LightGreen;
            else if (turnoActual == 4)
                player4Label.BackColor = Color.LightGreen;
        }
        public void PonNoti(string text)
        {
            NotiLbl.Visible = true;
            NotiLbl.Text = text;
        }
        public void UpdatePartidaDGV(string[] trozos) //actualiza tablero de info partida
        {
            PartidaDGV.DataSource = null;
            PartidaDGV.ColumnCount = 3;
            PartidaDGV.RowCount = Convert.ToInt32(trozos[2]) + 1;
            PartidaDGV[0, 0].Value = "Players";
            PartidaDGV[1, 0].Value = "Money";
            PartidaDGV[2, 0].Value = "Position";
            int l = 3;
            for (int k = 1; k < Convert.ToInt32(trozos[2]) + 1; k++)
            {
                PartidaDGV[0, k].Value = trozos[l];
                PartidaDGV[1, k].Value = trozos[l + 1];
                PartidaDGV[2, k].Value = trozos[l + 2];
                if (trozos[l] == player1Label.Text)
                {
                    BuscarNewPosition(trozos[l + 2], pbs[0]);
                }
                else if (trozos[l] == player2Label.Text)
                {
                    BuscarNewPosition(trozos[l + 2], pbs[1]);
                }
                else if (trozos[l] == player3Label.Text)
                {
                    BuscarNewPosition(trozos[l + 2], pbs[2]);
                }
                else if (trozos[l] == player4Label.Text)
                {
                    BuscarNewPosition(trozos[l + 2], pbs[3]);
                }
                else if ((Convert.ToInt32(trozos[l + 1]) < 0) && (trozos[l].ToString() == user))
                {
                    string mensaje = "21/" + numPartida.ToString() + "/" + user;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                }
                l = l + 3;
                
            }
        }
        #endregion

        #region Chat
        public void addMessage(string[] trozos) //añade mensaje recibido en el chat
        {
            string sender = trozos[2];
            string text = trozos[3];
            if(sender==user)
            {
                sender = "Me";
            }
            chatTextBox.AppendText(sender+": "+text +"\r\n");
            writeTextBox.Text = "";
        }
        private void sendButton_Click(object sender, EventArgs e) //envia mensaje al servidor para el chat
        {
            string text = writeTextBox.Text.Trim();
            if (text.Length != 0)
            {
                string mensaje = "13/" + Convert.ToString(this.numPartida) + "/" + this.user + "/" + text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }
        #endregion

        #region Dado
        private void UpdateDado(int num) //actualiza foto dado
        {
            switch (num)
            {
                case 1:
                    dadoPicture.Image = Image.FromFile("one.png");
                    this.numDado = 1;
                    break;
                case 2:
                    dadoPicture.Image = Image.FromFile("two.png");
                    this.numDado = 2;
                    break;
                case 3:
                    dadoPicture.Image = Image.FromFile("three.png");
                    this.numDado = 3;
                    break;
                case 4:
                    dadoPicture.Image = Image.FromFile("four.png");
                    this.numDado = 4;
                    break;
                case 5:
                    dadoPicture.Image = Image.FromFile("five.png");
                    this.numDado = 5;
                    break;
                case 6:
                    dadoPicture.Image = Image.FromFile("six.png");
                    this.numDado = 6;
                    break;
            }
        }
        private void tirarButton_Click(object sender, EventArgs e) //pone en marcha el dado
        {
            if (turno == 1)
            {
                turno = 0;
                duration = rnd.Next(15, 30);
                timer.Start();
            }
        }
        #endregion

        #region PictureBox
        private void BuscarNewPosition(string casillaActual, PictureBox pictureBox)
        {
            // Update de la posición de los picturebox.
            int i;
            int casilla = Convert.ToInt32(casillaActual);
            for (i = 0; i < 40; i++)
            {
                if (tablero[i].ID == casilla)
                {
                    int X = tablero[i].Xpos;
                    int Y = tablero[i].Ypos;
                    Point newcasilla = new Point(X, Y);
                    pictureBox.Location = newcasilla;
                    pictureBox.BringToFront();
                    i = 40;
                }
            }
        }
        #endregion

        #region Recuperar/Guardar Partida
        private void Guardar_Click(object sender, EventArgs e)
        {
            // El usuario decide guardar la partida [Código: 18].
            string mensaje = "18/" + numPartida.ToString();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MessageBox.Show("Partida Guardada");
        }
        #endregion

        private void timer_Tick(object sender, EventArgs e) //en cada tick cambia el valor hasta que se para y envia el valor final
        {
            int value=rnd.Next(1, 6);
            UpdateDado(value);
            if (counter>duration)
            {
                timer.Stop();
                counter = 0; 
                string mensaje = "15/" + numPartida.ToString() + "/" + user + "/" + value.ToString();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                counter++;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string mensaje = "20/" + numPartida.ToString();
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            string mensaje2 = "18/" + numPartida.ToString();
            byte[] msg2 = System.Text.Encoding.ASCII.GetBytes(mensaje2);
            server.Send(msg2);
        }
        public void NuevoPerdedor(string perdedor)
        {
            if (perdedor == player1) {
                player1Label.BackColor = Color.Red;
            }
            else if (perdedor == player2)
            {
                player2Label.BackColor = Color.Red;
            }
            else if (perdedor == player3)
            {
                player3Label.BackColor = Color.Red;
            }
            else if (perdedor == player4)
            {
                player4Label.BackColor = Color.Red;
            }
            if (perdedor == user)
            {
                string mensaje = "21/" + numPartida.ToString() + "/" + this.miNumTurno;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                MessageBox.Show("You have lost, the game ends for you!");
                //Close();
            }

        }
        public void PonCarcel()
        {
            string mensaje = "24/" + numPartida.ToString() + "/" + this.miNumTurno;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        public void Cerrar(int i)
        {
            Close();
        }
        public void DeleteLabel(string name)
        {
            if (player1Label.Text == name)
            {
                player1Label.Text = null;
                jugador1.Visible = false;
            }
            if (player2Label.Text == name)
            {
                player2Label.Text = null;
                jugador2.Visible = false;
            }
            if (player3Label.Text == name)
            {
                player3Label.Text = null;
                jugador3.Visible = false;
            }
            if (player4Label.Text == name)
            {
                player4Label.Text = null;
                jugador4.Visible = false;
            }
        }
        private void Start_Click(object sender, EventArgs e)
        {
            string mensaje = "14/" + numPartida.ToString() + "/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (encendido == 1)
            {
                p2.Visible = true;
                p1.Visible = true;
                p3.Visible = true;
                p4.Visible = true;
                encendido = 0;
            }
            else
            {
                p2.Visible = false;
                p1.Visible = false;
                p3.Visible = false;
                p4.Visible = false;
                encendido = 1;
            }

        }
    }
}
