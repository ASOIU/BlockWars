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

namespace BlockWars.Gameplay
{
    class Gameplay
    {
        public Player mActivePlayer; 

        public Player mPlayer1;

        public Player mPlayer2;

        private UIManager mUIManager;

        private Camera mCamera;

        private Vector2 pos;

        public Gameplay(Camera camera, UIManager UiManager)
        {
            mCamera = camera;
            mUIManager = UiManager;
        }
        public void Update(GameTime gameTime)
        {
            if (mActivePlayer == mPlayer1)
            {
                if (mPlayer1.mGun.CurrentMagazine == 0)
                {
                    mUIManager.SetActivePlayer(mPlayer2);
                    pos = mPlayer2.mGun.mPosition;
                    mCamera.SetPosition(pos);
                    mActivePlayer = mPlayer2;
                }
            }
            else if (mActivePlayer == mPlayer2)
            {
                if (mPlayer2.mGun.CurrentMagazine == 0)
                {
                    mUIManager.SetActivePlayer(mPlayer1);
                    pos = mPlayer1.mGun.mPosition;
                    mCamera.SetPosition(pos);
                    mActivePlayer = mPlayer1;
                }
            }
        }
    }
}
