using System;

namespace DivideBySheepSolver
{
    /// <summary>
    /// 坐標
    /// </summary>
    public struct Coordinate : IEquatable<Coordinate>
    {
        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return !a.Equals(b);
        }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Coordinate coordinate && Equals(coordinate);
        }

        public bool Equals(Coordinate other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <summary>
        /// 取得旁邊的坐標
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Coordinate Side(Direction direction)
        {
            var x = direction == Direction.Left
                ? X - 1
                : direction == Direction.Right
                ? X + 1
                : X;

            var y = direction == Direction.Up
                ? Y - 1
                : direction == Direction.Down
                ? Y + 1
                : Y;
            return new Coordinate(x, y);
        }

        /// <summary>
        /// 與另一個坐標距離
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int Distence(Coordinate other)
        {
            return Math.Abs(other.X - X) + Math.Abs(other.Y - Y);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}