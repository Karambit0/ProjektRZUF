namespace Sim
{
    class ArmoredSoldier : Soldier
    {
      bool shield;
        

      public void ShieldUp()
      {
          shield = true;
      }

      new public int TakeDamage(int _damage)
      {     if(shield)
              currentHP -= _damage;
            if(currentHP<=0)
              return 0;
            else return 1; 


      }
            
      
     
    }


    
}