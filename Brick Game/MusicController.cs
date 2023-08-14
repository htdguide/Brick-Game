using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Brick_Game
{
    internal class MusicController
    {
        public SoundPlayer Soundtrack;  
        public int onOff; //Checkbox on off
        public Timer timer;
        public int i = 0;

        public MusicController(SoundPlayer soundtrack, int onOff, Timer timer)
        {
            Soundtrack = soundtrack;
            this.onOff = onOff;
            this.timer = timer;
        }

        public void SoundTrack() //Main controller of music
        {
            if (onOff == 1) //on off checkbox
            {
                if (i == 0) //just an integer for turning music on for 1 time
                {
                    Soundtrack.PlayLooping();                   
                    i++;
                }
                //it makes 1 circle and then loops for nothing
            }
            else //when checkbox off
            {
                Soundtrack.Stop();
                i = 0; //bringing i back to start position
            }

        }
        public void Stop()
        {
            Soundtrack.Stop();
        }
    }
}
