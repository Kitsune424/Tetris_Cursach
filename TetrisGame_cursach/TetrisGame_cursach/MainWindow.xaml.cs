using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TetrisGame_cursach
{
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] TileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("TetrisAssets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/TileRed.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/TilePurple.png", UriKind.Relative))
        };

        private readonly ImageSource[] FigureImages = new ImageSource[]
        {
            new BitmapImage(new Uri("TetrisAssets/FigureEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/Figure-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/Figure-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/Figure-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/Figure-Q.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/Figure-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/Figure-Z.png", UriKind.Relative)),
            new BitmapImage(new Uri("TetrisAssets/Figure-T.png", UriKind.Relative))
        };

        string[] musiclist = new string[]
        {
            "Music/Tetris-ElectroSwing.wav",
            "Music/Tetris-metal.wav",
            "Music/Tetris-miatriss_remix_.wav",
            "Music/Tetris-original.wav",
            "Music/Tetris-phonk.wav"
        };

        private bool gamePause;
        private readonly Image[,] imageControls;
        private GameState gameState = new GameState();

        public MainWindow()
        {
            InitializeComponent();
            imageControls = GameWindowSetup(gameState.GameGrid);
            gamePause = true;
            
        }
        #region Draw machine
        /// <summary>
        /// Задаем параметры игрового поля
        /// </summary>
        private Image[,] GameWindowSetup(GameGrid grid)
        {
            int SqrSize = 25;
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];

            for (int rows = 0; rows < grid.Rows; rows++)
            {
                for (int columns = 0; columns < grid.Columns; columns++)
                {
                    Image imageControl = new Image
                    {
                        Width = SqrSize,
                        Height = SqrSize
                    };

                    Canvas.SetTop(imageControl, (rows - 2) * SqrSize + 10);
                    Canvas.SetLeft(imageControl, columns * SqrSize);
                    GameWindow.Children.Add(imageControl);
                    imageControls[rows, columns] = imageControl;
                }
            }
            return imageControls;
        }

        /// <summary>
        /// Отрисовка игровой сетки
        /// </summary>
        private void DrawGrid(GameGrid grid)
        {
            for (int rows = 0; rows < grid.Rows; rows++)
            {
                for (int columns = 0; columns < grid.Columns; columns++)
                {
                    int id = grid[rows, columns];
                    imageControls[rows, columns].Source = TileImages[id];
                    imageControls[rows, columns].Opacity = 1;
                }
            }
        }

        /// <summary>
        /// Отрисовка фигуры
        /// </summary>
        private void DrawFigure(Figure figure)
        {
            foreach (GridPosition pos in figure.TilePositions())
            {
                imageControls[pos.Row, pos.Column].Source = TileImages[figure.ID];
            }
        }

        /// <summary>
        /// Смена значка фигуры, которая будет следующая
        /// </summary>
        /// <param name="figureQ"></param>
        private void DrawNextBlock(FigureQu figureQ)
        {
            Figure next = figureQ.NextFigure;
            NextIMG.Source = FigureImages[next.ID];
        }

        /// <summary>
        /// Отрисовка удерживаемой фигуры
        /// </summary>
        /// <param name="heldFigure"></param>
        private void DrawHeldFigure(Figure heldFigure)
        {
            if (heldFigure == null)
                HoldIMG.Source = FigureImages[0];
            else
                HoldIMG.Source = FigureImages[heldFigure.ID];
        }

        private void DrawGhostFigure(Figure figure)
        {
            int dropDistance = gameState.FigureDropDistance();

            foreach (GridPosition pos in figure.TilePositions())
            {
                imageControls[pos.Row + dropDistance, pos.Column].Source = TileImages[figure.ID];
                imageControls[pos.Row + dropDistance, pos.Column].Opacity = 0.25;

            }
        }
        
        /// <summary>
        /// Отрисовка всех игровых объектов
        /// </summary>
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawGhostFigure(gameState.CurrentFigure);
            DrawFigure(gameState.CurrentFigure);
            DrawNextBlock(gameState.FigureQu);
            DrawHeldFigure(gameState.HeldFigure);

            ScoreTxt.Text = $"Score: {gameState.Score}";
            LevelTxt.Text = $"Level: {gameState.Level}";
            LinesTxt.Text = $"Lines: {gameState.Lines}";
            
        }
        #endregion

        #region Game loop machine
        /// <summary>
        /// Ускоряет падение фигур в зависимости от уровня
        /// </summary>
        /// <returns></returns>
        private int SpeedMultiply()
        {
            int speed = 450;
            if (gameState.Level >= 0 && gameState.Level <= 10 && gamePause == false)
            {
                speed = speed - gameState.Level * 5;
            }
            else if (gamePause == true) { return 9999999; }
            else
            {
                if (gameState.Level == 13) { speed = 145; }
                if (gameState.Level == 16) { speed = 100; }
                if (gameState.Level == 19) { speed = 70; }
                if (gameState.Level == 29) { speed = 20; }
            }

            return speed;
        }

        /// <summary>
        /// асинхронный GameLoopMachine для автоматического перемещения игровых фигур
        /// не блокируя пользовательский ввод
        /// </summary>
        /// <returns></returns>
        private async Task GameLoop()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                await Task.Delay(SpeedMultiply());
                gameState.MoveBlockDown();
                Draw(gameState);
            }

            //Цикл сверху бесконечный, а значит мы придем сюда только по окончанию игры

            GameOverMenu.Visibility = Visibility.Visible;
            GameMusicPlayer musicPlayer = new GameMusicPlayer(musiclist);
            musicPlayer.Stop();
            MusicName.Text = musicPlayer.MusicName();
            FinalScoreTxt.Text = $"Score: {gameState.Score}";
            FinalLevelTxt.Text = $"Level: {gameState.Level}";
            FinalLineTxt.Text = $"Lines: {gameState.Lines}";
        }

        //ставим игру на паузу
        private void GamePause()
        {
            gamePause = true;
            GamePauseMenu.Visibility = Visibility.Visible;

            GameMusicPlayer musicPlayer = new GameMusicPlayer(musiclist);
            musicPlayer.Stop();
            MusicName.Text = musicPlayer.MusicName();
        }
        #endregion

        /// <summary>
        /// Загрузка игрового окна
        /// </summary>
        private async void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        /// <summary>
        /// Перемещение фигуры нажатием клавиш
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver) { return; }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveFigureL();
                    break;

                case Key.Right:
                    gameState.MoveFigureR();
                    break;

                case Key.Down:
                    gameState.MoveBlockDown();
                    break;

                case Key.Up:
                    gameState.RotateFigureR();
                    break;

                case Key.Z:
                    gameState.RotateFigureL();
                    break;

                case Key.C:
                    gameState.HoldFigure();
                    break;

                case Key.Space:
                    gameState.DropFigure();
                    break;

                case Key.P:
                    GamePause();
                    break;

                default:
                    return;
            }
            Draw(gameState);
        }
        
        
        // кнопка перезапуска
        private async void PlayAgain_click(object sender, RoutedEventArgs e) 
        {
            GameMusicPlayer musicPlayer = new GameMusicPlayer(musiclist);
            musicPlayer.PlayRandom();
            MusicName.Text = musicPlayer.MusicName();

            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        // кнопка старта игры
        private async void Play_click(object sender, RoutedEventArgs e)
        {
            GameMusicPlayer musicPlayer = new GameMusicPlayer(musiclist);
            musicPlayer.PlayRandom();
            MusicName.Text = musicPlayer.MusicName();

            gamePause = false;
            GamePauseMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private void Exit_click(object sender, RoutedEventArgs e) 
        {
            Application.Current.Shutdown();
        }
    }
}