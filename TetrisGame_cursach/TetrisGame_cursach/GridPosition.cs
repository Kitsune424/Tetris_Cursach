namespace TetrisGame_cursach
{   
    public class GridPosition
    {
        /// <summary>
        /// Строка
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Столбец
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// хранит информациюю по положении внутри сетки
        /// </summary>
        public GridPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
