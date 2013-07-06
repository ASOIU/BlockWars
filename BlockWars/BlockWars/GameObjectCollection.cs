using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockWars
{
    class GameObjectCollection
    {
        public List<Box> Boxes { get; set; }

        public List<Bullet> Bullets { get; set; }

        public List<Gun> Guns { get; set; }

        public GameObjectCollection()
        {
            Boxes = new List<Box>();
            Bullets = new List<Bullet>();
            Guns = new List<Gun>();
        }
    }
}
