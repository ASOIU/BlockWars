using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Box2D.XNA;
using BlockWars.UI;
using BlockWars.Gameplay;

namespace BlockWars
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private PrimitiveRender mPrimitiveRender;
        private BasicEffect mBasicEffect;
        private World mWorld;
        private List<Box> mBoxes;
        private List<Bullet> mBullets;
        private Gun mGun;
        private ContactListener mContactListener;
        private Builder mBuilder;
        private Camera mCamera;
        private UIManager mUiManager;
        private Player mPlayer;

        private List<Box> mImmortalBoxes;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mImmortalBoxes = new List<Box>();
            mBullets = new List<Bullet>();
            mBasicEffect = new BasicEffect(graphics.GraphicsDevice);
            mBasicEffect.VertexColorEnabled = true;
            mCamera = new Camera(GraphicsDevice.Viewport, mBasicEffect);
            mPrimitiveRender = new PrimitiveRender(graphics.GraphicsDevice, Content, mCamera);
            mPlayer = new Player();

            Vector2 gravity = new Vector2(0, -10f);
            mWorld = new World(gravity, true);
            mContactListener = new ContactListener();
            mWorld.ContactListener = mContactListener;

            Vector2 pos = new Vector2(0, 30);
            Vector2 size = new Vector2(2, 2);
            mBoxes = new List<Box>();

            AddPyramid(300, -19.5f, 12);

            pos = new Vector2(0, -20);
            size = new Vector2(10000, 1);
            Box groundBox = new Box(mWorld, pos, size, "block", true, mPlayer);
            mImmortalBoxes.Add(groundBox);

            pos = new Vector2(-20, -15);
            mGun = new Gun(mWorld, pos, mPlayer);

            mBuilder = new Builder(mWorld, mCamera, mPlayer);
            mBuilder.Activate();

            mUiManager = new UIManager(spriteBatch, Content, mBuilder, mGun);

            base.Initialize();
        }

        private void AddPyramid(float x, float y, int n)
        {
            float bw = 6, bh = 3;
            float cx = x - bw * n / 2f + bw / 2f,
                  cy = y + bh / 2f;
            Vector2 size = new Vector2(bw, bh);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    Vector2 pos = new Vector2(cx + j * bw, cy + i * bh);
                    Box box = new Box(mWorld, pos, size, "block", true, mPlayer);
                    MassData massData = new MassData();
                    massData.mass = 10;
                    box.mBody.SetMassData(ref massData);
                    mBoxes.Add(box);
                }
                cx += bw / 2f;
            }
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Bullet bullet = mGun.Update(gameTime);
            if (bullet != null)
            {
                mBullets.Add(bullet);
            }

            const float timeStep = 1f / 60f;
            mWorld.Step(timeStep, 6, 2);

            List<Box> newBoxes = ProcessCollisions();
            mBoxes.AddRange(newBoxes);

            object obj = mBuilder.Update(gameTime);
            if (obj is Box)
            {
                mBoxes.Add((Box)obj);
            }
            mCamera.Update(gameTime);

            mUiManager.Update(gameTime);

            base.Update(gameTime);
        }

        private List<Box> ProcessCollisions()
        {
            List<Body[]> contactBodies = mContactListener.GetContacts();
            List<Bullet> contactBullets = new List<Bullet>();
            for (int j = 0; j < contactBodies.Count; j++)
            {
                object obj1 = contactBodies[j][0].GetUserData();
                object obj2 = contactBodies[j][1].GetUserData();

                Bullet bullet = null;
                if (obj2 is Bullet)
                {
                    bullet = (Bullet)obj2;
                }
                else if (obj1 is Bullet)
                {
                    bullet = (Bullet)obj1;
                }
                if (bullet != null && !contactBullets.Contains(bullet))
                {
                    contactBullets.Add(bullet);
                }
            }

            List<Box> newBoxes = new List<Box>();
            for (int i = 0; i < contactBullets.Count; i++)
            {
                Bullet bullet = contactBullets[i];
                for (int j = 0; j < mBoxes.Count; j++)
                {
                    Box box = mBoxes[j];
                    Vector2 distance = bullet.Body.Position - box.mBody.Position;
                    float explosionDistance = distance.Length();
                    float damage = bullet.GetDamageValue(explosionDistance);
                    box.ApplyDamage(damage);
                    if (box.mHealth <= 0)
                    {
                        List<Box> boxs = BoxDestroyer.Destroy(box, mWorld, explosionDistance, mPlayer);
                        newBoxes.AddRange(boxs);
                        box.Destroy();
                        mBoxes.Remove(box);
                        j--;
                    }
                }
                bullet.Destroy();
                mBullets.Remove(bullet);
            }
            return newBoxes;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (EffectPass pass in mBasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }

            mPrimitiveRender.BeginDraw();
            spriteBatch.Begin();

            for (int i = 0; i < mBoxes.Count; i++)
            {
                mBoxes[i].Draw(mPrimitiveRender);
            }
            for (int i = 0; i < mImmortalBoxes.Count; i++)
            {
                mImmortalBoxes[i].Draw(mPrimitiveRender);
            }
            for (int i = 0; i < mBullets.Count; i++)
            {
                mBullets[i].Draw(mPrimitiveRender);
            }

            mGun.Draw(mPrimitiveRender);
            mBuilder.Draw(mPrimitiveRender, spriteBatch);

            mUiManager.Draw();

            mPrimitiveRender.EndDraw();
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
