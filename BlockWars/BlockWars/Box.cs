using Box2D.XNA;
using Microsoft.Xna.Framework;

namespace BlockWars
{
    class Box
    {
        public Vector2 mSize;

        private Color mColor;

        public Body mBody;

        private bool mIsDestroyed;

        private World mWorld;

        public float mHealth;

        public Box(World world, Vector2 position, Vector2 size, Color color, bool isStatic)
        {
            mHealth = 100;
            mIsDestroyed = false;
            mSize = size;
            mColor = color;
            mWorld = world;

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

        public void Draw(PrimitiveRender primitiveRender)
        {
            if (!mIsDestroyed)
            {
                Transform transform;
                mBody.GetTransform(out transform);
                //primitiveRender.DrawRectangle(transform, mSize.X, mSize.Y, mColor);
                primitiveRender.DrawSprite(transform, mSize.X, mSize.Y, "block");
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
