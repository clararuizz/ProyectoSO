using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public partial class Monopoly : Form
    {
        Socket server;
        string Name;
        string[] Conectados;
        string[] Invitaciones;

        int GameNum = 0;

        List<string> listaInvitar = new List<string>();
        List<int> listaGames = new List<int>();

        public Monopoly(Socket s, string name, string[] connected)
        {
            InitializeComponent();
            this.Name = name;
            this.server = s;
            this.Conectados=connected;
        }
        private void Monopoly_Load(object sender, EventArgs e)
        {
            conectadosGridView.ColumnHeadersVisible = false;
            conectadosGridView.RowHeadersVisible = false;
            InvitadosDataGrid.ColumnHeadersVisible = false;
            InvitadosDataGrid.RowHeadersVisible = false;
            InvitacionesDataGrid.RowHeadersVisible = false;
            InvitacionesDataGrid.ColumnHeadersVisible = false;
            JoinPanel.Visible = false;
            invitePanel.Visible = false;
            gameLabel.Text = "No Game";
        }
        public void setLabelNum(int num) //actualiza el label con el num de la partida
        {
            gameLabel.Text = num.ToString();
            this.GameNum = num;
        }
        public void DameConectados(string[] trozos) //funcion para que el thread pueda introducir nuevos conectados
        {
            this.Conectados = trozos;
            updateDataGridView(this.Conectados);
        }

        public void DameInvitaciones(string[] trozos) //funcion para que el thread pueda introducir las nuevas invitaciones
        {
            this.Invitaciones = trozos;
            updateDataGridViewInvitaciones(this.Invitaciones);
        }

        private void updateDataGridView(string[] partes) //actualizar tabla usuarios disponibles a invitar
        {
            conectadosGridView.DataSource = null;
            conectadosGridView.ColumnCount = 2;
            conectadosGridView.RowCount = partes.Length - 1;
            conectadosGridView[0, 0].Value = "Total Users";
            conectadosGridView[1, 0].Value = partes[1];
            if (partes[1] != "0")
            {
                for (int i = 1; i < partes.Length - 1; i++)
                {
                    conectadosGridView[1, i].Value = partes[i + 1];
                    conectadosGridView[0, i].Value = Convert.ToString(i);
                }
            }
        }
        private void updateDataGridView_2(List<string> lista2) //actualizar tabla usuarios a invitar
        {
            InvitadosDataGrid.DataSource = null;
            InvitadosDataGrid.ColumnCount = 1;
            InvitadosDataGrid.RowCount = lista2.Count + 1;
            InvitadosDataGrid[0, 0].Value = "Total Invitaciones";
            for (int n = 0; n < lista2.Count; n++)
            {
                InvitadosDataGrid[0, n + 1].Value = lista2[n];
            }
        }

        private void updateDataGridViewInvitaciones(string[] partes) //actualizar tabla invitaciones entrantes
        {
            InvitacionesDataGrid.DataSource = null;
            InvitacionesDataGrid.ColumnCount = 2;
            if(partes.Length <= 2)
            {
                InvitacionesDataGrid.RowCount = 1;
                InvitacionesDataGrid[0, 0].Value = "Total Invitations";
                InvitacionesDataGrid[1, 0].Value = "0, no tienes amigos";
            }
            else
            {
                InvitacionesDataGrid.RowCount = partes.Length - 2;
                InvitacionesDataGrid[0, 0].Value = "Total Invitations";
                InvitacionesDataGrid[1, 0].Value = (partes.Count()-1)/2;
                if (partes[1] != "0")
                {
                    for (int i = 1; i < partes.Length - 1; i++)
                    {
                        InvitacionesDataGrid[1, i].Value = partes[i];
                        InvitacionesDataGrid[0, i].Value = partes[i+1];
                        i++;
                    }
                }
            }
        }

        private void invitePlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GameNum == 0)
                MessageBox.Show("You need to create a game first");
            else
            {
                invitePanel.Visible = true;
                updateDataGridView(Conectados);
            }
        }

        private void createGameToolStripMenuItem_Click(object sender, EventArgs e) //solicitud de creacion de partida
        {
            string mensaje = "11/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void inviteButton_Click(object sender, EventArgs e) //manda mensaje con las invitaciones
        {
            string mensaje = "8" + "/" + Convert.ToString(this.GameNum) + "/" + Name;
            foreach (string i in listaInvitar)
            {
                mensaje = mensaje + "/" + i;
            }
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void conectadosGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { //añade personas a invitar a partida
            int i = e.RowIndex;
            string nombre = Convert.ToString(conectadosGridView[1, i].Value);
            if (nombre == Name)
            {
                MessageBox.Show("no te puede invitar a ti mism@");
            }
            else if(CheckInvitado(nombre)==false)
                listaInvitar.Add(nombre);
                updateDataGridView_2(listaInvitar);
        }

        private void InvitadosDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            string nombre = Convert.ToString(InvitadosDataGrid[0, i].Value);
            listaInvitar.Remove(nombre);
            updateDataGridView_2(listaInvitar);

        }

        private bool CheckInvitado(string name) {
            bool encontrado=false;
            int i= 0;
            while(i<listaInvitar.Count&&!encontrado)
            {
                if (listaInvitar[i] == name)
                    encontrado = true;
                i++;
            }
            return encontrado;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            invitePanel.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            JoinPanel.Visible = false;
        }

        private void joinGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mensaje = "10/" + Convert.ToString(this.Name);
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            JoinPanel.Visible = true;
        }

        private void Refresh_Click(object sender, EventArgs e) //pide que servidor envie las invitaciones de nuevo
        {
            string mensaje = "10/" + Convert.ToString(this.Name);
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void AceptarButton_Click(object sender, EventArgs e) //envia mensaje de aceptar partida
        {
            string mensaje = "12/1/" + this.Name + "/";
            if (listaGames.Count() > 0)
            {
                foreach (int i in listaGames)
                {
                    mensaje = mensaje + i + "/";
                }
                listaGames.Clear();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
                MessageBox.Show("Selecciona partidas para acceptar");
        }

        private void InvitacionesDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            int numGame = Convert.ToInt32(InvitacionesDataGrid[0, i].Value);
            listaGames.Add(numGame);
        }

        private void Denybutton_Click(object sender, EventArgs e) //envia mensaje de rechazar partida
        {
            string mensaje = "12/-1/" + this.Name + "/";
            if (listaGames.Count > 0)
            {
                foreach (int i in listaGames)
                {
                    mensaje = mensaje + i + "/";
                }
                listaGames.Clear();
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
                MessageBox.Show("Selecciona partidas para cancelar");
        }
    }
}
