using SFML.Graphics;

namespace Sim
{
    class Rzuf : Creature
    {
 
        int lv;
        Weapon Gun = new Weapon();

         public void Heal()
        {
        }

        public void LvUp()
        {
        }

        public override int Die()
        {
            return 0;
        }

    }

    class Weapon
    {
        int damage;
        int currentAmmo,maxAmmo;

        public void Reload()
        {
            currentAmmo = maxAmmo;
            ///
        }
        

    }
}