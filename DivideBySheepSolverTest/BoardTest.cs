using DivideBySheepSolver;
using System;
using System.Collections.Generic;
using Xunit;

namespace DivideBySheepSolverTest
{
    public class BoardTest
    {
        

        [Fact]
        public void 相等測試_空相同()
        {
            var a = new Board();
            var b = new Board();
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
            Assert.Equal(a, b);
        }

        [Fact]
        public void 相等測試_單一島與單一救生艇相同()
        {
            var a = new Board
            {
                Platforms = new HashSet<Platform>(new[]
                {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3)),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,3)
                })
            };

            var b = new Board
            {
                Platforms = new HashSet<Platform>(new[]
                {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3)),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,3)
                })
            };
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
            Assert.Equal(a, b);
        }

        [Fact]
        public void 相等測試_多島與多救生艇相同()
        {
            var a = new Board
            {
                Platforms = new HashSet<Platform>(new[]
                {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3)),
                    new Platform(new Coordinate(1,2),4,new AnimalAmount(0,4,0)),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,3),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),1,0,4)
                })
            };

            var b = new Board
            {
                Platforms = new HashSet<Platform>(new[]
                {
                    new Platform(new Coordinate(1,2),4,new AnimalAmount(0,4,0)),
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3)),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),1,0,4),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,3)
                })
            };

            Assert.Equal(a.GetHashCode(), b.GetHashCode());
            Assert.Equal(a, b);
        }

        [Fact]
        public void 相等測試_島不相同()
        {
            var a = new Board
            {
                Platforms = new HashSet<Platform>(new[]
               {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3)),
                })
            };

            var b = new Board
            {
                Platforms = new HashSet<Platform>(new[]
                {
                    new Platform(new Coordinate(1,2),4,new AnimalAmount(0,4,0))
                })
            };
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void 相等測試_島數量不相同()
        {
            var a = new Board
            {
                Platforms = new HashSet<Platform>(new[]
               {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3)),
                })
            };

            var b = new Board
            {
                Platforms = new HashSet<Platform>(new[]
                {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3)),
                    new Platform(new Coordinate(1,2),4,new AnimalAmount(0,4,0))
                })
            };

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void 相等測試_多救生艇順序不同()
        {
            var a = new Board
            {
                Platforms = new HashSet<Platform>(new[]
                {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3)),
                    new Platform(new Coordinate(1,2),4,new AnimalAmount(0,4,0)),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,3),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),1,0,4)
                })
            };

            var b = new Board
            {
                Platforms = new HashSet<Platform>(new[]
                {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3)),
                    new Platform(new Coordinate(1,2),4,new AnimalAmount(0,4,0)),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),1,3),
                    new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,0,4)
                })
            };
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void 移動測試_嘗試由不存在的平台移出()
        {
            var board = new Board
            {
                Platforms = new HashSet<Platform>
                {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3))
                }
            };
            var (success, newBoard) = board.Move(new Coordinate(0, 0), Direction.Down);
            Assert.False(success);
            Assert.Null(newBoard);
        }

        [Fact]
        public void 移動測試_嘗試移動到不存在的平台()
        {
            var board = new Board
            {
                Platforms = new HashSet<Platform>
                {
                    new Platform(new Coordinate(1,1),4,new AnimalAmount(3))
                }
            };
            var (success, newBoard) = board.Move(new Coordinate(1, 1), Direction.Down);
            Assert.False(success);
            Assert.Null(newBoard);
        }

        [Fact]
        public void 移動測試_起始點沒有動物()
        {
            var board = new Board
            {
                Platforms = new HashSet<Platform>
                {
                    new Platform(new Coordinate(1,1),4),
                    new Platform(new Coordinate(1,2),4)
                }
            };
            var (success, newBoard) = board.Move(new Coordinate(1, 1), Direction.Down);
            Assert.False(success);
            Assert.Null(newBoard);
        }
    }
}