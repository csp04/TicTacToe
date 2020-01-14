namespace TicTacToe
{
    public interface IPlayer
    {
        string Name { get; }
        Chip Chip { get; }

        bool Play(int slot);

        bool Win();
    }
}
