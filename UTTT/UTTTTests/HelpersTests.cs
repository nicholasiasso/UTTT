using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTTT;

namespace UTTTTests
{
    [TestClass]
    public class HelpersTests
    {
        [TestMethod]
        public void TestNewBoard()
        {
            var newBoard = Helpers.NewBoard();
            Assert.AreEqual(81, newBoard.Length);
            for (int i = 0; i < newBoard.Length; i++)
            {
                Assert.AreEqual(newBoard[i], 0);
            }
        }

        [TestMethod]
        public void TestMoveStringToInt()
        {
            var moveStrings = new string[]  { "a0", "b0", "i0", "i8", "f2"};
            var moveInts = new int[]        { 0,    1,    8,    80,   23};

            for (int i = 0; i < moveStrings.Length; i++)
            {
                Assert.AreEqual(moveInts[i], Helpers.MoveStringToInt(moveStrings[i]));
            }
        }

        [TestMethod]
        public void TestMoveIntToString()
        {
            for (int i = 0; i < 81; i++)
            {
                Assert.AreEqual(i, Helpers.MoveStringToInt(Helpers.MoveIntToString(i)));
            }
        }

        [TestMethod]
        public void TestGetSubBoardWinner()
        {
            var board = Helpers.NewBoard();
            board[0] = 1;
            board[1] = 1;
            Assert.AreEqual(0, Helpers.GetSubBoardWinner(board, 0));

            board[2] = 1;
            Assert.AreEqual(1, Helpers.GetSubBoardWinner(board, 0));

            board[0] = 2;
            board[9] = 1;
            board[10] = 2;
            board[20] = 2;
            Assert.AreEqual(2, Helpers.GetSubBoardWinner(board, 0));
        }
    }
}