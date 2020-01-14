using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TicTacToe;
using UnbeatableTictacToe.Helpers;

namespace UnbeatableTictacToe.ViewModels
{

    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private IBoard _board;
        private IPlayer _player1;
        private IPlayer _player2;
        private IComputer _computer;
        public bool IsPlayerTurn { get; set; } = true;
        public bool EnableUI { get; set; } = true;
        public bool PlayVisible { get; set; } = false;
        

        public ICommand TurnCommand { get; }
        public ICommand PlayCommand { get; }

        public string Chip1 { get; set; }
        public string Chip2 { get; set; }
        public string Chip3 { get; set; }
        public string Chip4 { get; set; }
        public string Chip5 { get; set; }
        public string Chip6 { get; set; }
        public string Chip7 { get; set; }
        public string Chip8 { get; set; }
        public string Chip9 { get; set; }

        public string Announcement { get; set; }

        public GameViewModel()
        {
            Initialize();

            TurnCommand = new RelayCommand(parm =>
           {
               int slot = int.Parse(parm.ToString());
               if(Play(_player1, slot))
               {
                   Update();
                   IsPlayerTurn = false;
               }
           });

            PlayCommand = new RelayCommand(_ =>
           {
               Initialize();
           });

            PropertyChanged += GameViewModel_PropertyChanged;
        }

        private void Initialize()
        {
            _board = new Board();
            _player1 = new Player("Player 1", Chip.O, _board);
            _player2 = new Computer("Computer", Chip.X, _board);
            _computer = _player2 as IComputer;

             Chip1 = " ";
             Chip2 = " ";
             Chip3 = " ";
             Chip4 = " ";
             Chip5 = " ";
             Chip6 = " ";
             Chip7 = " ";
             Chip8 = " ";
             Chip9 = " ";

            EnableUI = true;
            PlayVisible = false;
            Announcement = "";
        }

        private async void GameViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsPlayerTurn")
            {
                if(!IsPlayerTurn)
                {
                    int slot = await Task.Run(() =>_computer.BestMove());
                    Play(_player2, slot);
                    Update();
                    IsPlayerTurn = true;
                }
            }
        }


        private bool Play(IPlayer p, int slot)
        {
            if(p.Play(slot))
            {
                switch (slot)
                {
                    case 1:
                        Chip1 = p.Chip.ToString();
                        break;
                    case 2:
                        Chip2 = p.Chip.ToString();
                        break;
                    case 3:
                        Chip3 = p.Chip.ToString();
                        break;
                    case 4:
                        Chip4 = p.Chip.ToString();
                        break;
                    case 5:
                        Chip5 = p.Chip.ToString();
                        break;
                    case 6:
                        Chip6 = p.Chip.ToString();
                        break;
                    case 7:
                        Chip7 = p.Chip.ToString();
                        break;
                    case 8:
                        Chip8 = p.Chip.ToString();
                        break;
                    case 9:
                        Chip9 = p.Chip.ToString();
                        break;
                }
                return true;
            }
            return false;
        }

        private bool IsGameOver(out string name)
        {
            bool draw = _board.IsFull();
            bool gameOver = draw || _player1.Win() || _player2.Win();
            name = string.Empty;

            if(gameOver)
            {
                if (_player1.Win())
                    name = _player1.Name;
                else if (_player2.Win())
                    name = _player2.Name;
            }

            return gameOver;
        }

        private void Update()
        {
            if(IsGameOver(out var name))
            {
                if (!string.IsNullOrEmpty(name))
                {
                    Announcement = name + " wins!";
                }
                else
                    Announcement = "Draw!";
                EnableUI = false;
                PlayVisible = true;
            }

        }
    }
}
