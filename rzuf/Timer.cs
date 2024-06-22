using SFML.System;
using SFML.Audio;
using System.Diagnostics.Contracts;
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

        public override string ToString()
        {
            return Convert.ToString(timeElapsed); 
        }

    }

}


