using Chess.Engine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chess
{
    public class MarkItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;

        [SerializeField] Image _image;
        public Image Image => _image;

        private Position position;
        private System.Action<Position> onClick;

        public MarkItem Init(Position position, Color color, System.Action<Position> onClick = null)
        {
            RectTransform.anchorMin = Vector2.right * ((((Point)position).x * 1f + 0.5f) / 8f) + Vector2.up * ((((Point)position).y * 1f + 0.5f) / 8f);
            RectTransform.anchorMax = RectTransform.anchorMin;
            RectTransform.anchoredPosition = Vector2.zero;
            Image.color = color;
            this.position = position;
            this.onClick = onClick;
            gameObject.SetActive(true);

            return this;
        }

        public MarkItem Hide()
        {
            gameObject.SetActive(false);

            return this;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(position);
        }
    }
}