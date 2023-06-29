using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Map map;
        [SerializeField] private TileVisualManager tileVisualManager;
        private Tile nextTile;

        private void Awake()
        {
            nextTile = map.SpawnNextTile();
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
            return (TileStates)Random.Range(0, 6);
        }
    }
}
