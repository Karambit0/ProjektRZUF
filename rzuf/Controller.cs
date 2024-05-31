using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;

namespace Sim
{
    class Controller
    {   
        //variables
        public RenderWindow window; //de window
        VideoMode videoMode; //de resolution
        uint frameRate; //de framerate

        //game objects

        public static List<Creature> enemies = new List<Creature>(); //list of enemies
        public static Rzuf rzuf = new Rzuf(); //player
        Random losu = new Random(); //random bullshit generator

        //private functions
        private void InitWindow() //declare properties of window
        {
            this.videoMode.Width = 1920; //maybe pass it as argument later?
            this.videoMode.Height = 1280; //maybe pass it as argument later?
            this.frameRate = 60;
            this.window = new RenderWindow(this.videoMode,"Rzuf!");
            this.window.SetFramerateLimit(frameRate);
        }
        public bool Running() //checking if window is open
        {
            return this.window.IsOpen;
        }
        public void HandleClose(object sender, EventArgs e) //closing the window
            {
                this.window.Close();
            }
        //fuctions

        public void SpawnPlayer()
        {  //creates texture for rzuf
            Texture rzufSprite = new Texture("resources/rzuf.png");
            //sets rzuf position
            rzuf.sprite.Position = new SFML.System.Vector2f(900,600);
            //sets rzuf sprite
            rzuf.sprite.Texture = rzufSprite;

        }
        public void GenerateEnemies(int number, int chanceSoldier, int chanceTurret, int chanceArmoredSoldier, int chanceAngrySoldier)
        {
            /*
            randomly fills enemies list with random enemy types
            first parameter: number of enemies
            second parameter: chance for spawning Soldier (in %)
            third parameter: chance for spawning Turret (in %)
            forth parameter: chance for spawning Armored Soldier (in %)
            fifth parameter: chance for spawning Angry Soldier (in %)
            */
            int losulosu;
            for(int i = 0; i<number+1; i++)
            {

                losulosu = losu.Next(0,100);
                if(losulosu>0&&losulosu<chanceSoldier)
                {
                    Soldier soldier = new Soldier();
                    enemies.Add(soldier);
                }
                if(losulosu>chanceSoldier+1&&losulosu<chanceSoldier+chanceTurret)
                {
                    Turret soldier = new Turret();
                    enemies.Add(soldier);
                }
                if(losulosu>chanceSoldier+chanceTurret+1&&losulosu<chanceSoldier+chanceTurret+chanceArmoredSoldier)
                {
                    ArmoredSoldier soldier = new ArmoredSoldier();
                    enemies.Add(soldier);
                }
                if(losulosu>chanceSoldier+chanceTurret+chanceArmoredSoldier+1&&losulosu<101)
                {
                    AngrySoldier soldier = new AngrySoldier();
                    enemies.Add(soldier);
                }
           }
        }

        public void SpawnEnemies()
        {
            /*
            Spawns enemies and sets their textures and positions, depending on the type;
            */
            //creates textures for enemies
           Texture soldierSprite = new Texture("resources/soldier.png");
           Texture turretSprite = new Texture("resources/turret.png");
           Texture armoredSoldierSprite = new Texture("resources/armoredsoldier.png");
           Texture angrySoldierSprite = new Texture("resources/angrysoldier.png");
           foreach(Creature soldier in enemies)
           {    //randomizes enemies' positions
                soldier.posX = losu.Next(100,1820);
                soldier.posY = losu.Next(100,1180);
                soldier.sprite.Position = new SFML.System.Vector2f(soldier.posX,soldier.posY);
                //sets enemies' sprites
                if(soldier.GetType()== typeof(Soldier))
                soldier.sprite.Texture = soldierSprite;
                if(soldier.GetType()== typeof(AngrySoldier))
                soldier.sprite.Texture = angrySoldierSprite;
                if(soldier.GetType()== typeof(ArmoredSoldier))
                soldier.sprite.Texture = armoredSoldierSprite;
                if(soldier.GetType()== typeof(Turret))
                soldier.sprite.Texture = turretSprite;
           }
        }
        //game logic
        void UpdateEnemies()
        {

        }
        public void Update()
        {
            this.UpdateEnemies();
        }
        //game rendering
        void RenderPlayer()  //draws player sprite
        {
            this.window.Draw(rzuf.sprite);
        }
        void RenderEnemies() //draws enemies sprites
        {  
            foreach(Creature soldier in enemies)
           {
            this.window.Draw(soldier.sprite);
           }
        }
        public void Render() // renders the game objects
        {
            /*
            - clear old frame
            - render objects
            - display frame in window
            */
            this.window.Clear();

            this.RenderPlayer();
            this.RenderEnemies();

            this.window.Display();
        }
        //constructors
        public Controller(){
            InitWindow();
        }
    }


    
}