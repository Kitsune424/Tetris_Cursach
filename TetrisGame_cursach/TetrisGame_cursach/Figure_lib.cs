namespace TetrisGame_cursach
{
    /// <summary>
    /// Описывает фигру типа I
    /// </summary>
    public class Figure_I : Figure
    {
        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(1, 0), new(1, 1), new(1, 2), new(1, 3)},
            new GridPosition[] {new(0, 2), new(1, 2), new(2, 2), new(3, 2)},
            new GridPosition[] {new(2, 0), new(2, 1), new(2, 2), new(2, 3)},
            new GridPosition[] {new(0, 1), new(1, 1), new(2, 1), new(3, 1)}
        };

        /// <summary>
        /// Идентификация фигуры
        /// </summary>
        public override int ID => 1;

        /// <summary>
        /// Изначальнй отступ по матрице игровго поля
        /// </summary>
        protected override GridPosition StartOffset => new GridPosition(-1, 3);

        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        protected override GridPosition[][] Tiles => tiles;
    }

    /// <summary>
    /// Описывает фигуру типа J
    /// </summary>
    public class Figure_J : Figure
    {
        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 0), new(1, 0), new(1, 1), new(1, 2)},
            new GridPosition[] {new(0, 1), new(0, 2), new(1, 1), new(2, 1)},
            new GridPosition[] {new(1, 0), new(1, 1), new(1, 2), new(2, 2)},
            new GridPosition[] {new(0, 1), new(1, 1), new(2, 0), new(2, 1)}
        };

        /// <summary>
        /// Идентификация фигуры
        /// </summary>
        public override int ID => 2;

        /// <summary>
        /// Изначальнй отступ по матрице игровго поля
        /// </summary>
        protected override GridPosition StartOffset => new GridPosition(0, 3);

        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        protected override GridPosition[][] Tiles => tiles;
    }

    /// <summary>
    /// Описывает фигру типа L
    /// </summary>
    public class Figure_L : Figure
    {
        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 2), new(1, 0), new(1, 1), new(1, 2)},
            new GridPosition[] {new(0, 1), new(1, 1), new(2, 1), new(2, 2)},
            new GridPosition[] {new(1, 0), new(1, 1), new(1, 2), new(2, 0)},
            new GridPosition[] {new(0, 0), new(0, 1), new(1, 1), new(2, 1)}
        };

        /// <summary>
        /// Идентификация фигуры
        /// </summary>
        public override int ID => 3;

        /// <summary>
        /// Изначальнй отступ по матрице игровго поля
        /// </summary>
        protected override GridPosition StartOffset => new GridPosition(0, 3);

        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        protected override GridPosition[][] Tiles => tiles;
    }

    /// <summary>
    /// Описывает фигуру типа Q
    /// </summary>
    public class Figure_Q : Figure
    {
        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 0), new(0, 1), new(1, 0), new(1, 1)}
        };

        /// <summary>
        /// Идентификация фигуры
        /// </summary>
        public override int ID => 4;

        /// <summary>
        /// Изначальнй отступ по матрице игровго поля
        /// </summary>
        protected override GridPosition StartOffset => new GridPosition(0, 4);

        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        protected override GridPosition[][] Tiles => tiles;
    }

    /// <summary>
    /// Описывает фигуру типа S
    /// </summary>
    public class Figure_S : Figure
    {
        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 1), new(0, 2), new(1, 0), new(1, 1)},
            new GridPosition[] {new(0, 1), new(1, 1), new(1, 2), new(2, 2)},
            new GridPosition[] {new(1, 1), new(1, 2), new(2, 0), new(2, 1)},
            new GridPosition[] {new(0, 0), new(1, 0), new(1, 1), new(2, 1)}
        };

        /// <summary>
        /// Идентификация фигуры
        /// </summary>
        public override int ID => 5;

        /// <summary>
        /// Изначальнй отступ по матрице игровго поля
        /// </summary>
        protected override GridPosition StartOffset => new GridPosition(0, 3);

        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        protected override GridPosition[][] Tiles => tiles;
    }

    /// <summary>
    /// Описывает фигуру типа Z
    /// </summary>
    public class Figure_Z : Figure
    {
        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 0), new(0, 1), new(1, 1), new(1, 2)},
            new GridPosition[] {new(0, 2), new(1, 1), new(1, 2), new(2, 1)},
            new GridPosition[] {new(1, 0), new(1, 1), new(2, 1), new(2, 2)},
            new GridPosition[] {new(0, 1), new(1, 0), new(1, 1), new(2, 0)}
        };

        /// <summary>
        /// Идентификация фигуры
        /// </summary>
        public override int ID => 6;

        /// <summary>
        /// Изначальнй отступ по матрице игровго поля
        /// </summary>
        protected override GridPosition StartOffset => new GridPosition(0, 3);

        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        protected override GridPosition[][] Tiles => tiles;
    }

    /// <summary>
    /// Описывает фигуру типа T
    /// </summary>
    public class Figure_T : Figure
    {
        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 1), new(1, 0), new(1, 1), new(1, 2)},
            new GridPosition[] {new(0, 1), new(1, 1), new(1, 2), new(2, 1)},
            new GridPosition[] {new(1, 0), new(1, 1), new(1, 2), new(2, 1)},
            new GridPosition[] {new(0, 1), new(1, 0), new(1, 1), new(2, 1)}
        };

        /// <summary>
        /// Идентификация фигуры
        /// </summary>
        public override int ID => 7;

        /// <summary>
        /// Изначальнй отступ по матрице игровго поля
        /// </summary>
        protected override GridPosition StartOffset => new GridPosition(0, 3);

        /// <summary>
        /// Положение плиток образующих фигру
        /// </summary>
        protected override GridPosition[][] Tiles => tiles;
    }
}
