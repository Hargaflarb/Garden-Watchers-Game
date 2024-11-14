using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Garden_Watchers
{
    public class GameWorld : Game
    {
        //Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont textFont;
        private static List<GameObject> gameObjects;
        private static List<GameObject> removedObjects;
        private static List<GameObject> addedObjects;
        private static Vector2 screenSize;
        private static Player player;
        private static Texture2D background;
        private static Random random;
        private bool isAlive;
        private static bool winning;

        private static Vector2 playerLocation;

        //Properties
        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        public static List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        public static List<GameObject> RemovedObjects { get => removedObjects; set => removedObjects = value; }
        public static List<GameObject> AddedObjects { get => addedObjects; set => addedObjects = value; }

        public static Vector2 PlayerCharacterPosition { get => playerLocation; set => playerLocation = value; }
        public static GameWorld TheGameWorld { get; set; }
        public static Random Random { get => random; private set => random = value; }
        public static Player Player { get => player; private set => player = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }
        public static bool Winning { get => winning; set => winning = value; }

#if DEBUG
        private Texture2D hitboxPixel;
#endif



        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        static GameWorld()
        {
            Random = new Random();
        }

        private bool GetGnomeBoss(out GameObject gnomeBossClass)
        {
            foreach (GameObject gameObject in GameObjects)
            {
                if (gameObject is GnomeBoss)
                {
                    gnomeBossClass = gameObject;
                    return true;
                }
            }

            gnomeBossClass = null;
            return false;
        }

        protected override void Initialize()
        {
            TheGameWorld = this;
            IsAlive = true;
            Winning = false;
            Map.ResetMap();

            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            Vector2 playerPosition = new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2);
            Player = new Player(10, playerPosition, 500);


            GameObjects = new List<GameObject>() { Player };
            RemovedObjects = new List<GameObject>();
            AddedObjects = new List<GameObject>();
            Map.GoToRoom(0, 0, Direction.None, false);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textFont = Content.Load<SpriteFont>("File");
            background = Content.Load<Texture2D>("dirt");


            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.LoadContent(Content);
            }

#if DEBUG
            //loads the hitbox sprite.
            hitboxPixel = Content.Load<Texture2D>("Hitbox pixel");
#endif

        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // game object update
            Vector2 screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Update(gameTime, screenSize);

                foreach (GameObject other in GameObjects)
                {
                    gameObject.CheckCollision(other);
                }
            }

            // remove game objects
            foreach (GameObject removedObject in RemovedObjects)
            {
                GameObjects.Remove(removedObject);
            }
            RemovedObjects.Clear();

            // add game objects
            GameObjects.AddRange(AddedObjects);
            AddedObjects.Clear();

            // lose thing
            KeyboardState keyState = Keyboard.GetState();
            if (IsAlive == false && keyState.IsKeyDown(Keys.Space))
            {
                GameOver();
                Initialize();
            }

            //Checking for Victory conditions
            if (Winning == true)
            {
                YouWon();
                if (keyState.IsKeyDown(Keys.Space))
                {
                    Initialize();
                }
            }



            base.Update(gameTime);
        }



        public static int GetNumberOfEnemies()
        {
            int output = 0;
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is Enemy)
                {
                    output++;
                }
            }
            return output;
        }


        public static void YouWon()
        {
            GameObjects.Clear();
        }

        public static void GameOver()
        {
            GameObjects.Clear();
        }

        public static void KillObject(GameObject gameObject)
        {
            removedObjects.Add(gameObject);
            if (gameObject is Enemy)
            {
                if (GetNumberOfEnemies() == 1)
                {
                    HealthRecovery health = new HealthRecovery(new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2));
                    MakeObject(health);
                }
            }
        }

        public static void KillAllObjects()
        {
            foreach (GameObject theObject in gameObjects)
            {
                if (!(theObject is Player))
                {
                    removedObjects.Add(theObject);
                }
            }
        }

        public static void AddObjects(List<GameObject> gameObjects)
        {
            addedObjects.AddRange(gameObjects);
        }

        public static void MakeObject(GameObject gameObject)
        {
            gameObject.LoadContent(TheGameWorld.Content);
            addedObjects.Add(gameObject);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();



            _spriteBatch.Draw(background, new Rectangle(0, 0, (int)ScreenSize.X, (int)ScreenSize.Y), Color.Orange);

            

            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }


            // is UI so do after other stuff.
            _spriteBatch.DrawString(textFont, "Health: " + player.Health, new Vector2(10, 5), Color.Red);
            _spriteBatch.DrawString(textFont, "Bullet: " + player.Bullets, new Vector2(10, 40), Color.Red);

            if (Player.UsingGun)
            {
                _spriteBatch.DrawString(textFont, "Current Weapon: Shotgun", new Vector2(10, 75), Color.Red);
            }
            else
            {
                _spriteBatch.DrawString(textFont, "Current Weapon: Chainsaw", new Vector2(10, 75), Color.Red);
            }

            if (Map.RoomCount == 7)
            {
                if (GetGnomeBoss(out GameObject gameObject))
                {
                    _spriteBatch.DrawString(textFont, "BOSS HEALTH: " + ((GnomeBoss)gameObject).Health, new Vector2(((GnomeBoss)gameObject).Position.X-150, ((GnomeBoss)gameObject).Position.Y-250), Color.Gold);

                }
            }

            if (!IsAlive)
            {
                _spriteBatch.DrawString(textFont, "GAME OVER\nPRESS SPACE BAR TO RETRY", new Vector2(ScreenSize.X/2 - 200, ScreenSize.Y/2), Color.Gold);
            }

            if (Winning)
            {
                _spriteBatch.DrawString(textFont, "YOU WIN!\nPRESS SPACE BAR TO RESTART", new Vector2(ScreenSize.X / 2 - 200, ScreenSize.Y / 2), Color.Gold);
            }


#if DEBUG
            // draw the hitbox and position of every gameObject
            foreach (GameObject gameObject in GameObjects)
            {
                Rectangle hitBox = gameObject.Hitbox;
                Rectangle topline = new Rectangle(hitBox.X, hitBox.Y, hitBox.Width, 1);
                Rectangle bottomline = new Rectangle(hitBox.X, hitBox.Y + hitBox.Height, hitBox.Width, 1);
                Rectangle rightline = new Rectangle(hitBox.X + hitBox.Width, hitBox.Y, 1, hitBox.Height);
                Rectangle leftline = new Rectangle(hitBox.X, hitBox.Y, 1, hitBox.Height);

                _spriteBatch.Draw(hitboxPixel, topline, null, Color.White);
                _spriteBatch.Draw(hitboxPixel, bottomline, null, Color.White);
                _spriteBatch.Draw(hitboxPixel, rightline, null, Color.White);
                _spriteBatch.Draw(hitboxPixel, leftline, null, Color.White);

                Vector2 position = gameObject.Position;
                Rectangle centerDot = new Rectangle((int)position.X - 1, (int)position.Y - 1, 3, 3);

                _spriteBatch.Draw(hitboxPixel, centerDot, null, Color.White);
            }
#endif



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
