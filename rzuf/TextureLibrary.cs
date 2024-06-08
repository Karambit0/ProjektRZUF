using SFML.System;
using SFML.Graphics;
namespace Sim
{

    public class TextureLibrary
    {
        //sprites
        static Texture rzufSprite = new Texture("resources/sprites/rzuf.png");
        static Texture rzufDeadSprite = new Texture("resources/sprites/rzufDead.png");
        static Texture gunSprite = new Texture("resources/sprites/gun.png"); // https://tamamangrosse.itch.io/pistol-sprite
        static Texture soldierSprite = new Texture("resources/sprites/soldier.png");
        static Texture turretSprite = new Texture("resources/sprites/turret.png");
        static Texture armoredSoldierSprite = new Texture("resources/sprites/armoredsoldier.png");
        static Texture armoredSoldierShieldSprite = new Texture("resources/sprites/armoredsoldier.png");
        static Texture angrySoldierSprite = new Texture("resources/sprites/angrysoldier.png");
        static Texture hurtSprite = new Texture("resources/sprites/soldierHurt.png");

        //backgrounds
        static Texture space = new Texture("resources/backgrounds/space.jpg"); //Image by Gerd Altmann from Pixabay
        static Texture battlefield = new Texture("resources/backgrounds/battlefield.jpg"); //https://www.artstation.com/artwork/Oyolbv
        
    public static void SetTexture(string _spriteName, Sprite _sprite) //only for sprites!!!
    {
        if(_spriteName=="space")
        _sprite.Texture = space;
        if(_spriteName=="battlefield")
        _sprite.Texture = battlefield;
        if(_spriteName=="gun")
        _sprite.Texture = gunSprite;
    }
    public static void SetSprite(string _spriteName, Creature _creature) //only for Creatures!!!
        {
            if(_spriteName=="rzuf")
            _creature.sprite.Texture = rzufSprite;
            if(_spriteName=="soldier")
            _creature.sprite.Texture = soldierSprite;
             if(_spriteName=="turret")
            _creature.sprite.Texture = turretSprite;
             if(_spriteName=="angry soldier")
            _creature.sprite.Texture = angrySoldierSprite;
             if(_spriteName=="armored soldier")
            _creature.sprite.Texture = armoredSoldierSprite;
            if(_spriteName=="armored soldier shield")
            _creature.sprite.Texture = armoredSoldierShieldSprite;
            if(_spriteName=="soldier hurt")
            _creature.sprite.Texture = hurtSprite;
            if(_spriteName=="rzuf dead")
            _creature.sprite.Texture = rzufDeadSprite;
        }

    }
}

