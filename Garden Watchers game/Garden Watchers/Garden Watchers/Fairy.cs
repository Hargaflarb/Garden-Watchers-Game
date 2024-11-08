using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class Fairy : Enemy
    {
        private Texture2D bulletSprite;
        private float bulletRate = 0.4f;
        private bool fleeing = false;
        private float movementSpeed;
        public Fairy(int health, Vector2 position, float speed) : base(health, position, speed)
        {
            movementSpeed = speed;
            Health = health;
            this.position = position;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("tempFairySprite");
            bulletSprite = content.Load<Texture2D>("laserRed08");
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            FindPlayer();
            base.Update(gameTime, screenSize);
        }

        public void Flee()
        {
            velocity = Vector2.Zero;
            Vector2 direction = new Vector2(position.X-GameWorld.PlayerCharacterPosition.X, position.Y- GameWorld.PlayerCharacterPosition.Y);
            double test = Math.Atan2(direction.Y, direction.X);
            float XDirection = (float)Math.Cos(test);
            float YDirection = (float)Math.Sin(test);
            direction = new Vector2(XDirection, YDirection);
            velocity = (direction);
        }

        public void FindPlayer()
        {
            Vector2 distance = new Vector2(Math.Abs(GameWorld.PlayerCharacterPosition.X - position.X), Math.Abs(GameWorld.PlayerCharacterPosition.Y - position.Y));
            float pythagorasDistance = (float)Math.Pow(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2), 0.5f);

            if (pythagorasDistance <= 300)
            {
                speed = movementSpeed;
                Flee();
            }
            else
            {
                speed = 0;
            }
        }
    }
}
