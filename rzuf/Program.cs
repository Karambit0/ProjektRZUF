using SFML.Window;

namespace Sim
{
    class Program
    {
        static void Main(string[] args)
        {
            //init game engine
            Controller controller = new Controller();
            //don't touch, it allows window to be closed
            controller.window.Closed += controller.HandleClose;
            //generate list of enemies
            controller.GenerateEnemies(100,25,25,25,25);
            //spawn creatures (rzuf is always the same so no GenerateRzuf)
            controller.SpawnEnemies();
            controller.SpawnPlayer();

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