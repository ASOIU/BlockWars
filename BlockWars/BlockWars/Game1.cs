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
        private List<Gun> mGuns;
        private ContactListener mContactListener;
        private Builder mBuilder;
        private Camera mCamera;
        private UIManager mUiManager;
        private Player mPlayer;
        private Gameplay.Gameplay mGameplay;
        private Gameplay.CreateBase mCreateBase; 

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
            mGuns = new List<Gun>();
            mBasicEffect = new BasicEffect(graphics.GraphicsDevice);
            mBasicEffect.VertexColorEnabled = true;
            mCamera = new Camera(GraphicsDevice.Viewport, mBasicEffect);
            mPrimitiveRender = new PrimitiveRender(graphics.GraphicsDevice, spriteBatch, Content, mCamera);

            Vector2 gravity = new Vector2(0, -10f);
            mWorld = new World(gravity, true);
            mContactListener = new ContactListener();
            mWorld.ContactListener = mContactListener;

            mBuilder = new Builder(mWorld, mCamera);

            mUiManager = new UIManager(spriteBatch, Content, mBuilder);
            mGameplay = new Gameplay.Gameplay(mCamera, mUiManager);
            mPlayer = mGameplay.Player1;

            mCreateBase = new CreateBase();

            Vector2 pos = new Vector2(0, 30);
            Vector2 size = new Vector2(2, 2);
            mBoxes = new List<Box>();

            pos = new Vector2(0, -20);
            size = new Vector2(10000, 1);
            Box groundBox = new Box(mWorld, pos, size, "block", true, mPlayer);
            mImmortalBoxes.Add(groundBox);

            pos = new Vector2(-150, -7);
            Gun gunPlayer1 = new Gun(mWorld, pos, mPlayer);
            mGuns.Add(gunPlayer1);
            mPlayer.Gun = gunPlayer1;

            pos = new Vector2(140, -7);
            Gun gunPlayer2 = new Gun(mWorld, pos, mGameplay.Player2);
            mGuns.Add(gunPlayer2);
            mGameplay.Player2.Gun = gunPlayer2;

            mBoxes.AddRange(mCreateBase.CreateBuilding(mWorld, mGameplay, Gameplay.EntityCategory.Player1));
            mBoxes.AddRange(mCreateBase.CreateBuilding(mWorld, mGameplay, Gameplay.EntityCategory.Player2));

            mBuilder.Activate();
            base.Initialize();
        }

        /*private void CreateBuilding(Gameplay.EntityCategory playerType)
        {
            int strength = 1000;
            switch (playerType)
            {
                case(EntityCategory.Player1):
                    {
                        float x, y;
                        y = -18f;
                        float bw, bh;
                        bw = 6;
                        bh = 3;
                        Vector2 position;
                        Vector2 size;
                        Box box;
                        for (int i = 0; i < 8; i++)
                        {
                            x = -300;
                            for (int j = 0; j < 3; j++)
                            {
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(mWorld, position, size, "base-block", true, mGameplay.Player1, strength);
                                mBoxes.Add(box);
                                x -= bw;
                            }
                            y += bh;
                        } 
                        x = -297;
                        y = 5.8f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(mWorld, position, size, "base-block", true, mGameplay.Player1, strength);
                            mBoxes.Add(box);
                            x -= bw;
                        }
                        x = -297;
                        y = 8.8f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(mWorld, position, size, "base-block", true, mGameplay.Player1, strength);
                            mBoxes.Add(box);
                            x -= bw+bw/2f;
                        }
                        break;
                    }
                case (EntityCategory.Player2):
                    {
                        float x, y;
                        y = -18f;
                        float bw, bh;
                        bw = 6;
                        bh = 3;
                        Vector2 position;
                        Vector2 size;
                        Box box;
                        for (int i = 0; i < 8; i++)
                        {
                            x = 300;
                            for (int j = 0; j < 3; j++)
                            {
                                position = new Vector2(x, y);
                                size = new Vector2(bw, bh);
                                box = new Box(mWorld, position, size, "base-block", true, mGameplay.Player2, strength);
                                mBoxes.Add(box);
                                x -= bw;
                            }
                            y += bh;
                        }
                        x = 303;
                        y = 5.8f;
                        for (int i = 0; i < 4; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(mWorld, position, size, "base-block", true, mGameplay.Player2, strength);
                            mBoxes.Add(box);
                            x -= bw;
                        }
                        x = 303;
                        y = 8.8f;
                        for (int i = 0; i < 3; i++)
                        {
                            position = new Vector2(x, y);
                            size = new Vector2(bw, bh);
                            box = new Box(mWorld, position, size, "base-block", true, mGameplay.Player2, strength);
                            mBoxes.Add(box);
                            x -= bw + bw / 2f;
                        }
                        break;
                    }
            }
        }*/

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            for (int i = 0; i < mGuns.Count; i++)
            {
                Bullet bullet = mGuns[i].Update(gameTime);
                if (bullet != null)
                {
                    mBullets.Add(bullet);
                }
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

            mGameplay.Update(gameTime);

            /*mUiManager.SetActivePlayer(mPlayer);*/

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
                        bullet.mPlayer.Resources.AddResourcesBlockDestroy((int)box.mStartHealth);
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
            //spriteBatch.Begin();

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

            for (int i = 0; i < mGuns.Count; i++)
            {
                mGuns[i].Draw(mPrimitiveRender);
            }
            mBuilder.Draw(mPrimitiveRender, spriteBatch);

            mUiManager.Draw();

            mPrimitiveRender.EndDraw();
            //spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
