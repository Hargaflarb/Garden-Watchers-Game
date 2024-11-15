using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class Enemy : Character
    {
        public Enemy(int health, Vector2 position, float speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
        }

        public Enemy(Vector2 position)
        {
            Position = position;
        }
        

        /// <summary>
        /// Loads sprites
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        /// <summary>
        /// Runs methods so they can be used in GameWorld
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (timeExisted >= 0.25f)
            {
                Move(gameTime, screenSize);
            }

            base.Update(gameTime, screenSize);
        }


        public static Enemy GetRandomNewEnemy(Vector2 position)
        {
            int random = GameWorld.Random.Next(0, 3);

            switch (random)
            {
                case 0:
                    return new Gnome(position);
                case 1:
                    return new Flamingo(position);
                case 2:
                    return new Fairy(position);
                default:
                    return new Gnome(position);
            }
        }
    }
}
