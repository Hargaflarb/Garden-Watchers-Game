using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Garden_Watchers
{
    public abstract class GameObject
    {
        //Fields
        protected Texture2D sprite;
        protected Texture2D[] sprites;
        private Rectangle hitbox;
        protected Vector2 position;
        protected Vector2 origin;
        protected float rotation;
        protected float invincibilityFrames = 0.3f;
        protected float invincibilityTimer;
        protected bool takingDamage = false;
        protected Vector2 velocity;

        /// <summary>
        /// The Hitbox is a rectangle the surounds the game objects sprite, and is used to detect colission.
        /// </summary>
        public Rectangle Hitbox { get => hitbox; protected set => hitbox = value; }

        /// <summary>
        /// The Position is at the center of the gameObject and determines where the object is on the in the game
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set { position = value; Hitbox = new Rectangle((int)value.X - (Hitbox.Width / 2), (int)value.Y - (Hitbox.Height / 2), Hitbox.Width, Hitbox.Height); }
        }

        //Methods
        public void CheckCollision(GameObject other)
        {
            if (Hitbox.Intersects(other.Hitbox))
            {
                OnCollision(other);
            }
        }

        /// <summary>
        /// Runs when GameObject is colliding with something else
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(GameObject other)
        {
            if (this == other)
            {
                return;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenSize"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public virtual Vector2 CheckOutOfBounds(Vector2 screenSize, Vector2 vector)
        {
            bool hitWall = false;
            float X = vector.X;
            float Y = vector.Y;
            if (vector.Y > screenSize.Y - Hitbox.Height / 2)
            {
                Y = screenSize.Y - Hitbox.Height / 2;
                hitWall = true;
                if(this is Bullet)
                {
                    GameWorld.KillObject(this);
                }
            }
            else if (vector.Y < 0 + Hitbox.Height / 2)
            {
                Y = 0 + Hitbox.Height / 2;
                hitWall = true;
                if (this is Bullet)
                {
                    GameWorld.KillObject(this);
                }
            }

            if (vector.X > screenSize.X - Hitbox.Width / 2)
            {
                X = screenSize.X - Hitbox.Width / 2;
                hitWall = true;
                if (this is Bullet)
                {
                    GameWorld.KillObject(this);
                }
            }
            else if (vector.X < 0 + Hitbox.Width / 2)
            {
                X = 0 + Hitbox.Width / 2;
                hitWall = true;
                if (this is Bullet)
                {
                    GameWorld.KillObject(this);
                }
            }
            if (hitWall)
            {
                velocity = Vector2.Zero;
            }
            return new Vector2(X, Y);
        }
        /// <summary>
        /// Abstract method for loading sprites
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            //hitbox does not currently change with rotation
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
            
        }


        /// <summary>
        /// Abstract method for updating
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        public virtual void Update(GameTime gameTime, Vector2 screenSize)
        {
            Position = CheckOutOfBounds(screenSize, Position);
            
            invincibilityTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (invincibilityTimer <= 0)
            {
                takingDamage = false;
            }
        }

        /// <summary>
        /// Draws out sprites
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (takingDamage)
            {
                spriteBatch.Draw(sprite, Position, null, Color.Red, rotation, origin, 1, SpriteEffects.None, 1);
            }
            else
            {
                spriteBatch.Draw(sprite, Position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 1);
            }            
        }

        public virtual void TakeDamage(int damage, bool isMeleeAttack)
        {
            //this is empty on purpose, as to not activate on non-characters
        }


        public virtual void GiveInvincibilityFrames(float invincibilityTime = 0)
        {
            invincibilityTimer = invincibilityTime == 0 ? invincibilityFrames : invincibilityTime;
        }
      
        public virtual void RecoverHealth()
        {
         

        }
    }
}
