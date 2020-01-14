namespace TicTacToe
{
    public interface IBoard
    {
        int[] AvailableSlots { get; }

        Chip Get(int slot);
        void Put(int slot, Chip chip);

        bool IsFull();

        bool IsSlotAvailable(int slot);

        Chip WinningChip();


    }
}
