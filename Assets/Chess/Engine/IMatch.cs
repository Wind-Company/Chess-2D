
using Chess.Engine.Figures;

namespace Chess.Engine 
{
    public interface IMatch
    {
        Board Board { get; }
        Team TeamTurn { get; }
        int Turn { get; }
        IFigure? FigureToUpgrade { get; }

        void Run();
        void MakeMove(Position from, Position to);
    }
}