using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluidLib;
using System.Threading;

#if DEBUG

using System.Diagnostics;

#endif

namespace FluidSimV2 {


    class Simulator {

        public IFluidBehaviour Behaviour { get; set; }
        public int Age { get; private set; }


        public Simulator(IFluidBehaviour behaviour) {

            Behaviour = behaviour;
        } 


        public void Simulate(List<FluidParticle> fluids, int steps) {

            Propogate(fluids);
        }


        private List<FluidParticle> Propogate(List<FluidParticle> fluids) {

            fluids = Behaviour.TranslateParticles(fluids);
            Age++;

            return fluids;
        }


    }
}
