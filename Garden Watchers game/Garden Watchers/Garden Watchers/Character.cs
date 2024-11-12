﻿using Microsoft.Xna.Framework;
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
        private int health;

        //Properties
        public int Health 
        { 
            get => health; 
            set
            {
                if (value < 0)
                {
                    health = 0;
                }
                else if (value >= 10)
                {
                    health = 10;
                }
                else
                {
                    health = value;
                }
            }
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

        public override void TakeDamage(int damage, bool isMeleeAttack)
        {
            if (invincibilityTimer >= invincibilityFrames || isMeleeAttack == false)
            {
                
                Health -= damage;
                GiveInvincibilityFrames();
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
