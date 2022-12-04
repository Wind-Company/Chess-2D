using Chess.Engine.Figures;
using System.Linq;

namespace Chess.Engine
{
    public class Match : IMatch
    {
        public IBoard Board { get; private set; }
        public Team TeamTurn => (Team)(Turn % 2);
        public int Turn { get; private set; }
        public IFigure FigureToUpgrade { get; private set; }

        public void Run()
        {
            Board = new Board(this);
            Turn = 0;
        }

        public void MakeMove(Position from, Position to)
        {
            if (Equals(FigureToUpgrade, null))
            {
                if (!Board.IsValidPosition(to)) throw new System.Exception($"Position {to} not valid!");
                if (Board.TryGetFigure(from, out IFigure figure))
                {
                    if (Equals(figure.Team, TeamTurn) && figure.MovePaths.Contains(to))
                    {
                        figure.Position = to;
                        if (figure.Type == FigureType.Pawn && (((Point)figure.Position).y.Equals(0) || ((Point)figure.Position).y.Equals(7)))
                        {
                            FigureToUpgrade = figure;
                        }
                        else
                        {
                            Turn++;
                        }
                    }
                }
            }
        }

        public void MakeUpgrade(FigureType type)
        {
            if(!Equals(FigureToUpgrade, null))
            {
                switch (type)
                {
                    case FigureType.Bishop:
                        new BishopFigure(FigureToUpgrade.Position, FigureToUpgrade.Team, Board);
                        break;
                    case FigureType.Knight:
                        new KnightFigure(FigureToUpgrade.Position, FigureToUpgrade.Team, Board);
                        break;
                    case FigureType.Rook:
                        new RookFigure(FigureToUpgrade.Position, FigureToUpgrade.Team, Board);
                        break;
                    case FigureType.Queen:
                        new QueenFigure(FigureToUpgrade.Position, FigureToUpgrade.Team, Board);
                        break;
                    default: return;
                }
                Turn++;
                FigureToUpgrade = null;
            }
        }
    }
}