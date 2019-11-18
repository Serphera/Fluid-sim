using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluidLib;

namespace FluidSimV2 {

    class DisplayHandler {

        public void PrintGrid(int xBound, int yBound) {

            // i is the row number
            for (int i = 0; i < yBound; i++) {

                if (i > 0) {
                    Console.WriteLine();
                }

                if (i < 10) {

                    Console.Write(" " + i + " ");
                }
                else {

                    Console.Write(i + " ");
                }

                Console.Write(" ");
                PrintRow(xBound);
                Console.Write(" ");
            }
        }


        public void PrintRow(int xBound) {

            for (int i = 0; i < xBound - 1; i++) {

                Console.Write("0 ");                
            }
        }


        /// <summary>
        /// Prints fluid display in console
        /// </summary>
        /// <param name="fluids"></param>
        /// <param name="xBound"></param>
        /// <param name="yBound"></param>
        public void PrintResult(List<FluidParticle> fluids, int xBound, int yBound) {

            var waterHeight = new int[xBound, yBound];

            for (int i = 0; i < fluids.Count(); i++) {

                var offset = (int)fluids[i].Pos.Y < 20 ? 0 : 1;

                var x = (int)fluids[i].Pos.X;                
                var y = (int)fluids[i].Pos.Y - offset;

                if (x < xBound && x >= 0 && y < yBound && y >= 0) {
                    waterHeight[x, y]++;
                }
                
            }

            for (int y = 0; y < yBound; y++) {

                for (int x = 0; x < xBound; x++) {

                    // Prints value in console coordinates(inverted Y axis)
                    Console.SetCursorPosition(x + 4 + (x), (yBound - y) - 1);
                    Console.Write(waterHeight[x, y] + "  ");
                }
            }
        }


        public void ClearOldInput(int yBound) {

            Console.SetCursorPosition(0, yBound + 3);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, yBound + 3);
        }
    }
}
