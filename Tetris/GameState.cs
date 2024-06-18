namespace Tetris
{
    public class GameState
    {
        private Tetramino current_mino;

        public Tetramino CurrentMino
        {
            get => current_mino;
            private set
            {
                // setting current tetramino and spawning it in center of a grid
                current_mino = value;
                current_mino.ResetMino();

                for (int i = 0; i < 2; i++)
                {
                    current_mino.Move(1, 0);

                    if (!MinoFits()) current_mino.Move(-1, 0);
                }
            }
        }

        public GameGrid grid { get; }
        public Tetramino_Queue MinoQueue { get; }

        public bool game_over { get; private set; }

        public int Score { get; private set; }

        public Tetramino HeldMino { get; private set; }

        public bool CanHold { get; private set; }

        // game constructor
        public GameState()
        {
            grid = new GameGrid();
            MinoQueue = new Tetramino_Queue();
            CurrentMino = MinoQueue.GetandSetMinos();
            CanHold = true;
        }

        // checking: can tetramino be in this position?
        // first of all using for checking rotations because tetraminos can go beyond of grid
        private bool MinoFits()
        {
            foreach (Position p in CurrentMino.TilePositions())
            {
                if (!grid.IsEmpty(p.row, p.column)) return false;
            }
            return true;
        }
        
        // holding tetramino
        public void HoldMino()
        {
            if (!CanHold) return;

            if (HeldMino == null)
            {
                HeldMino = CurrentMino;
                CurrentMino = MinoQueue.GetandSetMinos();
            }
            else
            {
                Tetramino tmp = CurrentMino;
                CurrentMino = HeldMino;
                HeldMino = tmp;
            }

            CanHold = false;
        }

        // rotate mino clockwise
        public void RotateMino()
        {
            CurrentMino.Rotate();

            if (!MinoFits()) CurrentMino.RotateCount();
        }

        // frotate mino counter clockwise
        public void RotateMinoCounter()
        {
            CurrentMino.RotateCount();

            if (!MinoFits()) CurrentMino.Rotate();
        }

        //moving tetramino left on one cell
        public void MoveMinoLeft()
        {
            CurrentMino.Move(0, -1);

            if (!MinoFits()) CurrentMino.Move(0, 1);
        }

        // moving tetramino right on one cell
        public void MoveMinoRight()
        {
            CurrentMino.Move(0, 1);

            if (!MinoFits()) CurrentMino.Move(0, -1);
        }

        // checking is game over or not
        private bool IsGameOver()
        {
            return !(grid.CheckEmptyRow(0) && grid.CheckEmptyRow(1));
        }

        // placing minos on the grid
        private void PlaceMino()
        {
            // setting position for placed tetramino
            foreach (Position p in CurrentMino.TilePositions())
            {
                grid[p.row, p.column] = CurrentMino.id;
            }

            // adding score if player cleared row(-s)
            Score += grid.ClearRow();

            // if game voer - end game, else get new tetramino and set variable hold to true
            if (IsGameOver()) game_over = true;
            else
            {
                CurrentMino = MinoQueue.GetandSetMinos();
                CanHold = true;
            }
        }

        // moving tetramino down on one cell
        public void MoveMinoDown()
        {
            CurrentMino.Move(1, 0);

            if (!MinoFits())
            {
                CurrentMino.Move(-1, 0);
                PlaceMino();
            }
        }

        // calculating free cells under current tetramino
        private int DropDistance(Position p)
        {
            int drop = 0;

            while (grid.IsEmpty(p.row + drop + 1, p.column)) drop++;

            return drop;
        }

        // calculating free cells (including tiles of tetramino) under current mino
        public int DropMinoDistance()
        {
            int drop = grid.rows;

            foreach (Position p in CurrentMino.TilePositions())
            {
                drop = System.Math.Min(drop, DropDistance(p));
            }

            return drop;
        }

        // hard drop of current tetramino
        public void DropMino()
        {
            CurrentMino.Move(DropMinoDistance(), 0);
            PlaceMino();
        }
    }
}
