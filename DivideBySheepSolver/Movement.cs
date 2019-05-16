using System;
using System.Collections.Generic;

namespace DivideBySheepSolver
{
    public class Movement : IEquatable<Movement>
    {
        public Board Board { get; set; }

        public Coordinate Coordinate { get; set; }

        public Direction Direction { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Movement);
        }

        public bool Equals(Movement other)
        {
            return other != null &&
                   EqualityComparer<Board>.Default.Equals(Board, other.Board) &&
                   Coordinate.Equals(other.Coordinate) &&
                   Direction == other.Direction;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Board, Coordinate, Direction);
        }
    }
}