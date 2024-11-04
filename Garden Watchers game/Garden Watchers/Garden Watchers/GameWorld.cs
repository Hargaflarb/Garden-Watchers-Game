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
        private List<GameObject> gameObjects;
        private List<GameObject> removedObjects;
        private List<GameObject> addedObjects;
        private static Vector2 screenSize;

        //Properties
        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Vector2 playerPosition = new Vector2(ScreenSize.X / 2, ScreenSize.Y);
            GameObject player = new Player(playerPosition, 200);
            gameObjects = new List<GameObject>() { player };
            removedObjects = new List<GameObject>();
            addedObjects = new List<GameObject>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // game object update
            Vector2 screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime, screenSize);
            }

            // remove game objects
            foreach (GameObject removedObject in removedObjects)
            {
                gameObjects.Remove(removedObject);
            }
            removedObjects.Clear();

            // add game objects
            gameObjects.AddRange(addedObjects);
            addedObjects.Clear();




            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();


            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
