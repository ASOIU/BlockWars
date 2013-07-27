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
        private const int TAB_COUNT = 5;

        public event EventHandler<GunEventArgs> GunChanged;

        private List<UIControl> mControls;
        private List<UIControl>[] mButtonsPerTab;
        private List<Switcher> mTabs;
        private Builder mBuilder;
        private Cursor mCursor;
        private SpriteBatch mSpriteBatch;
        private ContentManager mContentManager;
        private SpriteFont mFont;
        private Player mPlayer;
        private string mPlayerName;
        private bool mBuildModeActive;
        private Gun mActiveGun;
        private KeyboardState mKeyboardState;

        public UIManager(SpriteBatch spriteBatch, ContentManager contentManager, Builder builder)
        {
            mFont = contentManager.Load<SpriteFont>("Font");
            mSpriteBatch = spriteBatch;
            mContentManager = contentManager;
            mBuilder = builder;
            mBuildModeActive = false;

            CreateControls();
        }

        private void BButton_BuildClick(object sender, EventArgs e)
        {
            mBuildModeActive = false;
            for (int i = 0; i < mControls.Count; i++)
            {
                mControls[i].Visible = false;
            }

            if (mActiveGun != null)
            {
                mActiveGun.IsActive = true;
            }
        }

        private void gunSwitch_Click(object sender, EventArgs e)
        {
            Switcher gunSwitcher = (Switcher)sender;
            if (gunSwitcher.Enable)
            {
                gunSwitcher.IsSwitchedOn = true;
                List<UIControl> gunButtons = mButtonsPerTab[1];
                for (int i = 0; i < gunButtons.Count; i++)
                {
                    if (gunButtons[i] != gunSwitcher)
                    {
                        ((Switcher)gunButtons[i]).IsSwitchedOn = false;
                    }
                }
                mBuilder.SetBuildingObjectType(PlayerData.ObjectType.Gun);
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
            mBuilder.SetCurrentPlayer(player);
            mBuildModeActive = true;
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
            if(player.Guns.Count>0)
                mActiveGun = player.Guns[0];
        }

        public void Update(GameTime gameTime)
        {
            mKeyboardState = Keyboard.GetState();
            if(mKeyboardState.IsKeyDown(Keys.Tab))
            {
                if (mActiveGun != null)
                {
                    int gunIndex = mPlayer.Guns.IndexOf(mActiveGun);
                    gunIndex = (gunIndex + 1) % mPlayer.Guns.Count;
                    mActiveGun = mPlayer.Guns[gunIndex];

                    if (GunChanged != null)
                    {
                        GunChanged(this, new GunEventArgs(mActiveGun));
                    }
                }
            }

            MouseState curMouseState = Mouse.GetState();
            for (int i = 0; i < mControls.Count; i++)
            {
                mControls[i].Update(gameTime);
            }

            mCursor.Visible = !mBuilder.IsActive;
            mCursor.Position = new Vector2(curMouseState.X, curMouseState.Y);
            bool isAnyControlActive = false;
            if (mBuildModeActive)
            {
                for (int i = 0; i < mControls.Count; i++)
                {
                    if (mControls[i].IsActive)
                    {
                        isAnyControlActive = true;
                        break;
                    }
                }
                mCursor.Visible = isAnyControlActive;

                if (isAnyControlActive)
                {
                    mBuilder.Deactivate();
                }
                else
                {
                    mBuilder.Activate();
                }
            }
            else
            {
                if (mBuilder.IsActive)
                {
                    mBuilder.Deactivate();
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
            pos = new Vector2(180, 0);
            mSpriteBatch.DrawString(mFont, info, pos, Color.DarkRed);

            string cage = "Обойма: [";
            for (int i = 0; i < mActiveGun.CurrentMagazine.Count; i++)
            {
                string bullet = "";
                switch (mActiveGun.CurrentMagazine[i])
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
                if (i < mPlayer.Resources.GunMagazineSize - 1)
                {
                    cage += bullet + "|";
                }
                else
                {
                    cage += bullet;
                }

            }
            if (mActiveGun.CurrentMagazine.Count < mPlayer.Resources.GunMagazineSize)
            {
                for (int i = 0; i < mPlayer.Resources.GunMagazineSize - mActiveGun.CurrentMagazine.Count; i++)
                {
                    if (i < mPlayer.Resources.GunMagazineSize - mActiveGun.CurrentMagazine.Count - 1)
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

            pos = new Vector2(0, 41);
            mSpriteBatch.DrawString(mFont, cage, pos, Color.Black);

            int[] Upgrades = mPlayer.Resources.GetUpgradeLevels();
            for (int i = 0; i < Upgrades.Length; i++)
            {
                pos = new Vector2(0, 61 + i * 20);
                mSpriteBatch.DrawString(mFont, string.Format("Upgrade{0} level: {1}", i + 1, Upgrades[i]), pos, Color.Black);
            }
        }

        public void GameOver(Player player)
        {
            mBuilder.Deactivate();
            string congrats = "Игра окончена\n" + player.Name + "ПОБЕДИЛ!";
            Vector2 pos = new Vector2(200, 100);
            mSpriteBatch.DrawString(mFont, congrats, pos, Color.Red);
        }

        private void bullet_Click(object sender, EventArgs e)
        {
            mActiveGun.AddBulletToMagazine(1);
        }

        private void bulletA_Click(object sender, EventArgs e)
        {
            if (mPlayer.Resources.CheckResourceAvaliabe(PlayerData.ObjectType.BulletA))
            {
                if (mActiveGun.AddBulletToMagazine(2))
                {
                    mPlayer.Resources.RemoveResources(PlayerData.ObjectType.BulletA);
                }
            }
        }

        private void Upgrade_Click(object sender, EventArgs e)
        {
            int c = mButtonsPerTab[4].IndexOf((Button)sender);
            mPlayer.Resources.BuyUpgrade(c);
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
