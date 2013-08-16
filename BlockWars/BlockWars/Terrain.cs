using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Box2D.XNA;

namespace BlockWars
{
    class Terrain : AGameObject
    {
        World mWorld;
        float[] mTerrainBaseVertexs;
        Vector2[] mTerrainDrawVertexs;
        int mScale = 25;
        int mCountBaseVertexs = 7; //-3
        int mAmplitude = 800;
        BodyDef mBodyDef;
        Body mBody;
        public Terrain(World world)
        {
            mWorld = world;
            mBodyDef = new BodyDef();
            mBody = world.CreateBody(mBodyDef);
            Generate();
            GenerateDrawVertexs();
        }

        private void GenerateDrawVertexs()
        {
            Vector2[] vertexs = new Vector2[mCountBaseVertexs * 10];
            for (int i = 1; i < mCountBaseVertexs - 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    float x = (i - 1) * 10 * mScale + j * mScale;
                    float y = CubicInterpolate(
                              mTerrainBaseVertexs[i - 1],
                              mTerrainBaseVertexs[i],
                              mTerrainBaseVertexs[i + 1],
                              mTerrainBaseVertexs[i + 2], j / 10f);
                    vertexs[i * 10 + j] = new Vector2(x, y);
                }
            }
            int count = (mCountBaseVertexs - 3) * 10;
            mTerrainDrawVertexs = new Vector2[count + 3];
            Array.Copy(vertexs, 10, mTerrainDrawVertexs, 0, count);
            for (int i = 0; i < count; i++)
            {
                mTerrainDrawVertexs[i].X -= ((mCountBaseVertexs - 4) * 10 * mScale + 9 * mScale) / 2f;
                mTerrainDrawVertexs[i].Y -= 50;
            }
            float y1 = -200;
            mTerrainDrawVertexs[count] = new Vector2(mTerrainDrawVertexs[count - 1].X, y1);
            mTerrainDrawVertexs[count + 1] = new Vector2(mTerrainDrawVertexs[0].X, y1);
            mTerrainDrawVertexs[count + 2] = mTerrainDrawVertexs[0];

            for (int i = 0; i < count - 1; i++)
            {
                PolygonShape shape = new PolygonShape();
                shape.SetAsEdge(mTerrainDrawVertexs[i], mTerrainDrawVertexs[i + 1]);
                mBody.CreateFixture(shape, 0.0f);
            }
        }

        private void Generate()
        {
            Random r = new Random();
            mTerrainBaseVertexs = new float[mCountBaseVertexs];
            for (int i = 0; i < mCountBaseVertexs; i++)
            {
                mTerrainBaseVertexs[i] = r.Next(0, mAmplitude) / 10f;
            }
        }

        float CubicInterpolate(double y0, double y1, double y2, double y3, double mu)
        {
            double a0, a1, a2, a3, mu2;

            mu2 = mu * mu;
            a0 = y3 - y2 - y0 + y1;
            a1 = y0 - y1 - a0;
            a2 = y2 - y0;
            a3 = y1;

            return (float)(a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3);
        }
        public override void Draw(PrimitiveRender primitiveRender)
        {
            int count = (mCountBaseVertexs - 3) * 10 + 3;
            primitiveRender.DrawLineList(mTerrainDrawVertexs, count, Color.White);
        }

        public float GetHeight(float x)
        {
            for (int i = 0; i < (mCountBaseVertexs - 3) * 10-1; i++)
            {
                if (x>mTerrainDrawVertexs[i].X&&x<mTerrainDrawVertexs[i+1].X)
                {
                    float c = x - mTerrainDrawVertexs[i].X;
                    c *= 0.04f;
                    return Vector2.Lerp(mTerrainDrawVertexs[i], mTerrainDrawVertexs[i + 1], c).Y;
                }
            }
            throw new ArgumentException();
        }

        public override void Destroy()
        {
            mWorld.DestroyBody(mBody);
        }
        public override Vector2 GetPosition()
        {
            return Vector2.Zero;
        }
        public override void SetPosition(Vector2 position)
        {
            throw new InvalidOperationException();
        }
    }
}
