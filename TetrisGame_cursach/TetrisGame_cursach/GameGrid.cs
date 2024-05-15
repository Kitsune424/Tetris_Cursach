using System.Windows.Controls;

namespace TetrisGame_cursach
{   
    /// <summary>
    /// Отвечает за игровую сетку
    /// </summary>
    public class GameGrid
    {
        private readonly int[,] grid;

        public int Rows { get; }
        public int Columns { get; }

        public int this[int rows, int columns]
        {
            get => grid[rows, columns];
            set => grid[rows, columns] = value;
        }

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
        /// <param name="rows"></param>
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
        /// <param name="rows"></param>
        /// <param name="numRows"></param>
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
