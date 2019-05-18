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
        /// 待移動盤面
        /// </summary>
        private List<Board> BoardForTry { get; set; }

        /// <summary>
        /// 到達過的盤面
        /// </summary>
        public HashSet<Board> ReachedBoards { get; } = new HashSet<Board>();

        public List<(Movement, Board)> Play()
        {
            ReachedBoards.Add(InitialBoard);
            BoardForTry = new List<Board> { InitialBoard };
            (Movement, Board)? lastMovementAndResult = null;
            while (lastMovementAndResult == null)
            {
                lastMovementAndResult = Try(BoardForTry);
            }
            var solveSteps = new List<(Movement, Board)>();
            var invertRoutes = Routes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
            while (true)
            {
                solveSteps.Add(lastMovementAndResult.Value);
                if (invertRoutes.TryGetValue(lastMovementAndResult.Value.Item1.Board, out var movement))
                {
                    lastMovementAndResult = (movement, lastMovementAndResult.Value.Item1.Board);
                }
                else
                    break;
            }
            solveSteps.Add((null, InitialBoard));
            solveSteps.Reverse();
            return solveSteps;
        }

        public (Movement, Board)? Try(IEnumerable<Board> boards)
        {
            var successMovement = boards.AsParallel().SelectMany(board => board.GetAllMovement())
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
            var nextBoardsForTry = new List<Board>();
            foreach (var (movement, board) in successMovement)
            {
                //如移動結果為曾經到過的盤面，則為無意義嘗試
                if (ReachedBoards.Contains(board))
                    continue;

                Routes.Add(movement, board);
                ReachedBoards.Add(board);
                nextBoardsForTry.Add(board);
                if (board.Solved) return (movement, board);
            }
            BoardForTry = nextBoardsForTry;
            return null;
        }
    }
}