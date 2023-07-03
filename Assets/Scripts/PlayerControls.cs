using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace PuzzleEighty
{
    public class PlayerControls : MonoBehaviour
    {
        public event EventHandler<OnInteractWithBlankTileEventArgs> OnInteractWithBlankTile;
        public event EventHandler<OnInteractWithPreviousTileEventArgs> OnInteractWithPreviousTile;
        public event EventHandler<OnPlayerTurnEndsEventArgs> OnPlayerTurnEnds;

        public class OnInteractWithBlankTileEventArgs : EventArgs
        {
            public Tile interactedTile;
            public Tile previousTile;
        }

        public class OnInteractWithPreviousTileEventArgs : EventArgs
        {
            public Tile interactedTile;
            public Tile previousTile;
        }

        public class OnPlayerTurnEndsEventArgs : EventArgs
        {
            public Stack<Tile> interactedTiles;
        }

        private Tile currentTile;
        private Stack<Tile> interactedTiles = new Stack<Tile>();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Tile"))
                    {
                        Tile tile = hit.collider.gameObject.GetComponentInParent<Tile>();

                        if (tile.TileState == TileStates.Blank) 
                        {
                            currentTile = tile;                           
                            OnInteractWithBlankTile?.Invoke(this, new OnInteractWithBlankTileEventArgs { interactedTile = tile, previousTile = null });
                            interactedTiles.Push(tile);
                        }
                    }
                }
            }
            else if (Input.GetMouseButton(0) && currentTile != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Tile"))
                    {
                        Tile tile = hit.collider.gameObject.GetComponentInParent<Tile>();
                        if (tile != currentTile)
                        {
                            Debug.Log("new tile");
                            if (tile.TileState == TileStates.Blank && currentTile.TileState > TileStates.Five)
                            {
                                Debug.Log("new blank tile");
                                OnInteractWithBlankTile?.Invoke(this, new OnInteractWithBlankTileEventArgs { interactedTile = tile, previousTile = currentTile });
                                currentTile = tile;
                                interactedTiles.Push(tile);
                            }else if(interactedTiles.Contains(tile))
                            {
                                Debug.Log("new previous tile");
                                OnInteractWithPreviousTile?.Invoke(this, new OnInteractWithPreviousTileEventArgs { interactedTile = tile, previousTile = currentTile });
                                currentTile = tile;
                                interactedTiles.Pop();
                            }                                                               
                        }                       
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnPlayerTurnEnds?.Invoke(this, new OnPlayerTurnEndsEventArgs { interactedTiles = interactedTiles });
                interactedTiles.Clear();
                currentTile = null;
            }
        }
    }
}
