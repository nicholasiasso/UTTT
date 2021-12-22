using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTT
{
    internal class PlayerRandomMover : IPlayer
    {
        public int GetMove(Match match)
        {
            var validMoves = match.validMoves;
            var move = validMoves[Helpers.Random.Next(validMoves.Length)];
            Console.WriteLine($">{Helpers.MoveIntToString(move)}");
            return move;
        }
    }
}
