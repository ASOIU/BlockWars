using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockWars
{
    class Camera
    {
        private Viewport mViewport;

        private BasicEffect mBasicEffect;

        public float mZoom;

        public Vector2 mPosition;

        public Vector2 mLowerView;

        private MouseState mPrevMouseState;

        public Camera(Viewport viewport, BasicEffect basicEffect)
        {
            mViewport = viewport;
            mBasicEffect = basicEffect;
            mZoom = 6;
            mPosition = new Vector2(0, 0);
            UpdateProjection();
        }

        private void UpdateProjection()
        {
            int width = mViewport.Width;
            int height = mViewport.Height;

            Vector2 upper = new Vector2(width / 2f, height / 2f);
            Vector2 lower = -upper;
            lower += mPosition;
            upper += mPosition;
            upper /= mZoom;
            lower /= mZoom;

            mLowerView = lower;

            mBasicEffect.Projection =
                Matrix.CreateOrthographicOffCenter(lower.X, upper.X, lower.Y, upper.Y, -1, 1);
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            if (mouseState.RightButton == ButtonState.Pressed &&
                mPrevMouseState.RightButton == ButtonState.Pressed)
            {
                mPosition.X -= mouseState.X - mPrevMouseState.X;
                mPosition.Y += mouseState.Y - mPrevMouseState.Y;
                UpdateProjection();
            }

            float deltaZoom = mouseState.ScrollWheelValue - mPrevMouseState.ScrollWheelValue;
            if (Math.Abs(deltaZoom) > 0)
            {
                if (deltaZoom > 0)
                {
                    mZoom *= deltaZoom / 100;
                }
                else
                {
                    mZoom /= -deltaZoom / 100;
                }
                UpdateProjection();
            }

            mPrevMouseState = mouseState;
        }

        public Vector2 ConvertScreenToWorld(Vector2 position)
        {
            float x = position.X;
            float y = position.Y;

            int width = mViewport.Width;
            int height = mViewport.Height;

            float u = x / width;
            float v = (height - y) / height;

            Vector2 upper = new Vector2(width / 2f, height / 2f);
            Vector2 lower = -upper;
            lower += mPosition;
            upper += mPosition;
            upper /= mZoom;
            lower /= mZoom;

            Vector2 p = new Vector2();
            p.X = (1.0f - u) * lower.X + u * upper.X;
            p.Y = (1.0f - v) * lower.Y + v * upper.Y;
            return p;
        }

        public void SetPosition(Vector2 position)
        {
            mPosition = position;
            UpdateProjection();
        }
    }
}
