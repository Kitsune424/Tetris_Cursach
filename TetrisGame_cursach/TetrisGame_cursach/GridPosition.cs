namespace TetrisGame_cursach
{   
    /// <summary>
    /// хранит информациюю по положении сетки
    /// </summary>
    public class GridPosition
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public GridPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
