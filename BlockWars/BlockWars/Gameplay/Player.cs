using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockWars.Gameplay
{
    class Player
    {
        public int mResources;
        public Gun mGun;
        public string Name { get; set; }

        public Player(Gun gun, string name = "Player")
        {
            mGun = gun;
            Name = name;
        }

        public EntityCategory PlayerType { get; private set; }

        public Player(EntityCategory playerType)
        {
            PlayerType = playerType;
        }
    }
}
