using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut2
{
    class Crawler : Enemy
    {
        private int bodyLength;
        private float angleModifier;
        private bool angleSwitch;

        private Texture2D bodyTex;
        private Texture2D tailTex;
        private Texture2D pointTex;
        public List<CrawlerPiece> BodyPieces { get; } = new List<CrawlerPiece>();

        public Crawler(Vector2 position, int size)
            : base (position, size)
        {
            bodyLength = 10;
            hitpoints = bodyLength;
            movementSpeed = 4f;
            angleModifier = 0;

            texture = ContentManager.Get<Texture2D>("crawlerHeadTex");
            bodyTex = ContentManager.Get<Texture2D>("crawlerVertebraTex");
            tailTex = ContentManager.Get<Texture2D>("crawlerTailTex");
            pointTex = ContentManager.Get<Texture2D>("crawlerPointTex");

            for (int i = 0; i <= bodyLength; i++)
            {
                if (i == 0)
                    BodyPieces.Add(new CrawlerPiece(position - new Vector2(0, 20), Constants.StandardSize, bodyTex));
                else if (i == bodyLength)
                    BodyPieces.Add(new CrawlerPiece(BodyPieces[i - 1].position - new Vector2(0, 30), Constants.StandardSize, tailTex));
                else
                    BodyPieces.Add(new CrawlerPiece(BodyPieces[i - 1].position - new Vector2(0, 20), Constants.StandardSize, bodyTex));
            }
        }

        public override void Update()
        {
            for (int i = bodyLength; i >= 0; i--)
            {
                if (i > 0)
                    BodyPieces[i].Update(BodyPieces[i - 1].position);
                else
                    BodyPieces[i].Update(position);
            }

            ModifyAngle();
            Vector2 angleModVector = new Vector2((float)Math.Sin(angle + angleModifier),-(float)Math.Cos(angle + angleModifier));
            position += angleModVector * movementSpeed;
            EnemyAngle();

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = bodyLength; i >= 0; i--)
            {
                BodyPieces[i].Draw(spriteBatch);
                if(BodyPieces[i].PieceHitpoints > 0)
                    spriteBatch.Draw(pointTex, new Vector2(BodyPieces[i].hitbox.X + pointTex.Width / 2, BodyPieces[i].hitbox.Y + pointTex.Height / 2), Color.White);
            }

            spriteBatch.Draw(texture, new Vector2(position.X, position.Y - Constants.ShadowOffset), new Rectangle(0, 0, texture.Width, texture.Height), Color.Black, angle+angleModifier, new Vector2(texture.Width / 2, texture.Height / 2), 1.1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), new Rectangle(0, 0, texture.Width, texture.Height), Color.White, angle+angleModifier, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }

        //----------------------------------------------------------------------------------------------------

        private void ModifyAngle()
        {
            if (angleSwitch)
            {
                angleModifier += 0.02f;
                if (angleModifier > 0.7f)
                    angleSwitch = false;
            }
            else
            {
                angleModifier -= 0.02f;
                if (angleModifier < -0.7f)
                    angleSwitch = true;
            }

            angle += angleModifier;
        }
    }
}
