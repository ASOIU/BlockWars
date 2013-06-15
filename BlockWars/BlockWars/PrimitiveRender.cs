using System;
using System.Collections.Generic;
using Box2D.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWars
{
    class PrimitiveRender
    {
        private GraphicsDevice mGraphicsDevice;

        private VertexPositionColor[] mVerteces;
        
        private VertexPositionColor[] mBufferVerteces;

        private Vector2[] mPointBuffer;

        private int mVertecesCount;

        private SpriteBatch mSpriteBatch;

        private Dictionary<string, Texture2D> mTextures;

        private Camera mCamera;

        public PrimitiveRender(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, ContentManager contentManager, Camera camera)
        {
            mCamera = camera;
            const int initalCapacity = 10240;//Начальный размер массива
            mVertecesCount = 0;
            mVerteces = new VertexPositionColor[initalCapacity];
            mGraphicsDevice = graphicsDevice;
            mBufferVerteces = new VertexPositionColor[64];
            mPointBuffer = new Vector2[64];

            mSpriteBatch = spriteBatch;
            mTextures = new Dictionary<string, Texture2D>();
            Texture2D texture = contentManager.Load<Texture2D>("textures\\block3");
            mTextures.Add("block", texture);
            texture = contentManager.Load<Texture2D>("textures\\block7");
            mTextures.Add("block2", texture);
            texture = contentManager.Load<Texture2D>("textures\\block8");
            mTextures.Add("block3", texture);
            texture = contentManager.Load<Texture2D>("textures\\gun");
            mTextures.Add("gun", texture);
            texture = contentManager.Load<Texture2D>("textures\\barrel");
            mTextures.Add("barrel", texture);
            texture = contentManager.Load<Texture2D>("textures\\bullet");
            mTextures.Add("bullet", texture);
        }

        public void DrawSprite(Transform transform, float width, float height, string spriteName)
        {
            Texture2D texture = mTextures[spriteName];
            Vector2 position = new Vector2(transform.Position.X, -transform.Position.Y);
            position -= mCamera.mLowerView;
            position *= mCamera.mZoom;
            position.Y += mCamera.mPosition.Y * 2;
            float rotation = -transform.GetAngle();
            float sc = mCamera.mZoom / 6;
            Vector2 scale = new Vector2(sc, sc);
            float defaultBoxWidth = 6;
            float defaultBoxHeight = 3;
            scale.X *= width / defaultBoxWidth;
            scale.Y *= height / defaultBoxHeight;
            Vector2 origin = new Vector2(width / 2, height / 2) * mCamera.mZoom / scale;
            mSpriteBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
        }

        public void DrawRectangle(Transform transform, float width, float height, Color color)
        {
            float halfHeight = height / 2f;
            float halfWidth = width / 2f;
            mPointBuffer[0] = new Vector2(-halfWidth, halfHeight);
            mPointBuffer[1] = new Vector2(halfWidth, halfHeight);
            mPointBuffer[2] = new Vector2(halfWidth, -halfHeight);
            mPointBuffer[3] = new Vector2(-halfWidth, -halfHeight);
            DrawPolygon(mPointBuffer, 4, transform, color);
        }

        private void DrawPolygon(Vector2[] points, int count, Transform transform, Color color)
        {
            for (int i = 0; i < count; i++)
            {
                points[i] = MathUtils.Multiply(ref transform, points[i]);
            }

            for (int i = 0; i < count; i++)
            {
                mBufferVerteces[i].Position = new Vector3(points[i], 0);
                mBufferVerteces[i].Color = color;
            }

            int index = mVertecesCount;
            for (int i = 0; i < count; i++)
            {
                mVerteces[index + i * 2] = mBufferVerteces[i];
                mVerteces[index + i * 2 + 1] = mBufferVerteces[i + 1];
            }
            mVerteces[index + count * 2 - 1] = mBufferVerteces[0];

            mVertecesCount += count * 2;
        }

        public void BeginDraw()
        {
            mSpriteBatch.Begin();
            mVertecesCount = 0;
        }

        public void EndDraw()
        {
            mGraphicsDevice.BlendState = BlendState.Opaque;
            if (mVertecesCount > 0)
            {
                //Рисуем линии по точкам. Для каждой линии нужно 2 точки.
                mGraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, mVerteces, 0, mVertecesCount / 2);
            }
            mSpriteBatch.End();
        }
    }
}
