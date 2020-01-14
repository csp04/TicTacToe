using System;
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
            var rnd = new Random();
            int[] moves = Board.AvailableSlots.OrderBy(slot => rnd.Next()).ToArray(); // randomize every turn
            int best = -100;
            int bestSlot = 1;

            foreach (int move in moves)
            {
                Play(move);
                int moveValue = miniMax(false);

                Board.Put(move, Chip.BLANK);

                if (moveValue > best)
                {
                    best = moveValue;
                    bestSlot = move;
                }
            }
            
            return bestSlot;
        }

        //minimax algorithm
        private int miniMax(bool isMax)
        {
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

                best = isMax ? Math.Max(best, miniMax(!isMax)) : Math.Min(best, miniMax(!isMax));

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
