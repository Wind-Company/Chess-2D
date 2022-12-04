using System.Collections.Generic;
using System.Linq;

namespace Chess.Engine.Figures 
{
    public class BishopFigure : IFigure
    {
        private Team team;
        public Team Team => team;

        public FigureType Type => FigureType.Bishop;

        private Position position;
        public Position Position 
        { 
            get => position;
            set
            {
                board[position] = null;
                position = value;
                lastMovedTurn = board.Match.Turn;
                board[position] = this;
            }
        }
        private IBoard board;
        private int lastMovedTurn;
        public int LastMovedTurn => lastMovedTurn;

        public LinkedList<Position> NonSafetyMovePaths
        {
            get
            {
                Position tempPosition;
                LinkedList<Position> list = new LinkedList<Position>();
                for(tempPosition = Position + new Point(1, 1); ; tempPosition += new Point(1, 1))
                {
                    if (!board.IsValidPosition(tempPosition)) break;
                    if (!board.IsFreePosition(tempPosition))
                    {
                        if (Equals(board[tempPosition].Team, Team)) break;
                        list.AddLast(tempPosition);
                        break;
                    }
                    list.AddLast(tempPosition);
                }
                for (tempPosition = Position + new Point(1, -1); ; tempPosition += new Point(1, -1))
                {
                    if (!board.IsValidPosition(tempPosition)) break;
                    if (!board.IsFreePosition(tempPosition))
                    {
                        if (Equals(board[tempPosition].Team, Team)) break;
                        list.AddLast(tempPosition);
                        break;
                    }
                    list.AddLast(tempPosition);
                }
                for (tempPosition = Position + new Point(-1, 1); ; tempPosition += new Point(-1, 1))
                {
                    if (!board.IsValidPosition(tempPosition)) break;
                    if (!board.IsFreePosition(tempPosition))
                    {
                        if (Equals(board[tempPosition].Team, Team)) break;
                        list.AddLast(tempPosition);
                        break;
                    }
                    list.AddLast(tempPosition);
                }
                for (tempPosition = Position + new Point(-1, -1); ; tempPosition += new Point(-1, -1))
                {
                    if (!board.IsValidPosition(tempPosition)) break;
                    if (!board.IsFreePosition(tempPosition))
                    {
                        if (Equals(board[tempPosition].Team, Team)) break;
                        list.AddLast(tempPosition);
                        break;
                    }
                    list.AddLast(tempPosition);
                }
                return list;
            }
        }
        
        public Position[] MovePaths => NonSafetyMovePaths.Where(path=>IsKingSafetyWhenMoveTo(path)).ToArray();

        private bool IsKingSafetyWhenMoveTo(Position position)
        {
            var hostileFigure = board[position];
            var lastPosition = this.position;
            board[this.position] = null;
            this.position = position;
            board[this.position] = this;
            foreach (var figure in board)
            {
                if (!Equals(figure.Team, Team))
                {
                    foreach(var path in figure.NonSafetyMovePaths)
                    {
                        if (board.TryGetFigure(path, out IFigure inPathFigure) && Equals(inPathFigure.Team, Team) && Equals(inPathFigure.Type, FigureType.King))
                        {
                            board[this.position] = hostileFigure;
                            this.position = lastPosition;
                            board[this.position] = this;
                            return false;
                        }
                    }
                }
            }
            board[this.position] = hostileFigure;
            this.position = lastPosition;
            board[this.position] = this;
            return true;
        }

        public BishopFigure(Position position, Team team, IBoard board)
        {
            this.team = team;
            this.position = position;
            this.board = board;
            lastMovedTurn = -1;
            board[position] = this;
        }
    }
}