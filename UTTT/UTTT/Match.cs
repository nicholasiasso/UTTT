using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTT
{
    internal struct Match
    {
        public int[] board;
        public int lastMove;
        public int[] validMoves;
        public bool isPlayerOneTurn;

        private IPlayer playerOne;
        private IPlayer playerTwo;
        
        /// <summary>
        /// Creates a new <see cref="Match"/>.
        /// </summary>
        /// <param name="playerOne">The first player.</param>
        /// <param name="playerTwo">The second player.</param>
        /// <param name="firstMove">1 for PlayerOne, 2 for PlayerTwo, Else for Random</param>
        public Match(IPlayer playerOne, IPlayer playerTwo, int firstMove = 0)
        {
            this.board = Helpers.NewBoard();
            this.lastMove = -1;
            this.validMoves = Helpers.GenerateValidMoves(Helpers.NewBoard(), -1);
            this.playerOne = playerOne;
            this.playerTwo = playerTwo;

            switch (firstMove)
            {
                case 1: this.isPlayerOneTurn = true; break;
                case 2: this.isPlayerOneTurn = false; break;
                default: this.isPlayerOneTurn = Helpers.Random.Next(2) == 0; break;
            }
        }

        public void playMatch()
        {
            Console.WriteLine();
            while (Helpers.GetBoardWinner(this.board, lastMove) == 0)
            {
                int move;
                Helpers.PrintBoard(this.board, this.lastMove);
                Console.WriteLine($"Player {(this.isPlayerOneTurn ? "One" : "Two")}'s Turn:");

                do
                {
                    move = this.isPlayerOneTurn ? this.playerOne.GetMove(this) : this.playerTwo.GetMove(this);
                } while (!this.isValidMove(move));

                this.applyMove(move);
            }

            Helpers.PrintBoard(this.board, this.lastMove);

            var winner = Helpers.GetBoardWinner(this.board, lastMove);
            if (winner == 1 || winner == 2)
            {
                Console.WriteLine($"Player {(winner == 1 ? "One" : "Two")} Wins!");
            }
            else
            {
                Console.WriteLine("It's a Tie!");
            }

            Console.WriteLine();
        }

        private void applyMove(int move)
        {
            if (this.isPlayerOneTurn)
            {
                this.board[move] = 1;
            }
            else
            {
                this.board[move] = 2;
            }
            this.isPlayerOneTurn = !this.isPlayerOneTurn;
            this.lastMove = move;
            this.validMoves = Helpers.GenerateValidMoves(this.board, this.lastMove);
        }

        private bool isValidMove(int move)
        {
            return this.validMoves.Contains(move);
        }
    }
}
