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
        int p1, p2;
        public Player Player1;
        public Player Player2;
        private UIManager mUIManager;
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
    }
}