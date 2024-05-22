
namespace Sim
{
    class Creture
    {
        int currentHP,maxHP;
        float posX, posY;
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

            
        }


    }
}