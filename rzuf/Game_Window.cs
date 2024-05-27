using SFML.Window;
using SFML.Graphics;

namespace Sim
{
    class Game_Window
    {
        public Game_Window()
        {
            RenderWindow okienko = new RenderWindow(new VideoMode(800,600),"Rzuf!");
            Color kolor = new Color(0,2,230);

            okienko.Closed += HandleClose;
            Texture tekstura = new Texture("resources/rzuf.jpeg");
            Sprite rzuf = new Sprite(tekstura);
            rzuf.Position = new SFML.System.Vector2f(200.0f, 200.0f);
            
            while (okienko.IsOpen)
            {
              okienko.DispatchEvents();  
              okienko.Clear(kolor);
              okienko.Draw(rzuf);
              okienko.Display();
            }
            void HandleClose(object sender, EventArgs e)
            {
                okienko.Close();
            }
           
        }
    }
}