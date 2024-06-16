using System.Numerics;
using SFML.System;
using SFML.Graphics;
namespace Sim
{
    class Soldier :Creature
    {
        protected double damage;
        public float baseSpeed, distance; 
        protected Vector2f speed;
        Random losu = new Random(); //random number generator
        public RectangleShape hpBar = new RectangleShape();
        protected int attackDelay;
        protected int attackRange;
         protected int xpPerKill;
      
        //sets speed vector, so enemy goes straight to rzuf
        public static Vector2f SetSpeed(Vector2f _position, Vector2f _target, float _baseSpeed) 
        {
            Vector2f tempSpeed = new Vector2f
            {
                X = _baseSpeed * (_target.X  - _position.X)/ Utility.Distance(_target, _position),
                Y = _baseSpeed * ( _target.Y - _position.Y  ) / Utility.Distance(_target, _position)
            };

            return tempSpeed;
        }
        //randomizes enemy position. enemies can only spawn 1/6 width from left or right to give rzuf some time to react
         public virtual void SetPosition(int width, int height) 
        { 
            int losulosu;
                losulosu = losu.Next(0,2); //gives equal chance to spawn on left or on right
                    if(losulosu == 0)
                         position.X = losu.Next(0, width/6);
                    else
                        position.X = losu.Next(width*5/6,width-100); 
                position.Y = losu.Next(0,height-100);   
                sprite.Position = position;
        }
        public void CreateHpBar() //creates hp bar under enemy's feet
        {
          hpBar.Position = new Vector2f(position.X,position.Y+110);
          hpBar.Size = new Vector2f(100, 10); 
          hpBar.FillColor = new Color(255,0,0); 
        }
        

        public Soldier(int _turn, int _width, int _height, double _xpMultiplayer)
        {
          maxHP = _turn*10; 
          currentHP = maxHP; 
          damage = 5+_turn*2.0;

          baseSpeed = 0.5F;
          attackRange = 100;

          attackDelay = 60;
          xpPerKill = (int)(_turn*6*_xpMultiplayer);

          alive = true;
          SetPosition(_width,_height);
          CreateHpBar();
        }

        public Soldier()
        {}
        //angry soldier actions: sets a course to rzuf, when near rzuf - attacks
        public virtual void Act(Creature _rzuf)
        {
            if(delay!=0) delay--;
            else
            {
              if(Utility.Distance(position,_rzuf.position) <= attackRange)
                {
                  Attack(damage,_rzuf);
                  SoundLibrary.PlaySound("bonk",Controller.sounds);
                  delay += attackDelay;
                }
              else
                Move(_rzuf.position);
            }
        }
        //moves towards a set of coordinates
        protected void Move(Vector2f _target)
        { 
          speed = SetSpeed(position, _target, baseSpeed);
       
            position.X += speed.X;
            position.Y += speed.Y;
       

          sprite.Position = position;
          distance = Utility.Distance(_target, position);
        }

        //returns amount of xp earned by kill
        public override int Die()
        {
          alive = false;
          SoundLibrary.PlaySound("oof",Controller.sounds);
          return xpPerKill;
        }



     
    }


    
}