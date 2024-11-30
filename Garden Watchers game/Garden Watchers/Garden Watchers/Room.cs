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
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
    }

    public class Room
    {
        private List<GameObject> roomObjects;
        private Vector2 coordinates;
        private Direction doorDirections;
        private static Random random;
        private static Dictionary<int, Enemy[]> enemyApearanceProgression;

        /// <summary>
        /// Makes a new Room.
        /// </summary>
        /// <param name="X">The x-coordinate of the room.<</param>
        /// <param name="Y">The y-coordinate of the room.<</param>
        /// <param name="entrenceSide">The side of the Room the Player will appear in. (middle if none)</param>
        public Room(int X, int Y, Direction entrenceSide)
        {
            coordinates = new Vector2(X, Y);
            roomObjects = new List<GameObject>();

            Initialize(entrenceSide);
        }

        /// <summary>
        /// Instatiates the random field.
        /// </summary>
        static Room()
        {
            random = new Random();

            enemyApearanceProgression = new Dictionary<int, Enemy[]>(7)
            {
                { 1, new Enemy[] { } },
                { 2, new Enemy[] { new Gnome(new Vector2(1500, 500)) } },
                { 3, new Enemy[] { new Gnome(new Vector2(1500, 250)), new Gnome(new Vector2(1500, 500)), new Gnome(new Vector2(1500, 750)) } },
                { 4, new Enemy[] { new Fairy(new Vector2(1000, 500)) } },
                { 5, new Enemy[] { new Fairy(new Vector2(1500, 500)), new Fairy(new Vector2(500, 500)) } },
                { 6, new Enemy[] { new Flamingo(new Vector2(1000, 500)) } },
                { 15, new Enemy[] { new GnomeBoss(100, new Vector2(1000, 500), 200)} },
            };
        }


        /// <summary>
        /// Re-instatiates the "tutorial-enemies", and is used when starting a new run/game.
        /// </summary>
        public static void ResetRooms()
        {
            enemyApearanceProgression = new Dictionary<int, Enemy[]>(7)
            {
                { 1, new Enemy[] { } },
                { 2, new Enemy[] { new Gnome(new Vector2(1500, 500)) } },
                { 3, new Enemy[] { new Gnome(new Vector2(1500, 250)), new Gnome(new Vector2(1500, 500)), new Gnome(new Vector2(1500, 750)) } },
                { 4, new Enemy[] { new Fairy(new Vector2(1000, 500)) } },
                { 5, new Enemy[] { new Fairy(new Vector2(1500, 500)), new Fairy(new Vector2(500, 500)) } },
                { 6, new Enemy[] { new Flamingo(new Vector2(1000, 500)) } },
                { 15, new Enemy[] { new GnomeBoss(100, new Vector2(1000, 500), 200)} },
            };
        }

        /// <summary>
        /// Places doors, random enemies and random obstacles in the room.
        /// Is used when entering the room for the first time.
        /// </summary>
        /// <param name="entrenceSide">The side of the room that the player enters from, where objects worn't be placed.</param>
        private void Initialize(Direction entrenceSide)
        {
            InitializeDoors();
            InitializeEnemies(entrenceSide);
            InitializeObstacles(entrenceSide);
            //remember to load content of new stuff
        }

        /// <summary>
        /// Places random enemies in the room, if there isn't a specified set of enemies to insert.
        /// Is used in instantiation.
        /// </summary>
        /// <param name="entrenceSide">The side of the room that the player enters from, where enemies worn't be placed.</param>
        private void InitializeEnemies(Direction entrenceSide)
        {
            if (!enemyApearanceProgression.ContainsKey(Map.RoomCount + 1)) // +1 cus this room hasn't been added yet.
            {
                // random enemy
                int enemyAmount = random.Next(2, 4);
                for (int i = 0; i < enemyAmount; i++)
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
                    GameWorld.MakeObject(Enemy.GetRandomNewEnemy(position));
                }
            }
            else
            {
                foreach (Enemy enemy in enemyApearanceProgression[Map.RoomCount + 1])
                {
                    GameWorld.MakeObject(enemy);
                }
            }
        }

        /// <summary>
        /// Places random obstacles in the room.
        /// Is used in instantiation.
        /// </summary>
        /// <param name="entrenceSide"></param>
        private void InitializeObstacles(Direction entrenceSide)
        {
            int roomNumber = Map.RoomCount + 1; // +1 cus this room hasn't been added yet.
            if (roomNumber != 15 & roomNumber != 1) // boss room and spawn room has no obstacles
            {
                int obstacleAmount = random.Next(2, 4);
                for (int i = 0; i < obstacleAmount; i++)
                {
                    Vector2 screenSize = GameWorld.ScreenSize;
                    Rectangle spawnBounds = new Rectangle(200, 200, (int)screenSize.X - 200, (int)screenSize.Y - 200);
                    switch (entrenceSide)
                    {
                        case Direction.Up:
                            spawnBounds.Y += 200;
                            spawnBounds.Height -= 200;
                            break;
                        case Direction.Down:
                            spawnBounds.Height -= 200;
                            break;
                        case Direction.Left:
                            spawnBounds.X += 200;
                            spawnBounds.Width -= 200;
                            break;
                        case Direction.Right:
                            spawnBounds.Width -= 200;
                            break;
                        default:
                            break;
                    }

                    Vector2 position = new Vector2(random.Next(spawnBounds.X, spawnBounds.Width), random.Next(spawnBounds.Y, spawnBounds.Height));
                    GameWorld.MakeObject(Obstacle.GetRandomNewObstacle(position));
                }
            }
        }

        /// <summary>
        /// Dectects when there are doors in adjacent room and make connecting doors thereto.
        /// If no room is found, it's randomly decided wether or not to place a door. (100% for top-door)
        /// Is used in instantiation.
        /// </summary>
        private void InitializeDoors()
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

        /// <summary>
        /// Deternines wether the room has a door pointing in opposite direction of the given one.
        /// Is often used when deternining wether an adjacent room has a conecting door.
        /// </summary>
        /// <param name="direction">The direction to check the opposite of.</param>
        /// <returns>Wether or not the room has a one the opposit side.</returns>
        public bool HasOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return doorDirections.HasFlag(Direction.Down);
                case Direction.Down:
                    return doorDirections.HasFlag(Direction.Up);
                case Direction.Left:
                    return doorDirections.HasFlag(Direction.Right);
                case Direction.Right:
                    return doorDirections.HasFlag(Direction.Left);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Is used when adding a Door to the Room, during instantiation.
        /// </summary>
        /// <param name="X">The x-coordinate of the room this door will lead to.</param>
        /// <param name="Y">The y-coordinate of the room this door will lead to.</param>
        /// <param name="direction">The side of the room where the door is placed.</param>
        private void MakeDoor(int X, int Y, Direction direction)
        {
            MakeObject(new Door(X, Y, direction));
            doorDirections += (int)direction;
        }

        /// <summary>
        /// Loads the content of the object and adds it to the Room's object-list.
        /// </summary>
        /// <param name="gameObject"></param>
        public void MakeObject(GameObject gameObject)
        {
            gameObject.LoadContent(GameWorld.TheGameWorld.Content);
            roomObjects.Add(gameObject);
        }

        /// <summary>
        /// Saves all the GameObjects from a list, given they aren't a Player or a Bullet.
        /// Is used when exiting a Room.
        /// </summary>
        /// <param name="gameObjects">The list of GameObjects to save.</param>
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

        /// <summary>
        /// Adds all the Rooms saved GameObjects to the GameWorld.
        /// Is used when entering a Room.
        /// </summary>
        /// <param name="killPrevious">If true; the GameObject in GameWorld will be removed, before adding new ones.</param>
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
