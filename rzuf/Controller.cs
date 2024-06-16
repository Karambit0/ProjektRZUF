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
        public RenderWindow window; 
        VideoMode videoMode; //the resolution
        uint frameRate;

        //game variables
        public static int width,height,currentFrameRate; //width and height of window for easier use
        int turn = 0; //number of turns to calculate enemies' stats
        bool turnEnd = false; //has turn ended?
        bool gamePaused; //is game paused?
        double waitTime; //used to store calculated conversion between miliseconds and frames
        bool spawningEnemies = false; //is controller still spawning enemies?
        public int enemyCount = 0; //how many enemies are on screen
        public int enemiesToKill; //how many enemies in turn are yet to be killed

        //game parameteres
        int chanceSoldier, chanceTurret, chanceArmoredSoldier, chanceAngrySoldier; //chances for each enemy
        int baseWave; //how large base wave is
        double xpMultiplayer; //how much xp rzuf will get

        //game objects

        public static List<Creature> enemies = new List<Creature>(); //list of enemies
        public Rzuf rzuf; //player
        Random losu = new Random(); //random number generator
        Soldier closestEnemy; //it is temporary Soldier object to make easier for rzuf to shoot to
        float closestEnemyDistance = 9999; //easier access to distance of the closest enemy for rzuf
        public Sprite background = new Sprite(); //background picture
        static public List<Sound> sounds = new List<Sound>(); //list of current sounds because SFML moment
        static public List<Music> music= new List<Music>(); //list of current music because SFML moment

        static public List<Text> gui = new List<Text>(); //list of strings of gui
        static public List<Button> buttons = new List<Button>(); //list of all buttons
        Timer timer = new Timer(); //time counter from the start of the game

        //general functions

        //checking if window is open
        public bool Running() 
        {
            return window.IsOpen;
        }
        //getting parameters from user to controller
        public void SetEnemySpawnRate(int _enemyCount, double _xpMultiplayer, int _soldier, int _turret, int _angry, int _armored)
        {
         baseWave = _enemyCount;
         xpMultiplayer = _xpMultiplayer;        
         chanceSoldier = _soldier;
         chanceTurret = _turret;
         chanceAngrySoldier = _angry;
         chanceArmoredSoldier = _armored;   
        }
        //sets background texture
        public void SetBackground(string _type) 
        {
            TextureLibrary.SetTexture(_type,background);
        }
        //closing the window when user wants it
        public void HandleClose(object sender, EventArgs e) 
            {
                window.Close(); 
            }
        //handling all of the mouse input
        public void HandleMouseEvents(object sender, MouseButtonEventArgs e)
        {   
            if(e.Button == 0) //handling left click
                foreach(Button button in buttons)
                {  
                    //clicking pause button    
                    if(button.IsMouseOver(window)==true&&button.function =="pause")
                    {
                        if(button.status ==0)
                        {
                            gamePaused = true;
                            TextureLibrary.SetShapeTexture("play",button.button);
                            button.status=1;
                            break;
                        }
                            else if(button.status ==1)
                        {
                            gamePaused = false;
                            TextureLibrary.SetShapeTexture("pause",button.button);
                            button.status =0;
                            break;
                        } 
                    }
                    //clicking speed button
                    if(button.IsMouseOver(window)==true&&button.function =="speed")
                    {
                        if(button.status ==0)
                        {
                            window.SetFramerateLimit((uint)currentFrameRate*2);
                            TextureLibrary.SetShapeTexture("x2",button.button);
                            button.status=1;
                            break;
                        }
                            else if(button.status ==1)
                        {
                            window.SetFramerateLimit((uint)currentFrameRate*3);
                            TextureLibrary.SetShapeTexture("x3",button.button);
                            button.status =2;
                            break;
                        } 
                           else if(button.status ==2)
                        {
                            window.SetFramerateLimit((uint)currentFrameRate);
                            TextureLibrary.SetShapeTexture("speed",button.button);
                            button.status =0;
                            break;
                        } 
                    }
                }
        }
        //creating objects

        //creates rzuf object
        public void SpawnPlayer(int _maxHP, int _damage, int _attackDelay, int _maxAmmo, double _heal) 
        {   
            rzuf = new Rzuf(_maxHP, _damage, _attackDelay, _maxAmmo, _heal);
            rzuf.position = new Vector2f(videoMode.Width/2-50,videoMode.Height/2-50);
            TextureLibrary.SetSprite("rzuf", rzuf);
            rzuf.sprite.Position = rzuf.position;
            rzuf.gun.position = new Vector2f(videoMode.Width/2,videoMode.Height/2+25);
            TextureLibrary.SetTexture("gun", rzuf.gun.sprite);
            rzuf.gun.sprite.Position = rzuf.gun.position;
            rzuf.gun.sprite.Origin = new Vector2f(25f,25f);
        }
        //creates all of the text of GUI
        public void CreateGui()
        {
            TextLibrary.WriteText("Tura: "+turn,width/2-200,height-50,gui); //current turn
            TextLibrary.WriteText("Ilość wrogów w fali: "+enemiesToKill,width/2-50,height-50,gui); //number of enemies to end turn
            TextLibrary.WriteText("HP Rzuf: "+rzuf.currentHP+"/"+rzuf.maxHP,width/2-100,50,gui); //current hp of rzuf
            TextLibrary.WriteText("Amunicja Rzuf: "+rzuf.gun.currentAmmo+"/"+rzuf.gun.maxAmmo,width/2+50,90,gui); //current ammo of rzuf
            TextLibrary.WriteText("Poziom Rzuf: "+rzuf.lv,width/2-200,10,gui); //current rzuf level
            TextLibrary.WriteText("DMG Rzuf: "+rzuf.gun.damage,width/2-200,90,gui); //current damage of rzuf
            TextLibrary.WriteText("Czas: "+timer.minutes+":"+timer.seconds,width/2+50,10,gui); //current damage of rzuf
            Button pause = new Button("pause",50,50,20,20); //pause button
            Button speed = new Button("speed",50,50,120,20); //speed the simulation button

        }
         /*
            randomly fills enemies list with random enemy types and sets their sprites
            first parameter: number of enemies
            second parameter: chance for spawning Soldier (in %)
            third parameter: chance for spawning Turret (in %)
            fourth parameter: chance for spawning Armored Soldier (in %)
            fifth parameter: chance for spawning Angry Soldier (in %)
            */
        public async void SpawnEnemies(int number, int _chanceSoldier, int _chanceTurret, int _chanceArmoredSoldier, int _chanceAngrySoldier, double _xpMultiplayer)
        {   
            spawningEnemies = true; 
            if(turnEnd==true&&gamePaused==false)
            {
                int losulosu, ii=0; 
                for(int i = ii; i<number; i++)
                {   if(gamePaused==true)
                    {
                        i = ii-1;
                        continue;
                    }
                    losulosu = losu.Next(0,100); //gets random number from 0 to 99
                    if(losulosu>=0&&losulosu<_chanceSoldier) 
                    { //spawns basic soldier
                        Soldier soldier = new Soldier(turn, width, height, _xpMultiplayer);
                        TextureLibrary.SetSprite("soldier",soldier);
                        enemies.Add(soldier);
                    }
                    if(losulosu>=_chanceSoldier&&losulosu<_chanceSoldier+_chanceTurret)
                    {  //spawns turret
                        Soldier soldier = new Turret(turn, width, height, _xpMultiplayer);
                        TextureLibrary.SetSprite("turret",soldier);
                        enemies.Add(soldier);
                    }
                    if(losulosu>=_chanceSoldier+_chanceTurret&&losulosu<_chanceSoldier+_chanceTurret+_chanceArmoredSoldier)
                    {  //spawns armored
                        Soldier soldier = new ArmoredSoldier(turn, width, height, _xpMultiplayer);
                        TextureLibrary.SetSprite("armored soldier",soldier);
                        enemies.Add(soldier);
                    }
                    if(losulosu>=_chanceSoldier+_chanceTurret+_chanceArmoredSoldier&&losulosu<=100)
                    {  //spawns angry soldier
                        Soldier soldier = new AngrySoldier(turn, width, height, _xpMultiplayer);
                        TextureLibrary.SetSprite("angry soldier",soldier);
                        enemies.Add(soldier);

                    }
                    ii++;
                    enemyCount++;
                    waitTime = (1000.0/currentFrameRate*30);
                    await Task.Delay((int)waitTime);
                }
            }
            spawningEnemies = false;
        }
        //starts next turn when time comes
        public void StartTurn() 
        {   
            if(turnEnd == true && rzuf.alive == true) 
            {   
                turn++;
                enemiesToKill = turn*baseWave;
                SpawnEnemies(enemiesToKill,chanceSoldier,chanceTurret,chanceArmoredSoldier,chanceAngrySoldier,xpMultiplayer);
                rzuf.LvUp();
                rzuf.Heal(rzuf.heal);
                turnEnd = false;
            }
        }
        //game logic

        //gets closest enemy and shoots them and sets flag to end a turn
        void UpdatePlayer()
        { 
            //checks which enemy is closest to rzuf 
            foreach(Soldier soldier in enemies) 
            {
                if(soldier.distance<closestEnemyDistance)
                {
                closestEnemyDistance = soldier.distance;
                closestEnemy = soldier;
                }
            }
            if(enemyCount!=0 &&rzuf.alive == true) 
            {rzuf.Act(closestEnemy); 
            float degrees = Utility.GetAngle(closestEnemy.position,rzuf.gun.position);
            rzuf.gun.sprite.Rotation = degrees;
            }
            if(enemyCount ==0 && spawningEnemies == false)
                turnEnd = true; 
            //rzuf is always on screen, so he can control if all enemies are dead and next turn can be started
        }
        //moves enemies, makes them shoot and removes them when they're dead
        void UpdateEnemies()
        {
                //moving enemies
            foreach(Soldier soldier in enemies.ToList()) 
            {   
                //hp bar is always under an enemy and is as long as its hp
                soldier.hpBar.Position = new Vector2f(soldier.position.X,soldier.position.Y+110);
                soldier.hpBar.Size = new Vector2f((float)(soldier.currentHP/soldier.maxHP*100),10);

                if(soldier.alive == true && rzuf.alive == true) 
                    soldier.Act(rzuf);
                else    //deletes enemy from list when they're dead
                {
                    enemies.Remove(soldier);
                    enemiesToKill--;
                    enemyCount--;
                    closestEnemyDistance = 9999;
                }
            }
        }
        //sound in SFML are stupid, so this function deletes all stopped sounds so computer not explode
        void UpdateSounds() 
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
        //checks values of all measured parameteres and displays them on screen perodically
        void UpdateGui()
        {
            foreach(Text line in gui)
            {
                if(line.DisplayedString.Contains("Tura")==true)
                    line.DisplayedString = "Tura: "+turn;
                if(line.DisplayedString.Contains("wrogów")==true)
                    line.DisplayedString = "Ilość wrogów w fali: "+enemiesToKill;
                if(line.DisplayedString.Contains("HP")==true)
                { 
                    if(rzuf.currentHP>=0)
                    line.DisplayedString = "HP Rzuf: "+rzuf.currentHP+"/"+rzuf.maxHP;
                    else
                    line.DisplayedString = "Rzuf is dead :( ";
                }
                if(line.DisplayedString.Contains("Amunicja")==true)
                {   
                    if(rzuf.gun.isReloading==false)
                    line.DisplayedString = "Amunicja Rzuf: "+rzuf.gun.currentAmmo+"/"+rzuf.gun.maxAmmo;
                    else
                    line.DisplayedString = "Amunicja Rzuf: przeładowuje";
                }
                if(line.DisplayedString.Contains("Poziom")==true)
                    line.DisplayedString = "Poziom rzuf: "+rzuf.lv;
                if(line.DisplayedString.Contains("DMG")==true)
                    line.DisplayedString = "DMG rzuf: "+rzuf.gun.damage;
                if(line.DisplayedString.Contains("Czas")==true &&rzuf.alive==true)
                    line.DisplayedString = "Czas: "+timer.minutes+":"+timer.seconds;
            }
        }
        //updates logic of game every frame
        public void Update() 
        {
            if(gamePaused==false)
            {
                StartTurn(); 
                timer.CountTime();
                UpdateSounds();
                UpdateGui();
                UpdateEnemies();
                UpdatePlayer();
            }

        }
        //game rendering

        //draws background
        void RenderWorld() 
        {
            window.Draw(background);
        }
        //draws player sprite
        void RenderPlayer() 
        {
            window.Draw(rzuf.sprite);
            window.Draw(rzuf.gun.sprite);
        }
        //draws enemies' sprites
        void RenderEnemies() 
        {  
            foreach(Soldier soldier in enemies.ToList())
           {
            window.Draw(soldier.sprite);
            window.Draw(soldier.hpBar);
           }
        }
        //draws gui text
        void RenderGui() 
        {  
            foreach(Text line in gui)
           {
            window.Draw(line);
           }
           foreach(Button button in buttons)
           {
            window.Draw(button.button);
           }
        }
        // renders all the game objects
        public void Render() 
        {
            //clears old frame
            window.Clear();

            //renders objects
            RenderWorld();
            RenderGui();
            RenderEnemies();
            RenderPlayer();

            //displays frame in window
            window.Display();
        }
        //constructors
        public Controller(uint _width, uint _height, uint _fps)
        {
            videoMode.Width = _width;
            videoMode.Height = _height;
            width = checked((int)_width);
            height = checked((int)_height);
            frameRate = _fps;
            currentFrameRate = (int)_fps;
            window = new RenderWindow(videoMode,"Symulacja R.Z.U.F");
            window.SetFramerateLimit(frameRate);
        }
    }
}