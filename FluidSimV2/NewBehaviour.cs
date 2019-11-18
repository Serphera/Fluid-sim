using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluidLib;

namespace FluidSimV2 {

    class NewBehaviour : IFluidBehaviour {

        public float DeltaTime { get; set; }
        SmoothingKernels Kernels { get; set; }

        public readonly Vector GRAVITY;

        public BoundingVolume Container { get; set; }
        private float _particleRadius;
        private float _particleDiameter;

        public float Mass { get; set; }
        public float Viscocity { get; set;}

        public const float BOUND_DAMPING = 0.01f;
        public const float REST_DENSITY = 100f;
        public const float GAS_CONST = 0.1f;


        public NewBehaviour(BoundingVolume container, float particleSize = 1.0f) {

            Container = container;
            _particleDiameter = particleSize;
            _particleRadius = particleSize / 2.0f;
            GRAVITY = new Vector(0, 12 * -9.8f);
            Kernels = new SmoothingKernels();
        }


        public void SetViscocity(float viscocity) => Viscocity = viscocity;
        public void SetMass(float mass) => Mass = mass;
        public void SetTimeStep(float time) => DeltaTime = time;


        public List<FluidParticle> Update(List<FluidParticle> particles) {

            CalculateDensity(ref particles);
            CalculateForces(ref particles);

            Integrate(ref particles);
            CheckParticleDistance(ref particles);

            return particles;
        }


        /// <summary>
        /// Calculates the density force of the fluid
        /// </summary>
        /// <param name="particles"></param>
        /// <returns></returns>
        private void CalculateDensity(ref List<FluidParticle> particles) {

            for (int i = 0; i < particles.Count; i++) {

                particles[i].Density = 1000 * (_particleRadius / 10);

                for (int j = 0; j < particles.Count; j++) {

                    if (particles[i] == particles[j]) {
                        continue;
                    }

                    var distance = particles[j].Pos - particles[i].Pos;
                    
                    // Distance between particles is less than diameter of particle
                    if (distance.Length() < _particleDiameter) {

                        particles[i].Density += Mass * Kernels.CalculateGeneral(distance, _particleRadius);
                    }
                }

                //particles[i].Pressure = GAS_CONST * (particles[i].Density - REST_DENSITY);
                particles[i].Pressure = (float)((1119 * 1000) * (Math.Pow((particles[i].Density / REST_DENSITY), 7) - 1));
            }
        }


        /// <summary>
        /// Calculates Forces acting on the particles
        /// </summary>
        /// <param name="particles"></param>
        /// <returns></returns>
        private void CalculateForces(ref List<FluidParticle> particles) {

            for (int i = 0; i < particles.Count; i++) {

                particles[i].Force += GRAVITY;

                for (int j = 0; j < particles.Count; j++) {

                    if (particles[i] == particles[j]) {
                        continue;
                    }

                    var deltaDistance = particles[i].Pos - particles[j].Pos;

                    if (deltaDistance.Length() < _particleDiameter) {

                        // Calculates pressure force contribution
                        // pressure = mass * delta pressure
                        // pressure = pressure / (density of particle acting on current particle * 2.0f)
                        // pressure vector = spikyKernel(delta distance, particle radius) * pressure
                        var pressure = Mass * (particles[i].Pressure + particles[j].Pressure);
                        pressure = pressure / (particles[j].Density * 2.0f);

                        var pressureVector = Kernels.CalculateSpiky(deltaDistance, _particleRadius) * pressure;

                        particles[i].Force -= pressureVector;
                        particles[j].Force += pressureVector;


                        //var pressure = Math.Pow(particles[i].Pressure - , 7)

                        // Calculates viscocity force contribution
                        // viscocity = fluid viscocity * 1 / density of partile acting on current particle
                        // viscocity = mass * laplacian(delta distance, particle radius) * viscocity
                        // viscocity vector = delta velocity * viscocity
                        var viscocity = Viscocity * 1 / particles[j].Density;
                        viscocity = Mass * Kernels.CalculateLaplacian(deltaDistance, _particleRadius) * viscocity;

                        var viscocityVector = (particles[j].Velocity - particles[i].Velocity) * viscocity;

                        particles[i].Force += viscocityVector;
                        particles[j].Force -= viscocityVector;                        
                    }
                }
            }
        }


        private float CalculateViscosity(FluidParticle particle, float f) {

            throw new NotImplementedException();
        }


        /// <summary>
        /// Applies movement to particles
        /// </summary>
        /// <param name="particles"></param>
        /// <returns></returns>
        private void Integrate(ref List<FluidParticle> particles) {

            for (int i = 0; i < particles.Count; i++) {
                
                if (particles[i].Pos.X < 0) {

                    particles[i].Pos.X = 0.5f;
                }
                if (particles[i].Pos.X >= Container.Max.X) {

                    particles[i].Pos.X = Container.Max.X - 0.5f;
                }
                if (particles[i].Pos.Y < 0) {

                    particles[i].Pos.Y = 0.5f;
                }
                if (particles[i].Pos.Y >= Container.Max.Y) {

                    particles[i].Pos.Y = Container.Max.Y - 0.5f;
                }

                var oldPos = particles[i].Pos;

                var acceleration = particles[i].Force * (DeltaTime * DeltaTime);
                var t = particles[i].Pos - particles[i].OldPos;
                t = t * (1.0f - BOUND_DAMPING);
                t += acceleration;
                particles[i].Pos += t;

                particles[i].OldPos = oldPos;
                t = particles[i].Pos - oldPos;
                particles[i].Velocity = t / DeltaTime;
                particles[i].Force = new Vector();
            }
        }


        private void CheckParticleDistance(ref List<FluidParticle> particles) {

            for (int i = 0; i < particles.Count; i++) {

                for (int j = 0; j < particles.Count; j++) {

                    if (particles[i] == particles[j]) {
                        continue;
                    }

                    var distance = particles[j].Pos - particles[i].Pos;
                    var distLen = (float)distance.Length();

                    if (distLen < _particleDiameter) {

                        if (distance.Length() > 1.192092896e-07f) {
                            
                            distance = distance * (0.5f * (distLen - _particleDiameter) / distLen);
                            particles[j].Pos -= new Vector(distance.X, distance.Y);
                            particles[j].OldPos -= new Vector(distance.X, distance.Y);

                            particles[i].Pos += distance;
                            particles[i].OldPos += distance;
                        }
                        else {

                            float diff = 0.5f * _particleDiameter;
                            particles[j].Pos.Y -= diff;
                            particles[j].OldPos.Y -= diff;
                            particles[i].Pos.Y += diff;
                            particles[i].OldPos.Y += diff;
                        }

                    }
                }
            }
        }

    }
}
