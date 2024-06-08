using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SFML.System;

namespace Sim
{
    class Turret : Soldier
    {   

              Random losu = new Random(); //random bullshit generator
        public Turret(int _turn, int _width, int _height)
        {
          maxHP = 10+_turn*10; //to do: better equation to calculate hp, in turn 10 difference between each class will be marginal
          currentHP = maxHP; 
          damage = 5+_turn*2.0;

          attackRange = 9999; 

          attackDelay = 60;
          xpPerKill = 10+_turn*10;

          alive = true;
          SetPosition(_width,_height);


          //turet have some delay at the spawn to give Rzuf time to react
          delay = 120;
          realoadDelay= 90;
          currentAmmo = 4;
          maxAmmo = 4;

        }

        
        int maxAmmo, currentAmmo, realoadDelay=15;

        private void Reload()
        {
            SoundLibrary.PlaySound("reload",Controller.sounds); //to do: different reload for turret
            currentAmmo=maxAmmo;
            delay+=realoadDelay;
        }

        public override void Act(Creature _rzuf)
        {
            if(delay!=0) delay--;
            else
            {
              if(Utility.Distance(position,_rzuf.position) <= attackRange)
                {   
                  if(currentAmmo>0)
                    {
                      SoundLibrary.PlaySound("bonk",Controller.sounds); //to do: different gun sound for turret
                      Attack(damage,_rzuf);
                      currentAmmo--;
                      delay += attackDelay;
                    }
                    else Reload();
                }
            }
        }
        public override int Die()
        {
          alive = false;
          SoundLibrary.PlaySound("turret oof",Controller.sounds);
          return xpPerKill;
        }

        public override void SetPosition(int width, int height) //randomizes enemy position
        { 
          //enemies can only spawn 300px from left or right to give rzuf some time
            int losulosu;
            losulosu = losu.Next(0,2); //it gives equal chance to spawn on left or on right
                    if(losulosu == 0)
                        this.position.X = losu.Next(0, width/3);
                    else
                        this.position.X = losu.Next(width*2/3,width-100);
                this.position.Y = losu.Next(0,height-100);
                sprite.Position = position;
        }
       
    
    }


    
}