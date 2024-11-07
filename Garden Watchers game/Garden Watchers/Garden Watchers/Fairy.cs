using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class Fairy : Enemy
    {
        public Fairy(int health, Vector2 position, float speed) : base(health, position, speed)
        {
        }
    }
}
