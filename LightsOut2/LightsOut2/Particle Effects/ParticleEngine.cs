using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace LightsOut2
{
    public class ParticleEngine
    {
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private Texture2D texture;

        public ParticleEngine()
        {
            texture = ContentManager.Get<Texture2D>("particle");

            particles = new List<Particle>();
        }

        public void Update()
        {
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].LifeSpan <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }

        //----------------------------------------------------------------------------------------------------

        public void CreateBloodSplatter(Vector2 enemyPosition, Vector2 direction)
        {
            EmitterLocation = enemyPosition;

            for (int i = 0; i < 20; i++)
            {
                particles.Add(GenerateNewParticle("enemyDeathEffect", direction));
            }
        }

        public void CreateRunParticle(Vector2 playerPosition)
        {
            EmitterLocation = playerPosition;
            particles.Add(GenerateNewParticle("sprintEffect", Vector2.Zero));
        }

        private Particle GenerateNewParticle(string type, Vector2 direction)
        {
            Texture2D tempTex = texture;
            Vector2 tempDirection = direction;
            Vector2 position = EmitterLocation;
            int tempSpeed = 0;
            int ttl = 10 + Constants.Randomizer.Next(10);
            float shade = 0f;
            Color color = new Color();
            float size = 0f;

            switch (type)
            {
                case "sprintEffect":
                    shade = (float)Constants.Randomizer.NextDouble();
                    color = new Color(shade, shade, shade);
                    size = 5 + 5 * (float)Constants.Randomizer.NextDouble();
                    tempSpeed = 1;
                    break;
                case "enemyDeathEffect":
                    shade = (float)Constants.Randomizer.NextDouble();
                    color = new Color(shade, 0, 0);
                    size = 5 + 5 * (float)Constants.Randomizer.NextDouble();
                    tempSpeed = Constants.Randomizer.Next(1, 5);
                    break;
            }

            return new Particle(tempTex, position, direction, color, size, tempSpeed, ttl);
        }
    }
}
