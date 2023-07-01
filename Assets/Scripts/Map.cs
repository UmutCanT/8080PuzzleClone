using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;
        private Vector3 nextTilePosition = new Vector3 (2, -2);
        private int gridWidth = 5;
        private int gridHeight = 5;

        private Grid<BlankTilePosition> levelMap;
        private List<Tile> tiles = new List<Tile>();

        public void CreateMap()
        {
            levelMap = new Grid<BlankTilePosition> (gridWidth, gridHeight, 1f, Vector3.zero,
                (Grid<BlankTilePosition> thisGrid, int xPosition, int yPosition) => new BlankTilePosition(thisGrid, xPosition, yPosition));

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    Tile tile = Instantiate(tilePrefab, transform.position, Quaternion.identity);
                    levelMap.GetGridObject(x, y).SetTile(tile);
                    tiles.Add(tile);
                }
            }
        }

        private int sameColorCount = 0;

        public void SearchNeighbors(int x, int y)
        {
            TileStates currentTileState = levelMap.GetGridObject(x, y).InsertedTile.TileState;

            if (Searchable(x + 1, y))
            {
                TileStates nextTileState = levelMap.GetGridObject(x + 1, y).InsertedTile.TileState;

                if (currentTileState == nextTileState)
                {
                    Debug.Log(currentTileState + " " + ++sameColorCount);
                    levelMap.GetGridObject(x + 1, y).InsertedTile.SearchedOnTurn = true;
                    SearchNeighbors(x + 1, y);
                }
            }
            else
                Debug.Log(x + " " + y);

            if (Searchable(x -1, y))
            {
                TileStates nextTileState = levelMap.GetGridObject(x - 1, y).InsertedTile.TileState;

                if (currentTileState == nextTileState)
                {
                    Debug.Log(currentTileState + " " + ++sameColorCount);
                    levelMap.GetGridObject(x - 1, y).InsertedTile.SearchedOnTurn = true;
                    SearchNeighbors(x - 1, y);
                }
            }
            else 
                Debug.Log(x + " " + y);


            if (Searchable(x, y + 1))
            {
                TileStates nextTileState = levelMap.GetGridObject(x, y + 1).InsertedTile.TileState;

                if (currentTileState == nextTileState)
                {
                    Debug.Log(currentTileState + " " + ++sameColorCount);
                    levelMap.GetGridObject(x, y + 1).InsertedTile.SearchedOnTurn = true;
                    SearchNeighbors(x, y + 1);
                }
            }
            else
                Debug.Log(x + " " + y);

            if (Searchable(x, y - 1))
            {
                TileStates nextTileState = levelMap.GetGridObject(x, y - 1).InsertedTile.TileState;

                if (currentTileState == nextTileState)
                {
                    Debug.Log(currentTileState + " " + ++sameColorCount);
                    levelMap.GetGridObject(x, y - 1).InsertedTile.SearchedOnTurn = true;
                    SearchNeighbors(x, y - 1);
                }
            }
            else
                Debug.Log(x + " " + y);
        }

        private bool Searchable(int x, int y)
        {
            return IsPositionInBounds(x, y) && !levelMap.GetGridObject(x, y).InsertedTile.SearchedOnTurn;
        }

        public Tile SpawnNextTile()
        {
            return Instantiate(tilePrefab, nextTilePosition, Quaternion.identity);
        }

        public bool IsPositionInBounds(int x, int y)
        {
            if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
