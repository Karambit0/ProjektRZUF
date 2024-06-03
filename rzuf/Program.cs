using SFML.Window;

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
            controller.GenerateEnemies(20,100,0,0,0); //number, Soldier, Turret, Armored, Angry chance
            //spawn creatures
            controller.CreateWorld();
            controller.SpawnEnemies();
            controller.SpawnPlayer(100,10,30,20);

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