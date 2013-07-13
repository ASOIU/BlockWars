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

        /*private Box mBuildingBox;
        private Gun mBuildingGun;*/
        private AGameObject mBuildingBlock;

        private ButtonState mLastButtonState;

        private Camera mCamera;

        private string mTexture;

        private PlayerData.ObjectType mBuildingObjectType;

        private float mHealth;

        public Player mPlayer;

        private GameObjectCollection mGameObjectCollection;

        public Builder(World world, Camera camera, GameObjectCollection gameObjectCollection)
        {
            mWorld = world;
            mCamera = camera;
            mGameObjectCollection = gameObjectCollection;

            mIsActive = false;
            mTexture = "block3";
            SetBuildingObjectType(PlayerData.ObjectType.Block1);
        }

        public void SetActivePlayer(Player player)
        {
            mPlayer = player;
        }

        public void SetBuildingObjectType(PlayerData.ObjectType blockType)
        {
            mBuildingObjectType = blockType;
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
                case PlayerData.ObjectType.Gun:
                    {
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
            mBuildingBlock = new Box(mWorld, position, size, mTexture, true, mPlayer, mHealth);
        }

        private void CreateGun()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 position = new Vector2(mouseState.X, mouseState.Y);
            mBuildingBlock = new Gun(mWorld, position, mPlayer);
        }

        public void Deactivate()
        {
            if (mIsActive)
            {
                mIsActive = false;
                if (mBuildingBlock != null)
                {
                    mBuildingBlock.Destroy();
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

                if (mPlayer.CheckBorder(position.X, position.Y, mPlayer.PlayerType))
                {
                    mBuildingBlock.SetPosition(position);
                }

                if (mouseState.LeftButton == ButtonState.Pressed &&
                    mLastButtonState == ButtonState.Released)
                {
                    if (IsLegalPosition())
                    {
                        if (mPlayer.CheckBorder(position.X, position.Y, mPlayer.PlayerType))
                        {
                        }
                    }
                }
                mLastButtonState = mouseState.LeftButton;
            }
            return buildingObject;
        }

        private bool IsLegalPosition()
        {
            EdgeShape shape =  new EdgeShape();
             
            bool isLegal = false;
            if (mBuildingObjectType == PlayerData.ObjectType.Gun)
            {
                isLegal = true;
            }
            else
            {
                isLegal = true;
                List<Box> boxes = mGameObjectCollection.Boxes;
                Body buildingBlockBody = ((Box) mBuildingBlock).mBody;
                Shape shape1 = buildingBlockBody.GetFixtureList().GetShape();
                Transform transform1, transform2;
                buildingBlockBody.GetTransform(out transform1);
                for (int i = 0; i < boxes.Count; i++)
                {
                    Body body = boxes[i].mBody;
                    Shape shape2 = body.GetFixtureList().GetShape();
                    body.GetTransform(out transform2);
                    bool isOverlap = AABB.TestOverlap(shape1, 0, shape2, 0, ref transform1, ref transform2);
                    if (isOverlap)
                    {
                        isLegal = false;
                        break;
                    }
                }
            }
            return isLegal;
        }

        public void Draw(PrimitiveRender primitiveRender, SpriteBatch spriteBatch)
        {
            if (mIsActive)
            {
                mBuildingBlock.Draw(primitiveRender);
            }
        }
    }
}
