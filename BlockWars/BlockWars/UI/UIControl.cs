using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWars.UI
{
    abstract class UIControl
    {
        public Vector2 Position { get; set; }

        public bool Visible { get; set; }

        public bool Enable { get; set; }

        public bool IsActive { get; protected set; }

        protected SpriteBatch mSpriteBatch;

        public UIControl(SpriteBatch spriteBatch)
        {
            Position = Vector2.Zero;
            Visible = true;
            Enable = true;
            mSpriteBatch = spriteBatch;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw();
    }
}