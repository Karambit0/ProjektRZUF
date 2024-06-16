using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;
namespace Sim
{
    class Program
    {
        static void Main(string[] args)
        {   /*Controller controller; 
            Console.WriteLine("Witaj w symulacji R.Z.U.F! Staramy się dobrać parametry strartowe w taki sposób, by Rzuf jak najskuteczniej pokonywał wrogów!");
            Console.WriteLine("Czy chcesz wpisać własne parametry (y/n)? Jeśli wybierzesz n, to zostaną zastosowane domyślne parametry:");
            Console.WriteLine("okno 1920/1080, hp 200, atak 20, szybkostrzelność 30, magazynek 10, leczenie 0.5, fala wrogów 5, mnożnik xp 1.0, szanse na wrogów 25/25/25/25.");
            char choice = Console.ReadKey().KeyChar;
            if(choice == 'y')
            {
                Console.WriteLine("Parametry okna");
                Console.WriteLine("Szerokość(int): ");
                int width = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Wysokość(int): ");
                int height = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Parametry Rzufa ");
                Console.WriteLine("Max HP (int): ");
                int maxHP = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Obrażenia broni (int): ");
                int damage = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Częstotliwość ataku w klatkach na sekundę (60 to 1sek, int): ");
                int attackDelay = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Wielkość magazynka(int): ");
                int maxAmmo = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Procent leczenia Rzufa po zakończeniu tury(float 0-1): ");
                double heal = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Parametry wrogów (suma wszystkich prawdopodobieństw musi być równa 100)");
                Console.WriteLine("Wielkość fali przeciwników(int): ");
                int enemyCount = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Mnożnik XP(float): ");
                double xpMultiplayer = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Szansa na Soldier(int): ");
                int chanceSoldier = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Szansa na Turret(int): ");
                int chanceTurret = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Szansa na Angry Soldier(int): ");
                int chanceAngrySoldier = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Szansa na Armored Soldier(int): ");
                int chanceArmoredSoldier = Convert.ToInt32(Console.ReadLine());

                //init game engine
                controller = new Controller((uint)width,(uint)height,60); //width, height, framerate
                controller.SetEnemySpawnRate(enemyCount,xpMultiplayer,chanceSoldier,chanceTurret,chanceAngrySoldier, chanceArmoredSoldier);
                //setting the stage
                controller.SetBackground("space"); //space or battlefield
                controller.SpawnPlayer(maxHP,damage,attackDelay,maxAmmo,heal);
            }
            else
            {*/
                //init game engine
                Controller controller = new Controller(1920,1080,60); //WIDTH, HEIGHT, FRAMERATE
                controller.SetEnemySpawnRate(5,1.0,25,25,25,25); //NUMBER OF ENEMIES, XP MULTIPLAYER, CHANCE FOR SPAWNING: SOLDIER, TURRET, ARMORED SOLDIER AND ANGRY SOLDIER
                //setting the stage
                controller.SetBackground("battlefield"); //SETS BACKGROUD. YOU CAN CHOOSE "space" OR "battlefield"
                controller.SpawnPlayer(200,20,30,10,0.5); //MAX HP, GUN DAMAGE, ATTACK DELAY, MAX AMMO, HOW MUCH HP RZUF HEALS PER TURN
            //}
        
            //all event handlers
            controller.window.Closed += controller.HandleClose;
            controller.window.MouseButtonPressed += new EventHandler<MouseButtonEventArgs>(controller.HandleMouseEvents);

            //gui and music
            controller.CreateGui();
            SoundLibrary.PlayMusic("inGame",Controller.music);
      
            //game loop
            while (controller.Running())            
            {
              //controll events
              controller.window.DispatchEvents();  
              //update
              controller.Update();

              //render
              controller.Render();

            }
        }
    }
}