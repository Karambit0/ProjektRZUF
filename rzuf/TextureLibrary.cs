using SFML.System;
using SFML.Graphics;
namespace Sim
{
//functions dedicated to working with all the sprites in project
    public class TextureLibrary
    {
        //all the sprites used in game

        //rzuf sprites
        static Texture rzufSprite = new Texture("resources/sprites/rzuf.png");
        static Texture rzufDeadSprite = new Texture("resources/sprites/rzufDead.png");
        static Texture gunSprite = new Texture("resources/sprites/gun.png"); 

        //enemy sprites
        static Texture soldierSprite = new Texture("resources/sprites/soldier.png");
        static Texture turretSprite = new Texture("resources/sprites/turret.png");
        static Texture armoredSoldierSprite = new Texture("resources/sprites/armoredsoldier.png");
        static Texture armoredSoldierShieldSprite = new Texture("resources/sprites/armoredsoldiershield.png");
        static Texture angrySoldierSprite = new Texture("resources/sprites/angrysoldier.png");
        
        //gui sprites
        static Texture pauseSprite = new Texture("resources/sprites/pause.png");
        static Texture playSprite = new Texture("resources/sprites/play.png");
        static Texture x1Sprite = new Texture("resources/sprites/x1.png");
        static Texture x2Sprite = new Texture("resources/sprites/x2.png");
        static Texture x3Sprite = new Texture("resources/sprites/x3.png");
        
        //backgrounds
        static Texture space = new Texture("resources/backgrounds/space.jpg"); //Image by Gerd Altmann from Pixabay
        static Texture battlefield = new Texture("resources/backgrounds/battlefield.jpg"); //https://www.artstation.com/artwork/Oyolbv
        
    //sets sprites from list only for shapes!!!
    public static void SetShapeTexture(string _spriteName, Shape _shape)
    {
        if(_spriteName=="pause")
        _shape.Texture = pauseSprite;
        if(_spriteName=="play")
        _shape.Texture = playSprite;
        if(_spriteName=="speed")
        _shape.Texture = x1Sprite;
        if(_spriteName=="x2")
        _shape.Texture = x2Sprite;
        if(_spriteName=="x3")
        _shape.Texture = x3Sprite;
    }
    //sets sprites from list only for sprites!!!
    public static void SetTexture(string _spriteName, Sprite _sprite) 
    {
        if(_spriteName=="space")
        _sprite.Texture = space;
        if(_spriteName=="battlefield")
        _sprite.Texture = battlefield;
        if(_spriteName=="gun")
        _sprite.Texture = gunSprite;
    }
    //sets sprites from list only for Creatures!!!
    public static void SetSprite(string _spriteName, Creature _creature) 
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
            if(_spriteName=="rzuf dead")
            _creature.sprite.Texture = rzufDeadSprite;
        }

    }
}

