using Chess.Engine;
using Chess.Engine.Figures;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chess
{
    public class FigureItem : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler
    {
        private IFigure figure;
        public IFigure Figure
        {
            get => figure;
            set
            {
                figure = value;

                switch (figure.Team)
                {
                    case Team.Team1:
                        Image.color = ChessResources.SkinPacks[ChessSettings.SkinPack].Team1Color;
                        break;
                    case Team.Team2:
                        Image.color = ChessResources.SkinPacks[ChessSettings.SkinPack].Team2Color;
                        break;
                }

                switch (figure.Type)
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
                Image.rectTransform.sizeDelta = Image.sprite.rect.size;
                Image.rectTransform.localScale = Vector2.one * ChessResources.SkinPacks[ChessSettings.SkinPack].ImageScale;

                RectTransform.anchorMin = Vector2.right * ((((Point)figure.Position).x * 1f + 0.5f) / 8f) + Vector2.up * ((((Point)figure.Position).y * 1f + 0.5f) / 8f);
                RectTransform.anchorMax = RectTransform.anchorMin;
                RectTransform.anchoredPosition = Vector2.zero;
            }
        }

        public ChessBoard Board { get; private set; }

        [SerializeField] RectTransform _rectTransfrom;
        public RectTransform RectTransform => _rectTransfrom;
        [SerializeField] Image _image;
        public Image Image => _image;
        private bool isTouch;
        private bool isHold;

        public FigureItem Init(IFigure figure, ChessBoard board)
        {
            Board = board;
            Figure = figure;
            gameObject.SetActive(true);
            return this;
        }

        public FigureItem Hide()
        {
            gameObject.SetActive(false);
            return this;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isHold)
            {
                if (!Equals(Board.ChessMatchBehaviour.Match.TeamTurn, Figure.Team))
                {
                    Board.HideMoveMarks();
                    return;
                }
                Board.RenderCancleMark(figure.Position);
                Board.RenderMoveMarks(figure.MovePaths, (position) => Board.ChessMatchBehaviour.MakeMove(Figure.Position, position));
            }
            isHold = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!Equals(Board.ChessMatchBehaviour.Match.TeamTurn, Figure.Team)) return;
            isTouch = true;
            StartCoroutine(HoldCoroutine());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isTouch = false;
            if (isHold)
            {
                var currentPosition = RectTransform.position;
                RectTransform.anchorMin = Vector2.zero;
                RectTransform.anchorMax = Vector2.zero;
                RectTransform.position = currentPosition;
                var rect = (RectTransform.parent as RectTransform).rect;
                var x = (int)(RectTransform.anchoredPosition.x / rect.width * 8);
                var y = (int)(RectTransform.anchoredPosition.y / rect.height * 8);
                Position position = new Point(x, y);
                if (position != Position.Missing) Board.ChessMatchBehaviour.MakeMove(Figure.Position, position);
                Board.HideMoveMarks();
            }
        }

        IEnumerator HoldCoroutine()
        {
            for(float i = 0; i < 0.3f; i += Time.deltaTime)
            {
                if (!isTouch) yield break;
                yield return null;
            }
            Board.RenderMoveMarks(figure.MovePaths, (position) => Board.ChessMatchBehaviour.MakeMove(Figure.Position, position));
            isHold = true;
            while (isTouch)
            {
                transform.position = Input.mousePosition;
                yield return null;
            }
        }
    }
}