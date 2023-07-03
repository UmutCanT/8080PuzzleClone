using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class Tile : MonoBehaviour
    {
        public static event EventHandler<OnTileStateChangeEventArgs> OnTileStateChange;
        public static event EventHandler<OnExplosionStateEventArgs> OnExplosionState;

        public class OnTileStateChangeEventArgs : EventArgs
        {
            public Tile tile;
        }

        public class OnExplosionStateEventArgs : EventArgs
        {
            public Tile tile;
        }

        [SerializeField] private SpriteRenderer tileVisual;
        private BlankTilePosition tilePosition;
        private TileStates tileState;
        private bool searchedOnTurn;

        public TileStates TileState
        {
            get => tileState;
            set
            {
                tileState = value;
                OnTileStateChange?.Invoke(this, new OnTileStateChangeEventArgs { tile = this });
                if (tileState == TileStates.Explosion)
                {
                    OnExplosionState?.Invoke(this, new OnExplosionStateEventArgs { tile = this});
                }
            }
        }

        public BlankTilePosition TilePosition { get => tilePosition; set => tilePosition = value; }
        public bool SearchedOnTurn { get => searchedOnTurn; set => searchedOnTurn = value; }

        private void Awake()
        {
            TileState = TileStates.Blank;
        }

        public void ChangeTileVisualSprite(Sprite sprite)
        {
            tileVisual.sprite = sprite;
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