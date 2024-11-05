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
    internal class Shotgun: Weapon
    {
        Texture2D attackSprite;
        MouseState mouse;
        private int bullets = 6;
        public Shotgun() 
        {
            projectiles = true;
            mouse = new MouseState();
        }

        public override void LoadContent(ContentManager content)
        {
            attackSprite = content.Load<Texture2D>("laserRed08");
            sprite = content.Load<Texture2D>("tempShotgun");
        }

        public override void UseItem()
        {
            if (bullets > 0)
            {
                Vector2 direction = new Vector2(position.X+(mouse.X),position.Y+mouse.Y);
                Bullet newBullet = new Bullet(attackSprite, position,direction);
                bullets--;
            }
        }
    }
}
