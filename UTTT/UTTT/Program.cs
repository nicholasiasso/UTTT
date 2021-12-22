using System;
using System.Text;
using UTTT;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Ultimate Tic-Tac-Toe!");
        Console.WriteLine();

        var quit = false;
        while (!quit)
        {
            Console.WriteLine("Choose Player One:");
            var playerOne = PromptChooseOpponent();

            Console.WriteLine("Choose Player Two");
            var playerTwo = PromptChooseOpponent();

            var match = new Match(playerOne, playerTwo, firstMove: 1);
            match.playMatch();

            Console.WriteLine();
            Console.WriteLine("Press any key for another game...");
            Console.ReadLine();
        }
    }

    public static IPlayer PromptChooseOpponent()
    {
        Console.WriteLine("    0 - Human Player");
        Console.WriteLine("    1 - Random Mover");
        Console.WriteLine("    2 - First Mover");
        Console.WriteLine("    3 - Last Mover");
        Console.WriteLine("    4 - Always Score Random");
        var input = Console.ReadLine();
        
        switch (input)
        {
            case "0": return new PlayerHuman();
            case "1": return new PlayerRandomMover();
            case "2": return new PlayerFirstMover();
            case "3": return new PlayerLastMover();
            case "4": return new PlayerAlwaysScoreRandom();

            default: return PromptChooseOpponent();
        }
    }
}
