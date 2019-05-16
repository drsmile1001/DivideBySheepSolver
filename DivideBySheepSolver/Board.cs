using System;
using System.Collections.Generic;
using System.Linq;

namespace DivideBySheepSolver
{
    /// <summary>
    /// 盤面
    /// </summary>
    public class Board : IEquatable<Board>
    {
        /// <summary>
        /// 島嶼或救生艇
        /// </summary>
        public HashSet<Platform> Platforms { get; set; } = new HashSet<Platform>();

        /// <summary>
        /// 目前可操作的島嶼或救生艇
        /// </summary>
        public IEnumerable<Platform> PlatformsNow
        {
            get
            {
                var boat = BoatNow;
                var islands = Platforms
                    .Where(item => !item.BoatOrder.HasValue);
                return boat == null
                    ? islands
                    : islands.Concat(new[] { boat });
            }
        }

        /// <summary>
        /// 目前的救生艇
        /// </summary>
        public Platform BoatNow => Platforms
            .Where(item => item.BoatOrder.HasValue &&
               !item.FulfillBoat)
            .OrderBy(item => item.BoatOrder)
            .FirstOrDefault();

        /// <summary>
        /// 達成盤面條件
        /// </summary>
        public bool Solved => Platforms.Where(item => item.BoatOrder.HasValue)
            .All(item => item.FulfillBoat);

        public IEnumerable<Movement> GetAllMovement()
        {
            var directions = Enumerable.Range(0, 4).Select(i => (Direction)i);
            return PlatformsNow.Where(item => !item.BoatOrder.HasValue && item.AnimalAmount.HasMovableAnimal)
                .SelectMany(item => directions.Select(direction => new Movement
                {
                    Board = this,
                    Coordinate = item.Coordinate,
                    Direction = direction
                }));
        }

        /// <summary>
        /// 尋找當前盤面的特定坐標平台
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Platform FindPlatformNow(Coordinate coordinate)
        {
            return PlatformsNow
                .FirstOrDefault(item => item.Coordinate == coordinate);
        }

        /// <summary>
        /// 移動盤面
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public (bool success, Board board) Move(Coordinate coordinate, Direction direction)
        {
            //查找要操作的兩平台，並檢查是否可移動
            var source = FindPlatformNow(coordinate);
            if (source == null || !source.AnimalAmount.HasMovableAnimal) return (false, null);

            var targetCoordinate = source.Coordinate.Side(direction);
            var target = FindPlatformNow(targetCoordinate);
            if (target == null) return (false, null);

            var newPlatforms = new HashSet<Platform>(Platforms.Where(item => item != source && item != target));

            source = source.Clone();
            target = target.Clone();
            //TODO:檢查是否有刀片
            source.MoveTo(target, false);

            newPlatforms.Add(source);
            newPlatforms.Add(target);

            var newBoard = new Board
            {
                Platforms = newPlatforms
            };

            return (true, newBoard);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Board);
        }

        public bool Equals(Board other)
        {
            return other != null &&
                   Platforms.All(a => other.Platforms.Contains(a)) &&
                   other.Platforms.All(a=>Platforms.Contains(a));
        }

        public override int GetHashCode()
        {
            return Platforms.Aggregate(0, (agg, next) => agg ^ next.GetHashCode());
        }

        public string Visualize()
        {
            var minX = Platforms.Min(item => item.Coordinate.X);
            var minY = Platforms.Min(item => item.Coordinate.Y);
            var maxX = Platforms.Max(item => item.Coordinate.X);
            var maxY = Platforms.Max(item => item.Coordinate.Y);
            var text = "";
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    text += $"{ FindPlatformNow(new Coordinate(x, y))?.Visual() ?? "           "}{(x!= maxX ? " | " : "")}";
                }
                text += "\r\n";
            }
            return text;
        }
    }
}