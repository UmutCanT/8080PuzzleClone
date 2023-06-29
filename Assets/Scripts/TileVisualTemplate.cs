using UnityEngine;

namespace PuzzleEighty
{

    [CreateAssetMenu(fileName = "TileVisualTemplate", menuName = "Tiles/New Tile Visual")]
    public class TileVisualTemplate : ScriptableObject
    {
        [SerializeField] private Sprite tileSprite;

        public Sprite TileSprite => tileSprite;
    }
}