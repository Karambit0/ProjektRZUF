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
        int enemyCount;

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
            this.enemyCount = number;
            for(int i = 0; i<number; i++)
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
            Spawns enemies and sets their textures, speed and positions, depending on the type;
            */
            //creates textures for enemies
           Texture soldierSprite = new Texture("resources/soldier.png");
           Texture turretSprite = new Texture("resources/turret.png");
           Texture armoredSoldierSprite = new Texture("resources/armoredsoldier.png");
           Texture angrySoldierSprite = new Texture("resources/angrysoldier.png");
           foreach(Soldier soldier in enemies)
           {    //randomizes enemies' positions
                soldier.position.X = losu.Next(100,1820);
                soldier.position.Y = losu.Next(100,1180);
                soldier.sprite.Position = soldier.position;
                //sets enemies' properties
                if(soldier.GetType()== typeof(Soldier)) //here we can declare starting properties of Soldier
                {
                soldier.speedX = 5;
                soldier.speedY =  soldier.speedX;
                soldier.sprite.Texture = soldierSprite;
                }
                if(soldier.GetType()== typeof(AngrySoldier)) //here we can declare starting properties of Angry Soldier
                {
                soldier.speedX = 7;
                soldier.speedY =  soldier.speedX;
                soldier.sprite.Texture = angrySoldierSprite;
                }
                if(soldier.GetType()== typeof(ArmoredSoldier)) //here we can declare starting properties of Armored Soldier
                {
                soldier.speedX = 4;
                soldier.speedY =  soldier.speedX;
                soldier.sprite.Texture = armoredSoldierSprite;
                }
                if(soldier.GetType()== typeof(Turret)) //here we can declare starting properties of Turret
                {
                soldier.speedX = 0;
                soldier.speedY =  soldier.speedX;
                soldier.sprite.Texture = turretSprite;
                }
           }
        }
        //game logic
        void UpdateEnemies()
        {
                //moving enemies
            foreach(Soldier soldier in enemies)
            {   
                //to be removed later, now it allows enemies to bounce back in window, because it's funny
                if(soldier.position.X<0||soldier.position.X>1820)
                    soldier.speedX*= -1;
                if(soldier.position.Y<0||soldier.position.Y>1180)
                    soldier.speedY*= -1;
                    //moving enemy sprite based on its position and speed
                soldier.position.X += soldier.speedX; 
                soldier.position.Y += soldier.speedY;
                soldier.sprite.Position = soldier.position;
            }
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