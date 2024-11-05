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
    internal class Shotgun: Weapon
    {
        Texture2D attackSprite;
        private int bullets;
        private int maxBullets=6;
        public Shotgun(int bullets, Vector2 position,bool pickedUp) : base()
        {
            projectiles = true;    
            this.bullets = bullets;
            this.position = position;
            this.pickedUp = pickedUp;            
        }

        public override void LoadContent(ContentManager content)
        {
            attackSprite = content.Load<Texture2D>("laserRed08");
            sprite = content.Load<Texture2D>("tempShotgun");
            base.LoadContent(content);
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }

        public override void UseItem()
        {
            if (bullets > 0)
            {                
                Vector2 direction = new Vector2(position.X+(Mouse.GetState().Position.X-(GameWorld.ScreenSize.X/2)),position.Y+(Mouse.GetState().Position.Y-(GameWorld.ScreenSize.Y/2)));
                Bullet newBullet = new Bullet(attackSprite, position,direction,true);
                GameWorld.AddedObjects.Add(newBullet);
                bullets--;
            }
        }

        public void Reload(int moreBullets)
        {
            if (bullets + moreBullets <= maxBullets)
            {
                bullets = bullets + moreBullets;
            }
            else
            {
                bullets = maxBullets;
            }
        }
    }
}
