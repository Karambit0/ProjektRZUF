using SFML.Graphics;
using SFML.System;

namespace Sim
{
    public abstract class Creature
    {
        public double currentHP,maxHP;
        public bool alive;
        public int delay; //counter responible of waiting
        public Vector2f position; //contains position.X and position.Y, easier to move objects

        public Sprite sprite = new Sprite(); //sprite of the creature
        Texture soldierHurtSprite = new Texture("resources/sprites/soldierHurt.png");

        public virtual void Attack(double _damage, Creature _target)
        {
            _target.TakeDamage(_damage);
        }
        public virtual int TakeDamage(double _damage)
        {   
            if(this.GetType()== typeof(Soldier))
                {
                    this.sprite.Texture = soldierHurtSprite;
                    
                }
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