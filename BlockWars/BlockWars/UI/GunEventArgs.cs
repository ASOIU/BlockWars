using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockWars.UI
{
    class GunEventArgs:EventArgs
    {
        public Gun Gun { get; private set; }

        public GunEventArgs(Gun gun)
        {
            Gun = gun;
        }
    }
}
