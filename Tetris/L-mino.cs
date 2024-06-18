/*
Start position to drop block from center - (0, 3)
L-mino (represented with 3 in cell) rotates around center of matrix(3, 3). Center represented as *:

Matrix with center and without L-mino:

        -------------
        |   |   |   |                    
        ------------- 
        |   | * |   | 
        ------------- 
        |   |   |   | 
        -------------

Default position:                       Rotate 90 degrees:
        -------------                      ------------- 
        |   |   | 3 |                      |   | 3 |   |                      
        -------------                      -------------
        | 3 | 3 | 3 |                      |   | 3 |   | 
        -------------                      -------------
        |   |   |   |                      |   | 3 | 3 |
        -------------                      -------------

Rotate 180 degrees:                     Rotate 270 degrees:
        -------------                       -------------
        |   |   |   |                       | 3 | 3 |   |
        -------------                       -------------
        | 3 | 3 | 3 |                       |   | 3 |   |
        -------------                       -------------
        | 3 |   |   |                       |   | 3 |   |
        -------------                       -------------
*/

namespace Tetris
{
    public class L_mino : Tetramino
    {
        private readonly Position[][] tiles = new Position[][]
            {
            new Position[] { new(0, 2), new(1, 0), new(1, 1), new(1, 2) }, // default position
            new Position[] { new(0, 1), new(1, 1), new(2, 1), new(2, 2) }, // 90 degrees position
            new Position[] { new(1, 0), new(1, 1), new(1, 2), new(2, 0) }, // 180 degrees position
            new Position[] { new(0, 0), new(0, 1), new(1, 1), new(2, 1) }, // 270 degrees position
            };

        public override int id => 3;
        protected override Position StartPoint => new Position(0, 3);
        protected override Position[][] Tiles => tiles;
    }
}