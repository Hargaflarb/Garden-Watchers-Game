using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal abstract class GameObject
    {
        protected Texture2D sprite;
        public virtual void OnCollision(GameObject other)
        {

        }

        public abstract void LoadContent();

        public abstract void Update();
    }
}
