using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal abstract class Item: GameObject
    {
        protected bool pickedUp;

        public override void OnCollision(GameObject other)
        {
            if(other is Player)
            {
                PickUp();
            }
        }

        public override void LoadContent()
        {
            //load kode
        }

        public override void Update()
        {
            //update kode
        }
        public void PickUp()
        {
            //pickup code
        }

        public virtual void Use()
        {
            if (pickedUp) { }
        }
    }
}
