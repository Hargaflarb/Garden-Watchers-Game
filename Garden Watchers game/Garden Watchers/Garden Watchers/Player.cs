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
    internal class Player : Character
    {



        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("temp_playercharacter");
=======
            sprites = new Texture2D[1];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>("temp_playercharacter");
            }
            sprite = sprites[0];
>>>>>>> 87145a8fc06eefa9f9e91f67d718fed6823fc7ee
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            HandleInput();
            Move(gameTime, screenSize);
        }

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

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
        } 
    }
}
