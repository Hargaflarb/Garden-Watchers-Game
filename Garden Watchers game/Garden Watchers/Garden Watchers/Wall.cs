using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Garden_Watchers
{
    public class Wall : Obstacle
    {
        public Wall(Vector2 pos) : base(pos)
        {

        }

        public Wall(Vector2 pos, Vector2 hitboxSize) : base(pos, hitboxSize)
        {
            Position = pos;
        }

        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Obstacle");
            base.LoadContent(content);
        }


        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);

            if (other is Bullet)
            {
                GameWorld.KillObject(other);
            }
        }

    }
}
