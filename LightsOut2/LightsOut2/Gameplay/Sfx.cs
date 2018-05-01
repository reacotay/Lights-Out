using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace LightsOut2
{
    static class Sfx
    {
        static SoundEffect playerDeath = ContentManager.Get<SoundEffect>("Lights-Out-Original-Death-Sound");

        public static class Play
        {
            public static void PlayerDeath()
            {
                playerDeath.Play();
            }
        }
    }
}
