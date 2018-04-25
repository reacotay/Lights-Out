using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LightsOut2
{
    class Particle
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public int LifeSpan { get; set; }

        public Particle(Texture2D texture, Vector2 position, Color color, float size, int lifeSpan)
        {
            Texture = texture;
            Position = position;
            Color = color;
            Size = size;
            LifeSpan = lifeSpan;
        }

        public void Update()
        {
            LifeSpan--;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
                0f, origin, Size, SpriteEffects.None, 0f);
        }
    }
}
