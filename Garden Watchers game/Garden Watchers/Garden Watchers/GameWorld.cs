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
        private static List<GameObject> gameObjects;
        private static List<GameObject> removedObjects;
        private static List<GameObject> addedObjects;
        private static Vector2 screenSize;
        private static Vector2 player;

        //Properties
        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        public static List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        public static List<GameObject> RemovedObjects { get => removedObjects; set => removedObjects = value; }
        public static List<GameObject> AddedObjects { get => addedObjects; set => addedObjects = value; }

        public static Vector2 PlayerCharacterPosition { get => player; set => player = value; }

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
            Vector2 playerPosition = new Vector2(ScreenSize.X / 2, ScreenSize.Y);
            PlayerCharacterPosition = playerPosition;
            GameObject player = new Player(playerPosition, 200);
            GameObject tempObstacle = new Obstacle(new Vector2(200,200));
            GameObjects = new List<GameObject>() { player, tempObstacle };


            RemovedObjects = new List<GameObject>();
            AddedObjects = new List<GameObject>();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

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




            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();


            foreach (GameObject gameObject in GameObjects)
            {
                gameObject.Draw(_spriteBatch);
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
