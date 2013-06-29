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

        private Camera mCamera;

        private string mTexture;

		PlayerData.ObjectType CurrentType;

        private float mHealth;

        public Player mPlayer;

        public Builder(World world, Camera camera)
        {
            mWorld = world;
            mIsActive = false;
            mCamera = camera;
            mTexture = "block3";
        }

        public void SetActivePlayer(Player player)
        {
            mPlayer = player;
        }

        public void BuildingBlock(PlayerData.ObjectType blockType)
        {
			CurrentType = blockType;
            switch (blockType)
            {
				case PlayerData.ObjectType.Block1:
                    {
                        mTexture = "block3";
                        mHealth = 100;
                        break;
                    }
				case PlayerData.ObjectType.Block2:
                    {
                        mTexture = "block2";
                        mHealth = 200;
                        break;
                    }
				case PlayerData.ObjectType.Block3:
                    {
                        mTexture = "block";
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

                if (mPlayer.CheckingBorder(position.X, position.Y, mPlayer.PlayerType))
                {
                    mBuildingBox.mBody.Position = position;
                }

                if (mouseState.LeftButton == ButtonState.Pressed &&
                    mLastButtonState == ButtonState.Released)
                {
                    if (mPlayer.CheckingBorder(position.X, position.Y, mPlayer.PlayerType))
					if (mPlayer.Resources.RemoveResources(CurrentType))
                        {
                            buildingObject = mBuildingBox;
						    CreateBox();
                        }
                    }
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
