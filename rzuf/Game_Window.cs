using SFML.Window;
using SFML.Graphics;

namespace Sim
{
    class Game_Window
    {
        public Game_Window()
        {
            RenderWindow okienko = new RenderWindow(new VideoMode(800,600),"World!");
            Color kolor = new Color(0,2,230);

            okienko.Closed += HandleClose;

            while (okienko.IsOpen)
            {
              okienko.DispatchEvents();  
              okienko.Clear(kolor);
              okienko.Display();
            }
            void HandleClose(object sender, EventArgs e)
            {
                okienko.Close();
            }
        }
    }
}