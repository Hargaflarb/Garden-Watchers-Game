using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


using System;
using SharpDX.Direct3D9;

namespace Garden_Watchers
{
    public abstract class GameObject
    {
        //Fields
        protected Texture2D sprite;
        protected Texture2D[] sprites;
        private Rectangle hitbox;
        protected Vector2 position;
        protected Vector2 origin;

        /// <summary>
        /// The Hitbox is a rectangle the surounds the game objects sprite, and is used to detect colission.
        /// </summary>
        public Rectangle Hitbox { get => hitbox; private set => hitbox = value; }

        /// <summary>
        /// The Position is at the center of the gameObject and determines where the object is on the in the game
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set { position = value; Hitbox = new Rectangle((int)value.X - (Hitbox.Width / 2), (int)value.Y - (Hitbox.Height / 2), Hitbox.Width, Hitbox.Height); }
        }

        public GameObject()
        {
          
        }

        //Methods

        public void CheckCollision(GameObject other)
        {
            if (Hitbox.Intersects(other.Hitbox))
            {
                OnCollision(other);
            }
        }


        /// <summary>
        /// Runs when GameObject is colliding with something else
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(GameObject other)
        {
            if (this == other)
            {
                return;
            }
        }

        public virtual Vector2 CheckOutOfBounds(Vector2 screenSize, Vector2 vector)
        {
            float X = vector.X;
            float Y = vector.Y;
            if (vector.Y > screenSize.Y - Hitbox.Height / 2)
            {
                Y = screenSize.Y - Hitbox.Height / 2;
            }
            else if (vector.Y < 0 + Hitbox.Height / 2)
            {
                Y = 0 + Hitbox.Height / 2;
            }

            if (vector.X > screenSize.X - Hitbox.Width / 2)
            {
                X = screenSize.X - Hitbox.Width / 2;
            }
            else if (vector.X < 0 + Hitbox.Width / 2)
            {
                X = 0 + Hitbox.Width / 2;
            }

            return new Vector2(X, Y);
        }



        /// <summary>
        /// Abstract method for loading sprites
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }


        /// <summary>
        /// Abstract method for updating
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        public virtual void Update(GameTime gameTime, Vector2 screenSize)
        {
            Position = CheckOutOfBounds(screenSize, Position);
        }

        /// <summary>
        /// Draws out sprites
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Hitbox, null, Color.White, 0, origin = Vector2.Zero, SpriteEffects.None, 1);
        }

    }
}
