namespace DivideBySheepSolver
{
    public enum Direction
    {
        Up, Right, Down, Left
    }

    public static class DirectionExt
    {
        public static Direction Reverse(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;

                case Direction.Right:
                    return Direction.Left;

                case Direction.Down:
                    return Direction.Up;

                case Direction.Left:
                    return Direction.Right;

                default:
                    throw new System.Exception();
            }
        }
    }
}