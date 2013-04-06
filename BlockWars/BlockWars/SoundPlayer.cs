using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace WindowsGame2
{
    class SoundPlayer
    {
        private SoundEffect mBrickSound;

        public SoundPlayer(ContentManager contentManager)
        {
            mBrickSound = contentManager.Load<SoundEffect>("Sounds\\Box");
        }

        public void PlayBrickSound()
        {
            mBrickSound.Play();
        }
    }
}
