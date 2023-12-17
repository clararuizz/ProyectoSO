using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Consultas : Form
    {
        int ID;
        Socket server;
        string user;

        public Consultas(Socket S,int iD,string us)
        {
            // Inicialización.
            InitializeComponent();
            this.ID = iD;
            this.server = S;
            this.user=us;
        }
        private void Consultas_Load(object sender, EventArgs e)
        {
            resultsDataGrid.ColumnHeadersVisible = false;
            resultsDataGrid.RowHeadersVisible = false;
            this.resultsDataGrid.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            // Cierra el formulario
            Close();
        }

        #region Consultas
        private void Consulta1Button_Click(object sender, EventArgs e)
        {
            string mensaje = "3/" + this.user;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
        #endregion

        #region Respuestas
        public void UpdateRespuestasDGV1(string[] trozos) //actualiza tablero de respuestas
        {
            if (trozos.Length == 2)
            {
                resultsDataGrid.DataSource = null;
                resultsDataGrid.ColumnCount = 1;
                resultsDataGrid.RowCount = 1;
                resultsDataGrid[0, 0].Value = "No Results";
            }
            else
            {
                resultsDataGrid.DataSource = null;
                resultsDataGrid.ColumnCount = 2;
                resultsDataGrid.RowCount = trozos.Length - 1;
                resultsDataGrid[0, 0].Value = "Jugadores";
                resultsDataGrid[1, 0].Value = "";
                for (int k = 1; k < trozos.Length - 1; k++)
                {
                    resultsDataGrid[1, k].Value = trozos[k];
                    resultsDataGrid[0, k].Value = Convert.ToString(k);
                }
            }
        }
        public void UpdateRespuestasDGV2(string[] trozos,string header) //actualiza tablero de respuestas
        {
            if (trozos.Length == 2)
            {
                resultsDataGrid.DataSource = null;
                resultsDataGrid.ColumnCount = 1;
                resultsDataGrid.RowCount = 1;
                resultsDataGrid[0, 0].Value = "No Results";
            }
            else
            {
                resultsDataGrid.DataSource = null;
                resultsDataGrid.ColumnCount = 2;
                resultsDataGrid.RowCount = trozos.Length - 1;
                resultsDataGrid[0, 0].Value = "IDGame";
                resultsDataGrid[1, 0].Value =header;
                for (int k = 1; k < trozos.Length - 1; k++)
                {
                    string[] RespuestasRow = trozos[k].Split('_');
                    resultsDataGrid[0, k].Value = RespuestasRow[0];
                    resultsDataGrid[1, k].Value = RespuestasRow[1];
                }
            }
        }
        #endregion

        private void Consulta2Button_Click(object sender, EventArgs e)
        {
            string nombre = nombreTextBox.Text.Trim();
            if (nombre.Length == 0)
            {
                MessageBox.Show("Fill the box");
            }
            else
            {
                string mensaje = "4/" + this.user+"/"+nombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string from = dateTimePicker1.Value.ToString();
            string[] valuesFrom = from.Split(' ')[0].Split('/');
            string to = dateTimePicker2.Value.ToString();
            string[] valuesTo = to.Split(' ')[0].Split('/');
            string mensajeFrom = valuesFrom[2] + "-" + valuesFrom[1] + "-" + valuesFrom[0];
            string mensajeTo = valuesTo[2]+ "-" + valuesTo[1] + "-" + valuesTo[0];
            string mensaje = "5/" + mensajeFrom + "/" + mensajeTo+"/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void resultsDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            resultsDataGrid.ClearSelection();
        }
    }
}
