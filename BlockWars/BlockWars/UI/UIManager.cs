using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BlockWars.Gameplay;

namespace BlockWars.UI
{
    class UIManager
    {
        private List<UIControl> mControls;
        private List<Switcher> mButtons;
        private List<Switcher> mTabs;
        private Builder mBuilder;
        private Cursor mCursor;
        private SpriteBatch mSpriteBatch;
        private SpriteFont mFont;
        private bool mBuildMode;
        private Player mPlayer;
        private string mPlayerName;

        public UIManager(SpriteBatch spriteBatch, ContentManager contentManager, Builder builder)
        {
            mFont = contentManager.Load<SpriteFont>("Font");
            mSpriteBatch = spriteBatch;
            mBuilder = builder;
            mControls = new List<UIControl>();
            mButtons = new List<Switcher>();
            mTabs = new List<Switcher>();

            Texture2D texture = contentManager.Load<Texture2D>("Textures\\UI\\container");
            Background background = new Background(texture, spriteBatch);
            background.Position = new Vector2(0, 360);
            mControls.Add(background);

            Texture2D texture2, texture3;
            texture = contentManager.Load<Texture2D>("textures\\UI\\build_switched_on");
            texture2 = contentManager.Load<Texture2D>("textures\\UI\\build_switched_off");
            texture3 = contentManager.Load<Texture2D>("textures\\UI\\build_active");
            Switcher BButton = new Switcher(spriteBatch, texture, texture2, texture3);
            BButton.Position = new Vector2(745, 20);
            BButton.IsSwitchedOn = true;
            BButton.Click += BButton_BuildClick;
            mControls.Add(BButton);

            texture = contentManager.Load<Texture2D>("textures\\UI\\building_switched_on");
            texture2 = contentManager.Load<Texture2D>("textures\\UI\\building_switched_off");
            texture3 = contentManager.Load<Texture2D>("textures\\UI\\building_active");
            Switcher tab = new Switcher(spriteBatch, texture, texture2, texture3);
            tab.Position = new Vector2(5, 365);
            tab.Click += switcher_Click;
            tab.IsSwitchedOn = true;
            mControls.Add(tab);
            mTabs.Add(tab);

            texture = contentManager.Load<Texture2D>("textures\\UI\\weapon_switched_on");
            texture2 = contentManager.Load<Texture2D>("textures\\UI\\weapon_switched_off");
            texture3 = contentManager.Load<Texture2D>("textures\\UI\\weapon_active");
            tab = new Switcher(spriteBatch, texture, texture2, texture3);
            tab.Position = new Vector2(66, 364);
            tab.Click += switcher_Click;
            mControls.Add(tab);
            mTabs.Add(tab);

            texture = contentManager.Load<Texture2D>("textures\\UI\\extra_switched_on");
            texture2 = contentManager.Load<Texture2D>("textures\\UI\\extra_switched_off");
            texture3 = contentManager.Load<Texture2D>("textures\\UI\\extra_active");
            tab = new Switcher(spriteBatch, texture, texture2, texture3);
            tab.Position = new Vector2(132, 364);
            tab.Click += switcher_Click;
            mControls.Add(tab);
            mTabs.Add(tab);

            texture = contentManager.Load<Texture2D>("textures\\UI\\box_switched_on");
            texture2 = contentManager.Load<Texture2D>("textures\\UI\\box3_switched_off");
            texture3 = contentManager.Load<Texture2D>("textures\\UI\\box_active");
            Switcher block = new Switcher(spriteBatch, texture, texture2, texture3);
            block.Position = new Vector2(10, 390);
            block.Click += switcher_Click;
            block.IsSwitchedOn = true;
            mControls.Add(block);
            mButtons.Add(block);

            texture = contentManager.Load<Texture2D>("textures\\UI\\box_switched_on");
            texture2 = contentManager.Load<Texture2D>("textures\\UI\\box2_switched_off");
            texture3 = contentManager.Load<Texture2D>("textures\\UI\\box_active");
            block = new Switcher(spriteBatch, texture, texture2, texture3);
            block.Position = new Vector2(95, 390);
            block.Click += switcher_Click;
            mControls.Add(block);
            mButtons.Add(block);

            texture = contentManager.Load<Texture2D>("textures\\UI\\box_switched_on");
            texture2 = contentManager.Load<Texture2D>("textures\\UI\\box4_switched_off");
            texture3 = contentManager.Load<Texture2D>("textures\\UI\\box_active");
            block = new Switcher(spriteBatch, texture, texture2, texture3);
            block.Position = new Vector2(180, 390);
            block.Click += switcher_Click;
            mControls.Add(block);
            mButtons.Add(block);

            texture = contentManager.Load<Texture2D>("Textures\\UI\\cursor");
            Cursor cursor = new Cursor(texture, spriteBatch);
            mCursor = cursor;
            cursor.Visible = false;
            mControls.Add(cursor);

            mBuildMode = true;
        }

