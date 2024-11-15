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
        protected float invincibilityFrames = 0.3f;
        protected float invincibilityTimer;

        protected float timeExisted = 0;
        protected Vector2 velocity;
        //visual feedback
        protected float rotation;
        protected bool takingDamage = false;
        //animation
        protected int spriteNumber;
        protected bool moving;

        //to avoid having to manually resize & edit sprites & animation frames
        protected float scale = 1;
        protected int framerate = 6;
        protected int frames;
        protected bool facingRight;


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
        /// <summary>
        /// Checks if another GameObject is collieding with it, and calls OnCollision if it is.
        /// Is called for all the GameWorlds object every update.
        /// </summary>
        /// <param name="other">The other GameObject to check for.</param>
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
        /// <param name="other">the other Gameobject in the collision</param>
        public virtual void OnCollision(GameObject other)
        {
            if (this == other)
            {
                return;
            }
        }


        /// <summary>
        /// Makes sure the GameObject cannot go out of bounds & stops it if it tries
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
        /// Abstract method for loading sprites, aswell as setting the objects hitbox and draw origin.
        /// </summary>
        /// <param name="content">The ContentManager the is loading the content.</param>
        public virtual void LoadContent(ContentManager content)
        {
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            //hitbox does not currently change with rotation
            Hitbox = new Rectangle((int)position.X - (int)((sprite.Width / 2)/scale), (int)position.Y - (int)((sprite.Height / 2)/scale), (int)(sprite.Width*scale), (int)(sprite.Height*scale));
            
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
            timeExisted += (float)gameTime.ElapsedGameTime.TotalSeconds;

        }

        /// <summary>
        /// Draws out sprites
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //animation
            if ((this is Player && velocity != Vector2.Zero) || (this is not Player && this is Character))
            {
                sprite = sprites[spriteNumber];
                frames += 1;
                if (frames >= framerate)
                {
                    frames = 0;
                    if (sprites.Length == spriteNumber + 1)
                    {
                        spriteNumber = 0;
                    }
                    else
                    {
                        spriteNumber += 1;
                    }
                }
                
            }            
            //all GameObjects
            if (takingDamage)
            {
                if (facingRight)
                {
                    spriteBatch.Draw(sprite, Position, null, Color.Red, rotation, origin, scale, SpriteEffects.FlipHorizontally, 1);
                }
                else
                {
                    spriteBatch.Draw(sprite, Position, null, Color.Red, rotation, origin, scale, SpriteEffects.None, 1);
                }                
            }
            else
            {
                if (facingRight)
                {
                    spriteBatch.Draw(sprite, Position, null, Color.White, rotation, origin, scale, SpriteEffects.FlipHorizontally, 1);
                }
                else
                {
                    spriteBatch.Draw(sprite, Position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 1);
                }
            }            
        }

        /// <summary>
        /// Makes this GameObject invincible for a set time.
        /// triggered when a Character takes damage from a melee attack, to make sure not all health is lost within a few frames
        /// </summary>
        /// <param name="invincibilityTime">The length of time in ssecond. (is 0 by deafult)</param>
        public virtual void GiveInvincibilityFrames(float invincibilityTime = 0)
        {
            invincibilityTimer = invincibilityTime == 0 ? invincibilityFrames : invincibilityTime;
        }


        /// <summary>
        /// meant for using pickupable health packs, but not currently in use
        /// </summary>
        public virtual void RecoverHealth()
        {
        }
    }
}
