using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class Chainsaw: Weapon
    {
        private Texture2D attackSprite;
        
        MouseState mouse;
        public Chainsaw(Vector2 position,bool pickedUp) 
        { 
            this.position = position;
            this.pickedUp = pickedUp;
            projectiles = false;
            mouse = new MouseState();
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void UseItem()
        {
            
        }
    }
}
