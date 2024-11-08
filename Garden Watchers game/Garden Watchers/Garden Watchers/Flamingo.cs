using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class Flamingo : Enemy, IChase
    {
        // *boom!*
        public Flamingo(int health, Vector2 position, float speed) : base(health, position, speed)
        {
        }

        public void Chase()
        {
            
        }
    }
}
