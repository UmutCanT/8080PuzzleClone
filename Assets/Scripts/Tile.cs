using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Tile
    {
        private TileVisualTemplate tileVisual;
        private int tilePositionX;
        private int tilePositionY;
        private TileStates tileState;

        public Tile(TileVisualTemplate tileViusal, int tilePositionX, int tilePositionY)
        {
            this.tileVisual = tileViusal;
            this.tilePositionX = tilePositionX;
            this.tilePositionY = tilePositionY;
            tileState = (TileStates)Random.Range(0, 6);
        }
    }

    public enum TileStates
    {
        Blank,
        Five,
        Ten,
        Twenty,
        Forty,
        Eighty,
        EightyEighty,
        Explosion
    }
}