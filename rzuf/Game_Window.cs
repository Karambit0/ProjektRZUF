using SFML.Window;
using SFML.Graphics;

namespace Sim
{
    class Game_Window
    {
        public Game_Window()
        {
            RenderWindow okienko = new RenderWindow(new VideoMode(1920,1280),"Rzuf!");
            Color kolor = new Color(0,2,230);

            okienko.Closed += HandleClose;
            
            //Texture soldier = new Texture("resources/soldier.png");
           // Texture rzuf = new Texture("resources/rzuf.png");
           Rzuf gracz = new Rzuf();
          // Sprite sprite = new Sprite(rzuf);
            int posX, posY;
            Random cords = new Random();
            posX = cords.Next(100, 1820);
            posY = cords.Next(100, 1180);
           // sprite.Position = new SFML.System.Vector2f(posX, posY);
            
            while (okienko.IsOpen)
            {
              okienko.DispatchEvents();  
              okienko.Clear(kolor);
         //     okienko.Draw(sprite);
              okienko.Display();
            }
            void HandleClose(object sender, EventArgs e)
            {
                okienko.Close();
            }
           
        }
    }
}