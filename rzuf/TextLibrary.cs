using SFML.System;
using SFML.Graphics;
namespace Sim
{
    public class TextLibrary
    {
        static Font robotoBold = new Font("resources/fonts/Roboto-Bold.ttf");

        public static void WriteText(string _text, int _width, int _height,List<Text> _lines) //our text, where start x, y, list of texts to add to
        {
            Text myText = new Text();
            myText.Font = robotoBold;
            myText.DisplayedString = _text;
            myText.Position = new SFML.System.Vector2f(_width, _height);
            _lines.Add(myText);

        }

        

        
    

    }
}

