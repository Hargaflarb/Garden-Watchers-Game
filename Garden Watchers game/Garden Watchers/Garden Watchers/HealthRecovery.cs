using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class HealthRecovery: Item
    {
        private int healthRecovery=5;
        private int charges = 1;
        
        public HealthRecovery(int recoveredHP,int charges)
        {
            healthRecovery = recoveredHP;
            this.charges = charges;
        }
        public int RecoverHealth() 
        {
            charges--;
            return healthRecovery;
        }

        public void Update()
        {
            if (charges < 1)
            {
                GameWorld.RemovedObjects.Add(this);
            }
        }

        public override void UseItem()
        {
            RecoverHealth();
        }
    }
}
