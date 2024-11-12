using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Garden_Watchers
{
    public abstract class Obstacle : GameObject
    {

        public Obstacle(Vector2 pos) : base()
        {
            Position = pos;
        }


        /// <summary>
        /// Activates on collision and makes sure a character can not walk over/inside the obstacle
        /// </summary>
        /// <param name="other">The other obejct that is intersecting with this one.</param>
        public override void OnCollision(GameObject other)
        {
            if (other is Character)
            {
                Vector2 otherPos = ((Character)other).Position;
                Rectangle otherHitbox = ((Character)other).Hitbox;

                Vector2 distance = new Vector2(otherPos.X - position.X, otherPos.Y - position.Y);

                float newXPosition = otherPos.X;
                float newYPosition = otherPos.Y;
                
                if (Math.Abs(distance.X) > Math.Abs(distance.Y))
                { // most likely hit from x-axis
                    if (distance.X < 0)
                    { // hit from the left.
                        newXPosition = (int)position.X - ((Hitbox.Width + otherHitbox.Width) / 2);
                    }
                    else if (otherPos.X - position.X >= 0)
                    { // hit from the right.
                        newXPosition = (int)position.X + ((Hitbox.Width + otherHitbox.Width) / 2);
                    }
                }
                else
                { // most likely hit from y-axis
                    if (distance.Y < 0)
                    { // hit from the top.
                        newYPosition = (int)position.Y - ((Hitbox.Height + otherHitbox.Height) / 2);
                    }
                    else if (otherPos.Y - position.Y >= 0)
                    { // hit from the bottom.
                        newYPosition = (int)position.Y + ((Hitbox.Height + otherHitbox.Height) / 2);
                    }
                }


                ((Character)other).Position = new Vector2(newXPosition, newYPosition);
            }
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {

        }

    }
}
