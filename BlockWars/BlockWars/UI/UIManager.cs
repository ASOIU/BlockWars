using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWars.UI
{
    class UIManager
    {
        private Background mBackground;

        public UIManager(SpriteBatch spriteBatch, ContentManager contentManager)
        {
            Texture2D texture = contentManager.Load<Texture2D>("Textures\\UI\\UIBackground");
            mBackground = new Background(texture, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw()
        {
            mBackground.Draw();
        }
    }
}
