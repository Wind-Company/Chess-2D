using UnityEngine;

namespace Chess
{
    [CreateAssetMenu(fileName = "ChessResources", menuName = "Chess/Resources")]
    public class ChessResources : ScriptableObject
    {
        private static ChessResources instance;
        private static ChessResources Instance
        {
            get
            {
                if (!instance) instance = Resources.Load<ChessResources>("ChessResources");
                return instance;
            }
        }

        [SerializeField] FigureItem _figureItem;
        public static FigureItem FigureItem => Instance._figureItem;

        [SerializeField] MarkItem _markItem;
        public static MarkItem MarkItem => Instance._markItem;

        [SerializeField] SkinPack[] _skinPacks;
        public static SkinPack[] SkinPacks => Instance._skinPacks;

        [System.Serializable]
        public class SkinPack
        {
            [SerializeField] string _name;
            public string Name => _name;

            [SerializeField] Color _team1Color;
            public Color Team1Color => _team1Color;
            [SerializeField] Color _team2Color;
            public Color Team2Color => _team2Color;

            [SerializeField] float _imageScale;
            public float ImageScale => _imageScale;

            [SerializeField] Sprite _pawnSprite;
            public Sprite PawnSprite => _pawnSprite;
            [SerializeField] Sprite _bishopSprite;
            public Sprite BishopSprite => _bishopSprite;
            [SerializeField] Sprite _knightSprite;
            public Sprite KnightSprite => _knightSprite;
            [SerializeField] Sprite _rookSprite;
            public Sprite RookSprite => _rookSprite;
            [SerializeField] Sprite _queenSprite;
            public Sprite QueenSprite => _queenSprite;
            [SerializeField] Sprite _kingSprite;
            public Sprite KingSprite => _kingSprite;
            [SerializeField] Sprite _markSprite;
            public Sprite MarkSprite => _markSprite;

            [SerializeField] Color _cancelMarkColor;
            public Color CancelMarkColor => _cancelMarkColor;

            [SerializeField] Color _moveMarkColor;
            public Color MoveMarkColor => _moveMarkColor;
        }
    }
}