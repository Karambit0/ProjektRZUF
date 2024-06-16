using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Sim
{
    class Button
    {
        public RectangleShape button = new RectangleShape(); 
        public string function; //what does button do to differentiate each button
        public int status; //number to remember if button is clicked or not
        public Button(string _function, int _sizeX, int _sizeY, int _posX, int _posY)
        {
            button.Position = new Vector2f(_posX,_posY);
            button.Size = new Vector2f(_sizeX,_sizeY); 
            TextureLibrary.SetShapeTexture(_function,button);
            function = _function;
            Controller.buttons.Add(this);

        }
        //checks if mouse is within button borders
        public bool IsMouseOver(RenderWindow _window)
        {
            Vector2i mousePos = Mouse.GetPosition(_window);
            if(mousePos.X>button.Position.X&&mousePos.Y>button.Position.Y&&mousePos.X<button.Position.X+button.Size.X&&mousePos.Y<button.Position.Y+button.Size.Y)
            return true;
            else
            return false;
        }


    }
}






