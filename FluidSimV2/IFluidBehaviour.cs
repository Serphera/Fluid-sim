using System.Collections.Generic;
using FluidLib;

namespace FluidSimV2 {
    interface IFluidBehaviour {
        BoundingVolume Container { get; set; }
        float DeltaTime { get; set; }
        float Mass { get; set; }
        float Viscocity { get; set; }

        void SetMass(float mass);
        void SetTimeStep(float time);
        void SetViscocity(float viscocity);
        List<FluidParticle> Update(List<FluidParticle> particles);
    }
}