using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;
using Microsoft.Xna.Framework;

namespace BlockWars
{
    class Bullet:AGameObject
    {
        private Box mBox;

        private bool mIsActive;

        private bool mIsDestoyed;

        public Body Body
        {
            get { return mBox.mBody; }
        }

        private float mDamageRadius;

        private float mMaxDamageValue;

        public Bullet(World world, Vector2 position, float damageRadius, float maxDamageValue)
        {
            ObjectType = EObjectType.Bullet;
            mDamageRadius = damageRadius;
            mMaxDamageValue = maxDamageValue;
            Vector2 size = new Vector2(1, 1);
            mBox = new Box(world, position, size, Color.Red, true);
            MassData massData = new MassData();
            massData.mass = 1000;
            mBox.mBody.SetMassData(ref massData);
            mIsActive = false;
            mIsDestoyed = false;
            Body.SetUserData(this);
        }

        public override Vector2 GetPosition()
        {
            return Body.Position;
        }

        public override void SetPosition(Vector2 position)
        {
            Body.Position = position;
        }

        public void Draw(PrimitiveRender primitiveRender)
        {
            if (mIsActive)
            {
                mBox.Draw(primitiveRender);
            }
        }

        public void Shot(float angle, float power)
        {
            if (mIsActive)
                return;
            float x, y;
            x = power * (float)Math.Cos(angle);
            y = power * (float)Math.Sin(angle);
            Vector2 impulse = new Vector2(x, y);
            mBox.mBody.SetType(BodyType.Dynamic);
            mBox.mBody.ApplyLinearImpulse(impulse, Vector2.Zero);
            mIsActive = true;
        }

        public void Destroy()
        {
            if (!mIsDestoyed)
            {
                mIsDestoyed = true;
                mBox.Destroy();
            }
        }

        public float GetDamageValue(float distance)
        {
            float damage = 0f;
            if (distance < mDamageRadius)
            {
                damage = (float)(mMaxDamageValue * Math.Sqrt((mDamageRadius - distance) / mDamageRadius));
            }
            return damage;
        }
    }
}
