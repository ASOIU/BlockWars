using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockWars.Gameplay
{
    class Player
    {
        public int mResources;
        private Gun mGun;

        public EntityCategory PlayerType { get; private set; }

        public Player(EntityCategory playerType)
        {
            PlayerType = playerType;
        }
    }
}
