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
        
        public HealthRecovery(Vector2 position)
        {
            Position = position;
        }
        
        

        public void Update()
        {
           
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("healthItem");
            }

            sprite = sprites[0];

            base.LoadContent(content);
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
            if (other is Player)
            {
                if (GameWorld.Player.Health < 10)
                {
                    ((Player)other).RecoverHealth();
                    GameWorld.KillObject(this);
                }
            }
        }
    }
}
