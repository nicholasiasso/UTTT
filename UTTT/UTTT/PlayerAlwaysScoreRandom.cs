using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTT
{
    internal class PlayerAlwaysScoreRandom : IPlayer
    {
        public int GetMove(Match match)
        {
            var playerNumber = match.isPlayerOneTurn ? 1 : 2;

            var scoringMoves = new List<int>();
            foreach (var validMove in match.validMoves)
            {
                var copyBoard = Helpers.CopyBoard(match.board);
                var moveSubBoardIndex = Helpers.MoveToSubBoardIndex(validMove);
                copyBoard[validMove] = playerNumber;
                if (Helpers.GetSubBoardWinner(copyBoard, moveSubBoardIndex) == playerNumber)
                {
                    scoringMoves.Add(validMove);
                }
            }

            int move;
            if (scoringMoves.Count > 0)
            {
                move = scoringMoves.ToArray()[Helpers.Random.Next(scoringMoves.Count)];
                Console.WriteLine($">{Helpers.MoveIntToString(move)}");
                return move;
            }

            move = match.validMoves[Helpers.Random.Next(match.validMoves.Length)];
            Console.WriteLine($">{Helpers.MoveIntToString(move)}");
            return move;
        }
    }
}
