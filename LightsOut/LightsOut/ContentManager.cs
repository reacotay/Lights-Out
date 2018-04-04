using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lights_Out
{
    public class ContentManager
    {
        public static Game Game { get; set; }

        public ContentManager(Game game)
        {
            Game = game;
        }

        static public T Get<T>(string tag)
        {
            return (T)Game.Content.Load<T>(tag);
        }

        static public GraphicsDevice TransferGraphicsDevice()
        {
            return Game.GraphicsDevice;
        }
    }
}
