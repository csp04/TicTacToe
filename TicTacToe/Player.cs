namespace TicTacToe
{
    public class Player : IPlayer
    {
        public string Name { get; private set; }

        public Chip Chip { get; private set; }

        protected IBoard Board { get; private set; }
        public Player(string name, Chip chip, IBoard board)
        {
            Name = name;
            Chip = chip;
            Board = board;
        }

        public bool Play(int slot)
        {
            if (!Board.IsSlotAvailable(slot))
                return false;

            Board.Put(slot, Chip);
            return true;
        }

        public bool Win()
        {
            return Board.WinningChip() == Chip;
        }
    }
}
