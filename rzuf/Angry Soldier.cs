namespace Sim
{
    class AngrySoldier : Soldier
    {
        int baseAttackDelay;
        float givenSpeed;

        public AngrySoldier(int _turn, int _width, int _height, double _xpMultiplayer)
        {
          maxHP = _turn*7.5; 
          currentHP = maxHP; 
          damage = 5+_turn*2.5;

          givenSpeed = 0.5F;
          attackRange = 75;

          baseAttackDelay = 60;
          xpPerKill = (int)(_turn*9*_xpMultiplayer);

          alive = true;
          SetPosition(_width,_height);
          CreateHpBar();
        }
       
        //angry soldier actions: sets a course to rzuf, when hit increases speed, when near rzuf - attacks
        public override void Act(Creature _rzuf)
        {   
            //move speed increses as hp goes down up to 500% of base 
            baseSpeed  = givenSpeed *( 4-4*(float)currentHP/(float)maxHP+1);
            //attack delay decreses as hp goes down up to 150% of base
            attackDelay = (int)(baseAttackDelay* (0.5 * (float)currentHP/(float)maxHP + 0.5));

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
    }
}