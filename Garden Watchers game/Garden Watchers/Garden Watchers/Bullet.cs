using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Garden_Watchers
{
    internal class Bullet : GameObject
    {
        private int damage = 3;
        private Vector2 direction;
        private float speed = 2;
        bool playerBullet;

        public Bullet(Texture2D sprite, Vector2 position,Vector2 direction,bool playerBullet):base()
        { 
            this.sprite = sprite;
            Position = position;
            this.direction = direction;
            this.playerBullet=playerBullet;
            Hitbox = new Rectangle((int)position.X - (sprite.Width / 2), (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
        }


        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            position += direction * speed;
        }

        public override void OnCollision(GameObject other)
        {
            if(other is Character)
            {
                if (playerBullet && other is Enemy)
                {
                    //that enemy takes damage
                    //add this to list of objects to be destroyed
                }
                else if (!playerBullet && other is Player) 
                {
                    //Player takes damage
                    //add this to list of objects to be destroyed
                }
                //potentially add wall collision as well
            }
        }
    }
}
