using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class BlankTilePosition
    {
        private int xPosition;
        private int yPosition;
        private Grid<BlankTilePosition> parentGrid;
        private Tile insertedTile;

        public int XPosition => xPosition;
        public int YPosition => yPosition;
        public Tile InsertedTile => insertedTile;

        public BlankTilePosition(Grid<BlankTilePosition> parentGrid, int xPosition, int yPosition)
        {
            this.parentGrid = parentGrid;
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }

        public void SetTile(Tile insertedTile)
        {
            this.insertedTile = insertedTile;
            this.insertedTile.transform.position = parentGrid.GetWorldPosition(xPosition, yPosition);
            this.insertedTile.TilePosition = this;
            parentGrid.TriggerGridObjectChanged(xPosition, yPosition);
        }

        public TileStates GetTileState()
        {
            return insertedTile.TileState;
        }
    }
}
