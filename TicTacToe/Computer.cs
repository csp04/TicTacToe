using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public interface IComputer
    {
        int BestMove();
    }

    public class Computer : Player, IComputer
    {
        protected IPlayer Opponent { get; private set; }

        public Computer(string name, Chip chip, IBoard board) : base(name, chip, board)
        {
            Opponent = new Player("", chip == Chip.O ? Chip.X : Chip.O, board);
        }

        public int BestMove()
        {
            int[] moves = Board.AvailableSlots; 
            int best = -1;
            List<(int Slot, int Depth)> bestMoves = new List<(int Slot, int Depth)>();

            foreach (int move in moves)
            {
                Play(move);
                int _depth = 0;
                int moveValue = miniMax(false, ref _depth);

                Board.Put(move, Chip.BLANK);

                if (moveValue >= best)
                {
                    if (moveValue > best)
                            bestMoves.Clear();
                            
                    best = moveValue;
                    bestMoves.Add((move, _depth));
                }
            }
            int bestSlot = bestMoves.
                                OrderBy(bm => bm.Depth)
                                .First().Slot;
            return bestSlot;
        }

        //minimax algorithm
        private int miniMax(bool isMax, ref int depth)
        {
            depth += 1;
            int score = evaluate();

            if (score != 0)
                return score;

            int[] moves = Board.AvailableSlots;

            if (moves.Length == 0)
                return 0;

            int best = isMax ? -100 : 100;

            foreach (int move in moves)
            {
                if (isMax)
                    Play(move);
                else
                    Opponent.Play(move);

                best = isMax ? Math.Max(best, miniMax(!isMax, ref depth)) : Math.Min(best, miniMax(!isMax, ref depth));
                Board.Put(move, Chip.BLANK);
            }

            return best;
        }

        private int evaluate()
        {
            if (this.Win())
                return 10;

            if (this.Opponent.Win())
                return -10;

            return 0;
        }
    }
}
