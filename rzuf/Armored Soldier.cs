using SFML.System;

namespace Sim
{
    class ArmoredSoldier : Soldier
    {
      bool shield;
      int moveTime,waitTime,moveTimeCounter;
      
       public ArmoredSoldier(int _turn, int _width, int _height, double _xpMultiplayer)
        {
          maxHP = _turn*15; //to do: better equation to calculate hp, in turn 10 difference between each class will be marginal
          currentHP = maxHP; 
          damage = 3+_turn*2;

          baseSpeed = 0.5F;
          attackRange = 100;

          attackDelay = 60;
          xpPerKill = (int)(_turn*10*_xpMultiplayer);

          alive = true;
          SetPosition(_width,_height);
          CreateHpBar();
          shield = false;

          //move time indicates how long armored soldeir moves before it stops
          //similarry wait time says how long it stays in place
          waitTime = 60;
          moveTime = 240;
          moveTimeCounter = moveTime;
        }
        //armored soldier actions: sets a course to rzuf, after few seconds stops, uses shield and is invincible for 2 seconds, when near rzuf - attacks
        public override void Act(Creature _rzuf)
        {
            if(delay!=0) delay--;
            else
            { shield = false;
              if(Utility.Distance(position,_rzuf.position) <= attackRange)
                {
                  Attack(damage,_rzuf);
                  SoundLibrary.PlaySound("bonk",Controller.sounds);
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
            if(shield == true)
              TextureLibrary.SetSprite("armored soldier shield",this);
            else
              TextureLibrary.SetSprite("armored soldier",this);
        }
      public override int TakeDamage(double _damage)
      {     if(!shield)
              currentHP -= _damage;
            if(currentHP<=0)
            return Die();
            else return 0;
      }   
     
    }
    
}