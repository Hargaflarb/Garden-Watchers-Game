﻿using Microsoft.Xna.Framework;
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

#if DEBUG
        private Texture2D hitboxPixel;
#endif


        //Properties
        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        private static GameWorld TheGameWorld { get; set; }



        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            TheGameWorld = this;
        
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            Vector2 playerPosition = new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2);
            GameObject player = new Player(playerPosition, 500);
            GameObject tempObstacle = new Obstacle(new Vector2(200,200));
            gameObjects = new List<GameObject>() { player, tempObstacle };

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
            
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime, screenSize);

                foreach (GameObject other in gameObjects)
                {
                    gameObject.CheckCollision(other);
                }
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

        public static void KillObject(GameObject gameObject)
        {
            removedObjects.Add(gameObject);
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


            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }


#if DEBUG
            // draw the hitbox and position of every gameObject
            foreach (GameObject gameObject in gameObjects)
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
