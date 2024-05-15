namespace TetrisGame_cursach
{
    public class Figure_I : Figure
    {
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(1, 0), new(1, 1), new(1, 2), new(1, 3)},
            new GridPosition[] {new(0, 2), new(1, 2), new(2, 2), new(3, 2)},
            new GridPosition[] {new(2, 0), new(2, 1), new(2, 2), new(2, 3)},
            new GridPosition[] {new(0, 1), new(1, 1), new(2, 1), new(3, 1)}
        };

        public override int ID => 1;
        protected override GridPosition StartOffset => new GridPosition(-1, 3);
        protected override GridPosition[][] Tiles => tiles;
    }

    public class Figure_J : Figure
    {
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 0), new(1, 0), new(1, 1), new(1, 2)},
            new GridPosition[] {new(0, 1), new(0, 2), new(1, 1), new(2, 1)},
            new GridPosition[] {new(1, 0), new(1, 1), new(1, 2), new(2, 2)},
            new GridPosition[] {new(0, 1), new(1, 1), new(2, 0), new(2, 1)}
        };

        public override int ID => 2;
        protected override GridPosition StartOffset => new GridPosition(0, 3);
        protected override GridPosition[][] Tiles => tiles;
    }

    public class Figure_L : Figure
    {
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 2), new(1, 0), new(1, 1), new(1, 2)},
            new GridPosition[] {new(0, 1), new(1, 1), new(2, 1), new(2, 2)},
            new GridPosition[] {new(1, 0), new(1, 1), new(1, 2), new(2, 0)},
            new GridPosition[] {new(0, 0), new(0, 1), new(1, 1), new(2, 1)}
        };

        public override int ID => 3;
        protected override GridPosition StartOffset => new GridPosition(0, 3);
        protected override GridPosition[][] Tiles => tiles;
    }

    public class Figure_Q : Figure
    {
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 0), new(0, 1), new(1, 0), new(1, 1)}
        };

        public override int ID => 4;
        protected override GridPosition StartOffset => new GridPosition(0, 4);
        protected override GridPosition[][] Tiles => tiles;
    }

    public class Figure_S : Figure
    {
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 1), new(0, 2), new(1, 0), new(1, 1)},
            new GridPosition[] {new(0, 1), new(1, 1), new(1, 2), new(2, 2)},
            new GridPosition[] {new(1, 1), new(1, 2), new(2, 0), new(2, 1)},
            new GridPosition[] {new(0, 0), new(1, 0), new(1, 1), new(2, 1)}
        };

        public override int ID => 5;
        protected override GridPosition StartOffset => new GridPosition(0, 3);
        protected override GridPosition[][] Tiles => tiles;
    }

    public class Figure_Z : Figure
    {
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 0), new(0, 1), new(1, 1), new(1, 2)},
            new GridPosition[] {new(0, 2), new(1, 1), new(1, 2), new(2, 1)},
            new GridPosition[] {new(1, 0), new(1, 1), new(2, 1), new(2, 2)},
            new GridPosition[] {new(0, 1), new(1, 0), new(1, 1), new(2, 0)}
        };

        public override int ID => 6;
        protected override GridPosition StartOffset => new GridPosition(0, 3);
        protected override GridPosition[][] Tiles => tiles;
    }

    public class Figure_T : Figure
    {
        private readonly GridPosition[][] tiles = new GridPosition[][]
        {
            new GridPosition[] {new(0, 1), new(1, 0), new(1, 1), new(1, 2)},
            new GridPosition[] {new(0, 1), new(1, 1), new(1, 2), new(2, 1)},
            new GridPosition[] {new(1, 0), new(1, 1), new(1, 2), new(2, 1)},
            new GridPosition[] {new(0, 1), new(1, 0), new(1, 1), new(2, 1)}
        };

        public override int ID => 7;
        protected override GridPosition StartOffset => new GridPosition(0, 3);
        protected override GridPosition[][] Tiles => tiles;
    }
}
