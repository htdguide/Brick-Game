using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Brick_Game.Properties;

namespace Brick_Game
{
    internal class Countdown
    {
        private Label label;
        private Timer timer;
        private Timer start;

        public Countdown(Label label, Timer timer, Timer start)
        {
            this.label = label;
            this.timer = timer;
            this.start = start;
        }
        public int countdown(int i)
        {
            label.Visible = true;
            if (i >= 1)
            {
                label.Text = "  " + i.ToString();
            }
            else if (i == 0)
            {
                label.Text = "GO!";
            }
            else if (i < 0)
            {
                label.Visible = false;
                timer.Stop();
                start.Start();
            }
            return 0;
        }
    }
}
