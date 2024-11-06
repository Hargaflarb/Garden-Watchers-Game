using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal abstract class Weapon: Item
    {
        protected int damage;
        protected int range;
        protected bool projectiles;
        //protected float timer;

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            base.Update(gameTime, screenSize);
            Position = CheckOutOfBounds(screenSize, Position);
           // timer += (float)gameTime.TotalGameTime.TotalMilliseconds;

        }
    }
}
