using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace Zelda.Clases
{
    public class Hero:Object//, IAlive
    {

        bool equipedWeapon = false;
        PictureBox s;
        EDirection direction;
        Panel panel;
        System.Timers.Timer aTimer;
        Form1 instance;
        int lives = 3;
        COORD destination;

        public Hero(COORD casilla, Panel panel, PictureBox s, Form1 instance):base(new COORD(8, 2), panel)
        {
            this.s = s;
            COORD AbsoluteCOORD = COORD.GetCasillaCoords(panel, new COORD(8,2));
            s.Left = (int)AbsoluteCOORD.X;
            s.Top = (int)AbsoluteCOORD.Y;
            this.panel = panel;
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(Unattack);
            aTimer.Interval = 100;
            this.instance = instance;
            direction = EDirection.Down;
        }

        public void Move(EDirection direction)
        {
            int destination;
            switch(direction)
            {
                case EDirection.Down:
                    this.direction = EDirection.Down;
                    destination = (int)RelativeCOORD.Y + 1;
                    if (destination <= 9)
                    {
                        this.destination = new COORD(RelativeCOORD.X, destination);
                        List<Object> ob = instance.plane.FindAtCOORD(this.destination);
                        if (ob.Count != 0)
                        {
                            Collision(ob);
                        } else
                        {
                            
                            RelativeCOORD.Y = destination;
                            s.Top += (int)COORD.GetBoxSize(panel).Y;
                            
                        }
                        s.BackgroundImage = Properties.Resources.hero_down;
                        
                    }
                    return;
                case EDirection.Up:
                    this.direction = EDirection.Up;
                    destination = (int)RelativeCOORD.Y - 1;
                    if (destination >= 1)
                    {
                        this.destination = new COORD(RelativeCOORD.X, destination);
                        List<Object> ob = instance.plane.FindAtCOORD(this.destination);
                        if (ob.Count != 0)
                        {
                            Collision(ob);
                        }
                        else
                        {
                            
                            RelativeCOORD.Y = destination;
                            s.Top -= (int)COORD.GetBoxSize(panel).Y;
                            
                        }
                        s.BackgroundImage = Properties.Resources.hero_up;
                        
                    }
                    return;
                case EDirection.Right:
                    this.direction = EDirection.Right;
                    destination = (int)RelativeCOORD.X + 1;
                    if (destination <= 16)
                    {
                        this.destination = new COORD(destination, RelativeCOORD.Y);
                        List<Object> ob = instance.plane.FindAtCOORD(new COORD(destination, RelativeCOORD.Y));
                        if (ob.Count != 0)
                        {
                            Collision(ob);
                        }
                        else
                        {
                            
                            RelativeCOORD.X = destination;
                            s.Left += (int)COORD.GetBoxSize(panel).X;
                            
                        }
                        
                        s.BackgroundImage = Properties.Resources.hero_right;
                    }
                    return;
                case EDirection.Left:
                    destination = (int)RelativeCOORD.X - 1;
                    this.direction = EDirection.Left;
                    if (destination >= 1)
                    {
                        
                        this.destination = new COORD(destination, RelativeCOORD.Y);
                        List<Object> ob = instance.plane.FindAtCOORD(this.destination);
                        if (ob.Count != 0)
                        {
                            Collision(ob);
                            
                        }
                        else
                        {
                            
                            RelativeCOORD.X = destination;
                            s.Left -= (int)COORD.GetBoxSize(panel).X;
                            
                        }
                        s.BackgroundImage = Properties.Resources.hero_left;
                        
                    }
                    return;
            }
            
        }

        public void Collision(List<Object> ob)
        {
            if(ob.ElementAt(0) is Sword)
            {
                Sword obs = (Sword)ob.ElementAt(0);
                obs.s.Visible = false;
                instance.ui.EnableSword();
                instance.ui.AddScore(1000);
                instance.plane.objects.Remove(obs);
                Move(direction);
                equipedWeapon = true;
            } else if(ob.ElementAt(0) is Enemy)
            {
                Enemy e = (Enemy)ob.ElementAt(0);
                lives -= e.damage;
                instance.ui.UpdateLives(lives);
                if(lives <= 0)
                {
                    MessageBox.Show("Game Over");
                    instance.Close();
                }
            }
        }

        public void Attack()
        {
            if (equipedWeapon)
            {
                instance.atacando = true;
                switch (direction)
                {
                    case EDirection.Down:
                        s.Height *= 2;
                        s.BackgroundImage = Properties.Resources.attack_down;
                        aTimer.Enabled = true;
                        Enemy e = CheckIfEnemy(new COORD(RelativeCOORD.X, RelativeCOORD.Y + 1));
                        if(e != null)
                        {
                            AttackEnemy(e, 1);
                        }
                        return;
                    case EDirection.Up:
                        s.Top -= s.Height;
                        s.Height *= 2;
                        s.BackgroundImage = Properties.Resources.attack_up;
                        aTimer.Enabled = true;
                        Enemy e2 = CheckIfEnemy(new COORD(RelativeCOORD.X, RelativeCOORD.Y - 1));
                        if (e2 != null)
                        {
                            AttackEnemy(e2, 1);
                        }
                        return;
                    case EDirection.Left:
                        s.Left -= s.Width;
                        s.Width *= 2;
                        s.BackgroundImage = Properties.Resources.attack_left;
                        aTimer.Enabled = true;
                        Enemy e3 = CheckIfEnemy(new COORD(RelativeCOORD.X - 1, RelativeCOORD.Y));
                        if (e3 != null)
                        {
                            AttackEnemy(e3, 1);
                        }
                        return;
                    case EDirection.Right:
                        s.Width *= 2;
                        s.BackgroundImage = Properties.Resources.attack_right;
                        aTimer.Enabled = true;
                        Enemy e4 = CheckIfEnemy(new COORD(RelativeCOORD.X + 1, RelativeCOORD.Y));
                        if (e4 != null)
                        {
                            AttackEnemy(e4, 1);
                        }
                        return;
                }
            }
        }

        public Enemy CheckIfEnemy(COORD casilla)
        {
            List<Object> ob = instance.plane.FindAtCOORD(casilla);
            if (ob.Count != 0)
            {
                if (ob.ElementAt(0) is Enemy)
                {
                    return (Enemy)ob.ElementAt(0);
                } 

            }
            return null;
        }

        public void AttackEnemy(Enemy e, int damage)
        {
            e.live -= damage;
            if(e.live <= 0)
            {
                instance.plane.Remove(e);
                e.s.Visible = false;
                if(e.damage == 2)
                {
                    instance.ui.AddScore(10000);
                }
                else
                {
                    instance.ui.AddScore(5000);
                }
            }

            if(instance.plane.objects.Find(x=> x is Enemy) == null)
            {
                MessageBox.Show("Ya ganaste");
                instance.Close();
            }
        }

        private void Unattack(object source, ElapsedEventArgs e)
        {
            instance.ReturnToNormal(direction);
            aTimer.Enabled = false;
        }

        public override void OnResize()
        {
            base.OnResize();
            s.Location = new System.Drawing.Point((int)this.absoluteCOORD.X, (int)this.absoluteCOORD.Y);
            COORD boxSize = COORD.GetBoxSize(panel);
            s.Height = (int)boxSize.Y;
            s.Width = (int)boxSize.X;
        }

        public void Relocate(COORD Casilla)
        {
            COORD Absolute = COORD.GetCasillaCoords(panel, Casilla);
            s.Location = new System.Drawing.Point((int)Absolute.X, (int)Absolute.Y);
        }

    }

}
