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
    public partial class InvitarJugadores : Form
    {
        Socket server;
        string Name;
        int n = 1;
        int len;
        List<string> lista = new List<string>();
        public InvitarJugadores(Socket s, string name)
        {
            InitializeComponent();
            this.Name = name;
            this.server = s;
            updateDataGridView_1();
        }
        private void updateDataGridView_1()
        {
            conectadosGridView.DataSource = null;
            string mensaje = "7/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            //Recibimos mensaje del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            string[] partes = mensaje.Split('/');
            conectadosGridView.ColumnCount = 2;
            len = partes.Length;
            conectadosGridView.RowCount = len;
            conectadosGridView.ColumnHeadersVisible = false;
            conectadosGridView.RowHeadersVisible = false;
            conectadosGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            conectadosGridView[0, 0].Value = "Total Users";
            conectadosGridView[1, 0].Value = partes[0];
            for (int i = 1; i < partes.Length; i++)
            {
                conectadosGridView[1, i].Value = partes[i];
                conectadosGridView[0, i].Value = Convert.ToString(i);
            }
        }
        private void updateDataGridView_2(string name, int m)
        {
            InvitadosDataGrid.DataSource = null;
            InvitadosDataGrid.ColumnCount = 1;
            InvitadosDataGrid.RowCount = len;
            InvitadosDataGrid.ColumnHeadersVisible = false;
            InvitadosDataGrid.RowHeadersVisible = false;
            InvitadosDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            InvitadosDataGrid[0,0].Value = "Total Invitados";
            InvitadosDataGrid[0,m].Value = name;
        }

        private void conectadosGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            int j = e.ColumnIndex;
            string nombre = Convert.ToString(conectadosGridView[1,i].Value);
            updateDataGridView_2(nombre,n);
            lista.Add(nombre);
            n++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string mensaje = "8/"+ Name + "/";
            foreach(string i in lista)
            {
                mensaje = mensaje + i + "/";
            }
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }
}
