namespace Sim
{
    abstract class Creature
    {
        int currentHP,maxHP;
        public float posX, posY;

        //returns 1 if target is alive, 0 if dead;
        public int Attack(int _damage, Creature _target)
        {
                return _target.TakeDamage(_damage);
        }
        public int TakeDamage(int _damage)
        {
            currentHP -= _damage;
            if(currentHP<=0)
            {return 0;
            }
            else return 1; 
        }



        
     

    }


    
}