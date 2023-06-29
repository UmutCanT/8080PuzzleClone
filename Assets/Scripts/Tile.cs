using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Tile : MonoBehaviour
    {
        private int tilePositionX;
        private int tilePositionY;
        private TileStates tileState;

        public Tile(int tilePositionX, int tilePositionY)
        {
            this.tilePositionX = tilePositionX;
            this.tilePositionY = tilePositionY;
            tileState = (TileStates)Random.Range(0, 6);
        }

        public TileStates TileState { get => tileState; set => tileState = value; }
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