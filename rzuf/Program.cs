using SFML.Window;

namespace Sim
{
    class Program
    {
        static void Main(string[] args)
        {
           //Game_Window okienko = new Game_Window();
           List<Creature> potwory = new List<Creature>();
           Random losu = new Random();
           int losulosu;
           for(int i = 0; i<20; i++)
           {

            losulosu = losu.Next(0,100);
            if(losulosu>0&&losulosu<70)
            {
                Soldier soldier = new Soldier();
                potwory.Add(soldier);
            }
            if(losulosu>71&&losulosu<80)
            {
                Turret soldier = new Turret();
                potwory.Add(soldier);
            }
            if(losulosu>81&&losulosu<90)
            {
                ArmoredSoldier soldier = new ArmoredSoldier();
                potwory.Add(soldier);
            }
            if(losulosu>91&&losulosu<101)
            {
                AngrySoldier soldier = new AngrySoldier();
                potwory.Add(soldier);
            }
           // Console.WriteLine("Aktualne potwory: ");
            //foreach(Creature potwor in potwory)
            //{
           //   Console.WriteLine(potwor.GetType());  
           // }
           }

           
            Soldier s1 = new Soldier();
            s1.currentHP = 10;
            ArmoredSoldier s2 = new ArmoredSoldier();
            s2.currentHP = 15;

            s1.Attack(7,s2);
            System.Console.WriteLine(s2.currentHP);
            s2.ShieldUp();
            s1.Attack(3,s2);
            System.Console.WriteLine(s2.currentHP);


           
        }
    }
}