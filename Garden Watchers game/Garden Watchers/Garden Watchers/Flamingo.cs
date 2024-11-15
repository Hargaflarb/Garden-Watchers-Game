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
    internal class Flamingo : Enemy, IChase
    {
        //Field
        private float timer;
        private float explodeTime = 0.6F;
        private bool timerStarted = false;
        private bool explode = false;
        private int damage = 10;
        private Texture2D[] flashing;
        private Texture2D[] walking;

        public Flamingo(Vector2 position) : base(position)
        {
            Health = 3;
            speed = 200;
            scale = 0.2f;
        }

        public override void LoadContent(ContentManager content)
        {
            walking = new Texture2D[19];

            for (int i = 0; i < walking.Length; i++)
            {
                if (i < 10)
                {
                    walking[i] = content.Load<Texture2D>("Flamingo\\walk\\flamingoWalk000" + i);
                }
                else
                {
                    walking[i] = content.Load<Texture2D>("Flamingo\\walk\\flamingoWalk00" + i);
                }
            }
            sprites = walking;
            flashing=new Texture2D[2];
            flashing[0] = content.Load<Texture2D>("Flamingo\\blinking\\flamingo explosive frame 0000");
            flashing[1] = content.Load<Texture2D>("Flamingo\\blinking\\flamingo explosive frame 0001");
            sprite = sprites[0];
            base.LoadContent(content);
        }
        /// <summary>
        /// follows player while outside certain distance
        /// </summary>
        public void Chase()
        {
            Vector2 direction = new Vector2(GameWorld.PlayerCharacterPosition.X - position.X, GameWorld.PlayerCharacterPosition.Y - position.Y);
            double test = Math.Atan2(direction.Y, direction.X);
            float XDirection = (float)Math.Cos(test);
            float YDirection = (float)Math.Sin(test);
            direction = new Vector2(XDirection, YDirection);
            velocity = (direction);
        }
        /// <summary>
        /// if within certain distance of player, starts a timer then explodes
        /// </summary>
        public void Explode()
        {
            Vector2 distance = new Vector2(GameWorld.PlayerCharacterPosition.X - position.X, GameWorld.PlayerCharacterPosition.Y - position.Y);
            float pythagorasDistance = (float)Math.Pow(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2),0.5f);

            //stops moving & starts countdown
            if (pythagorasDistance <= 150 & !explode)
            {
                speed = 0;
                timer = 0;
                timerStarted = true;
                explode = true;
            }

            if (pythagorasDistance >= 400 & timer <= explodeTime/2)
            {
                speed = 400;
                timerStarted = false;
                explode = false;
            }

            if (explode == true)
            {
                frames = 0;
                spriteNumber = 0;
                sprites = flashing;
                if (timer >= explodeTime)

                {
                    GameWorld.KillObject(this);
                    Explosion explosion = new Explosion(Position);
                    GameWorld.MakeObject(explosion);
                }
                
                
            }
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (velocity.X > 0)
            {
                facingRight = true;
            }
            if (velocity.X < 0)
            {
                facingRight = false;
            }
            Chase();
            Explode();

            if (timerStarted)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            base.Update(gameTime, screenSize);
        }

        public override void OnCollision(GameObject other)
        {
            
            
        }
    }
}
