using System.ComponentModel.DataAnnotations;
using SFML.Graphics;

namespace Sim
{
    class Rzuf : Creature
    {
 
        int lv;
        Weapon gun;

         public void Heal()
        {
        }

        public void LvUp()
        {
        }

        public override int Die()
        {
            return 0; //to do: game ends
        }
        public void Act(Creature _enemy)
        {
            if(delay!=0) delay--;
            else
            {
                if (gun.currentAmmo ==0)
                    gun.Reload();
                else
                {
                    Attack(gun.damage,_enemy);
                    gun.currentAmmo--;
                    delay += gun.attackDelay;
                }   
            }
        }
        public Rzuf (int _maxHP, int _damage, int _attackDelay, int _maxAmmo)
        {
            maxHP = _maxHP;
            currentHP = maxHP;
            alive = true;
            gun = new Weapon(_damage, _attackDelay, _maxAmmo);
        }

    }

    class Weapon
    {
        public int damage, attackDelay;
        public int currentAmmo,maxAmmo;
        

        public void Reload()
        {
            currentAmmo = maxAmmo;
            ///
        }
        public Weapon(int _damage, int _attackDelay, int _maxAmmo){
            damage = _damage;
            attackDelay = _attackDelay;
            maxAmmo = _maxAmmo;
            currentAmmo = maxAmmo;

        }
        

    }
}