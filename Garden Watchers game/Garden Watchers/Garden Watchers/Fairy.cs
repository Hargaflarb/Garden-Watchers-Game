using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private float bulletTimer;
        private SoundEffect magic;
        public Fairy(Vector2 position) : base(position)
        {
            speed = 0;
            movementSpeed = 150;
            Health = 5;
            scale= 0.3f;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites=new Texture2D[6];            
            for(int i = 0; i < sprites.Length; i++)
            {
                sprites[i]=content.Load<Texture2D>("Fairy\\fairy v2\\fairy000" + i);
            }
            sprite = sprites[0];
            bulletSprite = content.Load<Texture2D>("laserRed08");
            magic = content.Load<SoundEffect>("magic bullet sfx");
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            bulletTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            FindPlayer();
            Shoot();
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

        public void Shoot()
        {
            if (bulletTimer >= bulletRate)
            {
                magic.Play();
                Vector2 direction = new Vector2(GameWorld.PlayerCharacterPosition.X - position.X, GameWorld.PlayerCharacterPosition.Y - position.Y);
                double directionSum = Math.Atan2(direction.Y, direction.X);
                float XDirection = (float)Math.Cos(directionSum);
                float YDirection = (float)Math.Sin(directionSum);
                direction = new Vector2(XDirection, YDirection);
                Bullet newBullet = new Bullet(bulletSprite, position, direction, false, (float)directionSum,200);
                GameWorld.AddedObjects.Add(newBullet);
                bulletTimer = 0;
            }
        }
    }
}
