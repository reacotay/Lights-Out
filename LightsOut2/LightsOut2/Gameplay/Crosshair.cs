using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LightsOut2
{
    public class Crosshair : GameObject    
    {
        Vector2 playerPosition;

        public Crosshair(Vector2 position, int size) : base(position, size)
        {
            texture = ContentManager.Get<Texture2D>("Crosshair");
            this.position = position;
        }

        public override void Update()
        {
            
            if(Constants.gamePadState.IsConnected)
            {
                position = new Vector2(playerPosition.X + Constants.tempDirection.X * 300, playerPosition.Y + Constants.tempDirection.Y * 300);
            }
            else
                position = new Vector2(Constants.mouseState.Position.X, Constants.mouseState.Position.Y);

            base.Update();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0, new Vector2(Constants.StandardSize / 2, Constants.StandardSize / 2), 1f, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }

        public void TransferPlayerPosition(Vector2 playerPosition)
        {
            this.playerPosition = playerPosition;
        }
    }
}
