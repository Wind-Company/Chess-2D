using Chess.Engine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chess
{
    public class PawnUpgradeItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] FigureType _figureType;
        public FigureType FigureType => _figureType;
        [SerializeField] Image _image;
        public Image Image => _image;

        private System.Action<FigureType> onClick;

        public PawnUpgradeItem Init(Team team, System.Action<FigureType> onClick = null)
        {
            switch (team)
            {
                case Team.Team1:
                    Image.color = ChessResources.SkinPacks[ChessSettings.SkinPack].Team1Color;
                    break;
                case Team.Team2:
                    Image.color = ChessResources.SkinPacks[ChessSettings.SkinPack].Team2Color;
                    break;
            }
            switch (FigureType)
            {
                case FigureType.Pawn:
                    Image.sprite = ChessResources.SkinPacks[ChessSettings.SkinPack].PawnSprite;
                    break;
                case FigureType.Bishop:
                    Image.sprite = ChessResources.SkinPacks[ChessSettings.SkinPack].BishopSprite;
                    break;
                case FigureType.Knight:
                    Image.sprite = ChessResources.SkinPacks[ChessSettings.SkinPack].KnightSprite;
                    break;
                case FigureType.Rook:
                    Image.sprite = ChessResources.SkinPacks[ChessSettings.SkinPack].RookSprite;
                    break;
                case FigureType.Queen:
                    Image.sprite = ChessResources.SkinPacks[ChessSettings.SkinPack].QueenSprite;
                    break;
                case FigureType.King:
                    Image.sprite = ChessResources.SkinPacks[ChessSettings.SkinPack].KingSprite;
                    break;
            }
            Image.rectTransform.localScale = Vector2.one * ChessResources.SkinPacks[ChessSettings.SkinPack].ImageScale;
            this.onClick = onClick;
            return this;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(FigureType);
        }
    }
}