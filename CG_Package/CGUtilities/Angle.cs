using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// from https://github.com/mathnet/mathnet-spatial/blob/master/src/Spatial

namespace CGUtilities
{
    public struct Angle : IComparable<Angle>, IEquatable<Angle>
    {
        /// <summary>
        /// The value in radians
        /// </summary>
        public readonly double Radians;

        /// <summary>
        /// Conversion factor for converting Radians to Degrees
        /// </summary>
        private const double RadToDeg = 180.0 / Math.PI;

        /// <summary>
        /// Conversion factor for converting Degrees to Radians
        /// </summary>
        private const double DegToRad = Math.PI / 180.0;

        

        /// <summary>
        /// Initializes a new instance of the <see cref="Angle"/> struct.
        /// </summary>
        /// <param name="radians">The value in Radians</param>
        private Angle(double radians)
        {
            this.Radians = radians;
        }

        /// <summary>
        /// Gets the value in degrees
        /// </summary>
        public double Degrees => this.Radians * RadToDeg;

        /// <summary>
        /// Returns a value that indicates whether two specified Angles are equal.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the angles are the same; otherwise false.</returns>
        public static bool operator ==(Angle left, Angle right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value that indicates whether two specified Angles are not equal.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the angles are different; otherwise false.</returns>
        public static bool operator !=(Angle left, Angle right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Returns a value that indicates if a specified Angles is less than another.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the first angle is less than the second angle; otherwise false.</returns>
        public static bool operator <(Angle left, Angle right)
        {
            return left.Radians < right.Radians;
        }

        /// <summary>
        /// Returns a value that indicates if a specified Angles is greater than another.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the first angle is greater than the second angle; otherwise false.</returns>
        public static bool operator >(Angle left, Angle right)
        {
            return left.Radians > right.Radians;
        }

        /// <summary>
        /// Returns a value that indicates if a specified Angles is less than or equal to another.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the first angle is less than or equal to the second angle; otherwise false.</returns>
        public static bool operator <=(Angle left, Angle right)
        {
            return left.Radians <= right.Radians;
        }

        /// <summary>
        /// Returns a value that indicates if a specified Angles is greater than or equal to another.
        /// </summary>
        /// <param name="left">The first angle to compare</param>
        /// <param name="right">The second angle to compare</param>
        /// <returns>True if the first angle is greater than or equal to the second angle; otherwise false.</returns>
        public static bool operator >=(Angle left, Angle right)
        {
            return left.Radians >= right.Radians;
        }

        /// <summary>
        /// Multiplies an Angle by a scalar
        /// </summary>
        /// <param name="left">The scalar.</param>
        /// <param name="right">The angle.</param>
        /// <returns>A new angle equal to the product of the angle and the scalar.</returns>
        public static Angle operator *(double left, Angle right)
        {
            return new Angle(left * right.Radians);
        }

        /// <summary>
        /// Multiplies an Angle by a scalar
        /// </summary>
        /// <param name="left">The angle.</param>
        /// <param name="right">The scalar.</param>
        /// <returns>A new angle equal to the product of the angle and the scalar.</returns>
        public static Angle operator *(Angle left, double right)
        {
            return new Angle(left.Radians * right);
        }

        /// <summary>
        /// Divides an Angle by a scalar
        /// </summary>
        /// <param name="left">The angle.</param>
        /// <param name="right">The scalar.</param>
        /// <returns>A new angle equal to the division of the angle by the scalar.</returns>
        public static Angle operator /(Angle left, double right)
        {
            return new Angle(left.Radians / right);
        }

        /// <summary>
        /// Adds two angles together
        /// </summary>
        /// <param name="left">The first angle.</param>
        /// <param name="right">The second angle.</param>
        /// <returns>A new Angle equal to the sum of the provided angles.</returns>
        public static Angle operator +(Angle left, Angle right)
        {
            return new Angle(left.Radians + right.Radians);
        }

        /// <summary>
        /// Subtracts the second angle from the first
        /// </summary>
        /// <param name="left">The first angle.</param>
        /// <param name="right">The second angle.</param>
        /// <returns>A new Angle equal to the difference of the provided angles.</returns>
        public static Angle operator -(Angle left, Angle right)
        {
            return new Angle(left.Radians - right.Radians);
        }

        /// <summary>
        /// Negates the angle
        /// </summary>
        /// <param name="angle">The angle to negate.</param>
        /// <returns>The negated angle.</returns>
        public static Angle operator -(Angle angle)
        {
            return new Angle(-1 * angle.Radians);
        }


        /// <summary>
        /// Creates a new instance of Angle.
        /// </summary>
        /// <param name="value">The value in degrees.</param>
        /// <returns> A new instance of the <see cref="Angle"/> struct.</returns>
        public static Angle FromDegrees(double value)
        {
            return new Angle(value * DegToRad);
        }

        /// <summary>
        /// Creates a new instance of Angle.
        /// </summary>
        /// <param name="value">The value in radians.</param>
        /// <returns> A new instance of the <see cref="Angle"/> struct.</returns>
        public static Angle FromRadians(double value)
        {
            return new Angle(value);
        }
      
        public int CompareTo(Angle value)
        {
            return this.Radians.CompareTo(value.Radians);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="T:MathNet.Spatial.Units.Angle"/> object within the given tolerance.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same angle as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:MathNet.Spatial.Units.Angle"/> object to compare with this instance.</param>
        /// <param name="tolerance">The maximum difference for being considered equal</param>
        public bool Equals(Angle other, double tolerance)
        {
            return Math.Abs(this.Radians - other.Radians) < tolerance;
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="T:MathNet.Spatial.Units.Angle"/> object within the given tolerance.
        /// </summary>
        /// <returns>
        /// true if <paramref name="other"/> represents the same angle as this instance; otherwise, false.
        /// </returns>
        /// <param name="other">An <see cref="T:MathNet.Spatial.Units.Angle"/> object to compare with this instance.</param>
        /// <param name="tolerance">The maximum difference for being considered equal</param>
        public bool Equals(Angle other, Angle tolerance)
        {
            return Math.Abs(this.Radians - other.Radians) < tolerance.Radians;
        }

        /// <inheritdoc />
        public bool Equals(Angle other) => this.Radians.Equals(other.Radians);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Angle a && this.Equals(a);

        /// <inheritdoc />
        //public override int GetHashCode() => HashCode.Combine(this.Radians);

      
    }
}

