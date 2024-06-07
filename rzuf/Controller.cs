using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;
using System.Runtime.Versioning;
using System.Net.Mail;

namespace Sim
{
    class Controller
    {   
        //variables for window
        public RenderWindow window; //de window
        VideoMode videoMode; //de resolution
        uint frameRate; //de framerate

        //game variables
        int width,height; //width and height of window for easier use
        public int enemyCount = 0; //how many enemies are on screen
        float closestEnemyDistance = 9999; 
        int turn = 0; //number of turns to calculate enemies' stats
        bool turnEnd = false;

        bool spawningEnemies = false;

        //game objects

        public static List<Creature> enemies = new List<Creature>(); //list of enemies
        public static Rzuf rzuf; //player
        Random losu = new Random(); //random bullshit generator
        Soldier closestEnemy; //it is temporary Soldier object to make easier for rzuf to shoot to

         public Sprite background = new Sprite(); //spaaaaaaaace

        static public List<Sound> sounds = new List<Sound>(); //list of current sounds because SFML momento

        //functions
        public bool Running() //checking if window is open
        {
            return this.window.IsOpen;
        }
        public void HandleClose(object sender, EventArgs e) //closing the window
            {
                this.window.Close();
                
            }
        //creating objects
        public void SpawnPlayer(int _maxHP, int _damage, int _attackDelay, int _maxAmmo) //spawns rzuf object
        {   
            //creates rzuf object
            rzuf = new Rzuf(_maxHP, _damage, _attackDelay, _maxAmmo);
            rzuf.position = new SFML.System.Vector2f(this.videoMode.Width/2-50,this.videoMode.Height/2-50);
            TextureLibrary.SetSprite("rzuf", rzuf);
            rzuf.sprite.Position = rzuf.position;
        }
        public async void SpawnEnemies(int number, int chanceSoldier, int chanceTurret, int chanceArmoredSoldier, int chanceAngrySoldier)
        {   
            /*
            randomly fills enemies list with random enemy types and sets their sprites
            first parameter: number of enemies
            second parameter: chance for spawning Soldier (in %)
            third parameter: chance for spawning Turret (in %)
            forth parameter: chance for spawning Armored Soldier (in %)
            fifth parameter: chance for spawning Angry Soldier (in %)
            */
            spawningEnemies = true; //without this when first soldier is spawned and killed before spawning next, the next turn was starting
            if(turnEnd==true)
            {
                int losulosu; 
                for(int i = 0; i<number; i++)
                {   
                    losulosu = losu.Next(0,100); //gets random number from 0 to 99
                    if(losulosu>=0&&losulosu<chanceSoldier) 
                    { //spawns basic soldier and waits 0.5sec
                        Soldier soldier = new Soldier(turn, width, height);
                        TextureLibrary.SetSprite("soldier",soldier);
                        enemies.Add(soldier);
                        enemyCount++;
                        await Task.Delay(500);
                    }
                    if(losulosu>=chanceSoldier&&losulosu<chanceSoldier+chanceTurret)
                    {  //spawns turret and waits 0.5sec
                        Soldier soldier = new Turret(turn, width, height);
                        TextureLibrary.SetSprite("turret",soldier);
                        enemies.Add(soldier);
                        enemyCount++;
                        await Task.Delay(500);
                    }
                    if(losulosu>=chanceSoldier+chanceTurret&&losulosu<chanceSoldier+chanceTurret+chanceArmoredSoldier)
                    {  //spawns armored soldier and waits 0.5sec
                        Soldier soldier = new ArmoredSoldier(turn, width, height);
                        TextureLibrary.SetSprite("armored soldier",soldier);
                        enemies.Add(soldier);
                        enemyCount++;
                        await Task.Delay(500);
                    }
                    if(losulosu>=chanceSoldier+chanceTurret+chanceArmoredSoldier&&losulosu<=100)
                    {  //spawns angry soldier and waits 0.5sec
                        Soldier soldier = new AngrySoldier(turn, width, height);
                        TextureLibrary.SetSprite("angry soldier",soldier);
                        enemies.Add(soldier);
                        enemyCount++;
                        await Task.Delay(500);  

                    }
                }
            }
            spawningEnemies = false;
        }
        public void StartTurn() //starts next turn
        {
            if(turnEnd == true && rzuf.alive == true) 
            {
                turn++;
                SpawnEnemies(turn*5,25,25,25,25); //number of enemies, chance for soldier, turret, armored, angry
                turnEnd = false;
            }
        }
        //game logic
        void UpdatePlayer()
        { 
            foreach(Soldier soldier in enemies) //checks which enemy is closest to rzuf 
            {
                if(soldier.distance<closestEnemyDistance)
                {
                closestEnemyDistance = soldier.distance;
                closestEnemy = soldier;
                }
            }
            if(enemyCount!=0 &&rzuf.alive == true) //so rzuf cannot shot when there's no enemies on screen
            rzuf.Act(closestEnemy); //shoots closest enemy
            if(enemyCount ==0 && spawningEnemies == false)
                turnEnd = true; //rzuf is always on screen, so he can control if all enemies are dead and next turn can be started
        }
        void UpdateEnemies()
        {
                //moving enemies
            foreach(Soldier soldier in enemies.ToList()) 
            {   if(soldier.alive == true && rzuf.alive == true) //hmmm not the intended way, but when rzuf is dead all the enemies are cleared
                    soldier.Act(rzuf);
                else    //deletes enemy from list when they're dead
                {
                    enemies.Remove(soldier);
                    enemyCount--;
                    closestEnemyDistance = 9999;
                }
            }
        }
        void UpdateSounds() //sound in SFML are stupid, so this function deletes all stopped sounds so computer not explode XD
        {  SoundStatus status;
            foreach(Sound sound in sounds.ToList())
            {   
                status = sound.Status;
                if(status==SoundStatus.Stopped)
                {
                    sounds.Remove(sound);
                }
            }
        }
        public void Update() //updates logic of game every frame
        {
            this.UpdateSounds();
            this.UpdateEnemies();
            this.UpdatePlayer();
        }
        //game rendering
        public void SetBackground(string _type) 
        {
            TextureLibrary.SetTexture(_type,background);
        }
        void RenderWorld() //draws space background
        {
            this.window.Draw(background);
        }
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

            this.RenderWorld();
            this.RenderEnemies();
            this.RenderPlayer();

            this.window.Display();
        }
        //constructors
        public Controller(uint width, uint height, uint fps)
        {
            this.videoMode.Width = width;
            this.videoMode.Height = height;
            this.width = checked((int)width);
            this.height = checked((int)height);
            this.frameRate = fps;
            this.window = new RenderWindow(this.videoMode,"Rzuf!");
            this.window.SetFramerateLimit(frameRate);
        }
    }


    
}