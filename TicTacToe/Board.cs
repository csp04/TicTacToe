using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class Board : IBoard
    {
        public static readonly int MAX_ROW = 3;
        public static readonly int MAX_COLUMN = 3;
        public static readonly string[] WINNING_COMBINATIONS = new[]
        {
            "123",
            "456",
            "789",
            "147",
            "258",
            "369",
            "159",
            "357"
        };

        private readonly Chip[,] _slots = new Chip[MAX_ROW, MAX_COLUMN];

        public Chip this[int slot]
        {
            get => Get(slot);
        }

        public Chip this[int row, int column]
        {
            get => Get(ToSlot(row, column));
        }

        public int[] AvailableSlots => GetAvailableSlots().ToArray();

        public Board()
        {
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < MAX_ROW; i++)
                for (int j = 0; j < MAX_COLUMN; j++)
                    _slots[i, j] = Chip.BLANK;
        }

        protected IEnumerable<int> GetAvailableSlots()
        {
            for (int i = 0; i < MAX_ROW; i++)
                for (int j = 0; j < MAX_COLUMN; j++)
                    if (_slots[i, j] == Chip.BLANK)
                        yield return ToSlot(i, j);
        }


        public Chip Get(int slot)
        {
            (int r, int c) = ToRowColumn(slot);

            return _slots[r, c];
        }

        public bool IsFull()
        {
            return AvailableSlots.Length == 0;
        }

        public bool IsSlotAvailable(int slot)
        {
            return Get(slot) == Chip.BLANK;
        }

        public void Put(int slot, Chip chip)
        {
            (int r, int c) = ToRowColumn(slot);

            _slots[r, c] = chip;
        }

        
        public Chip WinningChip()
        {
            foreach (var combination in WINNING_COMBINATIONS)
            {
                Chip[] chips = combination.ToCharArray().Select(c => (int)char.GetNumericValue(c))
                                           .Select(slot => Get(slot)).ToArray();

                if(chips[0] != Chip.BLANK) // handle chips to match, should not be blank (Jan-16-2020)
                    if (chips[0] == chips[1] && chips[0] == chips[2])
                        return chips[0];
            }

            return Chip.BLANK;
        }

        public static (int Row, int Column) ToRowColumn(int slot)
        {
            slot -= 1;
            int r = slot / Board.MAX_ROW;
            int c = slot - (Board.MAX_COLUMN * r);

            return (r, c);
        }
        
        public static int ToSlot(int row, int column)
        {
            return ((Board.MAX_ROW * row) + column) + 1;
        }
    }
}
