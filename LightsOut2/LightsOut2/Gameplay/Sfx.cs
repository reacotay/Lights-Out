using System;
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
        private static SoundEffect playerDeath = ContentManager.Get<SoundEffect>("Lights-Out-Original-Death-Sound");
        private static Song bgMusic = ContentManager.Get<Song>("BackgroundMusic");
        private static SoundEffect enemyDamageSound = ContentManager.Get<SoundEffect>("enemyDamageSound");

        public static class Play
        {
            public static void PlayerDeath()
            {
                playerDeath.Play();
            }

            public static void EnemyDamage()
            {
                enemyDamageSound.Play();
            }

            public static void BGMStart()
            {
                MediaPlayer.Play(bgMusic);
                MediaPlayer.IsRepeating = true;
            }

            public static void BGMStop()
            {
                MediaPlayer.Stop();
            }
        }
    }
}
