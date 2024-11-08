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
        public Direction direction;

        public Door(Vector2 leadingTo, Direction direction) : base()
        {
            this.leadingTo = leadingTo;
            this.direction = direction;

            Vector2 size = GameWorld.ScreenSize;
            switch (direction)
            {
                case Direction.Up:
                    Position = new Vector2(size.X/2, size.Y);
                    break;
                case Direction.Down:
                    Position = new Vector2(size.X / 2, 0);
                    break;
                case Direction.Left:
                    Position = new Vector2(size.X, size.Y / 2);
                    break;
                case Direction.Right:
                    Position = new Vector2(0, size.Y / 2);
                    break;
                default:
                    break;
            }
        }
        public Door(int x, int y, Direction direction) : base()
        {
            leadingTo = new Vector2(x, y);
            this.direction = direction;

            Vector2 size = GameWorld.ScreenSize;
            switch (direction)
            {
                case Direction.Up:
                    Position = new Vector2(size.X / 2, 0);
                    break;
                case Direction.Down:
                    Position = new Vector2(size.X / 2, size.Y);
                    break;
                case Direction.Left:
                    Position = new Vector2(size.X, size.Y / 2);
                    break;
                case Direction.Right:
                    Position = new Vector2(0, size.Y / 2);
                    break;
                default:
                    break;
            }
        }


        public override void OnCollision(GameObject other)
        {
            if (other != this)
            {
                Vector2 size = GameWorld.ScreenSize;
                GameWorld.TheGameWorld.Player.Position = new Vector2(size.X / 2, size.Y / 2);
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
