/*

 We must store:
    - tiles of mino's
    - start point 
    - current position
    - one of 7 type of blocks (in fact colors too but for example I-mino can be only cyan)
    - offset
    - rotation

Every tetramino rotates around one point (point shows in every mino's file) and have it's own matrix to rotate.

Every mino must:
    - rotate
    - move

Tetramino position matrix (for all minos with same matrix shape is same):
2 x 2:                      3 x 3:
      0   1                         0   1   2     
    ---------                     -------------
0   |   |   |                 0   |   |   |   |
    ---------                     -------------
1   |   |   |                 1   |   |   |   |
    ---------                     -------------
                              2   |   |   |   |
                                  -------------
4 x 4:
     0   1  2  3
    -------------
0   |  |  |  |  |
    -------------
1   |  |  |  |  |
    -------------
2   |  |  |  |  |
    -------------
3   |  |  |  |  |
    -------------

*/
using System.Collections.Generic;

namespace Tetris
{
    public abstract class Tetramino
    {
        protected abstract Position[][] Tiles { get; }

        protected abstract Position StartPoint { get; }

        public abstract int id { get; }

        private int rotationstate;
        private Position offset;

        public Tetramino() => offset = new Position(StartPoint.row, StartPoint.column);

        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[rotationstate]) yield return new Position(p.row + offset.row, p.column + offset.column);
        }

        // rotating tetramino clockwise
        public void Rotate()
        {
            rotationstate = (rotationstate + 1) % Tiles.Length;
        }

        // rotating tetramino counter clockwise
        public void RotateCount()
        {
            if (rotationstate == 0) rotationstate = Tiles.Length - 1;
            else rotationstate--;
        }
        
        // move tetramino
        public void Move(int r, int c)
        {
            offset.row += r;
            offset.column += c;
        }

        // reset tetramino's position and rotation
        public void ResetMino()
        {
            rotationstate = 0;
            offset.row = StartPoint.row;
            offset.column = StartPoint.column;
        }
    }
}