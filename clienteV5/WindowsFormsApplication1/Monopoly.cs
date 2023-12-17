using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApplication1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApplication1
{

    public partial class Monopoly : Form
    {
        #region Inicializaciones
        // Inicializaciones.
        Socket server;
        string Name;
        string[] Conectados;
        string[] Invitaciones;
        string[] Invitados;

        int GameNum = 0;
        List<string> listaInvitar = new List<string>();
        List<int> listaGames = new List<int>();
        Recuperar Rec;

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
            this.InvitacionesDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.InvitadosDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.conectadosGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
        #endregion

        #region Update de los DataGridView
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
            InvitadosDataGrid[0, 0].Value = "Total Invitations";
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
                InvitacionesDataGrid[1, 0].Value = "0, you do not have friends";
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
        #endregion

        #region Click en los DataGridView
        private void conectadosGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { // El usuario añade personas a invitar a partida
            int i = e.RowIndex;
            if (i > 0)
            {
                string nombre = Convert.ToString(conectadosGridView[1, i].Value);
                if (nombre == Name)
                {
                    MessageBox.Show("you can not invite yourself");
                }
                else if (CheckInvitado(nombre) == false)
                    listaInvitar.Add(nombre);
                updateDataGridView_2(listaInvitar);
            }
        }
        private void InvitadosDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // El usuario quiere invitar a sus amigos.
            int i = e.RowIndex;
            if (i > 0)
            {
                string nombre = Convert.ToString(InvitadosDataGrid[0, i].Value);
                listaInvitar.Remove(nombre);
                updateDataGridView_2(listaInvitar);
            }
        }
        private void InvitacionesDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i > 0)
            {
                int numGame = Convert.ToInt32(InvitacionesDataGrid[0, i].Value);
                listaGames.Add(numGame);
            }
        }
        #endregion

        #region Botones
        private void joinGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // El usuario decide unirse a una partida [Código: 10].
            string mensaje = "10/" + Convert.ToString(this.Name);
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            JoinPanel.Visible = true;
            JoinPanel.BringToFront();
        }
        private void invitePlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GameNum == 0)
                MessageBox.Show("You need to create a game first");
            else
            {
                invitePanel.Visible = true;
                invitePanel.BringToFront();
                updateDataGridView(Conectados);
            }
        }
        private void createGameToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            // El usuario solicita la creación de una partida [Código: 11].
            string mensaje = "11/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        private int CompruebaSiEsta()
        {
            int i;
            int j = 1;
            int contador = 0;
            for (i = 0; i < listaInvitar.Count(); i++)
            {
                j = i + 1;
                while ( j < listaInvitar.Count())
               {
                    if (listaInvitar[i] == listaInvitar[j])
                    {
                        contador++;
                    }
                    j++;
               }
            }
            return contador;
        }
        private void inviteButton_Click(object sender, EventArgs e) 
        {
            // El usuario invita a sus amigos [Código: 8].
            string mensaje = "8" + "/" + Convert.ToString(this.GameNum) + "/" + Name;
            int comprobar = CompruebaSiEsta();
            foreach (string i in listaInvitar)
            {
                if (comprobar > 0)
                {
                    MessageBox.Show("You can not invite the same person two or more times to the same game.");
                }
                else
                {
                    mensaje = mensaje + "/" + i;
                }
            }
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            MessageBox.Show("Invitation sent.");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            invitePanel.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            JoinPanel.Visible = false;
        }
        private void Refresh_Click(object sender, EventArgs e) 
        {
            // El usuario pide de nuevo las invitaciones.
            string mensaje = "10/" + Convert.ToString(this.Name);
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        private void AceptarButton_Click(object sender, EventArgs e) 
        {
            // El usuario acepta la partida [Código: 12].
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
                MessageBox.Show("Select games to accept");
        }
        private void Denybutton_Click(object sender, EventArgs e)
        {
            // El usuario rechaza la partida [Código: 12].
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
                MessageBox.Show("Select games to cancel");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            AboutUsPanel.Visible = false;
        }
        private void statsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUsPanel.Visible = true;
            AboutUsPanel.BringToFront();
        }
        #endregion
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
        private void recoverGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rec = new Recuperar(server);
            Rec.ShowDialog();
        }
    }
}
