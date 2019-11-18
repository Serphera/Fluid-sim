using FluidLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluidSimV2 {

    public class SmoothingKernels {

        public Vector CalculateSpiky(Vector a, float H) {

            double len = a.Length();
            var lenSqr = len * len;
            var HSQ = Math.Pow(H, 2);

            var generalKernel = CosineGeneral(a, H);

            if (lenSqr > HSQ) {

                return new Vector(0.0f, 0.0f);
            }

            var f = (-generalKernel * 3.0f * (H - len)) * (H - len) / len;

            return new Vector(a.X * f, a.Y * f);
        }


        public float CalculateGeneral(Vector a, float H) {

            double len = a.Length();
            var lenSqr = len * len;
            var HSQ = Math.Pow(H, 2);
            var generalKernel = CosineGeneral(a, H);

            if (lenSqr > HSQ) {

                return 0.0f;
            }

            return (float)(generalKernel * Math.Pow((HSQ - lenSqr), 3));
        }


        public float CalculateLaplacian(Vector a, float H) {

            var VISC_LAP = (float)(15.0f / (2.0f * Math.PI * Math.Pow(H, 3f)));
            double len = a.Length();
            var lenSqr = len * len;
            var HSQ = Math.Pow(H, 2);

            if (lenSqr > HSQ) {
                return 0.0f;
            }

            return (float)(VISC_LAP * (6.0f / Math.Pow(H, 3.0f)) * (H - len));
        }


        /// <summary>
        /// Cosine Kernel function acc. https://www.sciencedirect.com/science/article/pii/S0307904X13007920
        /// </summary>
        /// <param name="a"></param>
        /// <param name="H"></param>
        /// <returns></returns>
        public float CosineGeneral(Vector a, float H) {

            double len = a.Length();
            var lenSqr = len * len;
            var HSQ = Math.Pow(H, 2);
            var piSqr = Math.Pow(Math.PI, 2);
            var k = 2.0f; // Smoothing value, recommended range 2.0 - 3.0f
            var a2 = Math.PI / ((3 * piSqr - 16) * (k * k * HSQ)); // coefficient for 2 dimensional space e.g pi / [(3 * pi^2 - 16) * (k * h)^2] 

            return (float)(a2 * (4 * Math.Cos(Math.PI / k) + Math.Cos((2 * Math.PI / k) * len) + 3));
        }

    }
}
