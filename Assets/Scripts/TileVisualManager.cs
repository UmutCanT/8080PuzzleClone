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

        private void Awake()
        {
            Tile.OnTileStateChange += Tile_OnTileStateChange;
        }

        private void Tile_OnTileStateChange(object sender, Tile.OnTileStateChangeEventArgs e)
        {
            ChangeTileVisual(e.tile);
        }

        public void ChangeTileVisual(List<Tile> tiles)
        {
            foreach (Tile tile in tiles)
            {
                ChangeTileVisual(tile);
            }
        }

        private void ChangeTileVisual(Tile tile) 
        {
            switch (tile.TileState)
            {
                case TileStates.Blank:
                    tile.ChangeTileVisualSprite(blankTileVisual.TileSprite);
                    break;
                case TileStates.Five:
                    tile.ChangeTileVisualSprite(fiveTileVisual.TileSprite);
                    break;
                case TileStates.Ten:
                    tile.ChangeTileVisualSprite(tenTileVisual.TileSprite);
                    break;
                case TileStates.Twenty:
                    tile.ChangeTileVisualSprite(twentyTileVisual.TileSprite);
                    break;
                case TileStates.Forty:
                    tile.ChangeTileVisualSprite(fortyTileVisual.TileSprite);
                    break;
                case TileStates.Eighty:
                    tile.ChangeTileVisualSprite(eightyTileVisual.TileSprite);
                    break;
                case TileStates.EightyEighty:
                    tile.ChangeTileVisualSprite(eightyEightyTileVisual.TileSprite);
                    break;
                case TileStates.Explosion:
                    tile.ChangeTileVisualSprite(blankTileVisual.TileSprite);
                    break;
                default:
                    tile.ChangeTileVisualSprite(blankTileVisual.TileSprite);
                    break;
            }
        }
    }
}
