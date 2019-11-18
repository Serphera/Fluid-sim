using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


using FluidLib;


namespace FluidSimV2 {


    class Program {

        static void Main(string[] args) {

            int xBound, yBound;
            xBound = 42;
            yBound = 20;

            var display = new DisplayHandler();
            display.PrintGrid(xBound, yBound);

            var sim = new NewBehaviour(new BoundingVolume(42, 20, new Vector(0, 20)));
            sim.SetTimeStep((float)(4.52 * Math.Pow(10, -2)));
            sim.SetViscocity(0.0002f);
            sim.SetMass(1f);

            var fluids = InitSPH();
            Console.CursorVisible = false;
            do {

                sim.Update(fluids);
                display.PrintResult(fluids, xBound, yBound);
                //Console.ReadKey();
                /*
                display.PrintResult(fluids, xBound, yBound);
                
                if (runOnce is true) {

                    Console.SetCursorPosition(0, yBound + 2);
                    Console.Write("How many steps do you want to simulate?");
                    Console.SetCursorPosition(0, yBound + 3);
                }

                display.ClearOldInput(yBound);
                if ((RequestInput(ref steps) is false) || fluids.Count > 399) { break; }

                for (int i = steps; i > 0; i--) {
                    
                    sim.Update(fluids);
                    display.PrintResult(fluids, xBound, yBound);
                    Thread.Sleep(150 / steps);
                }

                display.ClearOldInput(yBound);
                runOnce = false;
                */

            } while (true);
        }


        private static bool RequestInput(ref int val) {

            var input = Console.ReadLine();

            if (Regex.IsMatch(input, "[0-9]+")) {

                val = int.Parse(input);
                return true;
            }
            else if (input != "exit") {

                return RequestInput(ref val);
            }
            else {

                return false;
            }
            
        }


        /// <summary>
        /// Sets initial particles
        /// </summary>
        /// <returns></returns>
        public static List<FluidParticle> InitSPH() {

            var particles = new List<FluidParticle>();
            
            var xStart = 5;
            var yStart = 9;

            var columns = 5;
            var rows = 5;

            for (int i = 0; i < columns; i++) {

                for (int j = 0; j < rows; j++) {

                    FluidParticle particle;

                    // Adds a bit of jitter to allow particles to flow out
                    if (j % 2 == 0) {

                        particle = new FluidParticle(xStart + i + 0.3f, yStart - j);
                        particles.Add(particle);
                    }                        
                    else {

                        particle = new FluidParticle(xStart + i, yStart - j);
                        particles.Add(particle);
                    }                    
                    
                }
            }

            return particles;
        }

    }
}
