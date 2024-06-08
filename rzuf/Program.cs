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
            controller.SetBackground("battlefield"); //space or battlefield
            controller.SpawnPlayer(100,50,30,6); //maxHP, damage, attackDelay, maxAmmo
            controller.CreateGui();

            //game loop
            while (controller.Running())
            {

            controller.StartTurn(); 

            controller.window.DispatchEvents();  
              //update
            controller.Update();

              //render
            controller.Render();

            }

        }
    }
}