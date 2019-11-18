using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluidLib {

    public class FluidParticle {


        public FluidParticle(float x, float y) {

            Pos = new Vector(x, y);
            Velocity = new Vector(0, 0);
            Force = new Vector(0, 0);
            Density = 0.0f;
            Pressure = 0.0f;
            OldPos = new Vector(x, y);
        }


        public Vector Pos, OldPos, Velocity, Force;
        public float Density, Pressure;


        public static bool operator ==(FluidParticle a, FluidParticle b) {

            return a.Pos == b.Pos ? true : false;
        }

        public static bool operator !=(FluidParticle a, FluidParticle b) {

            return a.Pos == b.Pos ? false : true;
        }


        public static FluidParticle operator *(FluidParticle a, float value) {

            a.Pos.X *= value;
            a.Pos.Y *= value;

            return a;
        }

        public static FluidParticle operator +(FluidParticle a, float value) {

            throw new NotImplementedException();
        }




    }
}
