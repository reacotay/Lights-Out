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
            texture = ContentManager.Get<Texture2D>("circle");

            particles = new List<Particle>();
        }

        // Creates new paritcles, removes old ones and updates current particles
        public void Update()
        {
            EmitterLocation = new Vector2(/*playerposition*/);
            
            /*if (spelaren springer)
            {
                particles.Add(GenerateNewParticle());
            }
            */
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        // Creates a new particle containing information like color, angle, speed, and size
        private Particle GenerateNewParticle()
        {
            Texture2D tempTex = texture;
            Vector2 position = EmitterLocation;

            float shade = (float)Constants.Randomizer.NextDouble();
            Color color = new Color(shade, 0, 0);

            float size = (float)Constants.Randomizer.NextDouble();
            int ttl = 20 + Constants.Randomizer.Next(40);

            return new Particle(tempTex, position, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }

        // Recieves the amount of lives remaining to generate the correct amount of particles
        public void TransferLife(int life)
        {
            

        }
    }
}
