using SFML.System;
using SFML.Audio;
namespace Sim
{

    public class Utility
    {

        // fuction return flat containg distance between points _v1 and _v2
        public static float Distance(Vector2f _v1, Vector2f _v2)

        {
        return  (float)Math.Sqrt((double)(_v1.X-_v2.X)*(_v1.X-_v2.X)+ (_v1.Y-_v2.Y)*(_v1.Y-_v2.Y));
        }
        public static float GetAngle(Vector2f _v1, Vector2f _v2) //closest enemy position, gun position
        {
            _v1.X+=50;
            _v1.Y+=50;
            if(_v1.Y<_v2.Y)
            return -1*(float)Math.Acos((_v1.X-_v2.X)/Distance(_v1,_v2))*57.3F;
            else
            return (float)Math.Acos((_v1.X-_v2.X)/Distance(_v1,_v2))*57.3F;
        }

    }












}