using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zelda.Clases
{
    public class Enemy:Object
    {
        public int damage;
        public int live;
        public PictureBox s;
        Panel panel;
        Form1 instance;

        public Enemy(COORD casilla, Panel panel, PictureBox s, Form1 instance, int damage, int live) : base(casilla, panel)
        {
            this.s = s;
            COORD AbsoluteCOORD = COORD.GetCasillaCoords(panel, casilla);
            s.Left = (int)AbsoluteCOORD.X + ((int)COORD.GetBoxSize(panel).X / 10);
            s.Top = (int)AbsoluteCOORD.Y - ((int)COORD.GetBoxSize(panel).X / 7); ;
            this.panel = panel;
            this.instance = instance;
            this.damage = damage;
            this.live = live;
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
