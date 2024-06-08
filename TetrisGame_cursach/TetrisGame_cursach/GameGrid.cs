namespace TetrisGame_cursach
{   
    
    public class GameGrid
    {
        /// <summary>
        /// Матрица игрового поля
        /// </summary>
        private readonly int[,] grid;

        /// <summary>
        /// Строки матрицы игрового поля
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// Колонки матрицы игровго поля
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// Устанвка матрицы
        /// </summary>
        /// <param name="rows">строки</param>
        /// <param name="columns">колонки</param>
        /// <returns></returns>
        public int this[int rows, int columns]
        {
            get => grid[rows, columns];
            set => grid[rows, columns] = value;
        }

        /// <summary>
        /// Отвечает за игровую сетку
        /// </summary>
        public GameGrid (int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }

        /// <summary>
        /// Проверка на нахождение строки или столбца в рамках игровой сетки
        /// </summary>
        public bool IsInside(int rows, int columns)
        {
            return rows >= 0 &&
                rows < Rows &&
                columns >= 0 &&
                columns < Columns;
        }

        /// <summary>
        /// Проверка на то, что ячейка пуста
        /// </summary>
        public bool IsEmpty(int rows, int columns)
        {
            return IsInside(rows, columns) &&
                grid[rows, columns] == 0;
        }

        /// <summary>
        /// проверка на заполнение строки целиком
        /// </summary>
        public bool IsRowFull(int rows)
        {
            for (int columns = 0; columns < Columns; columns++)
            {
                if (grid[rows, columns] == 0) { return false; }
            }
            return true;
        }

        /// <summary>
        /// Проверка на то, что строка пустая
        /// </summary>
        public bool IsRowEmpty(int rows)
        {
            for (int columns = 0; columns < Columns; columns++)
            {
                if (grid[rows, columns] != 0) { return false; }
            }
            return true;
        }

        /// <summary>
        /// Очистка заполненной строки
        /// </summary>
        /// <param name="rows">строки</param>
        private void ClearRow(int rows)
        {
            for (int columns = 0; columns < Columns; columns++)
            {
                grid[rows, columns] = 0;
            }
        }

        /// <summary>
        /// Подсчет кол-ва очищенных строк.
        /// нужно для понимания на сколько строк в низ нужно сдвигать все элементы
        /// </summary>
        /// <returns></returns>
        public int ClearFullStackRow()
        {
            int cleared = 0;

            for (int rows = Rows-1; rows >= 0; rows--)
            {
                if (IsRowFull(rows))
                {
                    ClearRow(rows);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(rows, cleared);
                }
            }
            return cleared;
        }

        /// <summary>
        /// Перемещенеие всех строк вниз на кол-во очищенных
        /// </summary>
        /// <param name="rows">строки</param>
        /// <param name="numRows">номер строки</param>
        private void MoveRowDown(int rows, int numRows)
        {
            for (int columns = 0; columns < Columns; columns++)
            {
                grid[rows + numRows, columns] = grid[rows, columns];
                grid[rows, columns] = 0;
            }
        }
    }
}
