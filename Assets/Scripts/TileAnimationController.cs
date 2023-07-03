using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class TileAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator tileAnimator;

        private void Start()
        {
            //tileAnimator.SetInteger("TileState", 0);
        }
    }
}
