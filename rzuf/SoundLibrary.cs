using SFML.System;
using SFML.Audio;
namespace Sim
{
//functions dedicated to working with all the sounds in project
    public class SoundLibrary
    {
        //all the sound effect, used in game
        static SoundBuffer gunSound = new SoundBuffer("resources/sounds/gun.wav");
        static SoundBuffer reloadSound = new SoundBuffer("resources/sounds/reload.wav");
        static SoundBuffer deathSound = new SoundBuffer("resources/sounds/oof.wav");
        static SoundBuffer rzufDeathSound = new SoundBuffer("resources/sounds/rzoof.wav");
        static SoundBuffer bonkSound = new SoundBuffer("resources/sounds/bonk.wav");
        

        //plays sound effect from parameter ad puts it in main sound list, because of sfml moment
    public static void PlaySound(string _soundName, List<Sound> sounds)
        {
            Sound sound = new Sound();
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
            sounds.Add(sound);
            sound.Volume = 20;
            sound.Play();   
        }

    //main music loop, first track is an intro, so it plays only once, second track is looped
    public async static void PlayMusic(string _musicName, List<Music> _music)
    {
        if(_musicName =="inGame")
        {
            Music intro = new Music("resources/sounds/OST-intro.wav");
            _music.Add(intro);
            intro.Play();
            await Task.Delay(7000); //OST.wav starts after OST-intro ends
            Music music = new Music("resources/sounds/OST.wav");
            _music.Add(music);
            music.Loop = true;
            music.Play();

        }
    }
    }
}

