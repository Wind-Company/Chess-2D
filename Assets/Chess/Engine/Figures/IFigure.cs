
using System.Collections.Generic;

namespace Chess.Engine.Figures
{
    public interface IFigure
    {
        public Team Team { get; }
        public FigureType Type { get; }
        public Position Position { get; set; }
        public int LastMovedTurn { get; }
        public Position[] MovePaths { get; }
        public LinkedList<Position> NonSafetyMovePaths { get; }
    }
}