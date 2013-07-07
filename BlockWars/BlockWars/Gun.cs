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

        private bool mIsDestroyed;

        public bool IsActive
        {
            get
            {
                return mIsActive;
            }
            set
            {
                mIsActive = value;
            }
        }

        private bool mIsActive;

        public int mMagazineSize = 5;

        public List<int> CurrentMagazine
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
            CurrentMagazine = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                AddBulletToMagazine(1);
            }

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

        public override void Draw(PrimitiveRender primitiveRender)
        {
            mBaseBox.Draw(primitiveRender);
            mBarrelBox.Draw(primitiveRender);
        }

        public override void Destroy()
        {
            if (!mIsDestroyed)
            {
                mIsDestroyed = true;
                mWorld.DestroyBody(mBaseBox.mBody);
                mBaseBox.mBody = null;
                mWorld.DestroyBody(mBarrelBox.mBody);
                mBarrelBox.mBody = null;
            }
        }

        public bool AddBulletToMagazine(int bulletType)
        {
            if (CurrentMagazine.Count == mMagazineSize)
            {
                return false;
            }
            CurrentMagazine.Add(bulletType);
            return true;
        }

        public Bullet Update(GameTime gameTime)
        {
            if (!IsActive)
            {
                return null;
            }
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
            if (state.IsKeyDown(Keys.Space) && (CurrentMagazine.Count != 0))
            {
                if (!mShotDone && IsActive)
                {
                    bullet = new Bullet(mWorld, mPosition, 200, mPlayer, CurrentMagazine[0]);
                    var fixture = bullet.Body.GetFixtureList();
                    fixture.SetFilterData(ref mFilter);

                    bullet.Shot(angle, 1000);
                    mShotDone = true;
                    CurrentMagazine.RemoveAt(0); ;
                }
            }
            else
            {
                mShotDone = false;
            }

            if (CurrentMagazine.Count == 0)
            {
                mIsActive = false;
            }
            mBarrelBox.mBody.SetTransform(pos, angle);
            return bullet;
        }


    }
}
