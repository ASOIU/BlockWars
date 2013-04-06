using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWars.UI
{
    class Background : UIControl
    {
        private Texture2D mTexture;

        public Background(Texture2D texture, SpriteBatch spriteBatch)
            : base(spriteBatch)
        {
            mTexture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            //Do nothing
        }

        public override void Draw()
        {
            if (Visible)
            {
                Vector2 scale = new Vector2(800 / 1920f, 480 / 1080f);
                mSpriteBatch.Draw(mTexture, Position, null, Color.White,
                    0f, Vector2.Zero, scale, SpriteEffects.None, 1);
            }
        }
    }
}
