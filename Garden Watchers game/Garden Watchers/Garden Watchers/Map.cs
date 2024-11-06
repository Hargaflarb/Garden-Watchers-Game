using System;
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

        static Map()
        {
            rooms = new Dictionary<Vector2, Room>();
        }


        public static void GoToRoom(int x, int y)
        {
            if (x != 0 & y != 0)
            {
                shownRoom.SaveRoomObjects(GameWorld.GameObjects);
            }

            if (!rooms.ContainsKey(new Vector2(x, y)))
            {
                AddRoom(x, y);
            }
            shownRoom = rooms[new Vector2(x, y)];

            shownRoom.WriteRoomObjects();
        }


        private static void AddRoom(int X, int Y)
        {
            rooms.Add(new Vector2(X, Y), new Room(X, Y));
        }
    }
}
