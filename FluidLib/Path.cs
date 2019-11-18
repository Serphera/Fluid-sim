using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluidLib {

    public class Path {

        public List<Vector> ParticlePath { get; set; }
        public int Score { get; private set; }
        public string Name { get; set; }
               
        public int SetScore(int val) => Score = val;

        public Path() {
            ParticlePath = new List<Vector>();
        }

        public Path(string name) : this() {
            Name = name;
        }
    }
}
