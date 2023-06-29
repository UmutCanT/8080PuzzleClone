using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleEighty
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Map map;
        [SerializeField] private TileVisualManager tileVisualManager;

        private void Awake()
        {
            
        }

        private void Start()
        {
            map.CreateMap();
        }
    }
}
