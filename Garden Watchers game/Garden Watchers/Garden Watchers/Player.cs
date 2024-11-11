using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    public class Player : Character
    {
        //gun things
        private int bullets;
        private int maxBullets = 6;
        private float timeBetweenBullets = 1f;
        private Texture2D bulletSprite;
        //chainsaw things
        private Texture2D meleeSprite;
        private float timeBetweenAttacks = 0.5f;

        //which weapon is in use
        private bool usingGun = true;

        private float timer;

        MouseState mouseState;

        private float buttonCooldown = 0.3f;
        private float buttonTimer;

        //Constructor

        /// <summary>
        /// The Player construction
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public Player (int health, Vector2 position, float speed)
        {
            Health = health;
            Position = position;
            this.speed = speed;
            bullets = 6;
            GameWorld.PlayerCharacterPosition = Position;
        }

        //Methods

        /// <summary>
        /// Loads textures for the player character into an array
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("temp playercharacter");
            }
            sprite = sprites[0];

            bulletSprite = content.Load<Texture2D>("laserRed08");
            meleeSprite = content.Load<Texture2D>("tempSwipe");

            // base.LoadContent is called to (fx.) set the hitbox
            base.LoadContent(content);
        }

        /// <summary>
        /// Runs methods so that it can be used in GameWorld
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            HandleInput();
            Move(gameTime, screenSize);
            // world board check
            base.Update(gameTime, screenSize);
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            GameWorld.PlayerCharacterPosition= position;
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;   
            buttonTimer+= (float)gameTime.ElapsedGameTime.TotalSeconds;
            mouseState =Mouse.GetState();
        }

        /// <summary>
        /// Reads player input and changes movement speed accordingly
        /// </summary>
        private void HandleInput()
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, +1);
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(+1, 0);
            }

            if (keyState.IsKeyDown(Keys.Q)&&buttonTimer>=buttonCooldown)
            {
                usingGun = !usingGun;
                buttonTimer = 0;
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                UseWeapon();
            }

            if (keyState.IsKeyDown(Keys.R))
            {
                Reload();
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            

        } 

        private void UseWeapon()
        {
            if (usingGun)
            {
                if (bullets > 0 && timer >= timeBetweenBullets)
                {
                    Vector2 direction = new Vector2((Mouse.GetState().Position.X - position.X), (Mouse.GetState().Position.Y - position.Y));
                    double directionSum = Math.Atan2(direction.Y, direction.X);
                    float XDirection = (float)Math.Cos(directionSum);
                    float YDirection = (float)Math.Sin(directionSum);
                    direction = new Vector2(XDirection, YDirection);
                    Bullet bullet1 = new Bullet(bulletSprite, position, direction, true, (float)directionSum, 200);
                    float XDirection2 = (float)Math.Cos(directionSum-0.2);
                    float YDirection2 = (float)Math.Sin(directionSum-0.2);
                    Vector2 direction2 = new Vector2(XDirection2, YDirection2);
                    Bullet bullet2 = new Bullet(bulletSprite, position, direction2, true, (float)directionSum-0.2f, 200);
                    float XDirection3 = (float)Math.Cos(directionSum + 0.2);
                    float YDirection3 = (float)Math.Sin(directionSum + 0.2);
                    Vector2 direction3 = new Vector2(XDirection3, YDirection3);
                    Bullet bullet3 = new Bullet(bulletSprite, position, direction3, true, (float)directionSum + 0.2f, 200);
                    GameWorld.AddedObjects.Add(bullet1);
                    GameWorld.AddedObjects.Add(bullet2);
                    GameWorld.AddedObjects.Add(bullet3);
                    timer = 0;
                    bullets--;
                }
            }
            else
            {
                if (timer >= timeBetweenAttacks)
                {
                    Vector2 direction = new Vector2((Mouse.GetState().Position.X - position.X), (Mouse.GetState().Position.Y - position.Y));
                    double directionSum = Math.Atan2(direction.Y, direction.X);
                    float XDirection = (float)Math.Cos(directionSum);
                    float YDirection = (float)Math.Sin(directionSum);
                    direction = new Vector2(XDirection, YDirection);
                    Vector2 spawnpoint = new Vector2();
                    spawnpoint.Y = position.Y+(direction.Y * sprite.Height);
                    spawnpoint.X = position.X+(direction.X * sprite.Width);
                   
                    MeleeAttack attack = new MeleeAttack(meleeSprite, spawnpoint, direction, true,(float)directionSum);
                    GameWorld.AddedObjects.Add(attack);
                    timer = 0;
                }
            }
        }

        private void Reload()
        {
            bullets = maxBullets;
            //time to reload?
        }
    }
}
