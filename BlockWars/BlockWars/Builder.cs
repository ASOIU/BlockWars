using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame2
{
    class Builder
    {
        private World mWorld;

        private bool mIsActive;

        private Box mBuildingBox;

        private ButtonState mLastButtonState;

        private int mBoxLast;

        private SpriteFont mFont;

        private Camera mCamera;

        public Builder(World world, Camera camera, ContentManager contentManager)
        {
            mWorld = world;
            mIsActive = false;
            mFont = contentManager.Load<SpriteFont>("Font");
            mCamera = camera;
        }

        public void Activate()
        {
            if (!mIsActive)
            {
                mBoxLast = 100;
                mIsActive = true;
                CreateBox();
            }
        }

        private void CreateBox()
        {
            Vector2 size = new Vector2(6, 3);
            MouseState mouseState = Mouse.GetState();
            Vector2 position = new Vector2(mouseState.X, mouseState.Y);
            mBuildingBox = new Box(mWorld, position, size, Color.Green, true);
        }

        public void Deactivate()
        {
            if (mIsActive)
            {
                mIsActive = false;
                if (mBuildingBox != null)
                {
                    mBuildingBox.Destroy();
                }
            }
        }

        public object Update(GameTime gameTime)
        {
            object buildingObject = null;
            if (mIsActive)
            {
                MouseState mouseState = Mouse.GetState();
                Vector2 position = new Vector2(mouseState.X, mouseState.Y);

                position = mCamera.ConvertScreenToWorld(position);

                int grid = 3;
                position.X = (int)(position.X / grid) * grid;
                position.Y = (int)(position.Y / grid) * grid;

                mBuildingBox.mBody.Position = position;
                if (mouseState.LeftButton == ButtonState.Pressed &&
                    mLastButtonState == ButtonState.Released)
                {
                    buildingObject = mBuildingBox;
                    if (mBoxLast > 0)
                    {
                        CreateBox();
                        mBoxLast--;
                    }
                }
                if (mBoxLast == 0)
                {
                    Deactivate();
                }
                mLastButtonState = mouseState.LeftButton;
            }
            return buildingObject;
        }


        public void Draw(PrimitiveRender primitiveRender, SpriteBatch spriteBatch)
        {
            if (mIsActive)
            {
                mBuildingBox.Draw(primitiveRender);
                string boxLastText = "Box Last: " + mBoxLast;
                Vector2 pos = new Vector2(0, 0);
                spriteBatch.DrawString(mFont, boxLastText, pos, Color.Black);
            }
        }
    }
}
