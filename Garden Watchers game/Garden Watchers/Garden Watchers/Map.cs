﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    public static class Map
    {
        private static Dictionary<Vector2, Room> rooms;
        private static Room shownRoom;

        public static int RoomCount { get => rooms.Count; }

        static Map()
        {
            ResetMap();
        }

        public static void ResetMap()
        {
            rooms = new Dictionary<Vector2, Room>();
        }

        /// <summary>
        /// Finds the directions of doors in adjacent room, and is used to find out wether or not to place a door.
        /// </summary>
        /// <param name="roomCoordinates">The coordinates of the room whose adjacent rooms are search.</param>
        /// <returns>The directions in which there are door.</returns>
        public static Direction GetSurroundingDoors(Vector2 roomCoordinates)
        {
            Direction directions = Direction.None;

            // up
            if (rooms.ContainsKey(new Vector2(roomCoordinates.X, roomCoordinates.Y + 1)))
            {
                if (rooms[new Vector2(roomCoordinates.X, roomCoordinates.Y + 1)].HasOppositeDirection(Direction.Up))
                {
                    directions += (int)Direction.Up;
                }
            }

            // down
            if (rooms.ContainsKey(new Vector2(roomCoordinates.X, roomCoordinates.Y - 1)))
            {
                if (rooms[new Vector2(roomCoordinates.X, roomCoordinates.Y - 1)].HasOppositeDirection(Direction.Down))
                {
                    directions += (int)Direction.Down;
                }
            }

            // right
            if (rooms.ContainsKey(new Vector2(roomCoordinates.X + 1, roomCoordinates.Y)))
            {
                if (rooms[new Vector2(roomCoordinates.X + 1, roomCoordinates.Y)].HasOppositeDirection(Direction.Right))
                {
                    directions += (int)Direction.Right;
                }
            }

            // left
            if (rooms.ContainsKey(new Vector2(roomCoordinates.X - 1, roomCoordinates.Y)))
            {
                if (rooms[new Vector2(roomCoordinates.X - 1, roomCoordinates.Y)].HasOppositeDirection(Direction.Left))
                {
                    directions += (int)Direction.Left;
                }
            }

            return directions;
        }


        public static void GoToRoom(int x, int y, Direction comingFrom, bool saveRoom = true)
        {
            GameWorld.Player.GiveInvincibilityFrames(1);

            Vector2 size = GameWorld.ScreenSize;
            switch (comingFrom)
            {
                case Direction.None:
                    GameWorld.Player.Position = new Vector2(size.X / 2, size.Y / 2);
                    break;
                case Direction.Up:
                    GameWorld.Player.Position = new Vector2(size.X / 2, 0 + 300);
                    break;
                case Direction.Down:
                    GameWorld.Player.Position = new Vector2(size.X / 2, size.Y - 300);
                    break;
                case Direction.Left:
                    GameWorld.Player.Position = new Vector2(size.X - 300, size.Y / 2);
                    break;
                case Direction.Right:
                    GameWorld.Player.Position = new Vector2(0 + 300, size.Y / 2);
                    break;
                default:
                    break;
            }

            if (RoomCount == 12)
            {
                GameWorld.YouWon();
            }

            if (saveRoom)
            {
                shownRoom.SaveRoomObjects(GameWorld.GameObjects);
            }

            if (!rooms.ContainsKey(new Vector2(x, y)))
            {
                AddRoom(x, y, comingFrom);
            }
            shownRoom = rooms[new Vector2(x, y)];

            shownRoom.WriteRoomObjects(saveRoom);

        }


        private static void AddRoom(int X, int Y, Direction entrenceDirection)
        {
            rooms.Add(new Vector2(X, Y), new Room(X, Y, entrenceDirection));
        }
    }
}
