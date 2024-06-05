/*namespace Sim
//using SFML.System;
{
    class Turret : Soldier
    {   
        public Turret(int _HP, int _damdage, Vector2f _position) :base(_HP, _damdage, _position, )
        {
          currentHP = _HP;
          maxHP = _HP;
          damage = _damdage;
          position = _position;
          sprite.Position = _position;

          baseSpeed = _baseSpeed;
          speed.X = _baseSpeed;
          speed.Y = _baseSpeed;
          attackRange = 5;

          attackDelay = 10;
          xpPerKill = 10;

          alive = true;

          SetSpeed( _position, _target, _baseSpeed);
        }

        
        int maxAmmo, currentAmmo;
        
        public void SetPosition() 
        {
          losulosu = losu.Next(0,2); //it gives equal chance to spawn on left or on right
                    if(losulosu == 0)
                        soldier.position.X = losu.Next(0, width/3);
                    else
                        soldier.position.X = losu.Next(width*2/3,width-100);
                soldier.position.Y = losu.Next(0,height-100);
        }
       
     
    }


    
}*/