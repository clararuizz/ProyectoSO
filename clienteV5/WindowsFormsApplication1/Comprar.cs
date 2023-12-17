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
    public partial class Comprar : Form
    {
        Socket server;
        string Name;
        string propiedad;
        string precio;
        string numPartida;
        string posicion;
        public Comprar(Socket s, string name)
        {
            // Inicialización.
            InitializeComponent();
            this.Name = name;
            this.server = s;
        }
        
        public void DameInfo(string[] trozos)
        {
            // Actualización del formulario con los parámetros para informar sobre la compra dados por el servidor.
            propiedad = trozos[5];
            precio = trozos[6];
            numPartida = trozos[3];
            posicion = trozos[4];
            label1.Text = "Do you want to buy the property " + propiedad + " for the price " + precio + "?";
        }

        #region Comprar/Denegar 
        private void Si_Click(object sender, EventArgs e)
        {
            // El usuario compra la propiedad [Código: 16]
            string mensaje = "16/" + numPartida + "/" + posicion + "/"+ Name + "/" + propiedad + "/" + precio;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            Close();
        }

        private void No_Click(object sender, EventArgs e)
        {
            // El usuario no quiere comprar la propiedad.
            Close();
        }
        #endregion
    }
}
