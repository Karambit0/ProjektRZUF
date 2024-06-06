using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using SFML.System;

namespace Sim
{
    class Turret : Soldier
    {   


        public Turret(int _turn, int _width, int _height)
        {
          maxHP = 20+_turn*10;
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
            SoundLibrary.PlaySound("reload",Controller.sounds);
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
                      SoundLibrary.PlaySound("bonk",Controller.sounds);
                      Attack(damage,_rzuf);
                      currentAmmo--;
                      delay += attackDelay;
                    }
                    else Reload();
                }
            }
        }




        
      /*  public void SetPosition() 
        {
          losulosu = losu.Next(0,2); //it gives equal chance to spawn on left or on right
                    if(losulosu == 0)
                        soldier.position.X = losu.Next(0, width/3);
                    else
                        soldier.position.X = losu.Next(width*2/3,width-100);
                soldier.position.Y = losu.Next(0,height-100);
        }*/
       
     
    }


    
}