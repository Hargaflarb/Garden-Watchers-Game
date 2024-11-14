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

        //Constructors
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
            //if the gnome is stopped, it is no longer charging
            if (velocity == Vector2.Zero)
            {
                charging = false;
            }
            //control bool that flips sprite
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
        /// <summary>
        /// When not charging, the gnome follows the players location
        /// </summary>
        public void Chase()
        {
            //if not charging or on cooldown
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
            //if the gnome is stopped, it is no longer charging
            else if (velocity == Vector2.Zero)
            {
                charging = false;
            }
        }

        public void Charge()
        {
            //find distance between gnome & player
            Vector2 distance = new Vector2(Math.Abs(GameWorld.PlayerCharacterPosition.X - position.X), Math.Abs(GameWorld.PlayerCharacterPosition.Y - position.Y));
            float pythagorasDistance = (float)Math.Pow(Math.Pow(distance.X, 2) + Math.Pow(distance.Y, 2), 0.5f);

            //when within certain distance of player position, charge is initiated
            if (pythagorasDistance <= 600 && cooldown <= 0)
            {
                yell.Play();
                sprites = chargeAnim;
                charging = true;
                speed = 400;
                cooldown = 2;
            }
        }
        /// <summary>
        /// when colliding, the gnome is stopped & if it hit the player, the player takes damage
        /// </summary>
        /// <param name="other">the object which was collided with</param>
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
