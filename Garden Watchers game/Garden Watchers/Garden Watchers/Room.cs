using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


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
        private static Dictionary<int, Enemy[]> enemyApearanceProgression;

        public Room(int X, int Y, Direction entrenceSide)
        {
            coordinates = new Vector2(X, Y);
            roomObjects = new List<GameObject>();

            Initialize(entrenceSide);
        }
        static Room()
        {
            random = new Random();

           

            enemyApearanceProgression = new Dictionary<int, Enemy[]>(7)
            {
                { 0, new Enemy[] { } },
                { 1, new Enemy[] { new Gnome(new Vector2(1500, 500)) } },
                { 2, new Enemy[] { new Gnome(new Vector2(1500, 250)), new Gnome(new Vector2(1500, 500)), new Gnome(new Vector2(1500, 750)) } },
                { 3, new Enemy[] { new Fairy(new Vector2(1000, 500)) } },
                { 4, new Enemy[] { new Fairy(new Vector2(1500, 500)), new Fairy(new Vector2(500, 500)) } },
                { 5, new Enemy[] { new Flamingo(new Vector2(1000, 500)) } },
                { 12, new Enemy[] { new GnomeBoss(new Vector2(1000, 500)) } },
            };

        }

        public Direction DoorDirection { get => doorDirection; set => doorDirection = value; }


        /// <summary>
        /// Set puts obstacles, doors, enemies and more in the room.
        /// </summary>
        public void Initialize(Direction entrenceSide)
        {
            InitializeDoors();
            InitializeEnemies(entrenceSide);
            InitializeObstacles(entrenceSide);
            //remember to load content of new stuff
        }


        public void InitializeEnemies(Direction entrenceSide)
        {
            if (!enemyApearanceProgression.ContainsKey(Map.RoomCount))
            {
                // random enemy
                int enemyAmount = random.Next(2, 4);
                for (int i = 0; i < enemyAmount; i++)
                {
                    Vector2 screenSize = GameWorld.ScreenSize;
                    Rectangle spawnBounds = new Rectangle(0,0, (int)screenSize.X, (int)screenSize.Y);
                    switch (entrenceSide)
                    {
                        case Direction.Up:
                            spawnBounds.Y += 500;
                            spawnBounds.Height -= 500;
                            break;
                        case Direction.Down:
                            spawnBounds.Height -= 500;
                            break;
                        case Direction.Left:
                            spawnBounds.X += 500;
                            spawnBounds.Width -= 500;
                            break;
                        case Direction.Right:
                            spawnBounds.Width -= 500;
                            break;
                        default:
                            break;
                    }

                    Vector2 position = new Vector2(random.Next(spawnBounds.X, spawnBounds.Width), random.Next(spawnBounds.Y, spawnBounds.Height));
                    GameWorld.MakeObject(Enemy.GetRandomNewEnemy(position));
                }
            }
            else
            {
                Enemy[] roomEnemies = enemyApearanceProgression[Map.RoomCount];
                foreach (Enemy enemy in roomEnemies)
                {
                    GameWorld.MakeObject(enemy);
                }
            }
        }

        public void InitializeObstacles(Direction entrenceSide)
        {
            int obstacleAmount = random.Next(2, 4);
            for (int i = 0; i < obstacleAmount; i++)
            {
                Vector2 screenSize = GameWorld.ScreenSize;
                Rectangle spawnBounds = new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y);
                switch (entrenceSide)
                {
                    case Direction.Up:
                        spawnBounds.Y += 500;
                        spawnBounds.Height -= 500;
                        break;
                    case Direction.Down:
                        spawnBounds.Height -= 500;
                        break;
                    case Direction.Left:
                        spawnBounds.X += 500;
                        spawnBounds.Width -= 500;
                        break;
                    case Direction.Right:
                        spawnBounds.Width -= 500;
                        break;
                    default:
                        break;
                }

                Vector2 position = new Vector2(random.Next(spawnBounds.X, spawnBounds.Width), random.Next(spawnBounds.Y, spawnBounds.Height));
                GameWorld.MakeObject(Obstacle.GetRandomNewObstacle(position));
            }



            GameObject tempObstacle = new Wall(new Vector2(200, 200));
            GameWorld.MakeObject(tempObstacle);

            GameObject tempObstacle2 = new PitFall(new Vector2(400, 200));
            GameWorld.MakeObject(tempObstacle2);

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
                if (gameObject is not Player & gameObject is not Bullet)
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
