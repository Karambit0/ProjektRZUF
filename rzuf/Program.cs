using SFML.Window;
using SFML.Audio;
namespace Sim
{
    class Program
    {
        static void Main(string[] args)
        {
            //init game engine
            Controller controller = new Controller(1280,720,60); //width, height, framerate
            //don't touch, it allows window to be closed
            controller.window.Closed += controller.HandleClose;
            //generate list of enemies
            controller.SpawnEnemies(10,100,0,0,0); //number, Soldier, Turret, Armored, Angry chance
            //spawn creatures
            controller.SetBackground("space"); //space or battlefield
            controller.SpawnPlayer(100,8,30,6); //maxHP, damage, attackDelay, maxAmmo

            //game loop
            while (controller.Running())
            {
            controller.window.DispatchEvents();  
              //update
            controller.Update();

              //render
            controller.Render();

            }

        }
    }
}