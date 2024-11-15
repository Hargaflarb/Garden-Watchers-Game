using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Garden_Watchers
{
    /// <summary>
    /// damaging projectile spawned by player & fairies
    /// </summary>
    internal class Bullet : GameObject
    {
        private int damage;
        private Vector2 direction;
        private float speed;
        private bool playerBullet;

        /// <summary>
        /// Instantiates a new Bullet.
        /// </summary>
        /// <param name="sprite">The Bullets sprite.</param>
        /// <param name="position">The position.</param>
        /// <param name="direction">The direction of travel.</param>
        /// <param name="playerBullet">Is the Bullet shot by the player?</param>
        /// <param name="rotation">Rotation of the sprite.</param>
        /// <param name="speed">Speed of the Bullet.</param>
        public Bullet(Texture2D sprite, Vector2 position, Vector2 direction, bool playerBullet, float rotation, int speed) : base()
        {
            this.sprite = sprite;
            Position = position;
            this.direction = direction;
            this.playerBullet = playerBullet;
            this.rotation = rotation;
            this.speed = speed;
            damage = playerBullet ? 4 : 2;
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }

        /// <summary>
        /// Update the Bullets position and damage.
        /// </summary>
        /// <param name="gameTime">The GameTime.</param>
        /// <param name="screenSize">The size of the graphics.</param>
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //move bullet in determined direction
            Position += (direction * speed) * deltaTime;

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

        /// <summary>
        /// Handles collision with Characters.
        /// </summary>
        /// <param name="other">The ohter GameObject.</param>
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
