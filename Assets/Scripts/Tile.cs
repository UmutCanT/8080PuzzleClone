using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PuzzleEighty
{
    public class Tile : MonoBehaviour
    {
        public static event EventHandler<OnTileStateChangeEventArgs> OnTileStateChange;

        public class OnTileStateChangeEventArgs : EventArgs
        {
            public Tile tile;
        } 

        [SerializeField] private SpriteRenderer tileVisual;
        private TileStates tileState;

        private void Awake()
        {
            TileState = (TileStates)Random.Range(0, 6);
        }

        public void ChangeTileVisualSprite(Sprite sprite)
        {
            tileVisual.sprite = sprite;
        }

        public TileStates TileState { get => tileState; 
            set {
                tileState = value;
                OnTileStateChange?.Invoke(this, new OnTileStateChangeEventArgs { tile = this});
            }
        
        }
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