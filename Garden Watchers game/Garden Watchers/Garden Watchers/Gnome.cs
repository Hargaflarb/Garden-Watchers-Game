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
    internal class Gnome : Enemy, IChase
    {
        //Fields
        protected bool charging;
        protected float cooldown;
        protected int damage = 2;
        protected Texture2D[] chargeAnim;
        protected Texture2D[] walkAnim;
        protected SoundEffect yell;

        public Gnome(int health, Vector2 position, float speed) : base(health, position, speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
        }

        public Gnome(Vector2 position) : base(position)
        {
            Health = 10;
            speed = 250;
            scale = 0.3f;
        }

        public override void LoadContent(ContentManager content)
        {      
            walkAnim=new Texture2D[8];
            chargeAnim=new Texture2D[9];
            for (int i = 0; i < walkAnim.Length; i++)
            {
                walkAnim[i] = content.Load<Texture2D>("Gnome\\walking gnome\\gnomeWalk000"+i);
            }
            for (int i = 0; i < chargeAnim.Length; i++)
            {
                chargeAnim[i] = content.Load<Texture2D>("Gnome\\charging gnome\\gnomeCharge000" + i);
            }
            sprites = walkAnim;
            sprite = sprites[0];
            yell = content.Load<SoundEffect>("yell");
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
                sprites = walkAnim;
            }
            else if (velocity == Vector2.Zero)
            {
                charging = false;
                sprites = walkAnim;
            }
        }

        public void Charge()
        {
            Vector2 distance = new Vector2(Math.Abs(GameWorld.PlayerCharacterPosition.X - position.X),Math.Abs(GameWorld.PlayerCharacterPosition.Y - position.Y));
            float pythagorasDistance = (float)Math.Pow(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2), 0.5f);
            
            if (pythagorasDistance <= 600 && cooldown <= 0)
            {
                yell.Play();
                sprites = chargeAnim;
                charging = true;
                speed = 700;
                cooldown = 1;
            }            
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                velocity = Vector2.Zero;                
                charging = false;
                ((Player)other).TakeDamage(damage, true);
            }
        }
    }
}
