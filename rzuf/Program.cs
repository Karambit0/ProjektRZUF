using SFML.Window;
using SFML.Audio;
namespace Sim
{
    class Program
    {
        static void Main(string[] args)
        {
            //init game engine
            Controller controller = new Controller(1280,720,1200); //width, height, framerate
            //don't touch, it allows window to be closed
            controller.window.Closed += controller.HandleClose;
            //spawn creatures
            controller.SetBackground("space"); //space or battlefield
            controller.SpawnPlayer(50,20,30,10,0.5F); //maxHP, damage, attackDelay, maxAmmo, how much hp heal per turn
            controller.CreateGui();

            //game loop
            while (controller.Running())            {

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