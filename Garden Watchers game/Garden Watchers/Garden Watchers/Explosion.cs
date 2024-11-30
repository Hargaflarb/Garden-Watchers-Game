using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden_Watchers
{
    internal class Explosion : GameObject
    {
        //Field
        private float timer;
        private int damage = 10;
        private SoundEffect kaboom;

        public Explosion(Vector2 position)
        {
            Position = position;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("explosion");
            }
            sprite = sprites[0];
            kaboom = content.Load<SoundEffect>("explosion sfx");
            kaboom.Play();
            base.LoadContent(content);
        }

        /// <summary>
        /// explosion lasts certain amount of time, then disappears
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="screenSize"></param>
        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= 0.5f)
            {
                GameWorld.KillObject(this);
            }
            base.Update(gameTime, screenSize);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                ((Player)other).TakeDamage(damage, true);
            }
            base.OnCollision(other);
        }
    }
}
