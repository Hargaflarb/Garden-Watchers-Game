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

        /// <summary>
        /// Makes a Door leading the another room, in a specific side of the Room.
        /// </summary>
        /// <param name="x">The x-coordinate of the Room this Door leads to.</param>
        /// <param name="y">The y-coordinate of the Room this Door leads to.</param>
        /// <param name="direction">The side of the Room this Door is placed in.</param>
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

        /// <summary>
        /// returns the opposite direction.
        /// Is often used when a door in the opposite direction is needed/checked for.
        /// </summary>
        /// <param name="direction">The direction of which the opposite is returned.</param>
        /// <returns>The opposite direction of the one input.</returns>
        public static Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    return Direction.None;
            }

        }

        /// <summary>
        /// If the player touches this instance, they will go to the room that this instance is leading to, given there are no more enemies in the current room.
        /// </summary>
        /// <param name="other">The other collieding object.</param>
        public override void OnCollision(GameObject other)
        {
            if (other != this & other is Player)
            {
                if (GameWorld.GetNumberOfEnemies() == 0)
                {
                    Map.GoToRoom((int)leadingTo.X, (int)leadingTo.Y, GetOppositeDirection(direction));
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("door");

            base.LoadContent(content);
        }
    }
}
