namespace Tetris
{
    public class Position
    {
        public int row { get; set; }
        public int column { get; set; }

        public Position(int r, int c)
        {
            row = r;
            column = c;
        }
    }
}