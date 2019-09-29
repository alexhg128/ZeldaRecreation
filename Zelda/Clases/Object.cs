using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zelda.Clases
{
    public class Object
    {

        COORD relativeCOORD;
        public COORD absoluteCOORD;
        Panel panel;

        

        public Object(COORD relative, Panel panel)
        {
            RelativeCOORD = relative;
            this.panel = panel;
        }

        public virtual void OnResize()
        {
            absoluteCOORD = COORD.GetCasillaCoords(panel, relativeCOORD);
        }

        internal COORD RelativeCOORD { get => relativeCOORD; set => relativeCOORD = value; }

    }
}
