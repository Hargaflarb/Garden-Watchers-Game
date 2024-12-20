﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Garden_Watchers
{
    internal abstract class Item: GameObject
    {
        protected bool pickedUp;


        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (pickedUp) 
            {
                position = GameWorld.PlayerCharacterPosition;
            }
        }
        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }
        


    }
}
