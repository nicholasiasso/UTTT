using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTT
{
    internal class PlayerLastMover : IPlayer
    {
        public int GetMove(Match match)
        {
            var validMoves = match.validMoves;
            var move = validMoves.LastOrDefault();
            Console.WriteLine($">{Helpers.MoveIntToString(move)}");
            return move;
        }
    }
}
