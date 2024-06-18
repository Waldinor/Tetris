/*
Start position to drop block from center - (-1, 3)
I-mino (represented with 1 in cell) rotates around center of matrix(4, 4). Center represented as *:
Default position:                       Rotate 90 degrees:
        -----------------                       ----------------- 
        |   |   |   |   |                       |   |   | 1 |   |                      
        -----------------                       -----------------
        | 1 | 1 | 1 | 1 |                       |   |   | 1 |   | 
        --------*--------                       --------*--------
        |   |   |   |   |                       |   |   | 1 |   | 
        -----------------                       -----------------
        |   |   |   |   |                       |   |   | 1 |   | 
        -----------------                       -----------------

Rotate 180 degrees:                     Rotate 270 degrees:
        -----------------                       -----------------
        |   |   |   |   |                       |   | 1 |   |   |
        -----------------                       -----------------
        |   |   |   |   |                       |   | 1 |   |   |
        --------*--------                       --------*--------
        | 1 | 1 | 1 | 1 |                       |   | 1 |   |   |
        -----------------                       -----------------
        |   |   |   |   |                       |   | 1 |   |   |
        -----------------                       -----------------
*/

namespace Tetris
{
    public class I_mino : Tetramino
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new(1, 0), new(1, 1), new(1, 2), new(1, 3) }, // default position
            new Position[] { new(0, 2), new(1, 2), new(2, 2), new(3, 2) }, // 90 degrees position
            new Position[] { new(2, 0), new(2, 1), new(2, 2), new(2, 3) }, // 180 degrees position
            new Position[] { new(0, 1), new(1, 1), new(2, 1), new(3, 1) }, // 270 degrees position
        };

        public override int id => 1;
        protected override Position StartPoint => new Position(-1, 3);
        protected override Position[][] Tiles => tiles;
    }
}