using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTT
{
    internal class PlayerFirstMover : IPlayer
    {
        public int GetMove(Match match)
        {
            var validMoves = match.validMoves;
            var move = validMoves.FirstOrDefault();
            Console.WriteLine($">{Helpers.MoveIntToString(move)}");
            return move;
        }
    }
}
