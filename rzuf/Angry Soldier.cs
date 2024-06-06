namespace Sim
{
    class AngrySoldier : Soldier
    {
        int baseAttackDelay;
        float givenSpeed;

        public AngrySoldier(int _turn, int _width, int _height)
        {
          maxHP = 20+_turn*10;
          currentHP = maxHP; 
          damage = 5+_turn*2.5;

          givenSpeed = 0.5F;
          attackRange = 100;

          baseAttackDelay = 60;
          xpPerKill = 10+_turn*10;

          alive = true;
          SetPosition(_width,_height);
        }
       
        

        public override void Act(Creature _rzuf)
        {   baseSpeed  = givenSpeed *( 1-(float)currentHP/(float)maxHP+1);
            attackDelay = (int)(baseAttackDelay* (0.5 * (float)currentHP/(float)maxHP + 0.5)); //to repair

            if(delay!=0) delay--;
            else
            {
              if(Utility.Distance(position,_rzuf.position) <= attackRange)
                {
                  Attack(damage,_rzuf);
                  delay += attackDelay;
                }
              else
                Move(_rzuf.position);
            }
        }



     
    }


    
}