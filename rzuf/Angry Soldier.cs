namespace Sim
{
    class AngrySoldier : Soldier
    {
        int baseAttackDelay;
        float givenSpeed;

        public AngrySoldier(int _turn, int _width, int _height, float _xpMultiplayer)
        {
          maxHP = _turn*10; //to do: better equation to calculate hp, in turn 10 difference between each class will be marginal
          currentHP = maxHP; 
          damage = 5+_turn*2.5;

          givenSpeed = 0.5F;
          attackRange = 100;

          baseAttackDelay = 60;
          xpPerKill = (int)(_turn*9*_xpMultiplayer);

          alive = true;
          SetPosition(_width,_height);
          CreateHpBar();
        }
       
        

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
                  //SoundLibrary.PlaySound("bonk",Controller.sounds);
                  delay += attackDelay;
                }
              else
                Move(_rzuf.position);
            }
        }



     
    }


    
}