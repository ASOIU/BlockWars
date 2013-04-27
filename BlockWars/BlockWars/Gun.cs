using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame2
{
    class Gun
    {
        private Vector2 mPosition;

        private Box mBaseBox;

        private Box mBarrelBox;

        private World mWorld;

        private bool mShotDone;

        public Gun(World world, Vector2 position)
        {
            mPosition = position;
            mWorld = world;

            Vector2 size = new Vector2(14, 8);
            mBaseBox = new Box(world, position, size, Color.Blue, true);

            size = new Vector2(20, 2);
            mBarrelBox = new Box(world, position, size, Color.Blue, true);

            Filter filter = new Filter();
            filter.maskBits = 0;
            Fixture fixture = mBarrelBox.mBody.GetFixtureList();
            fixture.SetFilterData(ref filter);
            fixture = mBaseBox.mBody.GetFixtureList();
            fixture.SetFilterData(ref filter);
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
            if (state.IsKeyDown(Keys.Space))
            {
                if (!mShotDone)
                {
                    bullet = new Bullet(mWorld, mPosition, 10, 200);
                    bullet.Shot(angle, 1000);
                    mShotDone = true;
                }
            }
            else
            {
                mShotDone = false;
            }
            mBarrelBox.mBody.SetTransform(pos, angle);
            return bullet;
        }
    }
}
