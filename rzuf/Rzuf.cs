using System.ComponentModel.DataAnnotations;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
using System.Runtime.CompilerServices;
namespace Sim
{
    class Rzuf : Creature
    {
        int xp,xpToLv;

        public float baseHp, heal;
        public int lv;
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
                lv++;
                maxHP =baseHp*lv;

                gun.damage +=5;
                gun.maxAmmo = 10+(int)Math.Floor(lv*0.5);

                xp -= xpToLv;
                xpToLv += 100;   
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
        public async void Act(Creature _enemy)
        {   //LvUp();
            if(delay!=0) delay--;
            else
            {
                if (gun.currentAmmo ==0)
                {   gun.isReloading = true;
                    delay = 90;
                    SoundLibrary.PlaySound("reload",Controller.sounds);
                    await Task.Delay(1500);
                    gun.Reload();
                    gun.isReloading = false;
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
        public Rzuf (int _maxHP, int _damage, int _attackDelay, int _maxAmmo, float _heal)
        {
            baseHp = _maxHP;
            currentHP = baseHp;
            maxHP = baseHp;
            heal = _heal;
            alive = true;
            gun = new Weapon(_damage, _attackDelay, _maxAmmo);
            xpToLv = 100;
            lv = 1;
        }

    }

    class Weapon
    {
        public int damage, attackDelay;
        public Sprite sprite = new Sprite(); //sprite of the creature
        public Vector2f position; //contains position.X and position.Y, easier to move objects
        public int currentAmmo,maxAmmo;
        public bool isReloading; //it exists so we can dislapy "przeładowuje" instead of 0/number
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