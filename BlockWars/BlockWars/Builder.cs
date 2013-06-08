using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlockWars.Gameplay;

namespace BlockWars
{
    class Builder
    {
        private World mWorld;

        public bool mIsActive;

        private Box mBuildingBox;

        private ButtonState mLastButtonState;

        public int mBoxLast;

        private Camera mCamera;

        private string mTexture;

        private float mHealth;

        private Player mPlayer;

        public Builder(World world, Camera camera, Player player)
        {
            mWorld = world;
            mIsActive = false;
            mCamera = camera;
            mTexture = "block2";
            mPlayer = player;
        }

        public void BuildingBlock(EBlockType blockType)
        {
            switch (blockType)
            {
                case EBlockType.Light:
                    {
                        mTexture = "block2";
                        mHealth = 100;
                        break;
                    }
                case EBlockType.Normal:
                    {
                        mTexture = "block";
                        mHealth = 200;
                        break;
                    }
                case EBlockType.Hard:
                    {
                        mTexture = "block3";
                        mHealth = 300;
                        break;
                    }
                default:
                    break;
            }
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
            mBuildingBox = new Box(mWorld, position, size, mTexture, true, mPlayer, mHealth);
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
            }
        }
    }
}
