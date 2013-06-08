using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockWars.Gameplay
{
    class Gameplay
    {
        int p1, p2;
        public Player Player1;
        public Player Player2;
        private UI.UIManager mUIManager;
        private void UpdateBlock(List<Box> Boxes)
        {
            for (int i = 0; i < Boxes.Count; i++)
			{
                if (Boxes[i].Player == Player1)
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
