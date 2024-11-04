using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    /// <summary>
    /// The Character class which Enemy and Player uses
    /// </summary>
    internal abstract class Character : GameObject
    {
        //Fields
        protected Vector2 position = Vector2.Zero;
        protected Vector2 velocity;
        protected float speed;

        //Properties
        

        //Methods

        /// <summary>
        /// Method that allows characters to move
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        protected void Move(GameTime gameTime, Vector2 screenSize)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((velocity * speed) * deltaTime);
        }



    }
}
