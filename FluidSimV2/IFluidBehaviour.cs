using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluidLib;

namespace FluidSimV2 {


    interface IFluidBehaviour {

        void SetBounds(int x, int y);

        List<FluidParticle> TranslateParticles(List<FluidParticle> particles);
    }
}
