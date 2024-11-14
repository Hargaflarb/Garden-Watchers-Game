using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Garden_Watchers
{
    internal class Bullet : GameObject
    {
        private int damage;
        private Vector2 direction;
        private float speed;
        private bool playerBullet;
        private float timeExisted;

        public Bullet(Texture2D sprite, Vector2 position, Vector2 direction, bool playerBullet, float rotation, int speed) : base()
        {
            this.sprite = sprite;
            Position = position;
            this.direction = direction;
            this.playerBullet = playerBullet;
            this.rotation = rotation;
            this.speed = speed;
            damage = playerBullet ? 4 : 2;
            timeExisted = 0;
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += (direction * speed) * deltaTime;

            timeExisted += deltaTime;
            if (playerBullet)
            {
                if (timeExisted <= 0.5f)
                {
                    damage = 4;
                }
                else if (timeExisted <= 1f)
                {
                    damage = 3;
                }
                else
                {
                    damage = 2;
                }
            }


            base.Update(gameTime, screenSize);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Character)
            {
                if ((playerBullet && other is Enemy) || (!playerBullet && other is Player))
                {
                    ((Character)other).TakeDamage(damage, false);

                    if (!(other is Player & GameWorld.Player.IsDashing))
                    {
                        GameWorld.KillObject(this);
                    }
                }
            }
        }
    }
}
