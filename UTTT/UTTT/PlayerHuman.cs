using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTT
{
    internal class PlayerHuman : IPlayer
    {
        public int GetMove(Match match)
        {
            Console.Write(">");
            var moveString = Console.ReadLine();
            if (moveString == null) return -1;
            return Helpers.MoveStringToInt(moveString);
        }
    }
}
