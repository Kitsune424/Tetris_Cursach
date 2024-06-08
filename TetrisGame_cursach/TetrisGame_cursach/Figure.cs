namespace TetrisGame_cursach
{
    public abstract class Figure
    {
        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        protected abstract GridPosition[][] Tiles { get; }

        /// <summary>
        /// Изначальнй отступ по матрице игровго поля
        /// </summary>
        protected abstract GridPosition StartOffset { get;}

        /// <summary>
        /// Идентификация фигуры
        /// </summary>
        public abstract int ID { get; }

        /// <summary>
        /// Положение поворота
        /// </summary>
        private int rotationState;

        /// <summary>
        /// Смещение текущей фигру по матрице игрового поля
        /// </summary>
        private GridPosition offset;

        /// <summary>
        /// Содержит информацию о структуре фигуры и в матрице и ее идентификационном номере
        /// </summary>
        public Figure()
        {
            offset = new GridPosition(StartOffset.Row, StartOffset.Column);
        }

        /// <summary>
        /// возвращение смещения и позиции объекта
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GridPosition> TilePositions()
        {
            foreach (GridPosition pos in Tiles[rotationState])
            {
                yield return new GridPosition(pos.Row + offset.Row, 
                    pos.Column + offset.Column);
            }
        }

        /// <summary>
        /// Поворот по часовой стрелке (вправро)
        /// </summary>
        public void RotateR()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        /// <summary>
        /// поворот против часовой стрелки (влево)
        /// </summary>
        public void RotateL()
        {
            if (rotationState == 0)
                rotationState = Tiles.Length - 1;
            else
                rotationState--;
        }

        /// <summary>
        /// Сброс поворота фигуры и ее позиции при смене
        /// </summary>
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }

        /// <summary>
        /// Перемещение фигуры
        /// </summary>
        public void Move(int rows, int colums)
        {
            offset.Row += rows;
            offset.Column += colums;
        }
    }
}
