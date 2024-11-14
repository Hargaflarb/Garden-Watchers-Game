using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Garden_Watchers
{
    internal class MeleeAttack:GameObject
    {
        private Vector2 direction;
        private float swipeTime=0.2f;
        private bool playerAttack;
        private float timePassed = 0;
        private int damage = 4;
        public MeleeAttack(Texture2D sprite,Vector2 position, Vector2 direction, bool playerAttack,float rotation)
        {
            this.sprite = sprite;
            Position = position;
            this.direction = direction;
            this.playerAttack = playerAttack;
            this.rotation = rotation - 90;
            origin=new Vector2(sprite.Width/2,sprite.Height/2);
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }

        public override void OnCollision(GameObject other)
        {
            if(other is Character)
            {
                if ((playerAttack && other is Enemy) || (!playerAttack && other is Player))
                {
                    ((Player)other).TakeDamage(damage, true);
                }
            }
            else if (other is Bullet)
            {
                GameWorld.KillObject(other);
            }
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            base.Update(gameTime, screenSize);
            timePassed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timePassed>=swipeTime)
            {
                GameWorld.RemovedObjects.Add(this);
            }
        }
    }
}
