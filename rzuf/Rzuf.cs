using System.ComponentModel.DataAnnotations;
using SFML.Graphics;
using SFML.Audio;
namespace Sim
{
    class Rzuf : Creature
    {
        int xp,xpToLv,lv;
        public Weapon gun;

        //heals rzuf by portion of thier max hp 
         public void Heal(double _heal)
        {
            currentHP = currentHP + maxHP * _heal;
            if(currentHP>maxHP) currentHP=maxHP;

        }

        // lv ups rzuf as many times as it can incresing rzuf statistics
        public void LvUp()
        {
            while(xp>=xpToLv)
            {        
                maxHP += 5;
                currentHP +=5;

                gun.damage +=5;
                gun.maxAmmo ++;

                xp -= xpToLv;
                xpToLv += 100;   
                
                lv++;
            }

        }

        public override void Attack(double _damage, Creature _target)
        {
           xp += _target.TakeDamage(_damage);

        }
        public override int Die()
        {
            alive = false;
            TextureLibrary.SetSprite("rzuf dead",this);
            SoundLibrary.PlaySound("rzuf oof",Controller.sounds);
            return 0; //to do: game ends
        }
        public void Act(Creature _enemy)
        {   //LvUp();
            if(delay!=0) delay--;
            else
            {
                if (gun.currentAmmo ==0)
                {
                    delay = 90;
                    SoundLibrary.PlaySound("reload",Controller.sounds);
                    gun.Reload();
                }
                else
                {   
                    SoundLibrary.PlaySound("gun",Controller.sounds);
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
            xpToLv = 100;
        }

    }

    class Weapon
    {
        public int damage, attackDelay;
        public int currentAmmo,maxAmmo;
         public List<Sound> sounds = new List<Sound>();
    
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