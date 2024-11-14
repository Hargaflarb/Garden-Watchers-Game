using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Garden_Watchers
{
    internal class PitFall : Obstacle
    {
        /// <summary>
        /// Set the position of the instance.
        /// </summary>
        /// <param name="pos">The position.</param>
        public PitFall(Vector2 pos) : base(pos)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("pitfall");
            base.LoadContent(content);
        }

        /// <summary>
        /// Activates on collision and makes sure a character can not walk over/inside the obstacle, unless the player is dashing over it
        /// </summary>
        /// <param name="other">The other obejct that is intersecting with this one.</param>
        public override void OnCollision(GameObject other)
        {
            if (!GameWorld.Player.IsDashing)
            {
                base.OnCollision(other);
            }
        }

    }
}
