using DivideBySheepSolver;
using Xunit;

namespace DivideBySheepSolverTest
{
    public class CoordinateTest
    {
        [Fact]
        public void 相等()
        {
            var a = new Coordinate(1, 1);
            var b = new Coordinate(1, 1);
            var c = a;
            var d = new Coordinate(1, 2);
            Assert.Equal(a, b);
            Assert.True(a == b);
            Assert.Equal(a, c);
            Assert.True(a == c);
            Assert.NotEqual(a, d);
            Assert.True(a != d);
        }

        [Fact]
        public void 距離()
        {
            var a = new Coordinate(1, 1);
            var b = new Coordinate(1, 1);
            var d = new Coordinate(1, 2);
            var e = new Coordinate(2, 2);
            var f = new Coordinate(2, 1);
            Assert.Equal(0, a.Distence(b));
            Assert.Equal(1, a.Distence(d));
            Assert.Equal(2, a.Distence(e));
            Assert.Equal(1, a.Distence(f));
        }
    }
}