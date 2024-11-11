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
    internal class Gnome : Enemy, IChase
    {
        //Fields
        private bool charging;
        private float cooldown;
        private int damage = 2;
            
        public Gnome(int health, Vector2 position, float speed) : base(health, position, speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("garden_gnome");
            }

            sprite = sprites[0];
            base.LoadContent(content);
        }
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (velocity == Vector2.Zero)
            {
                charging = false;
            }
            Chase();
            Charge();

            if (!charging)
            {
                cooldown -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }            
            if (cooldown <= 0)
            {
                cooldown = 0;
            }
            base.Update(gameTime, screenSize);
        }

        public void Chase()
        {
            if (charging == false && cooldown <= 0)
            {
                Vector2 direction = new Vector2(GameWorld.PlayerCharacterPosition.X - position.X, GameWorld.PlayerCharacterPosition.Y - position.Y);
                double test = Math.Atan2(direction.Y, direction.X);
                float XDirection = (float)Math.Cos(test);
                float YDirection = (float)Math.Sin(test);
                direction = new Vector2(XDirection, YDirection);
                velocity = (direction);
                speed = 250;
            }
            else if (velocity == Vector2.Zero)
            {
                charging = false;
            }
        }

        public void Charge()
        {
            Vector2 distance = new Vector2(Math.Abs(GameWorld.PlayerCharacterPosition.X - position.X),Math.Abs(GameWorld.PlayerCharacterPosition.Y - position.Y));
            float pythagorasDistance = (float)Math.Pow(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2), 0.5f);
            
            if (pythagorasDistance <= 600 && cooldown <= 0)
            {
                charging = true;
                speed = 500;
                cooldown = 2;
            }            
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                velocity = Vector2.Zero;                
                charging = false;
                other.TakeDamage(damage, true);
            }
        }
    }
}
