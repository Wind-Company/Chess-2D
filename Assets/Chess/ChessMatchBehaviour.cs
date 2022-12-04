using Chess.Engine;
using UnityEngine;

namespace Chess
{
    public class ChessMatchBehaviour : MonoBehaviour
    {
        [SerializeField] ChessBoard _chessBoard;
        public ChessBoard ChessBoard => _chessBoard;

        [SerializeField] PawnUpgradePanel _pawnUpgradePanel;
        public PawnUpgradePanel PawnUpgradePanel => _pawnUpgradePanel;

        public Match Match { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            Match = new Match();
            Match.Run();
            ChessBoard.RenderBoard(Match.Board);
        }

        public void MakeMove(Position from, Position to)
        {
            Match.MakeMove(from, to);
            if(!Equals(Match.FigureToUpgrade, null))
            {
                PawnUpgradePanel.Show(Match.TeamTurn, MakeUpgrade);
            }
            else
            {
                PawnUpgradePanel.Hide();
            }
            ChessBoard.RenderBoard(Match.Board);
        }

        public void MakeUpgrade(FigureType figureType)
        {
            Match.MakeUpgrade(figureType);
            if (!Equals(Match.FigureToUpgrade, null))
            {
                PawnUpgradePanel.Show(Match.TeamTurn, MakeUpgrade);
            }
            else
            {
                PawnUpgradePanel.Hide();
            }
            ChessBoard.RenderBoard(Match.Board);
        }
    }
}