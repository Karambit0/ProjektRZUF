using SFML.System;
using SFML.Audio;
namespace Sim
{
//general functions to use in the project
    public class Utility
    {

        // fuction returns distance between points two positions
        public static float Distance(Vector2f _v1, Vector2f _v2)

        {
        return  (float)Math.Sqrt((double)(_v1.X-_v2.X)*(_v1.X-_v2.X)+ (_v1.Y-_v2.Y)*(_v1.Y-_v2.Y));
        } 
        //function gets the angle between gun and enemy
        public static float GetAngle(Vector2f _v1, Vector2f _v2) //closest enemy position, gun position
        {
            _v1.X+=50;
            _v1.Y+=50;
            if(_v1.Y<_v2.Y)
            return -1*(float)Math.Acos((_v1.X-_v2.X)/Distance(_v1,_v2))*57.3F;
            else
            return (float)Math.Acos((_v1.X-_v2.X)/Distance(_v1,_v2))*57.3F;
        }
        //function converts time from frames to seconds and minutes
        public static void ConvertTime(Timer _timer) 
        {
            _timer.inSeconds = (int)(_timer.timeElapsed/60);
            _timer.minutes = _timer.inSeconds/60-(_timer.inSeconds/60-(int)(_timer.inSeconds/60));
            _timer.seconds = _timer.inSeconds-_timer.minutes*60;
        }

    }












}