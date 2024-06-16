using System.ComponentModel.DataAnnotations;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
using System.Runtime.CompilerServices;
namespace Sim
{
    class Rzuf : Creature
    {
        private int xp,xpToLv;
        private double waitTime;

        public double baseHp, heal;
        public int lv;
        public Weapon gun;

        //heals rzuf by set percentage of hp 
         public void Heal(double _heal)
        {
            currentHP = currentHP + maxHP * _heal;
            if(currentHP>maxHP) currentHP=maxHP;

        }

        // lv ups rzuf as many times as it can after each turn, increasing rzuf stats in process
        public void LvUp()
        {
            while(xp>=xpToLv)
            {        
                lv++;
                maxHP =baseHp*lv;

                gun.damage +=5;
                gun.maxAmmo = gun.maxAmmo+(int)Math.Floor(lv*0.5);

                xp -= xpToLv;
                xpToLv += 100;   
            }

        }
        //attacks enemy and adds xp to rzuf
        public override void Attack(double _damage, Creature _target)
        {
           xp += _target.TakeDamage(_damage);

        }
        //rzuf dies, changes apperance and special sound plays
        public override int Die()
        {
            alive = false;
            TextureLibrary.SetSprite("rzuf dead",this);
            SoundLibrary.PlaySound("rzuf oof",Controller.sounds);
            return 0;
        }
        //rzuf actions: shoots at closest enemy if ammo is present, if not - reloads
        public async void Act(Creature _enemy)
        { 
            if(delay!=0) delay--;
            else
            {
                if (gun.currentAmmo ==0)
                {   gun.isReloading = true;
                    delay = 90;
                    SoundLibrary.PlaySound("reload",Controller.sounds);
                    waitTime = (1000.0/Controller.currentFrameRate*90);
                    await Task.Delay((int)waitTime);
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
        public Rzuf (int _maxHP, int _damage, int _attackDelay, int _maxAmmo, double _heal)
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
        public Sprite sprite = new Sprite(); 
        public Vector2f position; 
        public int currentAmmo,maxAmmo;
        public bool isReloading; //it exists so we can dislapy "przeładowuje" instead of 0/number when reloading
        protected List<Sound> sounds = new List<Sound>(); //list of all gun sounds because sfml moment and there's a lot of them
    
        //reloads a gun
        public void Reload()
        {
            currentAmmo = maxAmmo;
            
        }
        public Weapon(int _damage, int _attackDelay, int _maxAmmo){
            damage = _damage;
            attackDelay = _attackDelay;
            maxAmmo = _maxAmmo;
            currentAmmo = maxAmmo;

        }
        

    }
}