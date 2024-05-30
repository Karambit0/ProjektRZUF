using SFML.Graphics;

namespace Sim
{
    class Rzuf : Creature
    {
 
        int lv;
        public Sprite sprite = new Sprite();
        Weapon Gun = new Weapon();

         public void Heal()
        {
        }

        public void LvUp()
        {
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