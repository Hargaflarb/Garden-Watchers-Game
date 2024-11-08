﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class Flamingo : Enemy, IChase
    {
        // *boom!*
        public Flamingo(int health, Vector2 position, float speed) : base(health, position, speed)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("flamingo");
            }
            sprite = sprites[0];
            base.LoadContent(content);
        }

        public void Chase()
        {
            Vector2 direction = new Vector2(GameWorld.PlayerCharacterPosition.X - position.X, GameWorld.PlayerCharacterPosition.Y - position.Y);
            double test = Math.Atan2(direction.Y, direction.X);
            float XDirection = (float)Math.Cos(test);
            float YDirection = (float)Math.Sin(test);
            direction = new Vector2(XDirection, YDirection);
            velocity = (direction);
        }

        public void Explode()
        {
            Vector2 distance = new Vector2(GameWorld.PlayerCharacterPosition.X - position.X, GameWorld.PlayerCharacterPosition.Y - position.Y);
            float pythagorasDistance = (float)Math.Pow(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2),0.5f);

            if (pythagorasDistance <= 150)
            {
                speed = 0;
            }
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            Chase();
            Explode();
            base.Update(gameTime, screenSize);
        }

        public override void OnCollision(GameObject other)
        {
            
            
        }
    }
}
