using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockWars.UI
{
    class Button:UIControl
    {
        public event EventHandler Click;

        //public bool IsActive { get; private set; }
        private Texture2D mSwitchedOffTexture;
        private Texture2D mActiveTexture;

        public Vector2 mSize;

        private MouseState mPrevMouseState;

        public Button(SpriteBatch spriteBatch,
            Texture2D switchedOffTexture, 
            Texture2D activeTexture)

            : base(spriteBatch)
        {
            IsActive = false;
            mSwitchedOffTexture = switchedOffTexture;
            mActiveTexture = activeTexture;
            mSize.X = mActiveTexture.Width;
            mSize.Y = mActiveTexture.Height;

            mPrevMouseState = new MouseState();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState curMouseState = Mouse.GetState();
            if (curMouseState.X >= Position.X &&
                curMouseState.X <= Position.X + mSize.X &&
                curMouseState.Y >= Position.Y &&
                curMouseState.Y <= Position.Y + mSize.Y)
            {
                if (Visible)
                {
                    IsActive = true;
                }
            }
            else
            {
                IsActive = false;
            }
            if (IsActive &&
                curMouseState.LeftButton == ButtonState.Pressed &&
                mPrevMouseState.LeftButton == ButtonState.Released)
            {
                OnClick();
            }

            mPrevMouseState = curMouseState;

        }

        public override void Draw()
        {
            Texture2D texture;
            if (IsActive)
            {
                texture = mActiveTexture;
            }
            else
            {
                texture = mSwitchedOffTexture;
            }
            if(Visible)
                mSpriteBatch.Draw(texture, Position, Color.White);
        }

        private void OnClick()
        {
            if (Click != null)
            {
                Click(this, EventArgs.Empty);

            }
        }
 
    }
}
