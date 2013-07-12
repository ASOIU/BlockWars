using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWars.UI
{
	partial class UIManager
	{
		private void CreateControls()
		{
			mControls = new List<UIControl>();
			mButtonsPerTab = new List<UIControl>[TAB_COUNT];
			for (int i = 0; i < TAB_COUNT; i++)
			{
				mButtonsPerTab[i] = new List<UIControl>();
			}
			mTabs = new List<Switcher>(TAB_COUNT);

			Texture2D texture = mContentManager.Load<Texture2D>("Textures\\UI\\container");
			Background background = new Background(texture, mSpriteBatch);
			background.Position = new Vector2(0, 360);
			mControls.Add(background);

			Texture2D texture3;
			texture = mContentManager.Load<Texture2D>("textures\\UI\\build_switched_on");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\build_active");
			Button BButton = new Button(mSpriteBatch, texture, texture3);
			BButton.Position = new Vector2(745, 20);
			BButton.Click += BButton_BuildClick;
			mControls.Add(BButton);

			CreateTabs();
			CreateBlockTab();
			CreateGunTab();
			CreateBulletTab();
            CreateUpgradesTab();

			for (int i = 0; i < TAB_COUNT; i++)
			{
				for (int j = 0; j < mButtonsPerTab[i].Count; j++)
				{
					mButtonsPerTab[i][j].Visible = false;
				}
			}
			SetTabActive(0);
			texture = mContentManager.Load<Texture2D>("Textures\\UI\\cursor");
			Cursor cursor = new Cursor(texture, mSpriteBatch);
			mCursor = cursor;
			cursor.Visible = false;
			mControls.Add(cursor);
		}

		private void CreateGunTab()
		{
			Texture2D texture;
			Texture2D texture2;
			Texture2D texture3;
			texture = mContentManager.Load<Texture2D>("textures\\UI\\box4_switched_on");
			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\box4_switched_off");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\box4_active");
			Switcher gunSwitch = new Switcher(mSpriteBatch, texture, texture2, texture3);
			gunSwitch.Position = new Vector2(10, 390);
			gunSwitch.Click += gunSwitch_Click;
			gunSwitch.IsSwitchedOn = true;
			mControls.Add(gunSwitch);
			mButtonsPerTab[1].Add(gunSwitch);
		}

		private void CreateBulletTab()
		{
			Texture2D texture2;
			Texture2D texture3;
			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\block1_switched_off");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\block1_active");
			Button bulletSwitch = new Button(mSpriteBatch, texture2, texture3);
			bulletSwitch.Position = new Vector2(10, 390);
			bulletSwitch.Click += bullet_Click;
			mControls.Add(bulletSwitch);
			mButtonsPerTab[3].Add(bulletSwitch);

			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\block1_switched_off");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\block1_active");
			 bulletSwitch = new Button(mSpriteBatch, texture2, texture3);
			bulletSwitch.Position = new Vector2(100, 390);
			bulletSwitch.Click += bulletA_Click;
			mControls.Add(bulletSwitch);
			mButtonsPerTab[3].Add(bulletSwitch);
		}

        void CreateUpgradesTab()
        {
            Texture2D texture2;
            Texture2D texture3;
            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\block1_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\block1_active");
            Button bulletSwitch = new Button(mSpriteBatch, texture2, texture3);
            bulletSwitch.Position = new Vector2(10, 390);
            bulletSwitch.Click += Upgrade_Click;
            mControls.Add(bulletSwitch);
            mButtonsPerTab[4].Add(bulletSwitch);

            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\block1_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\block1_active");
            bulletSwitch = new Button(mSpriteBatch, texture2, texture3);
            bulletSwitch.Position = new Vector2(100, 390);
            bulletSwitch.Click += Upgrade_Click;
            mControls.Add(bulletSwitch);
            mButtonsPerTab[4].Add(bulletSwitch);

            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\block1_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\block1_active");
            bulletSwitch = new Button(mSpriteBatch, texture2, texture3);
            bulletSwitch.Position = new Vector2(200, 390);
            bulletSwitch.Click += Upgrade_Click;
            mControls.Add(bulletSwitch);
            mButtonsPerTab[4].Add(bulletSwitch);
        }
		private void CreateTabs()
		{
			Texture2D texture;
			Texture2D texture2, texture3;

			texture = mContentManager.Load<Texture2D>("textures\\UI\\building_switched_on");
			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\building_switched_off");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\building_active");
			Switcher tab = new Switcher(mSpriteBatch, texture, texture2, texture3);
			tab.Position = new Vector2(5, 365);
			tab.Click += tab_Click;
			tab.IsSwitchedOn = true;
			mControls.Add(tab);
			mTabs.Add(tab);

			texture = mContentManager.Load<Texture2D>("textures\\UI\\weapon_switched_on");
			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\weapon_switched_off");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\weapon_active");
			tab = new Switcher(mSpriteBatch, texture, texture2, texture3);
			tab.Position = new Vector2(66, 364);
			tab.Click += tab_Click;
			mControls.Add(tab);
			mTabs.Add(tab);

			texture = mContentManager.Load<Texture2D>("textures\\UI\\extra_switched_on");
			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\extra_switched_off");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\extra_active");
			tab = new Switcher(mSpriteBatch, texture, texture2, texture3);
			tab.Position = new Vector2(132, 364);
			tab.Click += tab_Click;
			mControls.Add(tab);
			mTabs.Add(tab);

			texture = mContentManager.Load<Texture2D>("textures\\UI\\bullets_Tab");
			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\bullets_Tab");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\bullets_Tab");
			tab = new Switcher(mSpriteBatch, texture, texture2, texture3);
			tab.Position = new Vector2(300, 364);
			tab.Click += tab_Click;
			mControls.Add(tab);
			mTabs.Add(tab);

            texture = mContentManager.Load<Texture2D>("textures\\UI\\Upgrades_tab");
            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\Upgrades_tab");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\Upgrades_tab");
            tab = new Switcher(mSpriteBatch, texture, texture2, texture3);
            tab.Position = new Vector2(500, 364);
            tab.Click += tab_Click;
            mControls.Add(tab);
            mTabs.Add(tab);
		}

		private void CreateBlockTab()
		{
			int tabIndex = 0;
			Texture2D texture;
			Texture2D texture2;
			Texture2D texture3;
			texture = mContentManager.Load<Texture2D>("textures\\UI\\box4_switched_on");
			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\box4_switched_off");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\box4_active");
			Switcher block = new Switcher(mSpriteBatch, texture, texture2, texture3);
			block.Position = new Vector2(10, 390);
			block.Click += BoxButton_Click;
			block.IsSwitchedOn = true;
			mControls.Add(block);
			mButtonsPerTab[tabIndex].Add(block);

			texture = mContentManager.Load<Texture2D>("textures\\UI\\box3_switched_on");
			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\box3_switched_off");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\box3_active");
			block = new Switcher(mSpriteBatch, texture, texture2, texture3);
			block.Position = new Vector2(95, 390);
			block.Click += BoxButton_Click;
			mControls.Add(block);
			mButtonsPerTab[tabIndex].Add(block);

			texture = mContentManager.Load<Texture2D>("textures\\UI\\box2_switched_on");
			texture2 = mContentManager.Load<Texture2D>("textures\\UI\\box2_switched_off");
			texture3 = mContentManager.Load<Texture2D>("textures\\UI\\box2_active");
			block = new Switcher(mSpriteBatch, texture, texture2, texture3);
			block.Position = new Vector2(180, 390);
			block.Click += BoxButton_Click;
			mControls.Add(block);
			mButtonsPerTab[tabIndex].Add(block);
		}
	}
}
