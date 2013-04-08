using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWars.UI
{
    class UIManager
    {
        private List<UIControl> mControls;

        private List<Switcher> mBuildSwitchers;

        private List<Switcher> mTabSwitchers;

        private Builder mBuilder;

        public UIManager(SpriteBatch spriteBatch, ContentManager contentManager, Builder builder)
        {
            mBuilder = builder;
            mBuildSwitchers = new List<Switcher>();
            mControls = new List<UIControl>();
            mTabSwitchers = new List<Switcher>();

            Texture2D texture = contentManager.Load<Texture2D>("Textures\\UI\\UIBackground2");
            Background background = new Background(texture, spriteBatch);
            mControls.Add(background);

            Texture2D texture2, texture3;
            texture = contentManager.Load<Texture2D>("textures\\UI\\build_switched_on");
            texture2 = contentManager.Load<Texture2D>("textures\\UI\\build_switched_off");
            texture3 = contentManager.Load<Texture2D>("textures\\UI\\build_active");
            Switcher switcher = new Switcher(spriteBatch, texture, texture2, texture3);
            switcher.Position = new Vector2(740, 10);
            switcher.Click += switcher_Click;
            mControls.Add(switcher);

            Texture2D texture4, texture5, texture6;
            texture4 = contentManager.Load<Texture2D>("textures\\UI\\building_switched_on");
            texture5 = contentManager.Load<Texture2D>("textures\\UI\\building_switched_off");
            texture6 = contentManager.Load<Texture2D>("textures\\UI\\building_active");
            Switcher slider1 = new Switcher(spriteBatch, texture4, texture5, texture6);
            slider1.Position = new Vector2(10, 364);
            slider1.Click += slider1_Click;
            mControls.Add(slider1);
            mTabSwitchers.Add(slider1);

            Texture2D texture7, texture8, texture9;
            texture7 = contentManager.Load<Texture2D>("textures\\UI\\weapon_switched_on");
            texture8 = contentManager.Load<Texture2D>("textures\\UI\\weapon_switched_off");
            texture9 = contentManager.Load<Texture2D>("textures\\UI\\weapon_active");
            Switcher slider2 = new Switcher(spriteBatch, texture7, texture8, texture9);
            slider2.Position = new Vector2(76, 364);
            slider2.Click += slider1_Click;
            mControls.Add(slider2);
            mTabSwitchers.Add(slider2);

            Texture2D texture10, texture11, texture12;
            texture10 = contentManager.Load<Texture2D>("textures\\UI\\extra_switched_on");
            texture11 = contentManager.Load<Texture2D>("textures\\UI\\extra_switched_off");
            texture12 = contentManager.Load<Texture2D>("textures\\UI\\extra_active");
            Switcher slider3 = new Switcher(spriteBatch, texture10, texture11, texture12);
            slider3.Position = new Vector2(147, 364);
            slider3.Click += slider1_Click;
            mControls.Add(slider3);
            mTabSwitchers.Add(slider3);

            Texture2D texture13, texture14, texture15;
            texture13 = contentManager.Load<Texture2D>("textures\\UI\\box_switched_on");
            texture14 = contentManager.Load<Texture2D>("textures\\UI\\box_switched_off");
            texture15 = contentManager.Load<Texture2D>("textures\\UI\\box_active");
            Switcher block1Switch = new Switcher(spriteBatch, texture13, texture14, texture15);
            block1Switch.Position = new Vector2(30, 394);
            block1Switch.Click += block1_Click;
            mControls.Add(block1Switch);
            mBuildSwitchers.Add(block1Switch);

            Texture2D texture16, texture17, texture18;
            texture16 = contentManager.Load<Texture2D>("textures\\UI\\box_switched_on");
            texture17 = contentManager.Load<Texture2D>("textures\\UI\\box_switched_off");
            texture18 = contentManager.Load<Texture2D>("textures\\UI\\box_active");
            Switcher block2Switch = new Switcher(spriteBatch, texture16, texture17, texture18);
            block2Switch.Position = new Vector2(120, 394);
            block2Switch.Click += block1_Click;
            mControls.Add(block2Switch);
            mBuildSwitchers.Add(block2Switch);
        }

        private void block1_Click(object sender, EventArgs e)
        {
            Switcher switcher = (Switcher)sender;
            if (switcher.IsSwitchedOn)
            {
                for (int i = 0; i < mBuildSwitchers.Count; i++)
                {
                    if (mBuildSwitchers[i] != switcher)
                    {
                        mBuildSwitchers[i].IsSwitchedOn = false;
                    }
                }
            }
            else
            {
                switcher.IsSwitchedOn = true;
            }
        }

        private void slider1_Click(object sender, EventArgs e)
        {
            Switcher switcher = (Switcher)sender;
            if (switcher.IsSwitchedOn)
            {
                for (int i = 0; i < mTabSwitchers.Count; i++)
                {
                    if (mTabSwitchers[i] != switcher)
                    {
                        mTabSwitchers[i].IsSwitchedOn = false;
                    }
                }
            }
            else
            {
                switcher.IsSwitchedOn = true;
            }
        }

        private void switcher_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Switcher Pressed");
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < mControls.Count; i++)
            {
                mControls[i].Update(gameTime);
            }

            bool activateBuilding = true;
            for (int i = 0; i < mControls.Count; i++)
            {
                if (mControls[i].IsActive)
                {
                    activateBuilding = false;
                    break;
                }
            }

            if (activateBuilding)
            {
                mBuilder.Activate();
            }
            else
            {
                mBuilder.Deactivate();
            }
        }

        public void Draw()
        {
            for (int i = 0; i < mControls.Count; i++)
            {
                mControls[i].Draw();
            }
        }
    }
}
