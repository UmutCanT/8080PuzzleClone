using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PuzzleEighty
{
    public class PlayerControls : MonoBehaviour
    {
        public event EventHandler<OnInteractWithBlankTileEventArgs> OnInteractWithBlankTile;

        public class OnInteractWithBlankTileEventArgs : EventArgs
        {
            public Tile tile;
        }

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
                            OnInteractWithBlankTile?.Invoke(this, new OnInteractWithBlankTileEventArgs { tile = tile });
                        }
                    }
                }
            }
        }
    }
}
