using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Map map;
        [SerializeField] private TileVisualManager tileVisualManager;
        [SerializeField] private PlayerControls playerControls;

        private Tile nextTile;
        private Tile pointerTile;

        private void Awake()
        {
            playerControls.OnInteractWithBlankTile += PlayerControls_OnInteractWithBlankTile;
            playerControls.OnInteractWithPreviousTile += PlayerControls_OnInteractWithPreviousTile;
            playerControls.OnPlayerTurnEnds += PlayerControls_OnPlayerTurnEnds;
            Tile.OnExplosionState += Tile_OnExplosionState;
            nextTile = map.SpawnNextTile();
            //pointerTile = map.SpawnPointerTile();
            GenerateNextTileState();
        }

        private void Tile_OnExplosionState(object sender, Tile.OnExplosionStateEventArgs e)
        {
            map.TileToSearch(e.tile, out int x, out int y);
        }

        private void PlayerControls_OnPlayerTurnEnds(object sender, PlayerControls.OnPlayerTurnEndsEventArgs e)
        {
            while (e.interactedTiles.Count > 0)
            {
                SearchTile(e.interactedTiles.Pop());
            }          
            GenerateNextTileState();
        }

        private void PlayerControls_OnInteractWithBlankTile(object sender, PlayerControls.OnInteractWithBlankTileEventArgs e)
        {
            if (e.previousTile != null)
            {
                e.previousTile.TileState--;
                e.interactedTile.TileState = e.previousTile.TileState;
            }
            else
            {
                e.interactedTile.TileState = nextTile.TileState;
            }
        }

        private void PlayerControls_OnInteractWithPreviousTile(object sender, PlayerControls.OnInteractWithPreviousTileEventArgs e)
        {
            e.previousTile.TileState = TileStates.Blank;
            e.interactedTile.TileState++;
        }

        private void SearchTile(Tile tile)
        {
            map.TileToSearch(tile, out int x, out int y);
            map.SearchTileNeighbors(x, y);
            map.CheckSameStateStack(out Tile combinedTile);
            map.ClearSearch();
            if (combinedTile != null)
            {
                SearchTile(combinedTile);
            }
        }

        private void Start()
        {
            map.CreateMap();
        }

        private void GenerateNextTileState()
        {
            nextTile.TileState = RandomizeTileState();
        }

        private TileStates RandomizeTileState()
        {
            return (TileStates)Random.Range(1, 6);
        }      
    }
}
