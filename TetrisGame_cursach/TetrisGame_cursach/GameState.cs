using System;

namespace TetrisGame_cursach
{
    /// <summary>
    /// statemachine все возможные состояния
    /// </summary>
    public class GameState
    {
        private Figure currentFigure;
        public Figure CurrentFigure
        {
            get => currentFigure;
            private set
            {
                currentFigure = value;
                currentFigure.Reset();
            }
        }

        #region Parameters
        private int lines;
        private int streak;

        public GameGrid GameGrid { get; } //ссылка на игровую сетку
        public FigureQu FigureQu { get; } //ссылка на очередь
        public Figure HeldFigure { get; private set; }

        public bool GameOver { get; private set; }
        public bool CanHold {  get; private set; }

        public int Level { get; private set; } //lvl cnt
        public int Score { get; private set; } //Score cnt

        public int Lines 
        {
            get => lines;
            set
            {
                lines = value;
                Level = lines / 10;
                Score += StrakMultiply(streak) * (Level + 1);
            }
        }
        #endregion

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            FigureQu = new FigureQu();
            CurrentFigure = FigureQu.GetAndUpdate();
            CanHold = true;
        }

        /// <summary>
        /// Кол-во очков, получаемых в зависимости от кол-ва стертых срок за раз
        /// </summary>
        /// <param name="streak"> параметр, хранящий информацию о кол-ве
        /// стертых строк в прошлый раз</param>
        /// <returns></returns>
        private int StrakMultiply(int streak)
        {
            int streakMultiply = 0;
            if (streak == 1) { streakMultiply = 40; }
            if (streak == 2) { streakMultiply = 100; }
            if (streak == 3) { streakMultiply = 300; }
            if (streak == 4) { streakMultiply = 1200; }
            return streakMultiply;
        }

        #region Game staet for figure
        /// <summary>
        /// Фигура легальна? (в сетке и никого не пересекает)
        /// </summary>
        /// <returns></returns>
        private bool FigureFits()
        {
            foreach(GridPosition pos in CurrentFigure.TilePositions())
            {
                if (!GameGrid.IsEmpty(pos.Row, pos.Column))
                {
                    return false;
                }
            }
            return true;
        }

        public void HoldFigure()
        {
            if (!CanHold)
                return;

            if (HeldFigure == null)
            {
                HeldFigure = CurrentFigure;
                CurrentFigure = FigureQu.GetAndUpdate();
            }
            else
            {
                Figure temp = CurrentFigure;
                CurrentFigure = HeldFigure;
                HeldFigure = temp;
            }

            CanHold = false;
        }

        /// <summary>
        /// Поворот фигуры вправо (по часовой)
        /// </summary>
        public void RotateFigureR()
        {
            CurrentFigure.RotateR();

            if (!FigureFits())
            {
                CurrentFigure.RotateL();
            }
        }

        /// <summary>
        /// Поворот фигуры влево (против часовой)
        /// </summary>
        public void RotateFigureL()
        {
            CurrentFigure.RotateL();

            if (!FigureFits())
            {
                CurrentFigure.RotateR();
            }
        }

        /// <summary>
        /// Движение фигуры вправо
        /// </summary>
        public void MoveFigureR()
        {
            CurrentFigure.Move(0, 1);

            if (!FigureFits())
            {
                CurrentFigure.Move(0, -1);
            }
        }

        /// <summary>
        /// Движение фигуры влево
        /// </summary>
        public void MoveFigureL()
        {
            CurrentFigure.Move(0, -1);

            if (!FigureFits())
            {
                CurrentFigure.Move(0, 1);
            }
        }

        /// <summary>
        /// Проверка расстояния каждой плитки до возможного места размещения
        /// </summary>
        /// <returns></returns>
        private int TileDropDistance(GridPosition pos)
        {
            int distance = 0;

            while (GameGrid.IsEmpty(pos.Row + distance + 1, pos.Column))
                distance++;

            return distance;
        }

        /// <summary>
        /// Возможное расстояние для фигу в целом
        /// </summary>
        /// <returns></returns>
        public int FigureDropDistance()
        {
            int distance = GameGrid.Rows;

            foreach (GridPosition pos in CurrentFigure.TilePositions())
                distance = System.Math.Min(distance, TileDropDistance(pos));

            return distance;
        }

        /// <summary>
        /// Позволяет поместить фигуру в конец сетки сразу
        /// </summary>
        public void DropFigure()
        {
            CurrentFigure.Move(FigureDropDistance(), 0);
            PlaceFigure();
        }

        /// <summary>
        /// Можно ли вообще новую фигуру вставить?
        /// </summary>
        private void PlaceFigure()
        {
            foreach (GridPosition pos in CurrentFigure.TilePositions())
            {
                GameGrid[pos.Row, pos.Column] = CurrentFigure.ID;
            }

            streak = GameGrid.ClearFullStackRow();
            Lines += streak;

            Console.WriteLine(streak);
            Console.WriteLine(Lines);

            if (IsGameOver())
                GameOver = true;
            else
            {
                CurrentFigure = FigureQu.GetAndUpdate();
                CanHold = true;
            }
        }

        /// <summary>
        /// Двигаем фигуру вниз
        /// </summary>
        public void MoveBlockDown()
        {
            CurrentFigure.Move(1, 0);

            if (!FigureFits())
            {
                CurrentFigure.Move(-1, 0);
                PlaceFigure();
            }
        }
        #endregion

        /// <summary>
        /// Проверка, окончена ли игра
        /// </summary>
        /// <returns></returns>
        private bool IsGameOver()
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }
    }
}
