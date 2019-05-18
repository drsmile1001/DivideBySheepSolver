using DivideBySheepSolver;
using Xunit;

namespace DivideBySheepSolverTest
{
    public class WallTest
    {
        [Fact]
        public void 相等()
        {
            var a = new WallCoordinate(new Coordinate(2, 1), new Coordinate(3, 1));
            var b = new WallCoordinate(new Coordinate(2, 1), new Coordinate(3, 1));
            Assert.Equal(a, b);
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
            var c = new WallCoordinate(new Coordinate(3, 1), new Coordinate(2, 1));
            Assert.Equal(a, c);
            Assert.Equal(a.GetHashCode(), c.GetHashCode());
        }
    }
}