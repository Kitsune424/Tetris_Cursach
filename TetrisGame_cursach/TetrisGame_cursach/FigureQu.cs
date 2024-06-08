namespace TetrisGame_cursach
{
    public class FigureQu
    {
        /// <summary>
        /// Массив всех типов фигур
        /// </summary>
        private readonly Figure[] figurs = new Figure[]
        {
            new Figure_I(),
            new Figure_J(),
            new Figure_L(),
            new Figure_Q(),
            new Figure_S(),
            new Figure_Z(),
            new Figure_T()
        };

        /// <summary>
        /// Инициализирует новый экземпляр класса <c>Random<c>
        /// </summary>
        private readonly Random rnd = new Random();

        /// <summary>
        /// Информация о следующей фигуре
        /// </summary>
        public Figure NextFigure { get; private set; }

        /// <summary>
        /// Очередь из фигур
        /// </summary>
        public FigureQu()
        {
            NextFigure = RandomFigure();
        }

        /// <summary>
        /// Возвращает случайную фигуру из доступных в массиве
        /// </summary>
        /// <returns></returns>
        private Figure RandomFigure()
        {
            return figurs[rnd.Next(figurs.Length)];
        }

        /// <summary>
        /// Возвращает фигуру из массива и обновляет информацию о следующей
        /// </summary>
        /// <returns></returns>
        public Figure GetAndUpdate()
        {
            Figure figure = NextFigure;

            do { NextFigure = RandomFigure(); }
          
            while (figure.ID == NextFigure.ID);
            return figure;
        }
    }
}
