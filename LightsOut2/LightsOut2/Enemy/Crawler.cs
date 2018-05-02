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
        Texture2D headTex;
        Texture2D bodyTex;
        Texture2D tailTex;
        List<CrawlerPiece> bodyPieces = new List<CrawlerPiece>();
        public Crawler(Vector2 position, int size)
            : base (position, size)
        {
            hitpoints = 1;
            movementSpeed = 3f;

            headTex = ContentManager.Get<Texture2D>("crawlerHeadTex");
            bodyTex = ContentManager.Get<Texture2D>("crawlerVertebraTex");
            tailTex = ContentManager.Get<Texture2D>("crawlerTailTex");

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                    bodyPieces.Add(new CrawlerPiece(position, Constants.StandardSize, headTex));
                else if (i == 10)
                    bodyPieces.Add(new CrawlerPiece(bodyPieces[i - 1].position - new Vector2(10,0), Constants.StandardSize, bodyTex));
                else
                    bodyPieces.Add(new CrawlerPiece(bodyPieces[i - 1].position - new Vector2(10, 0), Constants.StandardSize, tailTex));
            }
        }

        public override void Update()
        {
            foreach(CrawlerPiece x in bodyPieces)
                x.Update();

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (CrawlerPiece x in bodyPieces)
                x.Draw(spriteBatch);
        }
    }
}
