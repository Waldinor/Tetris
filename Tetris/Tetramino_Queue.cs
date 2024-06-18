using System;

namespace Tetris
{
    public class Tetramino_Queue
    {
        // Setting all tetramino's in one mino's array
        private readonly Tetramino[] minos = new Tetramino[]
        {
            new I_mino(),
            new J_mino(),
            new L_mino(),
            new S_mino(),
            new Square_mino(),
            new T_mino(),
            new Z_mino()
        };

        // create an object of Random class
        private readonly Random block_choose = new Random();

        // object of Tetramino class for setting and getting Next Tetramino
        public Tetramino NextMino { get; private set; }

        // Setting random Tetramino for Mino's queue
        private Tetramino RandomMino()
        {
            return minos[block_choose.Next(minos.Length)];
        }

        // Constructor for queue
        public Tetramino_Queue() { NextMino = RandomMino(); }

        // getting and setting a tetramino queue
        // getting every tetramino in unique instance, so there is no duplicates in this queue
        public Tetramino GetandSetMinos()
        {
            Tetramino mino = NextMino;

            do
            {
                NextMino = RandomMino();
            }
            while (mino.id == NextMino.id);

            return mino;
        }

    }
}
