using DivideBySheepSolver;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace DivideBySheepSolverTest
{
    public class GameTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public GameTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void 玩_測試移動1步獲勝()
        {
            var game = new Game
            {
                InitialBoard = new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(9)),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,9)
                    })
                }
            };
            var result = game.Play();
            var expected = new List<Board>
            {
                new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(9)),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,9)
                    })
                },
                new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(1,1),9,new AnimalAmount()),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(9),0,9)
                    })
                }
            };
            Assert.Equal(expected, result.Select(item => item.Item2));
        }

        [Fact]
        public void 玩_測試移動2步獲勝()
        {
            var game = new Game
            {
                InitialBoard = new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(2,1),9,new AnimalAmount(9)),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount()),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,9)
                    })
                }
            };
            var result = game.Play();
            var expected = new List<Board>
            {
                new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(2,1),9,new AnimalAmount(9)),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount()),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,9)
                    })
                },
                new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(2,1),9,new AnimalAmount()),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(9)),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,9)
                    })
                },
                new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(2,1),9,new AnimalAmount()),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount()),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(9),0,9)
                    })
                }
            };
            var actual = result.Select(item => item.Item2).ToArray();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void 玩_測試移動2步獲勝包含踢出羊()
        {
            var game = new Game
            {
                InitialBoard = new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(2,1),9,new AnimalAmount(9)),
                        new Platform(new Coordinate(1,1),3,new AnimalAmount()),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,3)
                    })
                }
            };
            var result = game.Play();
            var expected = new List<Board>
            {
                new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(2,1),9,new AnimalAmount(9)),
                        new Platform(new Coordinate(1,1),3,new AnimalAmount()),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,3)
                    })
                },
                new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(2,1),9,new AnimalAmount()),
                        new Platform(new Coordinate(1,1),3,new AnimalAmount(3)),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(),0,3)
                    })
                },
                new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(2,1),9,new AnimalAmount()),
                        new Platform(new Coordinate(1,1),3,new AnimalAmount()),
                        new Platform(new Coordinate(0,1),9,new AnimalAmount(3),0,3)
                    })
                }
            };
            Assert.Equal(expected, result.Select(item => item.Item2));
        }

        [Fact]
        public void 玩_1_1()
        {
            var game = new Game
            {
                InitialBoard = new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),0,2),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),1,4),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),2,6),
                        new Platform(new Coordinate(2,1),6,new AnimalAmount()),
                        new Platform(new Coordinate(3,1),6,new AnimalAmount(2)),
                        new Platform(new Coordinate(4,1),6,new AnimalAmount(4)),
                        new Platform(new Coordinate(4,2),6,new AnimalAmount(6)),
                    })
                }
            };
            var result = game.Play();
            foreach (var item in result)
            {
                _testOutputHelper.WriteLine("---");
                if(item.Item1 != null)
                    _testOutputHelper.WriteLine($"{item.Item1.Coordinate} {item.Item1.Direction}");
                else
                    _testOutputHelper.WriteLine($"起始版面");
                _testOutputHelper.WriteLine(item.Item2.Visualize());
            }
        }

        [Fact]
        public void 玩_1_2()
        {
            var game = new Game
            {
                InitialBoard = new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),0,4),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),1,2),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),2,4),
                        new Platform(new Coordinate(2,1),4,new AnimalAmount()),
                        new Platform(new Coordinate(3,1),4,new AnimalAmount(4)),
                        new Platform(new Coordinate(3,2),4,new AnimalAmount(2)),
                        new Platform(new Coordinate(4,2),5,new AnimalAmount(5)),
                    })
                }
            };
            var result = game.Play();
            foreach (var item in result)
            {
                _testOutputHelper.WriteLine("---");
                if (item.Item1 != null)
                    _testOutputHelper.WriteLine($"{item.Item1.Coordinate} {item.Item1.Direction}");
                else
                    _testOutputHelper.WriteLine($"起始版面");
                _testOutputHelper.WriteLine(item.Item2.Visualize());
            }
        }

        [Fact]
        public void 玩_1_3()
        {
            var game = new Game
            {
                InitialBoard = new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),0,2),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),1,4),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),2,6),
                        new Platform(new Coordinate(2,1),6,new AnimalAmount()),
                        new Platform(new Coordinate(3,0),4,new AnimalAmount(2)),
                        new Platform(new Coordinate(3,1),6,new AnimalAmount(2)),
                        new Platform(new Coordinate(3,2),4,new AnimalAmount(4)),
                        new Platform(new Coordinate(4,2),4,new AnimalAmount(4)),
                    })
                }
            };
            var result = game.Play();
            foreach (var item in result)
            {
                _testOutputHelper.WriteLine("---");
                if (item.Item1 != null)
                    _testOutputHelper.WriteLine($"{item.Item1.Coordinate} {item.Item1.Direction}");
                else
                    _testOutputHelper.WriteLine($"起始版面");
                _testOutputHelper.WriteLine(item.Item2.Visualize());
            }
        }

        [Fact]
        public void 玩_1_4()
        {
            var game = new Game
            {
                InitialBoard = new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),0,4),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),1,1),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),2,5),
                        new Platform(new Coordinate(2,0),1,new AnimalAmount(1)),
                        new Platform(new Coordinate(2,1),3,new AnimalAmount(3)),
                        new Platform(new Coordinate(3,1),5,new AnimalAmount(1)),
                        new Platform(new Coordinate(4,1),3,new AnimalAmount(3)),
                        new Platform(new Coordinate(4,2),2,new AnimalAmount(2)),
                    })
                }
            };
            var result = game.Play();
            foreach (var item in result)
            {
                _testOutputHelper.WriteLine("---");
                if (item.Item1 != null)
                    _testOutputHelper.WriteLine($"{item.Item1.Coordinate} {item.Item1.Direction}");
                else
                    _testOutputHelper.WriteLine($"起始版面");
                _testOutputHelper.WriteLine(item.Item2.Visualize());
            }
        }

        [Fact]
        public void 玩_1_5()
        {
            var game = new Game
            {
                InitialBoard = new Board
                {
                    Platforms = new HashSet<Platform>(new[]
                    {
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),0,3),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),1,4),
                        new Platform(new Coordinate(1,1),9,new AnimalAmount(),2,2),
                        new Platform(new Coordinate(2,1),5,new AnimalAmount(5)),
                        new Platform(new Coordinate(3,1),3,new AnimalAmount()),
                        new Platform(new Coordinate(3,2),5,new AnimalAmount(5)),
                        new Platform(new Coordinate(4,1),2,new AnimalAmount(1)),
                        new Platform(new Coordinate(4,2),6,new AnimalAmount(6)),
                    })
                }
            };
            var result = game.Play();
            foreach (var item in result)
            {
                _testOutputHelper.WriteLine("---");
                if (item.Item1 != null)
                    _testOutputHelper.WriteLine($"{item.Item1.Coordinate} {item.Item1.Direction}");
                else
                    _testOutputHelper.WriteLine($"起始版面");
                _testOutputHelper.WriteLine(item.Item2.Visualize());
            }
        }
    }
}