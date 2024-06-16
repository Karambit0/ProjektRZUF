using SFML.Graphics;
using SFML.System;

namespace Sim
{
    public abstract class Creature
    {
        public double currentHP,maxHP;
        public bool alive; //is creature alive?
        public int delay; //counter responible of waiting
        public Vector2f position; 

        public Sprite sprite = new Sprite(); 

        public virtual void Attack(double _damage, Creature _target)
        {
            _target.TakeDamage(_damage);
        }
        public virtual int TakeDamage(double _damage)
        {   
            currentHP -= _damage;
            if(currentHP<=0)
            return Die();
            else return 0;
        }

        public virtual int Die()
        {
            alive = false;
            return 0;
        }

    }


    
}