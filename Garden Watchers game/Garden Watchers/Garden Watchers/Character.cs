using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Garden_Watchers
{
    /// <summary>
    /// The Character class which Enemy and Player uses
    /// </summary>
    internal abstract class Character : GameObject
    {
        //Fields
       
        protected Vector2 velocity;
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



    }
}
