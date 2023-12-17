using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class TableroPoints
    {
        public int ID;
        public int Xpos;
        public int Ypos;


        public TableroPoints(int ID, int Xpos, int Ypos)
        {
            this.ID = ID;
            this.Xpos = Xpos;
            this.Ypos = Ypos;
        }
        public TableroPoints(TableroPoints tablero)
        {
            this.ID = tablero.ID;
            this.Xpos = tablero.Xpos;
            this.Ypos = tablero.Ypos;
        }
    }
}
