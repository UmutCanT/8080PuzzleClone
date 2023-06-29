using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;
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

        public void Start()
        {
            CreateMap();
            GameObject.FindObjectOfType<TileVisualManager>().GetComponent<TileVisualManager>().ChangeTileVisual(tiles);
        }
    }
}
