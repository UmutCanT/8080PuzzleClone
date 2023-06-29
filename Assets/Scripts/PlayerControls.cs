using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PuzzleEighty
{
    public class PlayerControls : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("CanChange 0");

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("CanChange 1");

                    if (hit.collider.CompareTag("Tile"))
                    {
                        Debug.Log("CanChange 2");

                        Tile tile = hit.collider.gameObject.GetComponentInParent<Tile>();

                        if (tile.TileState == TileStates.Blank) 
                        {
                            Debug.Log("CanChange 3");
                        }
                    }
                }
            }
        }
    }
}
