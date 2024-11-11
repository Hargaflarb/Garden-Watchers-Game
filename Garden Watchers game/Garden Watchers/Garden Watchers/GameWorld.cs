using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Character-Implementation---Echo
        private Player player;
        private Texture2D background;
        private bool isAlive;

        private static Vector2 playerLocation;

        //Properties
        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        public static List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        public static List<GameObject> RemovedObjects { get => removedObjects; set => removedObjects = value; }
        public static List<GameObject> AddedObjects { get => addedObjects; set => addedObjects = value; }

        public static Vector2 PlayerCharacterPosition { get => playerLocation; set => playerLocation = value; }
        public static GameWorld TheGameWorld { get; set; }
        public static Player Player { get => player; private set => player = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }
 
#if DEBUG
        private Texture2D hitboxPixel;
#endif



        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            TheGameWorld = this;
            IsAlive = true;
            Map.ResetMap();

            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            Vector2 playerPosition = new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2);
            Player = new Player(10, playerPosition, 500);
            GameObjects = new List<GameObject>() { Player };



            GameObject gnome = new Gnome(6, new Vector2(50,50), 250);
            GameObjects.Add(gnome);

            GameObject flamingo = new Flamingo(3, new Vector2(25, 25), 200);
            GameObjects.Add(flamingo);

            GameObject fairy = new Fairy(2, new Vector2(1000, 900), 150);
            gameObjects.Add(fairy);
            RemovedObjects = new List<GameObject>();
            AddedObjects = new List<GameObject>();
            Map.GoToRoom(0,0, Direction.None, false);
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

            KeyboardState keyState = Keyboard.GetState();
            if (IsAlive == false && keyState.IsKeyDown(Keys.Space))
            {
                GameObjects.Clear();
                Initialize();
            }



            base.Update(gameTime);
        }

        public static void KillObject(GameObject gameObject)
        {
            removedObjects.Add(gameObject);
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
