﻿using System;
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
        private ContactListener mContactListener;
        private Builder mBuilder;
        private Camera mCamera;
        private UIManager mUiManager;
        private Player mPlayer;
        private Gameplay.Gameplay mGameplay;
        private GameObjectCollection mGameObjectCollection;
		Terrain mTerrain;

        private List<Box> mImmortalBoxes;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            mGameObjectCollection = new GameObjectCollection();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mImmortalBoxes = new List<Box>();
            mBasicEffect = new BasicEffect(graphics.GraphicsDevice);
            mBasicEffect.VertexColorEnabled = true;
            mCamera = new Camera(GraphicsDevice.Viewport, mBasicEffect);
            mPrimitiveRender = new PrimitiveRender(graphics.GraphicsDevice, spriteBatch, Content, mCamera);

            Vector2 gravity = new Vector2(0, -10f);
            mWorld = new World(gravity, true);
            mContactListener = new ContactListener();
            mWorld.ContactListener = mContactListener;

			mTerrain = new Terrain(mWorld);

            mBuilder = new Builder(mWorld, mCamera, mGameObjectCollection);

            mUiManager = new UIManager(spriteBatch, Content, mBuilder);
            mGameplay = new Gameplay.Gameplay(mCamera, mUiManager);
            mPlayer = mGameplay.Player1;

            Vector2 pos = new Vector2(0, 30);
            Vector2 size = new Vector2(2, 2);

            pos = new Vector2(-150, -7);
            Gun gunPlayer1 = new Gun(mWorld, pos, mPlayer);
            mGameObjectCollection.Guns.Add(gunPlayer1);
            mPlayer.Guns.Add(gunPlayer1);

            pos = new Vector2(140, -7);
            Gun gunPlayer2 = new Gun(mWorld, pos, mGameplay.Player2);
            mGameObjectCollection.Guns.Add(gunPlayer2);
            mGameplay.Player2.Guns.Add(gunPlayer2);

            List<Box> baseBoxes = PlayerBaseFactory.CreateBuilding(mWorld, mGameplay, EntityCategory.Player1);
            mGameObjectCollection.Boxes.AddRange(baseBoxes);

            baseBoxes = PlayerBaseFactory.CreateBuilding(mWorld, mGameplay, EntityCategory.Player2);
            mGameObjectCollection.Boxes.AddRange(baseBoxes);

            mGameplay.StartGame();
            mBuilder.Activate();
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            List<Gun> guns = mGameObjectCollection.Guns;
            for (int i = 0; i < guns.Count; i++)
            {
                Bullet bullet = guns[i].Update(gameTime);
                if (bullet != null)
                {
                    mGameObjectCollection.Bullets.Add(bullet);
                }
            }

            const float timeStep = 1f / 60f;
            mWorld.Step(timeStep, 6, 2);

            List<Box> newBoxes = ProcessCollisions();
            mGameObjectCollection.Boxes.AddRange(newBoxes);
            mBuilder.Update(gameTime);

            mCamera.Update(gameTime);

            mUiManager.Update(gameTime);

            mGameplay.Update(gameTime);

            /*mUiManager.SetCurrentPlayer(mPlayer);*/

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
                List<Box> boxes = mGameObjectCollection.Boxes;
                for (int j = 0; j < boxes.Count; j++)
                {
                    Box box = boxes[j];
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
                        boxes.Remove(box);
                        j--;
                    }
                }
                bullet.Destroy();
                mGameObjectCollection.Bullets.Remove(bullet);
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
			mTerrain.Draw(mPrimitiveRender);
            List<Box> boxes = mGameObjectCollection.Boxes;
            for (int i = 0; i < boxes.Count; i++)
            {
                boxes[i].Draw(mPrimitiveRender);
            }
            for (int i = 0; i < mImmortalBoxes.Count; i++)
            {
                mImmortalBoxes[i].Draw(mPrimitiveRender);
            }

            List<Bullet> bullets = mGameObjectCollection.Bullets;
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Draw(mPrimitiveRender);
            }

            List<Gun> guns = mGameObjectCollection.Guns;
            for (int i = 0; i < guns.Count; i++)
            {
                guns[i].Draw(mPrimitiveRender);
            }
            mBuilder.Draw(mPrimitiveRender, spriteBatch);

            mUiManager.Draw();

            mPrimitiveRender.EndDraw();
            //spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
