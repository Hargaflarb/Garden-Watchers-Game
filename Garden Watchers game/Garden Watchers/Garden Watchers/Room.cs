using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Garden_Watchers
{
    public enum Direction
    {
        None = 0,
        Up  = 1,
        Down = 2,
        Left = 4,
        Right = 8,
    }

    public class Room
    {
        private List<GameObject> roomObjects;
        private Vector2 coordinates;
        private Direction doorDirection;
        private static Random random;

        public Room(int X, int Y)
        {
            coordinates = new Vector2(X, Y);
            roomObjects = new List<GameObject>();

            Initialize();
        }
        static Room()
        {
            random = new Random();
        }

        public Direction DoorDirection { get => doorDirection; set => doorDirection = value; }


        /// <summary>
        /// Set puts obstacles, doors, enemies and more in the room.
        /// </summary>
        public void Initialize()
        {
            InitializeDoors();
            //member to load content of new stuff
        }


        public void InitializeDoors()
        {
            Direction directions = Map.GetSurroundingDoors(coordinates);

            if (directions.HasFlag(Direction.Up))
            {
                MakeDoor((int)coordinates.X, (int)coordinates.Y + 1, Direction.Up);
            }
            else
            {
                if (random.Next(0, 1) == 0)
                {
                    MakeDoor((int)coordinates.X, (int)coordinates.Y + 1, Direction.Up);
                }
            }

            if (directions.HasFlag(Direction.Down))
            {
                MakeDoor((int)coordinates.X, (int)coordinates.Y - 1, Direction.Down);
            }
            else
            {
                if (random.Next(0, 4) == 1)
                {
                    MakeDoor((int)coordinates.X, (int)coordinates.Y - 1, Direction.Down);
                }
            }

            if (directions.HasFlag(Direction.Right))
            {
                MakeDoor((int)coordinates.X + 1, (int)coordinates.Y, Direction.Right);
            }
            else
            {
                if (random.Next(0, 3) == 1)
                {
                    MakeDoor((int)coordinates.X + 1, (int)coordinates.Y, Direction.Right);
                }
            }

            if (directions.HasFlag(Direction.Left))
            {
                MakeDoor((int)coordinates.X - 1, (int)coordinates.Y, Direction.Left);
            }
            else
            {
                if (random.Next(0, 3) == 1)
                {
                    MakeDoor((int)coordinates.X - 1, (int)coordinates.Y, Direction.Left);
                }
            }

        }


        public bool HasOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return doorDirection.HasFlag(Direction.Down);
                case Direction.Down:
                    return doorDirection.HasFlag(Direction.Up);
                case Direction.Left:
                    return doorDirection.HasFlag(Direction.Right);
                case Direction.Right:
                    return doorDirection.HasFlag(Direction.Left);
                default:
                    return false;
            }
        }

        private void MakeDoor(int X, int Y, Direction direction)
        {
            MakeObject(new Door(X, Y, direction));
            doorDirection += (int)direction;
        }

        public void MakeObject(GameObject gameObject)
        {
            gameObject.LoadContent(GameWorld.TheGameWorld.Content);
            roomObjects.Add(gameObject);
        }


        public void SaveRoomObjects(List<GameObject> gameObjects)
        {
            roomObjects.Clear();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is not Player)
                {
                    roomObjects.Add(gameObject);
                }
            }
        }

        public void WriteRoomObjects(bool killPrevious)
        {
            if (killPrevious)
            {
                GameWorld.KillAllObjects();
            }
            GameWorld.AddObjects(roomObjects);
        }
    }
}
