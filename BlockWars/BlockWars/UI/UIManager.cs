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
    partial class UIManager
    {
        private const int TAB_COUNT = 4;

        private List<UIControl> mControls;
        private List<UIControl>[] mButtonsPerTab;
        private List<Switcher> mTabs;
        private Builder mBuilder;
        private Cursor mCursor;
        private SpriteBatch mSpriteBatch;
        private ContentManager mContentManager;
        private SpriteFont mFont;
        private bool mBuildMode;
        private Player mPlayer;
        private string mPlayerName;

        public UIManager(SpriteBatch spriteBatch, ContentManager contentManager, Builder builder)
        {
            mFont = contentManager.Load<SpriteFont>("Font");
            mSpriteBatch = spriteBatch;
            mContentManager = contentManager;
            mBuilder = builder;

            CreateControls();

            mBuildMode = true;
        }

        void BButton_BuildClick(object sender, EventArgs e)
        {
            Button BButton = (Button)sender;
			mBuildMode = false;
            for (int i = 0; i < mControls.Count; i++)
            {
                    mControls[i].Visible = false;
                    if (mPlayer.Gun != null)
                    {
                        mPlayer.Gun.IsActive = true;
                    }
                
            }
        }

        private void gunSwitch_Click(object sender, EventArgs e)
        {
            Switcher gun = (Switcher)sender;
            if (gun.IsSwitchedOn)
            {
                List<Switcher> gunButtons = mButtonsPerTab[1];
                for (int i = 0; i < gunButtons.Count; i++)
                {
                    if (gunButtons[i] != gun)
                    {
                        gunButtons[i].IsSwitchedOn = false;
                        mBuilder.SetBuildingObjectType((PlayerData.ObjectType)gunButtons.IndexOf(gun));
                    }
                }
            }
            else
            {
                gun.IsSwitchedOn = true;
            }
        }

        private void BoxButton_Click(object sender, EventArgs e)
        {
            Switcher block = (Switcher)sender;

            if (block.IsSwitchedOn)
            {
                List<UIControl> blockButtons = mButtonsPerTab[0];
                for (int i = 0; i < blockButtons.Count; i++)
                {
                    if (blockButtons[i] != block)
                    {
                        ((Switcher)blockButtons[i]).IsSwitchedOn = false;
                        mBuilder.SetBuildingObjectType((PlayerData.ObjectType)blockButtons.IndexOf(block));
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
            mPlayerName = "Игрок: " + mPlayer.Name;
            mBuilder.SetActivePlayer(player);
			mBuildMode = true;
			for (int i = 0; i < mControls.Count; i++)
			{
				mControls[i].Visible = true;
			}
			for (int i = 0; i < TAB_COUNT; i++)
			{
				for (int j = 0; j < mButtonsPerTab[i].Count; j++)
				{
					mButtonsPerTab[i][j].Visible = false;
				}
			}
			SetTabActive(0);
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
            string boxLastText = "Ресурсы: " + mBuilder.mPlayer.Resources.GetResources();
            Vector2 pos = new Vector2(0, 0);
            mSpriteBatch.DrawString(mFont, boxLastText, pos, Color.Black);

            pos = new Vector2(0, 21);
            mSpriteBatch.DrawString(mFont, mPlayerName, pos, Color.Black);
            
			
            string info = "Нажмите 'ENTER' для передачи хода";
            pos = new Vector2(180,0);
            mSpriteBatch.DrawString(mFont, info, pos, Color.DarkRed);

			string cage = "Обойма: [";
			for (int i = 0; i < mPlayer.Gun.CurrentMagazine.Count; i++)
			{
				string bullet = "";
				switch (mPlayer.Gun.CurrentMagazine[i])
				{
					case 1:
						bullet = "О";
						break;
					case 2:
						bullet = "Б";
						break;
					default:
						bullet = "Н";
						break;
				}
				if (i < mPlayer.Gun.mMagazineSize-1)
				{
					cage += bullet + "|";
				}
				else
				{
					cage += bullet;
				}
				
			}
			if (mPlayer.Gun.CurrentMagazine.Count<mPlayer.Gun.mMagazineSize)
			{
				for (int i = 0; i < mPlayer.Gun.mMagazineSize-mPlayer.Gun.CurrentMagazine.Count; i++)
				{
					if (i < mPlayer.Gun.mMagazineSize-mPlayer.Gun.CurrentMagazine.Count-1)
				{
					cage += "-" + "|";
				}
				else
				{
					cage += "-";
				}
				}
			}
			cage += "]";
			
			if (mPlayer.Gun.CurrentMagazine.Count>0)
			{
				 
			}
			
            pos = new Vector2(0, 41);
            mSpriteBatch.DrawString(mFont, cage, pos, Color.Black);
        }

        public void GameOver(Player player)
        {
            mBuilder.mIsActive = false;
            string congrats = "Игра окончена\n" + player.Name + "ПОБЕДИЛ!";
            Vector2 pos = new Vector2(200, 100);
            mSpriteBatch.DrawString(mFont, congrats, pos, Color.Red);
        }

		private void bullet_Click(object sender, EventArgs e)
		{
			mPlayer.Gun.AddBulletToMagazine(1);
		}

		private void bulletA_Click(object sender, EventArgs e)
		{
			if(mPlayer.Resources.CheckResourceAvaliabe(PlayerData.ObjectType.BulletA))
			{
				if (mPlayer.Gun.AddBulletToMagazine(2))
				{
					mPlayer.Resources.RemoveResources(PlayerData.ObjectType.BulletA);
				}
			}
		}

        private void tab_Click(object sender, EventArgs e)
        {
            int tabIndex = mTabs.IndexOf(sender as Switcher);
            SetTabActive(tabIndex);
        }

        private void SetTabActive(int tabIndex)
        {
            for (int i = 0; i < TAB_COUNT; i++)
            {
                for (int j = 0; j < mButtonsPerTab[i].Count; j++)
                {
                    mButtonsPerTab[i][j].Visible = false;
                }
            }
            for (int i = 0; i < mButtonsPerTab[tabIndex].Count; i++)
            {
                mButtonsPerTab[tabIndex][i].Visible = true;
            }
        }

    }
}
