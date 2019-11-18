using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluidLib {


    public class BoundingVolume {


        public float Width { get; private set; }
        public float Height { get; private set; }

        public Vector Position { get; private set; }
        public Vector Min { get; private set; }
        public Vector Max { get; private set; }

        public BoundingVolume(float width, float height, Vector position) {

            Width = width;
            Height = height;
            Position = position;

            Min = new Vector(Position.X, Position.Y - (height + 1));
            Max = new Vector(Position.X + (Width - 1), Position.Y);
        }


        public void SetPosition(Vector position) => Position = position;


        public bool Intersect(Vector a) {

            var intersect = false;

            // a.X greater than min value X and a.Y greater than min value Y
            if (a.X >= Min.X && a.Y >= Min.Y) {

                // a.X less than max value X and a.Y less than max value Y
                if (Max.X >= a.X && Max.Y >= a.Y) {

                    intersect = true;
                }
            }
            return intersect;
        }

    }
}
