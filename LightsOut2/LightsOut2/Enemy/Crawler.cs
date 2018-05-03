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
        Texture2D bodyTex;
        Texture2D tailTex;
        Texture2D pointTex;
        float maxAngle;
        float minAngle;
        int bodyLength;
        public List<CrawlerPiece> bodyPieces { get; } = new List<CrawlerPiece>();
        public Crawler(Vector2 position, int size)
            : base (position, size)
        {
            bodyLength = 20;
            hitpoints = bodyLength;
            movementSpeed = 3f;

            texture = ContentManager.Get<Texture2D>("crawlerHeadTex");
            bodyTex = ContentManager.Get<Texture2D>("crawlerVertebraTex");
            tailTex = ContentManager.Get<Texture2D>("crawlerTailTex");
            pointTex = ContentManager.Get<Texture2D>("crawlerPointTex");

            for (int i = 0; i <= bodyLength; i++)
            {
                if (i == 0)
                    bodyPieces.Add(new CrawlerPiece(position - new Vector2(0, 20), Constants.StandardSize, bodyTex));
                else if (i == bodyLength)
                    bodyPieces.Add(new CrawlerPiece(bodyPieces[i - 1].position - new Vector2(0, 30), Constants.StandardSize, tailTex));
                else
                    bodyPieces.Add(new CrawlerPiece(bodyPieces[i - 1].position - new Vector2(0, 20), Constants.StandardSize, bodyTex));
            }
        }

        public override void Update()
        {
            for (int i = bodyLength; i >= 0; i--)
            {
                if (i > 0)
                    bodyPieces[i].Update(bodyPieces[i - 1].position);
                else
                    bodyPieces[i].Update(position);
            }
            position += direction * movementSpeed;
            EnemyAngle();
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = bodyLength; i >= 0; i--)
            {
                bodyPieces[i].Draw(spriteBatch);
                if(bodyPieces[i].piecehitpoints > 0)
                    spriteBatch.Draw(pointTex, bodyPieces[i].position, Color.White);
            }
            base.Draw(spriteBatch);
        }

        //----------------------------------------------------------------------------------------------------


    }
}
