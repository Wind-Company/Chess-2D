using Chess.Engine;
using UnityEngine;

namespace Chess 
{
    public class PawnUpgradePanel : MonoBehaviour
    {
        [SerializeField] PawnUpgradeItem _bishop;
        public PawnUpgradeItem Bishop => _bishop;

        [SerializeField] PawnUpgradeItem _knight;
        public PawnUpgradeItem Knight => _knight;

        [SerializeField] PawnUpgradeItem _rook;
        public PawnUpgradeItem Rook => _rook;

        [SerializeField] PawnUpgradeItem _queen;
        public PawnUpgradeItem Queen => _queen;

        public void Show(Team team, System.Action<FigureType> onClick = null)
        {
            Bishop.Init(team, onClick);
            Knight.Init(team, onClick);
            Rook.Init(team, onClick);
            Queen.Init(team, onClick);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}