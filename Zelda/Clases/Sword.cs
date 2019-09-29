using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zelda.Clases
{
    public class Sword:Object
    {

        public PictureBox s;
        Panel panel;
        Form1 instance;
        int damage = 1;

        public Sword(COORD casilla, Panel panel, PictureBox s, Form1 instance) : base(new COORD(13, 8), panel)
        {
            this.s = s;
            COORD AbsoluteCOORD = COORD.GetCasillaCoords(panel, new COORD(13, 8));
            s.Left = (int)AbsoluteCOORD.X;
            s.Top = (int)AbsoluteCOORD.Y;
            this.panel = panel;
            this.instance = instance;
        }


        public override void OnResize()
        {
            base.OnResize();
            s.Location = new System.Drawing.Point((int)this.absoluteCOORD.X, (int)this.absoluteCOORD.Y);
            COORD boxSize = COORD.GetBoxSize(panel);
            s.Height = (int)boxSize.Y;
            s.Width = (int)boxSize.X;
        }

    }
}
