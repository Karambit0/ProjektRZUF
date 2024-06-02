
using SFML.System;

namespace Sim
{
    class Soldier :Creature
    {
        int damage;
        public float speedX, speedY,baseSpeed;
        int attackDelay;
        int  attackRange;
        int xpPerKill;


        
        public void Act(Creature _rzuf)
        {

            if(delay!=0) delay--;
            else
            {

              if(Utility.Distance(position,_rzuf.position) <= attackRange)
                {
                  if(Attack(damage,_rzuf)!=0)
                  {
                    _rzuf.Die();
                  }
                  delay += attackDelay;

                }
              else
                Move(_rzuf.position);
            }

        }
        //moves towards position indicated by _target
        void Move(Vector2f _target)
        { 
          if(position.X>_target.X)
            position.X -= speedX; 
          else if(position.X<_target.X)
            position.X += speedX;

          if(position.Y>_target.Y)
            position.Y -= speedY; 
          else if(position.Y<_target.Y)
            position.Y += speedY;
       

          sprite.Position = position;
        }

        //returns amount of xp earned by kill
        public override int Die()
        {
          alive = false;
          return xpPerKill;
        }



     
    }


    
}