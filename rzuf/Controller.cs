using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;
using System.Runtime.Versioning;
using System.Net.Mail;
using System.Threading.Channels;

namespace Sim
{
    class Controller
    {   //vairables for simulation output
        public static Type killer;

        //variables for window
        public RenderWindow window; //de window
        VideoMode videoMode; //de resolution
        static uint frameRate; //de framerate

        //game variables
        public int width,height, framerate; //width and height of window for easier use
        public int enemyCount = 0; //how many enemies are on screen

        public int enemiesToKill=1;
        float closestEnemyDistance = 9999; 
        int turn = 0; //number of turns to calculate enemies' stats
        bool turnEnd = false;
        double waitTime;

        int countSoldier=0,countTurret=0,countAngry=0, countArmor=0;
        
        Timer timer = new Timer();

        bool spawningEnemies = false; //flag for accesing enemies list, otherwise spawn enemies and update can crash simulation

        bool enemiesAcces = true; 

        //game objects

        public List<Creature> enemies = new List<Creature>(); //list of enemies

        public Rzuf rzuf; //player
        Random losu = new Random(); //random bullshit generator
        Soldier closestEnemy; //it is temporary Soldier object to make easier for rzuf to shoot to

         public Sprite background = new Sprite(); //spaaaaaaaace

         public List<Sound> sounds = new List<Sound>(); //list of current sounds because SFML momento

