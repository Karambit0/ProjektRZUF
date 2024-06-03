using SFML.Graphics;
using SFML.System;

namespace Sim
{
    abstract class Creature
    {
        public int currentHP,maxHP;
        public bool alive;
        public int delay; //counter responible of waiting
        public Vector2f position; //contains position.X and position.Y, easier to move objects

        public Sprite sprite = new Sprite(); //sprite of the creature

        //returns 1 if target is alive, 0 if dead;
        public void Attack(int _damage, Creature _target)
        {
            _target.TakeDamage(_damage);
        }
        virtual public void TakeDamage(int _damage)
        {   
            
            currentHP = currentHP - _damage;
            if(currentHP<=0)
            Die();
        }

        public virtual int Die()
        {
            alive = false;
            return 0;
        }

        
     

    }


    
}