using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Tile
    {
        private int xPosition;
        private int yPosition;
        private Grid<Tile> parentGrid;

        public Tile(Grid<Tile> parentGrid, int xPosition, int yPosition)
        {
            this.parentGrid = parentGrid;
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }
    }
}
