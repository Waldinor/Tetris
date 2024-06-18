/*
Start position to drop block from center - (0, 4)
Square-mino (represented with 4 in cell) rotates around center of matrix(2, 2). Center represented as *:

Matrix with center and without Square-mino:

        ---------
        |   |   |    
        ----*---- 
        |   |   | 
        --------- 

Default position:                       Rotate 90 degrees:
        ---------                           ---------
        | 4 | 4 |                           | 4 | 4 | 
        ----*----                           ----*----
        | 4 | 4 |                           | 4 | 4 | 
        ---------                           ---------

Rotate 180 degrees:                     Rotate 270 degrees:
        ---------                           ---------
        | 4 | 4 |                           | 4 | 4 | 
        ----*----                           ----*----
        | 4 | 4 |                           | 4 | 4 | 
        ---------                           ---------

In case of equality of all positions we can write only one and it willwork fine
*/

namespace Tetris
{
    public class Square_mino : Tetramino
    {
        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new(0, 0), new(0, 1), new(1, 0), new(1, 1) }, // positions of square-mino
        };

        public override int id => 4;
        protected override Position StartPoint => new Position(0, 4);
        protected override Position[][] Tiles => tiles;
    }
}