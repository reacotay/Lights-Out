using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace LightsOut2
{
    class Particle
    {
        public float Size { get; set; }
        public int TempSpeed { get; set; }
        public int LifeSpan { get; set; }

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Color Color { get; set; }

        public Particle(Texture2D texture, Vector2 position, Vector2 direction, Color color, float size, int tempSpeed, int lifeSpan)
        {
            Size = size;
            TempSpeed = tempSpeed;
            LifeSpan = lifeSpan;

            Texture = texture;
            Position = new Vector2(position.X - size / 2, position.Y - size / 2);
            Direction = direction;
            Color = color;
        }

        public void Update()
        {
            Position += Direction * TempSpeed;
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
