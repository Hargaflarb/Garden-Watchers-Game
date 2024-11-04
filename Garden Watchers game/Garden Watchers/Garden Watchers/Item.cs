using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Garden_Watchers
{
    internal abstract class Item: GameObject
    {
        protected bool pickedUp;


        protected override void Initialize()
        {

        }


        public override void LoadContent(ContentManager content)
        {
            //load kode
        }

        public override void Update(GameTime gameTime, Vector2 screenSize)
        {
            //update kode
        }
        public override void OnCollision(GameObject other)
        {
            if(other is Player)
            {
                PickUp();
            }
        }
        public void PickUp()
        {
            //pickup code
        }

        public virtual void Use()
        {
            if (pickedUp) { }
        }
    }
}
