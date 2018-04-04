using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lights_Out
{
    static class SquareDrawer
    {
        static public Texture2D rectTex = new Texture2D(ContentManager.TransferGraphicsDevice(), 5, 5);
        //rectTex.SetData<Color>(new Color[] { Color.White });
    }
}