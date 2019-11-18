using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluidLib;

namespace FluidSimV2 {


    public static class ExtendedArray {

        public static Vector WrapArray(this Vector[] array, int index) {

            index = index % array.Count();

            if (index < 0) {

                return array[array.Count() + index];
            }
            else {

                return array[index];
            }
        }
    }
}
