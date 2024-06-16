using SFML.System;
using SFML.Audio;
namespace Sim
{

    public class Timer
    {
        public int timeElapsed = 0;
        public int inSeconds, seconds =0, minutes= 0;

        //counts how many frames passed since creating a timer
        public void CountTime()
        {
            timeElapsed++;
            Utility.ConvertTime(this);
        }    

    }

}


