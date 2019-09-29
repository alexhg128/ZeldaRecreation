using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;


namespace Zelda.Clases
{
    public class UI
    {

        System.Timers.Timer aTimer;
        Label score;
        Label lives;
        Label plus;
        PictureBox sword;
        Form1 instance;

        public UI(Label score, Label lives, Label plus, PictureBox sword, Form1 instance)
        {
            this.score = score;
            this.lives = lives;
            this.plus = plus;
            this.sword = sword;
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(Hide);
            aTimer.Interval = 3000;
            this.instance = instance;
        }

        public void EnableSword()
        {
            sword.Visible = true;
        }

        public void UpdateLives(int newLives)
        {
            lives.Text = "x" + newLives;
        }

        public void AddScore(int increment)
        {
            int actual;
            try
            {
                actual = int.Parse(score.Text);
                actual += increment;
            } catch(Exception e)
            {
                actual = 0;
            }
            score.Text = actual.ToString("0000000000");
            plus.Text = "+" + increment.ToString();
            plus.Visible = true;
            
            aTimer.Enabled = true;
        }

        private void Hide(object source, ElapsedEventArgs e)
        {
            instance.OcultarPlus();
            aTimer.Enabled = false;
        }

    }
}
