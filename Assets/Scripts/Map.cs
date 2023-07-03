using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private int sameStateTileCount = 0;
        private Stack<Tile> sameStateTileStack = new Stack<Tile>();
        private Stack<Tile> tileStackOnTurn = new Stack<Tile>(); // Later when we add swipe



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

        public void SearchTileNeighbors(int x, int y)
        {
            TileStates currentTileState = levelMap.GetGridObject(x, y).InsertedTile.TileState;

            if (SearchableTile(x + 1, y))
            {
                TileStates nextTileState = levelMap.GetGridObject(x + 1, y).InsertedTile.TileState;

                if (currentTileState == nextTileState)
                {
                    levelMap.GetGridObject(x + 1, y).InsertedTile.SearchedOnTurn = true;
                    AddTileToSameStateStack(levelMap.GetGridObject(x + 1, y).InsertedTile);
                    SearchTileNeighbors(x + 1, y);
                }
            }

            if (SearchableTile(x - 1, y))
            {
                TileStates nextTileState = levelMap.GetGridObject(x - 1, y).InsertedTile.TileState;

                if (currentTileState == nextTileState)
                {
                    levelMap.GetGridObject(x - 1, y).InsertedTile.SearchedOnTurn = true;
                    AddTileToSameStateStack(levelMap.GetGridObject(x - 1, y).InsertedTile);
                    SearchTileNeighbors(x - 1, y);
                }
            }

            if (SearchableTile(x, y + 1))
            {
                TileStates nextTileState = levelMap.GetGridObject(x, y + 1).InsertedTile.TileState;

                if (currentTileState == nextTileState)
                {
                    levelMap.GetGridObject(x, y + 1).InsertedTile.SearchedOnTurn = true;
                    AddTileToSameStateStack(levelMap.GetGridObject(x, y + 1).InsertedTile);
                    SearchTileNeighbors(x, y + 1);
                }
            }

            if (SearchableTile(x, y - 1))
            {
                TileStates nextTileState = levelMap.GetGridObject(x, y - 1).InsertedTile.TileState;

                if (currentTileState == nextTileState)
                {
                    levelMap.GetGridObject(x, y - 1).InsertedTile.SearchedOnTurn = true;
                    AddTileToSameStateStack(levelMap.GetGridObject(x, y - 1).InsertedTile);
                    SearchTileNeighbors(x, y - 1);
                }
            }
        }

        public void CheckSameStateStack(out Tile combinedTile)
        {
            combinedTile = null;

            if (sameStateTileCount >= 3)
            {            

                for (int i = 0; i < sameStateTileStack.Count - 1; i++)
                {
                    sameStateTileStack.ElementAt(i).TileState = TileStates.Blank;
                }
                combinedTile = sameStateTileStack.Last();
                combinedTile.TileState++;
            }

            sameStateTileCount = 0;
            sameStateTileStack.Clear();
        }
        
        public void TileToSearch(Tile tile, out int x, out int y)
        {
            x = tile.TilePosition.XPosition;
            y = tile.TilePosition.YPosition;
            AddTileToSameStateStack(tile);
            tile.SearchedOnTurn = true;
        }

        private void AddTileToSameStateStack(Tile tile)
        {
            sameStateTileStack.Push(tile);;
            sameStateTileCount++;
        }

        public void ClearSearch()
        {
            foreach (Tile tile in tiles)
            {
                tile.SearchedOnTurn = false;
            }
        }

        private bool SearchableTile(int x, int y)
        {
            return IsPositionInBounds(x, y) && !levelMap.GetGridObject(x, y).InsertedTile.SearchedOnTurn;
        }

        public Tile SpawnNextTile()
        {
            return Instantiate(tilePrefab, nextTilePosition, Quaternion.identity);
        }

        public Tile SpawnPointerTile()
        {
            Tile tile = Instantiate(tilePrefab, nextTilePosition, Quaternion.identity);
            tile.gameObject.SetActive(false);
            return tile;
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
