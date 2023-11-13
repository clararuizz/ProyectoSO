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
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public partial class Monopoly : Form
    {
        Socket server;
        string Name;
        public Monopoly(Socket s, string name)
        {
            InitializeComponent();
            this.Name = name;
            this.server = s;
        }

        private void Invitar_Click(object sender, EventArgs e)
        {
        }
    }
}
