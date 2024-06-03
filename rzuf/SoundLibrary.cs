using SFML.System;
using SFML.Audio;
namespace Sim
{

    public class SoundLibrary
    {
        static SoundBuffer gunSound = new SoundBuffer("resources/gun.wav");
        static SoundBuffer reloadSound = new SoundBuffer("resources/reload.wav");
        static SoundBuffer deathSound = new SoundBuffer("resources/oof.wav");
        

        
    public static void PlaySound(string _soundName, List<Sound> sounds)
        {Sound sound = new Sound();
            if(_soundName == "gun")
                {sound.SoundBuffer = gunSound;}
            if(_soundName == "reload")
                {sound.SoundBuffer =reloadSound;} 
            if(_soundName == "oof")
               {sound.SoundBuffer =deathSound;} 
            sounds.Add(sound);
            sound.Play();   
        }

    }
}

