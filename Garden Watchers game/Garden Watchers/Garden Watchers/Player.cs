using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class Player : Character
    {


        //Constructor

        /// <summary>
        /// The Player construction
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public Player (Vector2 position, int speed)
        {
            Position = position;
            this.speed = 500;
        }

        //Methods

        /// <summary>
        /// Loads textures for the player character into an array
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("temp_playercharacter");
            }
            sprite = sprites[0];


            // base.LoadContent is called to (fx.) set the hitbox
            base.LoadContent(content);

        }

        /// <summary>
        /// Runs methods so that it can be used in GameWorld
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            HandleInput();
            Move(gameTime, screenSize);
        }

        /// <summary>
        /// Reads player input and changes movement speed accordingly
        /// </summary>
        private void HandleInput()
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, +1);
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(+1, 0);
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        } 
    }
}
