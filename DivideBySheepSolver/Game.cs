using System.Collections.Generic;
using System.Linq;

namespace DivideBySheepSolver
{
    /// <summary>
    /// 遊戲
    /// </summary>
    public class Game
    {
        /// <summary>
        /// 起始盤面
        /// </summary>
        public Board InitialBoard { get; set; }

        /// <summary>
        /// 盤面路由
        /// </summary>
        public Dictionary<Movement, Board> Routes { get; } = new Dictionary<Movement, Board>();

        /// <summary>
        /// 到達過的盤面
        /// </summary>
        public HashSet<Board> ReachedBoards { get; } = new HashSet<Board>();

        /// <summary>
        /// 無效移動
        /// </summary>
        public HashSet<Movement> UselessMovements { get; } = new HashSet<Movement>();

        public List<Board> Play()
        {
            ReachedBoards.Add(InitialBoard);
            Board successBoard = null;
            while (successBoard == null)
            {
                var boards = Routes.Values.Any() ? Routes.Values.ToArray() : new[] { InitialBoard };
                successBoard = Try(boards);
            }
            var solveBoards = new List<Board>();
            var invertRoutes = Routes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
            while (true)
            {
                solveBoards.Add(successBoard);
                if (invertRoutes.TryGetValue(successBoard, out var movement))
                {
                    successBoard = movement.Board;
                }
                else
                    break;
            }
            solveBoards.Reverse();
            return solveBoards;
        }

        public Board Try(IEnumerable<Board> boards)
        {
            var successMovement = boards.AsParallel().SelectMany(board => board.GetAllMovement())
                    .Where(movement => !Routes.ContainsKey(movement) && !UselessMovements.Contains(movement))
                .Select(movement =>
                {
                    var (success, board) = movement.Board.Move(movement.Coordinate, movement.Direction);
                    return new
                    {
                        movement,
                        success,
                        board
                    };
                }).Where(item => item.success)
                .Select(item => (item.movement, item.board))
                .ToList();
            foreach (var (movement, board) in successMovement)
            {
                var isUselessMovement = ReachedBoards.Contains(board);
                if (isUselessMovement)
                {
                    UselessMovements.Add(movement);
                    continue;
                }
                else
                {
                    Routes.Add(movement, board);
                    ReachedBoards.Add(board);
                    if (board.Solved) return board;
                }
            }
            return null;
        }
    }
}