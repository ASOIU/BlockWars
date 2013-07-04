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

namespace BlockWars.Gameplay
{
    class Gameplay
    {
        int p1, p2;
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        private UIManager mUIManager;
        private Camera mCamera;
        private Player mActivePlayer;

        public Gameplay(Camera camera, UIManager uiManager)
        {
            mCamera = camera;
            mUIManager = uiManager;
            Player1 = new Player(EntityCategory.Player1, "Player1");
            Player2 = new Player(EntityCategory.Player2, "Player2");
            mActivePlayer = Player1;
            mUIManager.SetActivePlayer(mActivePlayer);
        }

        private void UpdateBlock(List<Box> Boxes)
        {
            for (int i = 0; i < Boxes.Count; i++)
            {
                if (Boxes[i].mPlayer == Player1)
                    p1++;
                else
                    p2++;
            }
            if (p1 == 0)
                mUIManager.GameOver(Player2);
            if (p2 == 0)
                mUIManager.GameOver(Player1);
        }

        public void Update(GameTime gameTime)
        {
            if (mActivePlayer == Player1)
            {
                if (Player1.Gun.CurrentMagazine.Count == 0)
                {
                    mUIManager.SetActivePlayer(Player2);
                    Vector2 pos = Player2.Gun.mPosition;
                    mCamera.SetPosition(pos);
                    mActivePlayer = Player2;
                }
            }
            else if (mActivePlayer == Player2)
            {
                if (Player2.Gun.CurrentMagazine.Count == 0)
                {
                    mUIManager.SetActivePlayer(Player1);
                    Vector2 pos = Player1.Gun.mPosition;
                    mCamera.SetPosition(pos);
                    mActivePlayer = Player1;
                }
            }
        }
    }
}