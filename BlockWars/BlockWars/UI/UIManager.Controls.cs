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
            mButtonsPerTab = new List<Switcher>[TAB_COUNT];
            for (int i = 0; i < TAB_COUNT; i++)
            {
                mButtonsPerTab[i] = new List<Switcher>();
            }
            mTabs = new List<Switcher>(TAB_COUNT);

            Texture2D texture = mContentManager.Load<Texture2D>("Textures\\UI\\container");
            Background background = new Background(texture, mSpriteBatch);
            background.Position = new Vector2(0, 360);
            mControls.Add(background);

            CreateTabs();
            CreateBlockTab();
            CreateGunTab();

            texture = mContentManager.Load<Texture2D>("Textures\\UI\\cursor");
            Cursor cursor = new Cursor(texture, mSpriteBatch);
            mCursor = cursor;
            cursor.Visible = false;
            mControls.Add(cursor);
        }

        private void CreateGunTab()
        {
            //TODO
        }

        private void CreateTabs()
        {
            Texture2D texture;
            Texture2D texture2, texture3;
            texture = mContentManager.Load<Texture2D>("textures\\UI\\build_switched_on");
            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\build_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\build_active");
            Switcher BButton = new Switcher(mSpriteBatch, texture, texture2, texture3);
            BButton.Position = new Vector2(745, 20);
            BButton.IsSwitchedOn = true;
            BButton.Click += BButton_BuildClick;
            mControls.Add(BButton);

            texture = mContentManager.Load<Texture2D>("textures\\UI\\building_switched_on");
            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\building_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\building_active");
            Switcher tab = new Switcher(mSpriteBatch, texture, texture2, texture3);
            tab.Position = new Vector2(5, 365);
            tab.Click += switcher_Click;
            tab.IsSwitchedOn = true;
            mControls.Add(tab);
            mTabs.Add(tab);

            texture = mContentManager.Load<Texture2D>("textures\\UI\\weapon_switched_on");
            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\weapon_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\weapon_active");
            tab = new Switcher(mSpriteBatch, texture, texture2, texture3);
            tab.Position = new Vector2(66, 364);
            tab.Click += switcher_Click;
            mControls.Add(tab);
            mTabs.Add(tab);

            texture = mContentManager.Load<Texture2D>("textures\\UI\\extra_switched_on");
            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\extra_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\extra_active");
            tab = new Switcher(mSpriteBatch, texture, texture2, texture3);
            tab.Position = new Vector2(132, 364);
            tab.Click += switcher_Click;
            mControls.Add(tab);
            mTabs.Add(tab);
        }

        private void CreateBlockTab()
        {
            Texture2D texture;
            Texture2D texture2;
            Texture2D texture3;
            texture = mContentManager.Load<Texture2D>("textures\\UI\\box4_switched_on");
            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\box4_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\box4_active");
            Switcher block = new Switcher(mSpriteBatch, texture, texture2, texture3);
            block.Position = new Vector2(10, 390);
            block.Click += switcher_Click;
            block.IsSwitchedOn = true;
            mControls.Add(block);
            mButtonsPerTab[0].Add(block);

            texture = mContentManager.Load<Texture2D>("textures\\UI\\box3_switched_on");
            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\box3_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\box3_active");
            block = new Switcher(mSpriteBatch, texture, texture2, texture3);
            block.Position = new Vector2(95, 390);
            block.Click += switcher_Click;
            mControls.Add(block);
            mButtonsPerTab[0].Add(block);

            texture = mContentManager.Load<Texture2D>("textures\\UI\\box2_switched_on");
            texture2 = mContentManager.Load<Texture2D>("textures\\UI\\box2_switched_off");
            texture3 = mContentManager.Load<Texture2D>("textures\\UI\\box2_active");
            block = new Switcher(mSpriteBatch, texture, texture2, texture3);
            block.Position = new Vector2(180, 390);
            block.Click += switcher_Click;
            mControls.Add(block);
            mButtonsPerTab[0].Add(block);
        }
    }
}
