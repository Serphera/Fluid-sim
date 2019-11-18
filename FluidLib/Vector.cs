using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluidLib {


    public class Vector {

        public double X { get; set; }
        public double Y { get; set; }

        public double Length() {

            return this.Norm();
        }


        public Vector() {
            X = 0;
            Y = 0;
        }


        public Vector(double x) {

            X = x;
            Y = 0;
        }

        public Vector(double x, double y) {

            X = x;
            Y = y;
        }


        public static bool Equals(Vector a, Vector b) {

            var equal = false;

            if (a == null || b == null) {
                return false;
            }

            if (Math.Abs(a.X) == Math.Abs(b.X) && Math.Abs(a.Y) == Math.Abs(b.Y)) {

                equal = true;
            }

            return equal;
        }


        public static Vector operator +(Vector a, Vector b) {

            return new Vector(a.X + b.X, a.Y + b.Y);
        }


        public static Vector operator -(Vector a, Vector b) {

            return new Vector(a.X - b.X, a.Y - b.Y);
        }


        public static bool operator ==(Vector a, Vector b) {

            if (a is null || b is null) {
                return false;
            }
            return Equals(a, b);
        }


        public static bool operator !=(Vector a, Vector b) {

            return !Equals(a, b);
        }


        public static bool operator >=(Vector a, Vector b) {

            return Compare(a, b) == true ? true : false;
        }



        public static bool operator <=(Vector a, Vector b) {

            return Compare(a, b) == true ? true : false;
        }


        public static Vector operator +(Vector a, int value) {

            a.X += value;
            a.Y += value;

            return a;
        }


        public static Vector operator *(Vector a, Vector b) {

            if (b.X != 0) {
                a.X *= b.X;
            }
            if (b.Y != 0) {
                a.Y *= b.Y;
            }

            return a;
        }

        public static Vector operator /(Vector a, Vector b) {

            a.X = a.X / b.X;
            a.Y = a.Y / b.Y;

            return a;
        }


        public static Vector operator *(Vector a, float value) {

            if (a.X != 0 && value != 0) {

                a.X *= value;
            }
            if (a.Y != 0 && value != 0) {

                a.Y *= value;
            }

            return a;
        }

        public static Vector operator *(float value, Vector a) {

            return a * value;
        }


        public static Vector operator /(Vector a, double value) {

            if (value != 0) {

                a.X = a.X / value;
                a.Y = a.Y / value;
            }

            return a;
        }

        public static Vector operator -(Vector a) {

            return new Vector(-a.X, -a.Y);
        }

        public static Vector operator +(Vector a, float value) {

            return a + new Vector(value, value);
        }

        public static Vector operator -(Vector a, float value) {

            return a - new Vector(value, value);
        }


        private static bool Compare(Vector a, Vector b) {

            bool isGreater = true;

            if (a.X < b.X) {

                isGreater = false;
            }
            else if (a.Y < b.Y) {

                isGreater = false;
            }

            return isGreater;
        }


        public static implicit operator string(Vector a) {

            return $"({a.X}, {a.Y})";
        }

    }


    public static class Extension {


    public static double Square(this Vector a) {

        return DotProduct(a, a);
    }


    public static Vector Normalize(this Vector a) {

        var distance = (float)Norm(a);
        return new Vector(a.X / distance, a.Y / distance);
    }


    public static Vector Normalized(this Vector a) {

        return (a * a) / (float)Norm(a * a);
    }


    public static double DotProduct(this Vector a, Vector b) {

        return (a.X * b.X + a.Y * b.Y);
    }

    /// <summary>
    /// Gets the magnitude of Vector
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static double Norm(this Vector a) {

        return Math.Sqrt(DotProduct(a, a));
    }
}
}
