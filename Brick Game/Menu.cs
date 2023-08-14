using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Brick_Game.Properties;

namespace Brick_Game
    //Assignment 1, Brick Game, Nikita Mogilevskii
    //Sorry that i don't have classes of the ball and bat, classes are not so effective in my project.
{
    public partial class Menu : Form
    {
        MusicController music; //Music in the menu
        public CheckBox checkBox; //Checkbox for the music
        public int onOff = 1; //onOff music int
        public int io = 1; //music timer stopper
        public Menu()
        {
            InitializeComponent();
            SoundPlayer simpleSound = new SoundPlayer(Resources.mainmenu); 
            checkBox1 = checkBox;
            music = new MusicController(simpleSound, onOff, timer1); //new music controller
            DoubleBuffered = true;
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            //Opening new form
            gameplay gameplay = (gameplay)Application.OpenForms["gameplay"]; 
            if (gameplay == null) // Creating a form if it doesnt exist
            {
                gameplay = new gameplay();
                gameplay.gamemode = 1; //Super hot mode
                gameplay.onOff = music.onOff; //transfering music info
                this.Hide();
                gameplay.Show();
            }
            else
            {
                gameplay.onOff = music.onOff; //transfering music info
                this.Hide();
                gameplay.Show();
            }
        }
        private void button2_Click(object sender, EventArgs e) //Normal mode
        {
            gameplay gameplay = (gameplay)Application.OpenForms["gameplay"];
            if (gameplay == null) // Creating a form if it doesnt exist
            {
                gameplay = new gameplay();
                gameplay.gamemode = 0; //Normal mode
                gameplay.onOff = music.onOff; //transfering music info
                this.Hide();
                gameplay.Show();
            }
            else
            {
                gameplay.onOff = music.onOff; //transfering music info

                this.Hide();
                gameplay.Show();          
            }

        }

        private void timer1_Tick(object sender, EventArgs e) //Music timer
        { 
            //io is used for turning music on only for 1 time
            music.onOff = onOff; //sending info about music on off button
            if (io == 0) //io is a solvation of the problem with multiforms music, when we come back to menu form again, it restarts music.
            {
                music.i = 0;
                io = 1;
            }
            music.SoundTrack(); 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //Manual switch is made on a purpose to avoid windows forms issues
        {
            if (onOff == 1) onOff= 0;
            else onOff= 1;
        }

        private void Menu_KeyDown(object sender, KeyEventArgs e) //Q to quit
        {
            if (e.KeyCode == Keys.Q)
            {
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e) //Quit button
        {
            this.Close();
        }
    }
}
