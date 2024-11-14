using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Garden_Watchers
{   
    /// <summary>
    /// The Character class which Enemy and Player uses
    /// </summary>
    public abstract class Character : GameObject
    {
        //Fields

        protected float speed;
        protected int health;
        protected SoundEffect hurt;
        protected float hurtTime = 0.15f;
        protected float hurtTimer;

        //Properties
        public virtual int Health
        {
            get => health;
            set { health = value; }
        }

        //Methods


        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            hurtTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (hurtTimer <= 0)
            {
                takingDamage = false;
            }


            base.Update(gameTime, screenSize);
        }


        /// <summary>
        /// Method that allows characters to move
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        protected void Move(GameTime gameTime, Vector2 screenSize)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += ((velocity * speed) * deltaTime);
        }

        public virtual void TakeDamage(int damage, bool isMeleeAttack)
        {
            if (invincibilityTimer <= 0)
            {
                if(this is Player)
                {
                    hurt.Play();
                }
                Health -= damage;
                if (isMeleeAttack)
                {
                    GiveInvincibilityFrames();
                }
                takingDamage = true;
                hurtTimer = hurtTime;
                if (Health <= 0)
                {
                    if (this is Enemy)
                    {
                        //death animation?
                        GameWorld.KillObject(this);
                        if (this is GnomeBoss)
                        {
                            GameWorld.Winning = true;
                        }
                    }
                    else
                    {
                        //Game Over sequence
                        GameWorld.KillObject(this);
                        GameWorld.TheGameWorld.IsAlive = false;


                    }
                }
            }

        }

    }
}
