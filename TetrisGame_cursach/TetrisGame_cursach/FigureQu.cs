using System;

namespace TetrisGame_cursach
{
    public class FigureQu
    {
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

        private readonly Random rnd = new Random();
        public Figure NextFigure { get; private set; }

        public FigureQu()
        {
            NextFigure = RandomFigure();
        }

        private Figure RandomFigure()
        {
            return figurs[rnd.Next(figurs.Length)];
        }

        public Figure GetAndUpdate()
        {
            Figure figure = NextFigure;

            do { NextFigure = RandomFigure(); }
          
            while (figure.ID == NextFigure.ID);
            return figure;
        }
    }
}