         public List<Text> gui = new List<Text>(); //list of strings of gui



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
        public void SpawnPlayer(int _maxHP, int _damage, int _attackDelay, int _maxAmmo, float _heal) //spawns rzuf object
        {   
            //creates rzuf object
            rzuf = new Rzuf(_maxHP, _damage, _attackDelay, _maxAmmo, _heal);
            rzuf.position = new Vector2f(this.videoMode.Width/2-50,this.videoMode.Height/2-50);
            TextureLibrary.SetSprite("rzuf", rzuf);
            rzuf.sprite.Position = rzuf.position;
            rzuf.gun.position = new Vector2f(this.videoMode.Width/2,this.videoMode.Height/2+25);
            TextureLibrary.SetTexture("gun", rzuf.gun.sprite);
            rzuf.gun.sprite.Position = rzuf.gun.position;
            rzuf.gun.sprite.Origin = new Vector2f(25f,25f);
        }
        public void CreateGui()
        {
            TextLibrary.WriteText("Tura: "+turn,width/2-200,10,gui); //turn
            TextLibrary.WriteText("Ilość wrogów w fali: "+enemiesToKill,width/2-50,10,gui); //number of enemies to end turn
            TextLibrary.WriteText("HP Rzuf: "+rzuf.currentHP+"/"+rzuf.maxHP,width/2-300,50,gui); //current hp of rzuf
            TextLibrary.WriteText("Amunicja Rzuf: "+rzuf.gun.currentAmmo+"/"+rzuf.gun.maxAmmo,width/2,50,gui); //current ammo of rzuf
            TextLibrary.WriteText("Poziom Rzuf: "+rzuf.lv,width/2-250,90,gui); //current rzuf level
            TextLibrary.WriteText("DMG Rzuf: "+rzuf.gun.damage,width/2,90,gui); //current damage of rzuf
            TextLibrary.WriteText("Czas: "+timer.minutes+":"+timer.seconds,width/2-100,130,gui); //current damage of rzuf
            TextLibrary.WriteText("Soldier: "+countSoldier,width/2,height-50,gui); //soldiers spawned
            TextLibrary.WriteText("Angry: "+countAngry,width/2,height-90,gui); //angry soldiers spawned
            TextLibrary.WriteText("Armored: "+countArmor,width/2,height-130,gui); //armored soldiers spawned
            TextLibrary.WriteText("Turret: "+countTurret,width/2,height-170,gui); //turrets spawned

        }
        public async void SpawnEnemies(int number, int chanceSoldier, int chanceTurret, int chanceArmoredSoldier, int chanceAngrySoldier, float _xpMultiplayer)
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
                    if(enemiesAcces == false)
                        await Task.Delay(10);
                    else {
                    enemiesAcces = false;
                

                    losulosu = losu.Next(0,100); //gets random number from 0 to 99
                    if(losulosu>=0&&losulosu<chanceSoldier) 
                    { 


                        Soldier soldier = new Soldier(turn, width, height, _xpMultiplayer);
                        TextureLibrary.SetSprite("soldier",soldier);
                        enemies.Add(soldier);
                        countSoldier++;
                    }
                    if(losulosu>=chanceSoldier&&losulosu<chanceSoldier+chanceTurret)
                    {  


                        Soldier soldier = new Turret(turn, width, height, _xpMultiplayer);
                        TextureLibrary.SetSprite("turret",soldier);
                        enemies.Add(soldier);
                        countTurret++;
                    }
                    if(losulosu>=chanceSoldier+chanceTurret&&losulosu<chanceSoldier+chanceTurret+chanceArmoredSoldier)
                    {  


                        Soldier soldier = new ArmoredSoldier(turn, width, height, _xpMultiplayer);
                        TextureLibrary.SetSprite("armored soldier",soldier);
                        enemies.Add(soldier);
                        countArmor++;
                    }
                    if(losulosu>=chanceSoldier+chanceTurret+chanceArmoredSoldier&&losulosu<=100)
                    {  
                        
                        Soldier soldier = new AngrySoldier(turn, width, height, _xpMultiplayer);
                        TextureLibrary.SetSprite("angry soldier",soldier);
                        enemies.Add(soldier);
                        countAngry++;

                    }
                    enemiesAcces = true;                   

                    enemyCount++;
                    waitTime = (1000.0/framerate*30);
                    await Task.Delay((int)waitTime); //maybe pass time between enemies spawning as argument?

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
                enemiesToKill = turn*4;
                SpawnEnemies(enemiesToKill,25,25,25,25,1.0F); //number of enemies, chance for soldier, turret, armored, angry
                rzuf.LvUp();
                rzuf.Heal(rzuf.heal);
                turnEnd = false;
            }
        }
        //game logic
        async void UpdatePlayer()
        {   
            if(enemiesAcces == false)
                        await Task.Delay(10);
            else {
                enemiesAcces = false;
            foreach(Soldier soldier in enemies.ToList()) //checks which enemy is closest to rzuf 
            {
                if(soldier!=null)
                {
                    if(soldier.distance<closestEnemyDistance)
                    {
                    closestEnemyDistance = soldier.distance;
                    closestEnemy = soldier;
                    }
                }
            }
                enemiesAcces = true;
            }

            if(enemyCount!=0 &&rzuf.alive == true) //so rzuf cannot shot when there's no enemies on screen
            {rzuf.Act(closestEnemy); //shoots closest enemy
            float degrees = Utility.GetAngle(closestEnemy.position,rzuf.gun.position);
            rzuf.gun.sprite.Rotation = degrees;
            }
            if(enemyCount ==0 && spawningEnemies == false)
                turnEnd = true; //rzuf is always on screen, so he can control if all enemies are dead and next turn can be started
        }
        async void UpdateEnemies()
        {
             if(enemiesAcces == false)
                        await Task.Delay(10);
            else {

                enemiesAcces = false;
                //moving enemies
            foreach(Soldier soldier in enemies.ToList()) 
            {   if(soldier!=null)
                {//hp bar is always under an enemy and is as long as its hp
                //soldier.hpBar.Position = new Vector2f(soldier.position.X,soldier.position.Y+110);
                //soldier.hpBar.Size = new Vector2f((float)(soldier.currentHP/soldier.maxHP*100),10);
                if(soldier.alive == true && rzuf.alive == true&&soldier!=null) //hmmm not the intended way, but when rzuf is dead all the enemies are cleared
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
            enemiesAcces = true;
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
                if(line.DisplayedString.Contains("Soldier")==true &&rzuf.alive==true)
                    line.DisplayedString = "Soldier: "+countSoldier;
                if(line.DisplayedString.Contains("Angry")==true &&rzuf.alive==true)
                    line.DisplayedString = "Angry: "+countAngry;
                if(line.DisplayedString.Contains("Armored")==true &&rzuf.alive==true)
                    line.DisplayedString = "Armored: "+countArmor;
                if(line.DisplayedString.Contains("Turret")==true &&rzuf.alive==true)
                    line.DisplayedString = "Turret: "+countTurret;
            }
        }
        public void Update() //updates logic of game every frame
        {
            timer.StartTimer();
            UpdateSounds();
            UpdateGui();
            UpdateEnemies();
            UpdatePlayer();

            if(rzuf.alive==false || timer.minutes >= 20)
            {   
                System.Console.WriteLine("Time: " + timer + " ; turn: " + turn+ " ; lv: "+ rzuf.lv + " ; killer: "+killer );
                  string docPath =
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "test.txt"), true))
                    outputFile.WriteLine(timer + " ; " + turn+ " ; "+ rzuf.lv + " ; "+killer);
                //for some reson tnew instances of controler keeps enemy list?    
                enemies.Clear();
                this.window.Close();

            }

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
            this.window.Draw(rzuf.gun.sprite);
        }
        async void RenderEnemies() //draws enemies sprites
        {  
            if(enemiesAcces == false)
                        await Task.Delay(10);
            else {
                enemiesAcces = false;

            foreach(Soldier soldier in enemies.ToList())
           {
            window.Draw(soldier.sprite);
            window.Draw(soldier.hpBar);
           }
            enemiesAcces = true;
            }
        }
        void RenderGui() //draws enemies sprites
        {  
            foreach(Text line in gui)
           {
            this.window.Draw(line);
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
            this.RenderGui();
            this.RenderEnemies();
            this.RenderPlayer();

            this.window.Display();
        }
        //constructors
        public Controller(uint _width, uint _height, uint _fps)
        {   
            

            videoMode.Width = _width;
            videoMode.Height = _height;
            width = checked((int)_width);
            height = checked((int)_height);
            frameRate = _fps;
            framerate = checked((int)_fps);
            window = new RenderWindow(videoMode,"Rzuf!");
            window.SetFramerateLimit(frameRate);

            killer = typeof(Time);
        }
    }


    
}