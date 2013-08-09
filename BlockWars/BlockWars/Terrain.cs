using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BlockWars
{
	class Terrain:AGameObject
	{
		float[] mTerrainBaseVertexs;
		Vector2[] mTerrainDrawVertexs;
		int mScale = 25;
		public Terrain()
		{
			Generate();
			GenerateDrawVertexs();
		}

		private void GenerateDrawVertexs()
		{
			mTerrainDrawVertexs = new Vector2[70];
			for (int i = 1; i < 5; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					mTerrainDrawVertexs[i * 10 + j] = new Vector2((i-1) * 10*mScale + j*mScale, CubicInterpolate(mTerrainBaseVertexs[i-1], mTerrainBaseVertexs[i], mTerrainBaseVertexs[i + 1], mTerrainBaseVertexs[i + 2], j / 10f));
				}
			}
            for (int i = 0; i < 70; i++)
            {
                mTerrainDrawVertexs[i].X -= 500;
                mTerrainDrawVertexs[i].Y -= 50;
            }
		}

		private void Generate()
		{
			Random r = new Random();
			mTerrainBaseVertexs = new float[7];
			for (int i = 0; i < 7; i++)
			{
				mTerrainBaseVertexs[i] = r.Next(0, 800) / 10f;
			}
		}

		float CubicInterpolate(double y0, double y1,double y2, double y3,double mu)
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
			Vector2[] Vertexs = new Vector2[43];
			Array.Copy(mTerrainDrawVertexs, 10, Vertexs, 0, 40);
            float y = -200;
            Vertexs[40] = new Vector2(Vertexs[39].X, y);
            Vertexs[41] = new Vector2(Vertexs[0].X, y);
            Vertexs[42] = Vertexs[0];
			primitiveRender.DrawLineList(Vertexs, 43, Color.White);
		}
		public override void Destroy()
		{
			throw new NotImplementedException();
		}
		public override Vector2 GetPosition()
		{
			throw new NotImplementedException();
		}
		public override void SetPosition(Vector2 position)
		{
			throw new NotImplementedException();
		}
	}
}
