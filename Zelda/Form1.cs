using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zelda.Clases;
using System.Media;

//Alejandro Hahn Gallegos
//A01561774

namespace Zelda
{
    public partial class Form1 : Form
    {
        Hero hero;
        public UI ui;
        public Plane plane;
        public bool atacando = false;

        public Form1()
        {
            InitializeComponent();
            Inicializar();
        }

        private void Inicializar()
        {
            
            hero = new Hero(new COORD(2,8), panel2, pictureBox2, this);
            pictureBox1.Width = (int)COORD.GetBoxSize(panel2).X;
            pictureBox1.Height = (int)COORD.GetBoxSize(panel2).Y;
            pictureBox2.Width = (int) COORD.GetBoxSize(panel2).X;
            pictureBox2.Height = (int) COORD.GetBoxSize(panel2).Y;
            pictureBox6.Width = (int)COORD.GetBoxSize(panel2).X;
            pictureBox6.Height = (int)COORD.GetBoxSize(panel2).Y;
            pictureBox4.Width = (int)COORD.GetBoxSize(panel2).X;
            pictureBox4.Height = (int)COORD.GetBoxSize(panel2).Y;
            pictureBox5.Width = (int)COORD.GetBoxSize(panel2).X;
            pictureBox5.Height = (int)COORD.GetBoxSize(panel2).Y;
            Sword sword = new Sword(new COORD(13, 8), panel2, pictureBox1, this);
            Enemy enemy1 = new Enemy(new COORD(5, 6), panel2, pictureBox6, this, 2, 5);
            Enemy enemy2 = new Enemy(new COORD(13, 6), panel2, pictureBox4, this, 1, 2);
            Enemy enemy3 = new Enemy(new COORD(5, 4), panel2, pictureBox5, this, 1, 2);
            ui = new UI(score, lives, plus, swordInventory, this);
            plane = new Plane(panel2, hero);
            plane.objects.Add(sword);
            plane.objects.Add(enemy1);
            plane.objects.Add(enemy2);
            plane.objects.Add(enemy3);

        }

        private void ControlSetUp(object sender, KeyEventArgs e)
        {
            if (!atacando)
            {
                if (e.KeyCode == Keys.Up)
                {
                    hero.Move(EDirection.Up);
                }
                if (e.KeyCode == Keys.Down)
                {
                    hero.Move(EDirection.Down);
                }
                if (e.KeyCode == Keys.Right)
                {
                    hero.Move(EDirection.Right);
                }
                if (e.KeyCode == Keys.Left)
                {
                    hero.Move(EDirection.Left);
                }
                if ((e.KeyCode == Keys.Q || e.KeyCode == Keys.Space))
                {
                    hero.Attack();
                }
            }
        }

        private void OnResize(object sender, EventArgs e)
        {
            foreach(Zelda.Clases.Object ob in plane.objects)
            {
               
                try
                {
                    Hero obs = (Hero)ob;
                    obs.OnResize();
                }
                catch
                {
                    ob.OnResize(); 
                }
                
            }
        }

        delegate void Ocultarplus();

        public void OcultarPlus()
        {
            if (this.InvokeRequired)
            {
                Ocultarplus aT = new Ocultarplus(OcultarPlus);
                this.Invoke(aT);
            }
            else
            {
                plus.Visible = false;
            }
        }

        delegate void Returntonormal(EDirection direction);

        public void ReturnToNormal(EDirection direction)
        {
            if (this.InvokeRequired)
            {
                Returntonormal aT = new Returntonormal(ReturnToNormal);
                this.Invoke(aT, direction);
            }
            else
            {
                switch(direction)
                {
                    case EDirection.Down:
                        pictureBox2.Height /= 2;
                        pictureBox2.BackgroundImage = Properties.Resources.hero_down;
                        break;
                    case EDirection.Up:
                        pictureBox2.Height /= 2;;
                        pictureBox2.Top += pictureBox2.Height;
                        pictureBox2.BackgroundImage = Properties.Resources.hero_up;
                        break;
                    case EDirection.Left:
                        pictureBox2.Width /= 2;
                        pictureBox2.Left += pictureBox2.Width;
                        pictureBox2.BackgroundImage = Properties.Resources.hero_left;
                        break;
                    case EDirection.Right:
                        pictureBox2.Width /= 2;
                        
                        pictureBox2.BackgroundImage = Properties.Resources.hero_right;
                        break;

                }
                atacando = false;
            }
        }
    }
}
