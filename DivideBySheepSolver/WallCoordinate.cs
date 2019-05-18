using System;

namespace DivideBySheepSolver
{
    /// <summary>
    /// 墻坐標
    /// </summary>
    public struct WallCoordinate : IEquatable<WallCoordinate>
    {
        public WallCoordinate(Coordinate sideA, Coordinate sideB)
        {
            SideA = sideA;
            SideB = sideB;
        }

        public WallCoordinate(int ax, int ay, int bx, int by)
        {
            SideA = new Coordinate(ax, ay);
            SideB = new Coordinate(bx, by);
        }

        /// <summary>
        /// A側坐標
        /// </summary>
        public Coordinate SideA { get; }

        /// <summary>
        /// B側坐標
        /// </summary>
        public Coordinate SideB { get; }

        public override bool Equals(object obj)
        {
            return obj is WallCoordinate coordinate && Equals(coordinate);
        }

        public bool Equals(WallCoordinate other)
        {
            return SideA.Equals(other.SideA) &&
                   SideB.Equals(other.SideB) ||
                   SideA.Equals(other.SideB) &&
                   SideB.Equals(other.SideA);
        }

        public override int GetHashCode()
        {
            return SideA.GetHashCode() ^ SideB.GetHashCode();
        }

        public override string ToString()
        {
            return $"{SideA}-{SideB}";
        }
    }
}