using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Map : MonoBehaviour
    {
        private int gridWidth = 5;
        private int gridHeight = 5;

        private Grid<Tile> levelMap;

        public void CreateMap()
        {
            levelMap = new Grid<Tile> (gridWidth, gridHeight, 1f, Vector3.zero, 
                (Grid<Tile> thisGrid, int xPosition, int yPosition) => new Tile(thisGrid, xPosition, yPosition));
        }
    }
}
