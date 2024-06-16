using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SFML.System;

namespace Sim
{
    class Turret : Soldier
    {   

        Random losu = new Random(); //random number generator
        public Turret(int _turn, int _width, int _height, double _xpMultiplayer)
        {
          maxHP = _turn*5; 
          currentHP = maxHP; 
          damage = 2+_turn*2.0;

          attackRange = 9999; 

          attackDelay = 60;
          xpPerKill = (int)(_turn*8*_xpMultiplayer);

          alive = true;
          SetPosition(_width,_height);
          CreateHpBar();

          //turret have some delay at the spawn to give Rzuf time to react
          delay = 120;
          realoadDelay= 90;
          currentAmmo = 4;
          maxAmmo = 4;

        }

        
        int maxAmmo, currentAmmo, realoadDelay=15;

        //reloads itself
        private void Reload()
        {
            SoundLibrary.PlaySound("reload",Controller.sounds); 
            currentAmmo=maxAmmo;
            delay+=realoadDelay;
        }
        //turret actions: spawns, builds itself and attacks rzuf from spawn position
        public override void Act(Creature _rzuf)
        {
            if(delay!=0) delay--;
            else
            {
              if(Utility.Distance(position,_rzuf.position) <= attackRange)
                {   
                  if(currentAmmo>0)
                    {
                      SoundLibrary.PlaySound("bonk",Controller.sounds); 
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
          return xpPerKill;
        }
        //randomizes enemy position, turret can only spawn 1/3 width from left or right to give rzuf some time
        public override void SetPosition(int width, int height)
        { 
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