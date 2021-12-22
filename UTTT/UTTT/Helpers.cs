using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTTT
{
    public class Helpers
    {
        public static Random Random = new Random();
        public static int[] NewBoard()
        {
            var board = new int[81];
            return board;
        }

        public static int[] CopyBoard(int[] board)
        {
            var newBoard = new int[81];
            for (int i = 0; i < 81; i++)
            {
                newBoard[i] = board[i];
            }
            return newBoard;
        }

        public static int MoveStringToInt(string move)
        {
            if (move.Length != 2)
            {
                return -1;
            }

            if (move[0] < 'a' || move[0] > 'i' || move[1] < '0' || move[1] > '9')
            {
                return -1;
            }

            return (int.Parse(move.Substring(1, 1)) * 9) + (move[0] - 'a');
        }

        public static string MoveIntToString(int move)
        {
            if (move < 0 || move > 80)
            {
                return "";
            }

            var posX = move % 9;
            var posY = (move - posX) / 9;
            return $"{(char)('a' + posX)}{posY}";
        }

        public static int[] SubBoardIndexToOrderedPositions(int subBoardIndex)
        { 
            switch (subBoardIndex) 
            {
                case 0: return new int[] { 0, 1, 2, 9, 10, 11, 18, 19, 20 };
                case 1: return new int[] { 3, 4, 5, 12, 13, 14, 21, 22, 23 };
                case 2: return new int[] { 6, 7, 8, 15, 16, 17, 24, 25, 26 };
                case 3: return new int[] { 27, 28, 29, 36, 37, 38, 45, 46, 47 };
                case 4: return new int[] { 30, 31, 32, 39, 40, 41, 48, 49, 50 };
                case 5: return new int[] { 33, 34, 35, 42, 43, 44, 51, 52, 53 };
                case 6: return new int[] { 54, 55, 56, 63, 64, 65, 72, 73, 74 };
                case 7: return new int[] { 57, 58, 59, 66, 67, 68, 75, 76, 77 };
                case 8: return new int[] { 60, 61, 62, 69, 70, 71, 78, 79, 80 };
                default: return new int[] { };
            }
        }

        public static int[] GenerateValidMoves(int[] board, int lastMove)
        {
            var generateAllMoves = (int[] board) =>
            {
                int[] validMoves = new int[0];
                for (int subBoardIndex = 0; subBoardIndex < 9; subBoardIndex++)
                {
                    if (Helpers.GetSubBoardWinner(board, subBoardIndex) == 0)
                    {
                        foreach (int move in Helpers.SubBoardIndexToOrderedPositions(subBoardIndex))
                        {
                            if (board[move] == 0)
                            {
                                validMoves = validMoves.Append(move).ToArray();
                            }
                        }
                    }
                }
                return validMoves;
            };

            //First move of the game
            if (lastMove == -1) return generateAllMoves(board);

            // Opponent sent you to a won subboard
            var nextSubBoardIndex = Helpers.MoveToNextSubBoardIndex(lastMove);
            if(Helpers.GetSubBoardWinner(board, nextSubBoardIndex) != 0) return generateAllMoves(board);

            // Opponent sent you to an incomplete subboard
            var validMoves = new int[0];
            foreach (int move in Helpers.SubBoardIndexToOrderedPositions(nextSubBoardIndex))
            {
                if (board[move] == 0)
                {
                    validMoves = validMoves.Append(move).ToArray();
                }
            }

            return validMoves;
        }

        // 0 for no winner, 1 for player one, 2 for player two, 3 for tie
        public static int GetSubBoardWinner(int[] board, int subBoardIndex)
        {
            var checkWinner = (int a, int b, int c, out int winner) =>
            {
                if (board[a] == board[b] && board[b] == board[c] && board[a] != 0)
                {
                    winner = board[a];
                    return true;
                }

                winner = 0;
                return false;
            };

            var orderedPositions = Helpers.SubBoardIndexToOrderedPositions(subBoardIndex);

            int winner;
            if (checkWinner(orderedPositions[0], orderedPositions[1], orderedPositions[2], out winner)) return winner;
            if (checkWinner(orderedPositions[3], orderedPositions[4], orderedPositions[5], out winner)) return winner;
            if (checkWinner(orderedPositions[6], orderedPositions[7], orderedPositions[8], out winner)) return winner;

            if (checkWinner(orderedPositions[0], orderedPositions[3], orderedPositions[6], out winner)) return winner;
            if (checkWinner(orderedPositions[1], orderedPositions[4], orderedPositions[7], out winner)) return winner;
            if (checkWinner(orderedPositions[2], orderedPositions[5], orderedPositions[8], out winner)) return winner;

            if (checkWinner(orderedPositions[0], orderedPositions[4], orderedPositions[8], out winner)) return winner;
            if (checkWinner(orderedPositions[2], orderedPositions[4], orderedPositions[6], out winner)) return winner;

            // Check if it's a tie
            var moveCount = 0;
            for (int i = 0; i < orderedPositions.Length; i++)
            {
                if (board[orderedPositions[i]] != 0)
                {
                    moveCount++;
                }
            }

            if (moveCount == 9) return 3;

            // No winner yet
            return 0;

        }

        internal static int MoveToSubBoardIndex(int move)
        {
            var posX = (move % 9) / 3;
            var posY = (move / 9) / 3;

            return (posY * 3) + posX;
        }

        internal static int MoveToNextSubBoardIndex(int move)
        {
            var posX = (move % 9);
            var posY = move / 9;

            return (posY % 3) * 3 + (posX % 3);
        }

        internal static int GetBoardWinner(int[] board, int lastMove)
        {
            var subBoardWinners = new int[9];
            for (int i = 0; i < subBoardWinners.Length; i++)
            {
                subBoardWinners[i] = Helpers.GetSubBoardWinner(board, i);
            }

            var checkWinner = (int a, int b, int c, out int winner) =>
            {
                if (a == b && b == c && a != 0 && a != 3)
                {
                    winner = a;
                    return true;
                }

                winner = 0;
                return false;
            };

            // Check three in a row
            int winner;
            if (checkWinner(subBoardWinners[0], subBoardWinners[1], subBoardWinners[2], out winner)) return winner;
            if (checkWinner(subBoardWinners[3], subBoardWinners[4], subBoardWinners[5], out winner)) return winner;
            if (checkWinner(subBoardWinners[6], subBoardWinners[7], subBoardWinners[8], out winner)) return winner;

            if (checkWinner(subBoardWinners[0], subBoardWinners[3], subBoardWinners[6], out winner)) return winner;
            if (checkWinner(subBoardWinners[1], subBoardWinners[4], subBoardWinners[7], out winner)) return winner;
            if (checkWinner(subBoardWinners[2], subBoardWinners[5], subBoardWinners[8], out winner)) return winner;

            if (checkWinner(subBoardWinners[0], subBoardWinners[4], subBoardWinners[8], out winner)) return winner;
            if (checkWinner(subBoardWinners[2], subBoardWinners[4], subBoardWinners[6], out winner)) return winner;

            //Check if tied
            if (Helpers.GenerateValidMoves(board, lastMove).Length == 0)
            {
                int playerOneWins = 0;
                int playerTwoWins = 0;
                for (int i = 0; i < subBoardWinners.Length; i++)
                {
                    if (subBoardWinners[i] == 1) playerOneWins++;
                    if (subBoardWinners[i] == 2) playerTwoWins++;
                }

                if (playerOneWins > playerTwoWins) return 1;

                if (playerOneWins < playerTwoWins) return 2;
                return 3;
            }

            // No winner yet
            return 0;
        }

        public static void PrintBoard(int[] board, int lastMove, bool debug = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();

            var intToChar = (int val) =>
            {
                switch (val)
                {
                    case 1: return 'X';
                    case 2: return 'O';
                    default: return '*';
                }
            };

            var printOneRow = (int pos) =>
            {
                sb.Append($"{pos / 9} ");
                sb.Append(intToChar(board[pos]));
                sb.Append(intToChar(board[pos + 1]));
                sb.Append(intToChar(board[pos + 2]));
                sb.Append('|');
                sb.Append(intToChar(board[pos + 3]));
                sb.Append(intToChar(board[pos + 4]));
                sb.Append(intToChar(board[pos + 5]));
                sb.Append('|');
                sb.Append(intToChar(board[pos + 6]));
                sb.Append(intToChar(board[pos + 7]));
                sb.Append(intToChar(board[pos + 8]));
                sb.AppendLine();
            };

            var currentPos = 0;
            for (int i = 0; i < 3; i++)
            {
                printOneRow(currentPos);
                currentPos += 9;
            }
            sb.AppendLine("  ---+---+---");
            for (int i = 0; i < 3; i++)
            {
                printOneRow(currentPos);
                currentPos += 9;
            }
            sb.AppendLine("  ---+---+---");
            for (int i = 0; i < 3; i++)
            {
                printOneRow(currentPos);
                currentPos += 9;
            }

            sb = new StringBuilder(string.Join("\n", sb.ToString().Split('\n').Reverse()));
            sb.AppendLine();
            sb.AppendLine("  abc def ghi");
            sb.AppendLine();

            if (debug)
            {
                sb.AppendLine();
                sb.AppendLine($"Last Move: {lastMove}");
                sb.AppendLine($"SubBoardIndex: {Helpers.MoveToSubBoardIndex(lastMove)}");
                sb.AppendLine($"Next SubBoardIndex: {Helpers.MoveToNextSubBoardIndex(lastMove)}");
                sb.AppendLine($"Valid Next Moves: {Helpers.MoveArrayToString(Helpers.GenerateValidMoves(board, lastMove))}");
                sb.AppendLine($"SubBoardWinners: {Helpers.SubBoardWinnersToString(board)}");
                sb.AppendLine();
            }

            Console.Write(sb.ToString());
        }

        public static string MoveArrayToString(int[] ints)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            foreach (int i in ints)
            {
                sb.Append($"{Helpers.MoveIntToString(i)}, ");
            }

            sb.Append("}");
            return sb.ToString();
        }

        public static string SubBoardWinnersToString(int[] board)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");
            for (int i = 0; i < 9; i++)
            {
                sb.Append($"{Helpers.GetSubBoardWinner(board, i)}, ");
            }

            sb.Append("}");
            return sb.ToString();
        }
    }
}
