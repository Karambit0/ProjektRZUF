using SFML.Window;
using SFML.Audio;
namespace Sim
{
    class Program
    { 
        static void Main(string[] args)
        {   string docPath =
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "test.txt")))
                    outputFile.WriteLine("Time ; turn ; lv ; killer");

            Controller controller;

            //main test loop
            for(int i =0; i <30; i++){
          
            

            //init game engine
            controller = new Controller(1280,720,1200); //width, height, framerate
            //don't touch, it allows window to be closed
            controller.window.Closed += controller.HandleClose;
            //spawn creatures
            controller.SetBackground("space"); //space or battlefield
            controller.SpawnPlayer(100,20,30,10,1.0F); //maxHP, damage, attackDelay, maxAmmo, how much hp heal per turn
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
}