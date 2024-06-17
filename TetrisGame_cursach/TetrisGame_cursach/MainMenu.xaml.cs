using System.IO;
using System.Windows;

namespace TetrisGame_cursach
{
  
    public partial class MainMenu : Window
    {
        /// <summary>
        /// Рекордное кол-во очков
        /// </summary>
        public int _score = 0;

        /// <summary>
        /// Рекордное кол-во уровней
        /// </summary>
        public int _level = 0;

        /// <summary>
        /// Рекордное кол-во линий
        /// </summary>
        public int _lines = 0;

        /// <summary>
        /// Главное меню
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
            PrintScore();
        }

        /// <summary>
        /// Отображение рекордной статистики
        /// </summary>
        private void PrintScore()
        {
            int _score = 0;
            int _level = 0;
            int _lines = 0;

            ReadScore(out _score, out _level, out _lines);
            score.Content = $"Score: {_score}";
            level.Content = $"Level: {_level}";
            lines.Content = $"Lines: {_lines}";
        }

        /// <summary>
        /// Считывание рекордных значений с score.cfg
        /// </summary>
        /// <param name="score">очки</param>
        /// <param name="level">уровни</param>
        /// <param name="lines">линии</param>
        private void ReadScore(out int score, out int level, out int lines)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = Path.Combine(currentDirectory, "score.cfg");

            score = 0;
            level = 0;
            lines = 0;

            if (File.Exists(path))
            {
                string[] Lines = File.ReadAllLines(path);
                if (Lines.Length >= 3) 
                {
                    int.TryParse(Lines[0], out score);
                    int.TryParse(Lines[1], out level);
                    int.TryParse(Lines[2], out lines);
                }
            }
        }

        /// <summary>
        /// Начало игровой сессии по нажатию кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            this.Close();
            game.Show();
        }

        private void GetInfo(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            info.ShowDialog();
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