        void BButton_BuildClick(object sender, EventArgs e)
        {
            Switcher BButton = (Switcher)sender;
            for (int i = 0; i < mControls.Count; i++)
            {
                bool isUiVisible = BButton.IsSwitchedOn;
                if (mControls[i] != BButton)
                {
                    mControls[i].Visible = isUiVisible;
                    mBuildMode = isUiVisible;
                    if (mPlayer.Gun != null)
                    {
                        mPlayer.Gun.IsActive = !isUiVisible;
                    }
                }
            }
        }

        void switcher_Click(object sender, EventArgs e)
        {
            Switcher block = (Switcher)sender;

            if (block.IsSwitchedOn)
            {
                for (int i = 0; i < mButtons.Count; i++)
                {
                    if (mButtons[i] != block)
                    {
                        mButtons[i].IsSwitchedOn = false;
                        mBuilder.BuildingBlock((EBlockType)mButtons.IndexOf(block));
                    }
                }
            }
            else
            {
                block.IsSwitchedOn = true;
            }
            Switcher tab = (Switcher)sender;
            if (tab.IsSwitchedOn)
            {
                for (int i = 0; i < mTabs.Count; i++)
                {
                    if (mTabs[i] != tab)
                    {
                        mTabs[i].IsSwitchedOn = false;
                    }
                }
            }
            else
            {
                tab.IsSwitchedOn = true;
            }
        }

        public void SetActivePlayer(Player player)
        {
            mPlayer = player;
            mPlayerName = "Player: " + mPlayer.Name;
            mBuilder.SetActivePlayer(player);
            Console.WriteLine(mBuilder.mPlayer.Name);
        }

        public void Update(GameTime gameTime)
        {
            MouseState curMouseState = Mouse.GetState();
            for (int i = 0; i < mControls.Count; i++)
            {
                mControls[i].Update(gameTime);
            }

            if (!mBuildMode)
            {
                for (int i = 0; i < mControls.Count; i++)
                {
                    if (mControls[i] == mCursor)
                    {
                        mCursor.Visible = true;
                        mCursor.Position = new Vector2(curMouseState.X, curMouseState.Y);
                    }
                }
            }
            else
            {
                for (int i = 0; i < mControls.Count; i++)
                {
                    if (mControls[i].IsActive == true)
                    {
                        for (int j = 0; j < mControls.Count; j++)
                        {
                            if (mControls[j] == mCursor)
                            {
                                mCursor.Visible = true;
                                mCursor.Position = new Vector2(curMouseState.X, curMouseState.Y);
                            }
                        }
                        mBuilder.Deactivate();
                        break;
                    }
                    else
                    {
                        for (int j = 0; j < mControls.Count; j++)
                        {
                            if (mControls[j] == mCursor)
                            {
                                mCursor.Visible = false;
                            }
                        }
                        if (mBuildMode == true)
                        {
                            mBuilder.Activate();
                        }
                    }

                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < mControls.Count; i++)
            {
                mControls[i].Draw();
            }
            string boxLastText = "Box Last: " + mBuilder.mBoxLast;
            Vector2 pos = new Vector2(0, 0);
            mSpriteBatch.DrawString(mFont, boxLastText, pos, Color.Black);

            pos = new Vector2(0, 21);
            mSpriteBatch.DrawString(mFont, mPlayerName, pos, Color.Black);
        }

        public void GameOver(Player player)
        {
            mBuilder.mIsActive = false;
            string congrats = "Game Over\n" + player.Name + "WON!";
            Vector2 pos = new Vector2(200, 100);
            mSpriteBatch.DrawString(mFont, congrats, pos, Color.Red);
        }
    }
}
