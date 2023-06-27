using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Map : MonoBehaviour
    {
        private int gridWidth = 5;
        private int gridHeight = 5;

        private Grid<BlankTilePosition> levelMap;

        public void CreateMap()
        {
            levelMap = new Grid<BlankTilePosition> (gridWidth, gridHeight, 1f, Vector3.zero, 
                (Grid<BlankTilePosition> thisGrid, int xPosition, int yPosition) => new BlankTilePosition(thisGrid, xPosition, yPosition));
        }

        public void Start()
        {
            CreateMap();
        }
    }
}
