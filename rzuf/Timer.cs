using SFML.System;
using SFML.Audio;
namespace Sim
{

    public class Timer
    {
        public int timeElapsed = 0;
        public int inSeconds, seconds =0, minutes= 0;

        public void StartTimer()
        {
            timeElapsed++;
            Utility.ConvertTime(this);
        }    

    }

}


