using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BlockWars.Gameplay;

namespace BlockWars
{
    class Gun : AGameObject
    {

        public Vector2 mPosition;

        private Box mBaseBox;

        private Box mBarrelBox;

        private World mWorld;

        private bool mShotDone;

        public bool IsActive
        {
            get
            {
                return mIsActive;
            }
            set
            {
                mIsActive = value;
                CurrentMagazine = mMagazineSize;
            }
        }

        private bool mIsActive;

        private int mMagazineSize = 3;

        public int CurrentMagazine
        {
            get;
            set;
        }

        private Player mPlayer;
        private Filter mFilter;

        public Gun(World world, Vector2 position, Player player)
        {
            ObjectType = EObjectType.Gun;
            mPosition = position;
            mWorld = world;
            mPlayer = player;

            Vector2 size = new Vector2(10, 8);
            mBaseBox = new Box(world, position, size, "gun", true, player);


            size = new Vector2(10, 2);
            mBarrelBox = new Box(world, position, size, "barrel", true, player);

            Filter filter = new Filter();
            filter.maskBits = 0;
            Fixture fixture = mBarrelBox.mBody.GetFixtureList();
            fixture.SetFilterData(ref filter);
            fixture = mBaseBox.mBody.GetFixtureList();
            fixture.SetFilterData(ref filter);

            mIsActive = false;

            mFilter = new Filter();
            if (mPlayer.PlayerType == EntityCategory.Player2)
                mFilter.maskBits = (ushort)(EntityCategory.Player1);
            else
                mFilter.maskBits = (ushort)(EntityCategory.Player2);
            mFilter.categoryBits = (ushort)mPlayer.PlayerType;
            CurrentMagazine = mMagazineSize;
        }

        public override Vector2 GetPosition()
        {
            return mBaseBox.mBody.Position;
        }

        public override void SetPosition(Vector2 position)
        {
            mBaseBox.mBody.Position = position;
            mBarrelBox.mBody.Position = position;
        }

        public void Draw(PrimitiveRender primitiveRender)
        {
            mBaseBox.Draw(primitiveRender);
            mBarrelBox.Draw(primitiveRender);
        }

        public Bullet Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 pos = mBarrelBox.mBody.GetPosition();
            float angle = mBarrelBox.mBody.GetAngle();
            float da = 0.01f;
            if (state.IsKeyDown(Keys.Left))
            {
                angle += da;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                angle -= da;
            }
            Bullet bullet = null;
            if (state.IsKeyDown(Keys.Space) && (CurrentMagazine != 0))
            {
                if (!mShotDone && IsActive)
                {
                    bullet = new Bullet(mWorld, mPosition, 10, 200, mPlayer);
                    var fixture = bullet.Body.GetFixtureList();
                    fixture.SetFilterData(ref mFilter);

                    bullet.Shot(angle, 1000);
                    mShotDone = true;
                    CurrentMagazine--;
                }
            }
            else
            {
                mShotDone = false;
            }

            if (CurrentMagazine == 0)
            {
                mIsActive = false;
            }
            mBarrelBox.mBody.SetTransform(pos, angle);
            return bullet;
        }


    }
}
