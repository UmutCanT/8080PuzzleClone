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

        private void Awake()
        {
            playerControls.OnInteractWithBlankTile += PlayerControls_OnInteractWithBlankTile;
            nextTile = map.SpawnNextTile();
            GenerateNextTileState();
        }

        private void PlayerControls_OnInteractWithBlankTile(object sender, PlayerControls.OnInteractWithBlankTileEventArgs e)
        {
            e.tile.TileState = nextTile.TileState;
            map.SearchNeighbors(e.tile.TilePosition.XPosition, e.tile.TilePosition.YPosition);
            GenerateNextTileState();
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
