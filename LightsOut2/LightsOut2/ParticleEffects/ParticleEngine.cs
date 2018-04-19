using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Tower_Defence
{
    public class ParticleEngine
    {
        private int frame;
        private int life;

        private Random randomizer;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private Texture2D texture;

        public ParticleEngine()
        {
            life = 100;
            texture = ContentManager.Get<Texture2D>("circle");

            particles = new List<Particle>();
            randomizer = new Random();
        }

        // Creates new paritcles, removes old ones and updates current particles
        public void Update()
        {
            EmitterLocation = new Vector2(randomizer.Next(750, 800), 620);

            if (life != 100)
                frame++;
            
            if (frame >= life / 2)
            {
                particles.Add(GenerateNewParticle());
                frame = 0;
            }

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
            Vector2 velocity = new Vector2(
                                    1f * (float)(randomizer.NextDouble() * 2 - 1),
                                    -0.5f + ((float)randomizer.NextDouble() * -1f));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(randomizer.NextDouble() * 2 - 1);

            float shade = (float)randomizer.NextDouble();
            Color color = new Color(shade, 0, 0);

            float size = (float)randomizer.NextDouble();
            int ttl = 20 + randomizer.Next(40);

            return new Particle(tempTex, position, velocity, angle, angularVelocity, color, size, ttl);
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
            this.life = life;
        }
    }
}
