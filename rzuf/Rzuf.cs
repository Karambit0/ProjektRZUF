
namespace Sim
{
    class Rzuf : Creature
    {
 
        int lv;
        Weapon Gun = new Weapon();

    }

    class Weapon
    {
        int damage;
        int currentAmmo,maxAmmo;

        void Reload()
        {
            currentAmmo = maxAmmo;
            ///
        }


    }
}