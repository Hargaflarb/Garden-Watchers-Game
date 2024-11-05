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
        
        public int RecoverHealth() 
        {
                charges--;
                return healthRecovery;
        }

        public void Update()
        {
            if (charges < 1)
            {
                //despawn this gameobject
            }
        }
    }
}
