﻿using Microsoft.Xna.Framework;
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

        /// <summary>
        /// Loads sprites
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            
        }

        /// <summary>
        /// Runs methods so they can be used in GameWorld
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
           

            base.Update(gameTime, screenSize);
        }

        public void DetectPlayer(Vector2 playerPosition, Vector2 enemyPosition)
        {

        }
    }
}
