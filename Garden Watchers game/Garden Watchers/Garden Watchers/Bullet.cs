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

        public Bullet(Texture2D sprite, Vector2 position,Vector2 direction)
        { 
<<<<<<< Updated upstream
            
        }

=======
            this.sprite = sprite;
            this.position = position;
            this.direction = direction;
        }


>>>>>>> Stashed changes
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            position += direction * speed;
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void OnCollision(GameObject other)
        {
            
        }
    }
}
