using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        //Properties
        public virtual int Health
        {
            get => health;
            set { health = value; }
        }

        //Methods




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
            if (invincibilityTimer <= 0 || isMeleeAttack == false)
            {
                Health -= damage;
                GiveInvincibilityFrames();
                takingDamage = true;
                if (Health <= 0)
                {
                    if (this is Enemy)
                    {
                        //death animation?
                        GameWorld.KillObject(this);
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
