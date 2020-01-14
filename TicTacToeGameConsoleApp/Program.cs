using System;
using System.Collections.Generic;
using TicTacToe;

namespace TicTacToeGameConsoleApp
{
    class Program
    {
        private static readonly Dictionary<Chip, string> chipStringEquiv = new Dictionary<Chip, string>()
        {
            [Chip.BLANK] = " ",
            [Chip.O] = "O",
            [Chip.X] = "X"
        };

        private static void RenderChip(Chip chip)
        {
            Console.Write(chipStringEquiv[chip]);
        }

        private static void RenderBoard(IBoard board)
        {
            for (int i = 0; i < Board.MAX_ROW; i++)
            {
                bool next = false;

                for (int j = 0; j < Board.MAX_COLUMN; j++)
                {
                    if (next)
                        Console.Write("|");

                    Chip chip = board.Get(Board.ToSlot(i, j));

                    RenderChip(chip);
                    next = true;
                }

                if (i + 1 != Board.MAX_ROW)
                    Console.WriteLine("\n-----");
            }

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            IBoard board = new Board();
            bool gameOver = false, playerTurn = true;

            IPlayer p1 = new Player("Player 1", Chip.O, board);
            IPlayer p2 = new Computer("Computer", Chip.X, board);
            IComputer computer = p2 as IComputer;

            while (!gameOver)
            {
                RenderBoard(board);

                IPlayer p = playerTurn ? p1 : p2;

                string turnMsg = "Your turn " + p.Name + ":";
                Console.WriteLine(turnMsg);
                Console.Write("Choose available slot from 1 - 9: ");
                int slot = playerTurn ? Convert.ToInt32(Console.ReadLine()) : computer.BestMove();

                while (!p.Play(slot))
                {
                    Console.Write("Please choose another one: ");
                    slot = playerTurn ? Convert.ToInt32(Console.ReadLine()) : computer.BestMove();
                }

                playerTurn = !playerTurn;

                Console.WriteLine();

                bool isDraw = board.IsFull();
                gameOver = isDraw || p1.Win() || p2.Win();

                string announcement = default;

                if (p1.Win())
                    announcement = p1.Name + " wins!";
                else if (p2.Win())
                    announcement = p2.Name + " wins!";
                else if (isDraw)
                    announcement = "Draw!";

                if (gameOver)
                {
                    RenderBoard(board);
                    Console.WriteLine(announcement);
                }
            }
        }
    }
}
