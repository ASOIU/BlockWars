using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2D.XNA;
using Microsoft.Xna.Framework;

namespace BlockWars
{
    class Box : AGameObject
    {
        public Vector2 mSize;

        public Body mBody;

        private bool mIsDestroyed;

        private World mWorld;

        public float mHealth;

        public string Texture
        {
            get { return mTexture; }
        }

        private string mTexture;

        public Box(World world, Vector2 position, Vector2 size, string texture, bool isStatic, float health = 100)
        {
            ObjectType = EObjectType.Box;
            mHealth = health;
            mIsDestroyed = false;
            mSize = size;
            mWorld = world;
            mTexture = texture;
            PolygonShape polygonShape = new PolygonShape();
            polygonShape.SetAsBox(size.X / 2f, size.Y / 2f);

            BodyDef bodyDef = new BodyDef();
            bodyDef.position = position;
            bodyDef.bullet = true;
            if (isStatic)
            {
                bodyDef.type = BodyType.Static;
            }
            else
            {
                bodyDef.type = BodyType.Dynamic;
            }
            mBody = world.CreateBody(bodyDef);

            FixtureDef fixtureDef = new FixtureDef();
            fixtureDef.shape = polygonShape;//Форма
            fixtureDef.density = 0.1f;//Плотность
            fixtureDef.friction = 0.3f;//Сила трения
            fixtureDef.restitution = 0f;//Отскок

            mBody.CreateFixture(fixtureDef);
            MassData data = new MassData();
            data.mass = 0.01f;
            mBody.SetUserData(this);
        }


        public override Vector2 GetPosition()
        {
            return mBody.Position;
        }

        public override void SetPosition(Vector2 position)
        {
            mBody.Position = position;
        }

        public void Draw(PrimitiveRender primitiveRender)
        {
            if (!mIsDestroyed)
            {
                Transform transform;
                mBody.GetTransform(out transform);
                //primitiveRender.DrawRectangle(transform, mSize.X, mSize.Y, mColor);
                primitiveRender.DrawSprite(transform, mSize.X, mSize.Y, mTexture);
            }
        }

        public void Destroy()
        {
            if (!mIsDestroyed)
            {
                mIsDestroyed = true;
                mWorld.DestroyBody(mBody);
                mBody = null;
            }
        }

        public void ApplyDamage(float damageValue)
        {
            mHealth -= damageValue;
        }
    }
}
