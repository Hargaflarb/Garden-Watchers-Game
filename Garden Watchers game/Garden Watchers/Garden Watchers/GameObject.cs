using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


using System;

namespace Garden_Watchers
{
    internal abstract class GameObject
    {
        //Fields
        protected Texture2D sprite;
        private Rectangle hitbox;

        protected Rectangle Hitbox { get => hitbox; set => hitbox = value; }
        protected Vector2 position = Vector2.Zero;
        protected Vector2 origin;

        //Methods

        /// <summary>
        /// Runs when GameObject is colliding with something else
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(GameObject other)
        {

        }
      
        protected abstract void Initialize();

        /// <summary>
        /// Abstract method for loading sprites
        /// </summary>
        /// <param name="content"></param>
        public abstract void LoadContent(ContentManager content);


        /// <summary>
        /// Abstract method for updating
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        public abstract void Update(GameTime gameTime, Vector2 screenSize);

        /// <summary>
        /// Draws out sprites
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin = new Vector2(sprite.Width / 2, sprite.Height / 2), 1, SpriteEffects.None, 1);
        }

    }
}
