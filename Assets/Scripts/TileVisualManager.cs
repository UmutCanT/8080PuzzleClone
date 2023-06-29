using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class TileVisualManager : MonoBehaviour
    {
        [SerializeField] private TileVisualTemplate blankTileVisual;
        [SerializeField] private TileVisualTemplate fiveTileVisual;
        [SerializeField] private TileVisualTemplate tenTileVisual;
        [SerializeField] private TileVisualTemplate twentyTileVisual;
        [SerializeField] private TileVisualTemplate fortyTileVisual;
        [SerializeField] private TileVisualTemplate eightyTileVisual;
        [SerializeField] private TileVisualTemplate eightyEightyTileVisual;

        private void Start()
        {
            
        }

        private void ChangeTileVisual(List<Tile> tiles)
        {

        }
    }
}
