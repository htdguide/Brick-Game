using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Brick_Game.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Brick_Game 
{
    public partial class gameplay : Form
    {
        MusicController intro, superhot, normal; //Music 
        private Countdown startgame; //Game's start counter class
        public int gamemode; //Normal or Superhod gamemode
        public int onOff = 1; //On or off music
        int combo = 1; //Number of combo
        int ball_x = 1; //Ball's step
        int ball_y = 1; //Ball's step
        int score = 0; //Player's score
        int i = 5; //Integer for countdown
        int ability = 100; //Slowmo ability
        int win = 0; //Win detector
        private Point MouseDownLocation; //Point for using a mouse

        public gameplay()
        {
            InitializeComponent();
            SoundPlayer introS = new SoundPlayer(Resources.intro);
            intro = new MusicController(introS, onOff, timer4);
            SoundPlayer superhotS = new SoundPlayer(Resources.superhot);
            superhot = new MusicController(superhotS, onOff, timer4);
            startgame = new Countdown(label4, timer2, timer1);
            SoundPlayer normalS = new SoundPlayer(Resources.normalmode);
            normal = new MusicController (normalS, onOff, timer4);
        }
        

        private void scoring()
        { 
            foreach (Control x in this.Controls) //Checking the all controls for a pictureboxes
            {
                if (x is PictureBox && x.Tag == "block1")  //Tag property 
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds)) //if ball hits the block
                    {
                        Controls.Remove(x); //Removing the block
                        score = score + 5 * combo; //adding a score with multiplier
                        if (ability < 90) ability = ability + 10; //Adding slowmo ability
                        progressBar1.Value = ability;
                        ball_y = -ball_y; //reversing a balls direction
                        comboMeter.Text = "x " + combo; 
                        combo++; //Adding a multiplier
                        win++;
                        label2.Text = score.ToString();
                    }
                }
                if (x is PictureBox && x.Tag == "block2")  //Tag property 
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds)) //if ball hits the block
                    {
                        Controls.Remove(x); //Removing the block
                        score = score + 10 * combo; //adding a score with multiplier
                        if (ability < 90) ability = ability + 10; //Adding slowmo ability
                        progressBar1.Value = ability;
                        ball_y = -ball_y; //reversing a balls direction
                        comboMeter.Text = "x " + combo;
                        combo++; //Adding a multiplier
                        win++;
                        label2.Text = score.ToString();
                    }
                }
                if (x is PictureBox && x.Tag == "block3")  //Tag property 
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        Controls.Remove(x); //Removing the block
                        score = score + 15 * combo; //adding a score with multiplier
                        if (ability < 90) ability = ability + 10; //Adding slowmo ability
                        progressBar1.Value = ability;
                        ball_y = -ball_y; //reversing a balls direction
                        comboMeter.Text = "x " + combo;
                        combo++; //Adding a multiplier
                        win++;
                        label2.Text = score.ToString();
                    }
                }
            }
        }
        private void ballMovement()
        {
            ball.Left += ball_x;
            ball.Top += ball_y;

            if ((ball.Left + ball.Width > ClientSize.Width || ball.Left < 0) && ball.Top < 565) //Handling of the x axle
            {
                ball_x = -ball_x;
            }
            if (ball.Bounds.IntersectsWith(player.Bounds) && ball.Top == player.Top - 20 || ball.Top < 0) //Handling of the y axle. Bat colision system
            {
                ball_y = -ball_y;
                combo = 1; //Multiplier goes back to 1
                comboMeter.Text = "x 1";
            }
            if (ball.Top > player.Top && win != 66) //Lose
            {
                label8.Text = "Your score: " + score;
                label8.Visible = true;
            }
            if (win == 66)
            {
                label8.Text = "WIN! Score: " + score;
                label8.Visible = true;
            }
        }

      

        private void timer1_Tick(object sender, EventArgs e) //Timer for the movement and csoring
        {
            DoubleBuffered = true;
            for (int i = 0; i < 3; i++) //Normal movement
            {
                ballMovement();
                scoring();
            }
            if (gamemode == 1) //Movement for superhot mode
            {
                for (int i = 0; i < 5; i++)
                {
                    ballMovement();
                    scoring();
                }
            }
        }

        private void player_MouseMove(object sender, MouseEventArgs e) //Mouse movement
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                player.Left = e.X + player.Left - MouseDownLocation.X - 40;
            }
        }

        private void button1_Click(object sender, EventArgs e) //Start button
        {
            if (gamemode == 1)  //Choosing the music for the mode
            {
                superhot.SoundTrack();
            }
            else normal.SoundTrack();
            start.Visible = false;
            start.Enabled = false;
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e) //Countdown
        {
            startgame.countdown(i);
            --i;
        }

        private void label3_Click(object sender, EventArgs e) //Going back to main form
        {
            Menu Menu = (Menu)Application.OpenForms["Menu"];
            if (Menu == null) // Creating a form if it doesnt exist
            {
                Menu = new Menu();
                this.Close();
                intro.Stop(); //Stoping the music
                Menu.io = 0; //Starting music in the menu again
                Menu.Show();
            }
            else
            {
                this.Close();
                intro.Stop(); //Stoping the music
                Menu.io = 0; //Starting music in the menu again
                Menu.Show();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (gamemode == 0) //Removing progressbar and changing help image for different mode
            {
                progressBar1.Visible = false;
                label5.Visible = false;
                pictureBox1.Image = Resources.sadas;
            }
            else //Or bringing back superhot mode features
            {
                progressBar1.Visible = true;
                label5.Visible = true;
                pictureBox1.Image = Resources.xv;
            }
            intro.onOff = onOff; //Music checkbox from main menu
            int p = 0;
            if (p == 0) //Ambient music
              {
                p = 1;
                intro.SoundTrack();
                }
        }

        private void pictureBox1_Click(object sender, EventArgs e) //Hide help
        {
            pictureBox1.Hide();
        }

        private void label7_Click(object sender, EventArgs e) //Show help
        {
            pictureBox1.Show();
        }

        private void gameplay_KeyUp(object sender, KeyEventArgs e) //Stopping slowmotion
        {
            if (e.KeyCode == Keys.Space)
            {
                timer3.Stop();
                timer1.Interval = 1;
            }
        }
        private void gameplay_KeyDown(object sender, KeyEventArgs e) //Arrow keys usage
        {
            if (e.KeyCode == Keys.Left && player.Left > 0)
            {
                player.Left -= 10;
            }
            if (e.KeyCode == Keys.Right && player.Right < 656)
            {
                player.Left += 10;
            }
            if (e.KeyCode == Keys.Q) //Q to quit
            {
                Close();
            }
            if (e.KeyCode == Keys.Space) //Slowmotion
            {
                if (ability > 0)
                {
                    timer3.Start();
                    timer1.Interval = 100; 
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e) //Ability controller
        {
            progressBar1.Value = ability;
            if (ability != 0) ability = ability - 2;   
            if (ability == 0) timer1.Interval= 1;
        }
    }
}
