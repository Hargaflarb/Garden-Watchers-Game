using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Garden_Watchers
{
    internal class PitFall : Obstacle
    {
        public PitFall(Vector2 pos) : base(pos)
        {

        }

        public PitFall(Vector2 pos, Vector2 hitboxSize) : base(pos, hitboxSize)
        {
            Position = pos;
        }



        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("pitfall");
            base.LoadContent(content);
        }


        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }

    }
}
