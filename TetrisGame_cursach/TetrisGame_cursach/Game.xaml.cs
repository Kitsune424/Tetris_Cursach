using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TetrisGame_cursach
{
    /// <summary>
    /// Игровое окно
    /// </summary>
    public partial class Game : Window
    {
        #region Fields
        /// <summary>
        /// Массив спрайтов для игровых плиток
        /// </summary>
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

        /// <summary>
        /// Массив спрайтов для игровых фигур
        /// </summary>
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

        /// <summary>
        /// Флаг состояния паузы
        /// </summary>
        private bool gamePause;

        /// <summary>
        /// Общая матрица всех спратов игрового поля
        /// </summary> 
        private readonly Image[,] imageControls;

        /// <summary>
        /// Инициализирует новый экземпляр класса GameState
        /// </summary>
        private GameState gameState = new GameState();

        /// <summary>
        /// Инициализирует новый экземпляр класса GameState
        /// </summary>
        private MusicList musicList = new MusicList();

        /// <summary>
        /// Инициализирует новый экземпляр класса MediaPlayer
        /// </summary>
        public MediaPlayer mediaPlayer = new MediaPlayer();
        #endregion

        /// <summary>
        /// Инициализирует новый экземляр Window класса Game
        /// </summary>
        public Game()
        {
            InitializeComponent();
            StartPlayAudio();

            imageControls = GameWindowSetup(gameState.GameGrid);
            gamePause = false;
        }

        #region Draw machine
        /// <summary>
        /// Создает сетку игрового поля
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
        /// Отрисовывает игровую сетку
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
                imageControls[pos.Row, pos.Column].Opacity = 1;
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
        /// Отображение значка удреживаемой фигуры
        /// </summary>
        /// <param name="heldFigure"></param>
        private void DrawHeldFigure(Figure heldFigure)
        {
            if (heldFigure == null)
                HoldIMG.Source = FigureImages[0];
            else
                HoldIMG.Source = FigureImages[heldFigure.ID];
        }

        /// <summary>
        /// Отрисовка "призрака фигуры" (место и положение преземеления)
        /// </summary>
        /// <param name="figure"></param>
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
        /// Общий метод отрисовки всех обновляемых объектов
        /// </summary>
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawGhostFigure(gameState.CurrentFigure);
            DrawNextBlock(gameState.FigureQu);
            DrawHeldFigure(gameState.HeldFigure);
            DrawFigure(gameState.CurrentFigure);

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
        /// асинхронный GameLoopMachine для автоматического обновления игрового поля
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
            mediaPlayer.Stop();

            FinalScoreTxt.Text = $"Score: {gameState.Score}";
            FinalLevelTxt.Text = $"Level: {gameState.Level}";
            FinalLineTxt.Text = $"Lines: {gameState.Lines}";

            WriteNewScore(gameState.Score, gameState.Level, gameState.Lines);
        }

        /// <summary>
        /// Остановка/возобновление игровой сессии
        /// </summary>
        private async void GamePause()
        {
            if (gamePause == false)
            {
                gamePause = true;
                GamePauseMenu.Visibility = Visibility.Visible;
                mediaPlayer.Pause();
            }
            else
            {
                gamePause = false;
                GamePauseMenu.Visibility = Visibility.Hidden;
                mediaPlayer.Play();
                await GameLoop();
            }

        }
        #endregion

        #region Score writer
        /// <summary>
        /// Перезаписывает .cfg файл сохраняя рекордное кол-во очков
        /// </summary>
        /// <param name="newScore">новые очки</param>
        /// <param name="newLevel">новые пройденный уровни</param>
        /// <param name="newLines">новые разрушенные линии</param>
        public void WriteNewScore(int newScore, int newLevel, int newLines)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = System.IO.Path.Combine(currentDirectory, "score.cfg");

            int currentScore = 0;
            int currentLevel = 0;
            int currentLines = 0;

            if (File.Exists(path))
            {
                string[] Lines = File.ReadAllLines(path);
                if (Lines.Length >= 3)
                {
                    int.TryParse(Lines[0], out currentScore);
                    int.TryParse(Lines[1], out currentLevel);
                    int.TryParse(Lines[2], out currentLines);
                }
            }

            if (newScore > currentScore)
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(newScore);
                    sw.WriteLine(newLevel);
                    sw.WriteLine(newLines);
                }
            }
        }
        #endregion

        #region Music control
        /// <summary>
        /// Случайное воспроизведение фоновой музыки при старте игровой сессии
        /// </summary>
        private void StartPlayAudio()
        {
            string filePath = musicList.GetRandomFile();
            var path = new Uri(filePath, UriKind.Relative);

            mediaPlayer.Volume = 1f;
            mediaPlayer.Open(path);
            mediaPlayer.Play();
            mediaPlayer.MediaEnded += (sender, e) =>
            {
                PlayTrak(musicList.GetNextFile());
            };
        }

        /// <summary>
        /// Воспроизведение аудио файла по пути нахождения
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        private void PlayTrak(string filePath)
        {
            mediaPlayer.Open(new Uri(filePath, UriKind.Relative));
            mediaPlayer.Play();
        }
        #endregion

        /// <summary>
        /// Метод запуска Game loop machine при запуске окна игровой сессии
        /// </summary>
        private async void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        /// <summary>
        /// Управление с клавиатуры
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
        
        /// <summary>
        /// Перезапуск игровой сессии по нажатию кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void PlayAgain_click(object sender, RoutedEventArgs e) 
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            StartPlayAudio();
            await GameLoop();
        }

        /// <summary>
        /// Возобновление игровой сессии по нажатию кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Play_click(object sender, RoutedEventArgs e)
        {
            gamePause = false;
            GamePauseMenu.Visibility = Visibility.Hidden;
            mediaPlayer.Play();
            await GameLoop();
        }

        /// <summary>
        /// Возвращение в главное меню по нажатию кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMenu(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu();
            this.Close();
            menu.Show();
        }

        /// <summary>
        /// Выход из приложения по нажатию кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_click(object sender, RoutedEventArgs e) 
        {
            Application.Current.Shutdown();
        }
    }
}