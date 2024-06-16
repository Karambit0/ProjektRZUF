using SFML.System;
using SFML.Graphics;
namespace Sim
{
    //functions dedicated to writing text on screen
    public class TextLibrary
    {
        //all of the fonts used in game
        static Font robotoBold = new Font("resources/fonts/Roboto-Bold.ttf");

        //our text, where start x, y, list of texts to add to
        public static void WriteText(string _text, int _width, int _height,List<Text> _lines) 
        {
            Text myText = new Text();
            myText.Font = robotoBold;
            myText.DisplayedString = _text;
            myText.Position = new SFML.System.Vector2f(_width, _height);
            _lines.Add(myText);

        }

        

        
    

    }
}

