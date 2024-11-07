using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Garden_Watchers
{
    public class Room
    {
        private List<GameObject> roomObjects;
        private Vector2 coordinates;

        public Room(int X, int Y)
        {
            coordinates = new Vector2(X, Y);
            roomObjects = new List<GameObject>();

            Initialize();
        }


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
            MakeObject(new Door((int)coordinates.X + 1, (int)coordinates.Y));

        }


        public void MakeObject(GameObject gameObject)
        {
            gameObject.LoadContent(GameWorld.TheGameWorld.Content);
            roomObjects.Add(gameObject);
        }


        public void SaveRoomObjects(List<GameObject> gameObjects)
        {
            roomObjects = gameObjects;
        }

        public void WriteRoomObjects()
        {
            if (coordinates != new Vector2(0, 0))
            {
                GameWorld.KillAllObjects();
            }
            GameWorld.AddObjects(roomObjects);
        }
    }
}
