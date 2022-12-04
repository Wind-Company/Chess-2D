using Chess.Engine.Figures;
using System.Collections.Generic;

namespace Chess.Engine 
{
    public interface IBoard : IEnumerable<IFigure>
    {
        IMatch Match { get; }
        IFigure this[Position position] { get; set; }
        bool IsValidPosition(Position position);
        bool IsFreePosition(Position position);
        bool TryGetFigure(Position position, out IFigure figure);
    }
}