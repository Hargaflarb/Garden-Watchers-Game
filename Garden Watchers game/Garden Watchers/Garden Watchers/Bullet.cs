using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Garden_Watchers
{
    internal class Bullet : GameObject
    {
        private int damage = 2;
        private Vector2 direction;
        private float speed = 200;
        private bool playerBullet;

        public Bullet(Texture2D sprite, Vector2 position,Vector2 direction,bool playerBullet,float rotation,int speed):base()
        { 
            this.sprite = sprite;
            Position = position;
            this.direction = direction;
            this.playerBullet=playerBullet;
            this.rotation = rotation;
            this.speed = speed;
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += (direction * speed)*deltaTime;
            base.Update(gameTime, screenSize);
        }

        public override void OnCollision(GameObject other)
        {
            if(other is Character)
            {
                if ((playerBullet && other is Enemy)||(!playerBullet && other is Player))
                {
                    other.TakeDamage(damage, false);
                    GameWorld.KillObject(this);
                }
            }
        }
    }
}
