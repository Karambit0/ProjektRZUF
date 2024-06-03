
using System.Numerics;
using SFML.System;

namespace Sim
{
    class Soldier :Creature
    {
        int damage;
        public float baseSpeed, distance;
        public Vector2f speed;

        int attackDelay;
        int  attackRange = 100;
        int xpPerKill;
        
      
        private Vector2f SetSpeed(Vector2f _position, Vector2f _target, float _baseSpeed)
        {
            Vector2f tempSpeed = new Vector2f
            {
                X = _baseSpeed * (_target.X  - _position.X)/ Utility.Distance(_target, _position),
                Y = _baseSpeed * ( _target.Y - _position.Y  ) / Utility.Distance(_target, _position)
            };

            return tempSpeed;
        }


        public Soldier(int _HP, int _damage, Vector2f _position, float _baseSpeed, Vector2f _target)
        {
          currentHP = 20; //_HP
          maxHP = 20; //_HP
          damage = _damage;
          position = _position;
          sprite.Position = _position;

          baseSpeed = _baseSpeed;
          speed.X = _baseSpeed;
          speed.Y = _baseSpeed;
          attackRange = 5;

          attackDelay = 10;
          xpPerKill = 10;

          alive = true;

          speed = SetSpeed( _position, _target, _baseSpeed);
        }

        public Soldier()
        {}

        public void Act(Creature _rzuf)
        {
            if(delay!=0) delay--;
            else
            {
              if(Utility.Distance(position,_rzuf.position) <= attackRange)
                {
                  Attack(damage,_rzuf);
                  delay += attackDelay;
                  speed.X = 0;
                  speed.Y = 0;
                }
              else
                Move(_rzuf.position);
            }
        }
        //moves towards position indicated by _target
        void Move(Vector2f _target)
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
          return xpPerKill;
        }



     
    }


    
}