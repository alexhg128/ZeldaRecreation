using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Zelda.Clases
{
    public class Plane
    {
        const int TOPBOUND = 1;
        const int BOTTOMBOUND = 9;

        public List<Zelda.Clases.Object> objects;
        Panel plane;

        public Plane(Panel plane, Hero hero)
        {
            this.plane = plane;
            objects = new List<Object>();
            Add(hero);
            for (int i = 0; i <= 16; i++) {
                Add(new Object(new COORD(i, 1), plane));
            }
            Add(new Object(new COORD(1, 2), plane));
            Add(new Object(new COORD(1, 3), plane));
            Add(new Object(new COORD(1, 7), plane));
            Add(new Object(new COORD(1, 8), plane));
            Add(new Object(new COORD(1, 9), plane));
            Add(new Object(new COORD(2, 2), plane));
            Add(new Object(new COORD(2, 8), plane));
            Add(new Object(new COORD(2, 9), plane));
            Add(new Object(new COORD(3, 9), plane));
            Add(new Object(new COORD(4, 3), plane));
            Add(new Object(new COORD(4, 5), plane));
            Add(new Object(new COORD(4, 7), plane));
            Add(new Object(new COORD(4, 9), plane));
            Add(new Object(new COORD(5, 9), plane));
            Add(new Object(new COORD(6, 3), plane));
            Add(new Object(new COORD(6, 5), plane));
            Add(new Object(new COORD(6, 7), plane));
            Add(new Object(new COORD(6, 9), plane));
            Add(new Object(new COORD(7, 9), plane));
            Add(new Object(new COORD(10, 2), plane));
            Add(new Object(new COORD(10, 8), plane));
            Add(new Object(new COORD(10, 9), plane));
            Add(new Object(new COORD(11, 2), plane));
            Add(new Object(new COORD(11, 8), plane));
            Add(new Object(new COORD(11, 9), plane));
            Add(new Object(new COORD(12, 9), plane));
            Add(new Object(new COORD(13, 3), plane));
            Add(new Object(new COORD(13, 5), plane));
            Add(new Object(new COORD(13, 7), plane));
            Add(new Object(new COORD(13, 9), plane));
            Add(new Object(new COORD(14, 9), plane));
            Add(new Object(new COORD(15, 2), plane));
            Add(new Object(new COORD(15, 8), plane));
            Add(new Object(new COORD(15, 9), plane));
            Add(new Object(new COORD(16, 2), plane));
            Add(new Object(new COORD(16, 3), plane));
            Add(new Object(new COORD(16, 7), plane));
            Add(new Object(new COORD(16, 8), plane));
            Add(new Object(new COORD(16, 9), plane));
        }

        public void Add(Object ob)
        {
            objects.Add(ob);
        }

        public void Remove(Object ob)
        {
            objects.Remove(ob);
        }

        public List<Object> FindAtCOORD(COORD coord)
        {
            return objects.FindAll(x => x.RelativeCOORD.Equals(coord));
        }

    }

    public class COORD
    {
        double x;
        double y;

        public COORD(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }

        const double XPERCENT = 0.06251937584;
        const double YPERCENT = 0.09562193773;
        const double OFFSETPERCENTUP = 0.09072230125;
        const double OFFSETPERCENTDOWN = 0.048838312;

        public static COORD GetBoxSize(Panel panel)
        {
            return new COORD(panel.Width * XPERCENT, panel.Height * YPERCENT);
        }

        public static COORD GetCasillaCoords(Panel panel, COORD casilla)
        {
            double offset = panel.Height * OFFSETPERCENTUP;
            double height = (panel.Height / 9) * casilla.Y;
            COORD boxsize = GetBoxSize(panel);
            height -= (boxsize.Y / 2);
            double width = (panel.Width / 16) * casilla.X;
            width -= boxsize.X;

            return new COORD(width, height);
        }

        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString();
        }

        public override bool Equals(object obj)
        {
            COORD coord;
            
            try
            {
                coord = (COORD)obj;
                Console.WriteLine(coord.ToString());
                Console.WriteLine(this.ToString());
                if (this.X == coord.X && this.Y == coord.Y)
                {
                    return true;
                } else
                {
                    return false;
                }

            } catch
            {
                
                return false;
            }
        }
    }

}
