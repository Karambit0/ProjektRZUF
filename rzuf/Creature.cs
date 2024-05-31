using SFML.Graphics;
using SFML.System;

namespace Sim
{
    abstract class Creature
    {
        public int currentHP,maxHP;
        public Vector2f position; //contains position.X and position.Y, easier to move objects

        public Sprite sprite = new Sprite(); //sprite of the creature

        //returns 1 if target is alive, 0 if dead;
        public int Attack(int _damage, Creature _target)
        {
            return _target.TakeDamage(_damage);
        }
        virtual public int TakeDamage(int _damage)
        {   
            
            currentHP = currentHP - _damage;
            if(currentHP<=0)
            {return 0;
            }
            else return 1; 
        }



        
     

    }


    
}