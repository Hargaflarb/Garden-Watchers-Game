﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class MeleeAttack:GameObject
    {
        private Vector2 direction;
        private float swipeTime=0.3f;
        private bool playerAttack;
        private float timePassed = 0;
        public MeleeAttack(Texture2D sprite,Vector2 position, Vector2 direction, bool playerAttack)
        {
            this.sprite = sprite;
            Position = position;
            this.direction = direction;
            this.playerAttack = playerAttack;
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }

        public override void OnCollision(GameObject other)
        {
            if(other is Character)
            {
                if (playerAttack && other is Enemy)
                {
                    //that enemy takes damage
                    GameWorld.RemovedObjects.Add(this);
                }
                else if (!playerAttack && other is Player) 
                {
                    //Player takes damage
                    GameWorld.RemovedObjects.Add(this);
                }
            }
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            timePassed += gameTime.ElapsedGameTime.Milliseconds;
            if (timePassed>=swipeTime)
            {
                GameWorld.RemovedObjects.Add(this);
            }
        }
    }
}
