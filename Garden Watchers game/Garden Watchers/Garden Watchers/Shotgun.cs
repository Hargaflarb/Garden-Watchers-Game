using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private float timeBetweenBullets = 0.2f;
        private float timer;
        public Shotgun(int bullets, Vector2 position,bool pickedUp) : base()
        {
            projectiles = true;    
            this.bullets = bullets;
            this.position = position;
            this.pickedUp = pickedUp;            
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            base.Update(gameTime, screenSize);
            this.timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

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
            Debug.WriteLine(timer);
            if (bullets > 0&& timer>=timeBetweenBullets)
            {                                
                Vector2 direction = new Vector2((Mouse.GetState().Position.X-position.X),(Mouse.GetState().Position.Y-position.Y));
                double test=Math.Atan2 (direction.Y, direction.X);
                float XDirection=(float)Math.Cos(test);
                float YDirection= (float)Math.Sin(test);
                direction = new Vector2(XDirection,YDirection);
                Bullet newBullet = new Bullet(attackSprite, GameWorld.PlayerCharacterPosition, direction,true);
                GameWorld.AddedObjects.Add(newBullet);
                timer = 0;
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
