using SFML.System;

namespace Sim
{
    class ArmoredSoldier : Soldier
    {
      bool shield;
      int moveTime,waitTime,moveTimeCounter;
      
       public ArmoredSoldier(int _turn, int _width, int _height)
        {
          maxHP = 20+_turn*10;
          currentHP = maxHP; 
          damage = 5+_turn*2.5;

          baseSpeed = 0.5F;
          speed.X = baseSpeed;
          speed.Y = baseSpeed;
          attackRange = 100;

          attackDelay = 60;
          xpPerKill = 10+_turn*10;

          alive = true;
          SetPosition(_width,_height);

          shield = false;
          waitTime = 80;
          moveTime = 50;
          moveTimeCounter = moveTime;
        }

        public override void Act(Creature _rzuf)
        {
            if(delay!=0) delay--;
            else
            { shield = false;
              if(Utility.Distance(position,_rzuf.position) <= attackRange)
                {
                  Attack(damage,_rzuf);
                  delay += attackDelay;
                }
              else
                if(moveTimeCounter!=0)
                  {
                    Move(_rzuf.position);
                    moveTimeCounter--;
                  }
                else 
                {
                    shield = true;
                    delay += waitTime;
                    moveTimeCounter += moveTime;
                }
                
            }
        }

      public void TakeDamage(int _damage)
      {     if(!shield)
              currentHP -= _damage;
            if(currentHP<=0)
              Die();
      }   
     
    }
    
}