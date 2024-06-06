using SFML.System;
using SFML.Graphics;
namespace Sim
{

    public class TextureLibrary
    {
        static Texture rzufSprite = new Texture("resources/rzuf.png");
        static Texture rzufDeadSprite = new Texture("resources/rzufDead.png");
        static Texture soldierSprite = new Texture("resources/soldier.png");
        static Texture turretSprite = new Texture("resources/turret.png");
        static Texture armoredSoldierSprite = new Texture("resources/armoredsoldier.png");
        static Texture angrySoldierSprite = new Texture("resources/angrysoldier.png");
        static Texture hurtSprite = new Texture("resources/soldierHurt.png");
        static Texture space = new Texture("resources/space.jpg"); //Image by Gerd Altmann from Pixabay
        static Texture battlefield = new Texture("resources/battlefield.jpg"); //https://www.artstation.com/artwork/Oyolbv
        
    public static void SetTexture(string _spriteName, Sprite _sprite) //only for sprites!!!
    {
        if(_spriteName=="space")
        _sprite.Texture = space;
        if(_spriteName=="battlefield")
        _sprite.Texture = battlefield;
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
            if(_spriteName=="soldier hurt")
            _creature.sprite.Texture = hurtSprite;
            if(_spriteName=="rzuf dead")
            _creature.sprite.Texture = rzufDeadSprite;
        }

    }
}

