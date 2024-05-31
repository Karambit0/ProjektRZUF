using SFML.Window;

namespace Sim
{
    class Program
    {
        static void Main(string[] args)
        {
            //init game engine
            Controller controller = new Controller();
            controller.window.Closed += controller.HandleClose;
            controller.GenerateEnemies(100,10,40,20,20);
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