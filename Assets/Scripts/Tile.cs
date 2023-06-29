using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer tileVisual;
        private TileStates tileState;

        private void Awake()
        {
            tileState = (TileStates)Random.Range(0, 6);
        }

        public void ChangeTileVisualSprite(Sprite sprite)
        {
            tileVisual.sprite = sprite;
        }

        public TileStates TileState { get => tileState; set => tileState = value; }
    }

    public enum TileStates
    {
        Blank,
        Five,
        Ten,
        Twenty,
        Forty,
        Eighty,
        EightyEighty,
        Explosion
    }
}