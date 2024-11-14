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
    internal class GnomeBoss : Enemy
    {
        //Fields
        protected bool charging;
        protected float cooldown;
        protected int damage = 2;
        private Texture2D[] walkAnim;
        private Texture2D[] chargeAnim;
        private SoundEffect yell;

        //Constructor
        public GnomeBoss(int health, Vector2 position, float speed) : base(health, position, speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
        }

        public GnomeBoss(Vector2 position) : base(position)
        {
            Health = 40;
            speed = 200;
            scale = 0.8f;
        }


        //Methods
        public override void LoadContent(ContentManager content)
        {
            walkAnim = new Texture2D[8];
            chargeAnim = new Texture2D[9];
            for (int i = 0; i < walkAnim.Length; i++)
            {
                walkAnim[i] = content.Load<Texture2D>("Gnome\\walking gnome\\gnomeWalk000" + i);
            }
            for (int i = 0; i < chargeAnim.Length; i++)
            {
                chargeAnim[i] = content.Load<Texture2D>("Gnome\\charging gnome\\gnomeCharge000" + i);
            }
            sprites = walkAnim;
            sprite = sprites[0];
            yell = content.Load<SoundEffect>("yell");
            base.LoadContent(content);

            sprite = sprites[0];
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            if (velocity == Vector2.Zero)
            {
                charging = false;
            }
            if (velocity.X > 0)
            {
                facingRight = true;
            }
            if (velocity.X < 0)
            {
                facingRight = false;
            }
            Chase();
            Charge();

            if (!charging)
            {
                sprites = walkAnim;
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
                speed = 200;
            }
            else if (velocity == Vector2.Zero)
            {
                charging = false;
            }
        }

        public void Charge()
        {
            Vector2 distance = new Vector2(Math.Abs(GameWorld.PlayerCharacterPosition.X - position.X), Math.Abs(GameWorld.PlayerCharacterPosition.Y - position.Y));
            float pythagorasDistance = (float)Math.Pow(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2), 0.5f);

            if (pythagorasDistance <= 600 && cooldown <= 0)
            {
                yell.Play();
                sprites = chargeAnim;
                charging = true;
                speed = 400;
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
