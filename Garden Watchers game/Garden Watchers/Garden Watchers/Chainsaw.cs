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
    internal class Chainsaw: Weapon
    {
        private float timeBetweenAttacks=0.5f;
        private Texture2D attackSprite;
        
        public Chainsaw(Vector2 position,bool pickedUp) 
        { 
            this.position = position;
            this.pickedUp = pickedUp;
            projectiles = false;            
        }

        public override void LoadContent(ContentManager content)
        {
            attackSprite = content.Load<Texture2D>("tempSwipe");
            sprite = content.Load<Texture2D>("tempChainsaw");
            base.LoadContent(content);
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }

        public override void UseItem()
        {
            if (true)
            {
                Vector2 direction = new Vector2(position.X + (Mouse.GetState().Position.X - (GameWorld.ScreenSize.X / 2)), position.Y + (Mouse.GetState().Position.Y - (GameWorld.ScreenSize.Y / 2)));
                MeleeAttack attack = new MeleeAttack(attackSprite, GameWorld.PlayerCharacterPosition, direction, true);
                GameWorld.AddedObjects.Add(attack);
                //timer = 0;
            }
        }
    }
}
