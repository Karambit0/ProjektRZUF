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

    }












}