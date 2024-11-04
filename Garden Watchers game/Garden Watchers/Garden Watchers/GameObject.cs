using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Garden_Watchers
{
    internal abstract class GameObject
    {
        protected Texture2D sprite;
        private Rectangle hitbox;
        private Point position;

        protected Rectangle Hitbox { get => hitbox; set => hitbox = value; }
        protected Point Position { get => position; set => position = value; }

        protected abstract void Initialize();


        public virtual void OnCollision(GameObject other)
        {

        }

        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);

        protected virtual void Draw(GameTime gameTime)
        {

        }


    }
}
