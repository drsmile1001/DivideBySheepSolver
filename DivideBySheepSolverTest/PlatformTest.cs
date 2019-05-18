using DivideBySheepSolver;
using System.Collections.Generic;
using Xunit;

namespace DivideBySheepSolverTest
{
    public class PlatformTest
    {
        [Fact]
        public void 相等測試_島相同()
        {
            var a = new Platform(new Coordinate(1, 2), 4, new AnimalAmount(3));
            var b = new Platform(new Coordinate(1, 2), 4, new AnimalAmount(3));
            Assert.Equal(a, b);
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
            Assert.Single(new HashSet<Platform>(new[] { a, b }));
        }

        [Fact]
        public void 相等測試_島不同()
        {
            var a = new Platform(new Coordinate(1, 2), 4, new AnimalAmount(3));
            var b = new Platform(new Coordinate(2, 2), 4, new AnimalAmount(3));
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void 相等測試_動物不同()
        {
            var a = new Platform(new Coordinate(1, 2), 4, new AnimalAmount(3));
            var b = new Platform(new Coordinate(1, 2), 4, new AnimalAmount(4));
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void 相等測試_帶有救生艇ID相同()
        {
            var a = new Platform(new Coordinate(1, 2), 4, new AnimalAmount(3), 1);
            var b = new Platform(new Coordinate(1, 2), 4, new AnimalAmount(3), 1);
            Assert.Equal(a, b);
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
            Assert.Single(new HashSet<Platform>(new[] { a, b }));
        }

        [Fact]
        public void 相等測試_帶有救生艇ID不同()
        {
            var a = new Platform(new Coordinate(1, 2), 9, new AnimalAmount(3), 1);
            var b = new Platform(new Coordinate(1, 2), 9, new AnimalAmount(3), 2);
            Assert.NotEqual(a, b);
            Assert.Equal(2, new HashSet<Platform>(new[] { a, b }).Count);
        }

        [Fact]
        public void 相等測試_帶有救生艇條件不同()
        {
            var a = new Platform(new Coordinate(1, 2), 9, new AnimalAmount(3), 1, 3);
            var b = new Platform(new Coordinate(1, 2), 9, new AnimalAmount(3), 1, 4);
            Assert.NotEqual(a, b);
            Assert.Equal(2, new HashSet<Platform>(new[] { a, b }).Count);
        }

        [Fact]
        public void 達成救生艇條件測試()
        {
            var platform = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(3), 1, 3);
            Assert.True(platform.FulfillBoat);
            platform = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(0, 6), 1, 3);
            Assert.True(platform.FulfillBoat);
            platform = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(2, 2), 1, 3);
            Assert.True(platform.FulfillBoat);
            platform = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(0, 0, 3), 1, 0, 3);
            Assert.True(platform.FulfillBoat);
            platform = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(3), 1, 0, 3);
            Assert.False(platform.FulfillBoat);
            platform = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(0, 3), 1, 3);
            Assert.False(platform.FulfillBoat);
            platform = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(0, 0, 3), 1, 3);
            Assert.False(platform.FulfillBoat);
        }

        [Fact]
        public void 深度複製()
        {
            var a = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(3), 1, 3);
            var b = a.Clone();
            Assert.NotSame(a, b);
            Assert.Equal(a, b);
        }

        [Fact]
        public void 移動_目標平台為null_傳回false()
        {
            var a = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(3));
            Assert.False(a.MoveTo(null, false));
        }

        [Fact]
        public void 移動_來源平台為救生艇_傳回false()
        {
            var a = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(3), 0);
            var b = new Platform(new Coordinate(2, 1), 9, new AnimalAmount(3));
            Assert.False(a.MoveTo(b, false));
        }

        [Fact]
        public void 移動_不相鄰的平台移動_傳回false()
        {
            var a = new Platform(new Coordinate(1, 1), 9, new AnimalAmount(3));
            var b = new Platform(new Coordinate(3, 1), 9, new AnimalAmount(3));
            Assert.False(a.MoveTo(b, false));
        }

        private void MoveTest(MoveTestModel model)
        {
            var a = new Platform(new Coordinate(1, 1),
                model.SourceCapacity,
                new AnimalAmount(model.SourceSheep,
                model.SourceSheepHalf,
                model.SourceWolf,
                model.SourceWolfFull));
            var b = new Platform(new Coordinate(2, 1),
                model.TargetCapacity,
                new AnimalAmount(model.TargetSheep,
                model.TargetSheepHalf,
                model.TargetWolf,
                model.TargetWolfFull));
            Assert.True(a.MoveTo(b, model.razor));
            Assert.Equal(new AnimalAmount(0, 0, 0, model.SourceWolfFull), a.AnimalAmount);
            Assert.Equal(new AnimalAmount(model.ExpectedTargetSheep, model.ExpectedTargetSheepHalf, model.ExpectedTargetWolf, model.ExpectedTargetWolfFull),
                b.AnimalAmount);
        }

        private class MoveTestModel
        {
            public bool razor { get; set; }

            public int SourceCapacity { get; set; } = 9;
            public int TargetCapacity { get; set; } = 9;

            public int SourceSheep { get; set; }

            public int SourceSheepHalf { get; set; }

            public int SourceWolf { get; set; }

            public int SourceWolfFull { get; set; }

            public int TargetSheep { get; set; }

            public int TargetSheepHalf { get; set; }

            public int TargetWolf { get; set; }

            public int TargetWolfFull { get; set; }

            public int ExpectedTargetSheep { get; set; }

            public int ExpectedTargetSheepHalf { get; set; }

            public int ExpectedTargetWolf { get; set; }

            public int ExpectedTargetWolfFull { get; set; }
        }

        [Fact]
        public void 移動_2羊移入3格()
        {
            MoveTest(new MoveTestModel
            {
                SourceSheep = 2,
                TargetCapacity = 3,
                ExpectedTargetSheep = 2
            });
        }

        [Fact]
        public void 移動_3羊移入3格()
        {
            MoveTest(new MoveTestModel
            {
                SourceSheep = 3,
                TargetCapacity = 3,
                ExpectedTargetSheep = 3
            });
        }

        [Fact]
        public void 移動_4羊移入3格()
        {
            MoveTest(new MoveTestModel
            {
                SourceSheep = 4,
                TargetCapacity = 3,
                ExpectedTargetSheep = 3
            });
        }

        [Fact]
        public void 移動_2狼移入3格()
        {
            MoveTest(new MoveTestModel
            {
                SourceWolf = 2,
                TargetCapacity = 3,
                ExpectedTargetWolf = 2
            });
        }

        [Fact]
        public void 移動_3狼移入3格()
        {
            MoveTest(new MoveTestModel
            {
                SourceWolf = 3,
                TargetCapacity = 3,
                ExpectedTargetWolf = 3
            });
        }

        [Fact]
        public void 移動_4狼移入3格()
        {
            MoveTest(new MoveTestModel
            {
                SourceWolf = 4,
                TargetCapacity = 3,
                ExpectedTargetWolf = 3
            });
        }

        [Fact]
        public void 移動_2羊移入3格3狼()
        {
            MoveTest(new MoveTestModel
            {
                SourceSheep = 2,
                TargetCapacity = 3,
                TargetWolf = 3,
                ExpectedTargetWolf = 1,
                ExpectedTargetWolfFull = 2
            });
        }

        [Fact]
        public void 移動_3羊移入3格3狼()
        {
            MoveTest(new MoveTestModel
            {
                SourceSheep = 3,
                TargetCapacity = 3,
                TargetWolf = 3,
                ExpectedTargetWolfFull = 3
            });
        }

        [Fact]
        public void 移動_3羊移入3格2狼()
        {
            MoveTest(new MoveTestModel
            {
                SourceSheep = 3,
                TargetCapacity = 3,
                TargetWolf = 2,
                ExpectedTargetSheep = 1,
                ExpectedTargetWolfFull = 2
            });
        }
    }
}