using SFML.System;
using SFML.Audio;
namespace Sim
{

    public class SoundLibrary
    {
        static SoundBuffer gunSound = new SoundBuffer("resources/sounds/gun.wav");
        static SoundBuffer reloadSound = new SoundBuffer("resources/sounds/reload.wav");
        static SoundBuffer deathSound = new SoundBuffer("resources/sounds/oof.wav");
        static SoundBuffer rzufDeathSound = new SoundBuffer("resources/sounds/rzoof.wav");
        static SoundBuffer turretDeathSound = new SoundBuffer("resources/sounds/turretoof.wav");
        static SoundBuffer bonkSound = new SoundBuffer("resources/sounds/bonk.wav");
        

        
    public static void PlaySound(string _soundName, List<Sound> sounds)
        {Sound sound = new Sound();
            if(_soundName == "gun")
                {sound.SoundBuffer = gunSound;}
            if(_soundName == "reload")
                {sound.SoundBuffer =reloadSound;} 
            if(_soundName == "oof")
               {sound.SoundBuffer =deathSound;} 
            if(_soundName == "bonk")
               {sound.SoundBuffer =bonkSound;} 
            if(_soundName == "rzuf oof")
               {sound.SoundBuffer =rzufDeathSound;} 
            if(_soundName == "turret oof")
               {sound.SoundBuffer =turretDeathSound;} 
            sounds.Add(sound);
            sound.Play();   
        }

    }
}

