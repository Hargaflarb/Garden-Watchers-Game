using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Garden_Watchers
{
    internal class Door : GameObject
    {
        public Vector2 leadingTo;

        public Door(Vector2 leadingTo) : base()
        {
            this.leadingTo = leadingTo;
            Position = new Vector2(1000, 500);
        }
        public Door(int x, int y) : base()
        {
            leadingTo = new Vector2(x, y);
            Position = new Vector2(2000, 500);
        }


        public override void OnCollision(GameObject other)
        {
            if (other != this)
            {
                GameWorld.TheGameWorld.Player.Position = new Vector2(0, 500);
                Map.GoToRoom((int)leadingTo.X, (int)leadingTo.Y);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("door");

            base.LoadContent(content);
        }
    }
}
