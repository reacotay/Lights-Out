﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace LightsOut2
{
    static class Sfx
    {
        static SoundEffect playerDeath = ContentManager.Get<SoundEffect>("Lights-Out-Original-Death-Sound");
        static Song bgMusic = ContentManager.Get<Song>("Lights-Out-No-Farts");
        public static class Play
        {
            public static void PlayerDeath()
            {
                playerDeath.Play();
            }
            public static void BGMStart()
            {
                MediaPlayer.Play(bgMusic);
            }
            public static void BGMStop()
            {
                MediaPlayer.Stop();
            }
        }
    }
}