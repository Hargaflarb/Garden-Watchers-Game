using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Garden_Watchers
{
    public class Wall : Obstacle
    {
        /// <summary>
        /// Set the position of the instance.
        /// </summary>
        /// <param name="pos">The position.</param>
        public Wall(Vector2 pos) : base(pos)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Obstacle");
            base.LoadContent(content);
        }

        /// <summary>
        /// Activates on collision and makes sure a character can not walk over/inside the obstacle.
        /// If bullets collide they are removed.
        /// </summary>
        /// <param name="other">The other obejct that is intersecting with this one.</param>
        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);

            if (other is Bullet)
            {
                GameWorld.KillObject(other);
            }
        }

    }
}
