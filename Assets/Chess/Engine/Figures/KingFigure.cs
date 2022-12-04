using System.Collections.Generic;
using System.Linq;

namespace Chess.Engine.Figures 
{
    public class KingFigure : IFigure
    {
        private Team team;
        public Team Team => team;

        public FigureType Type => FigureType.King;

        private Position position;
        public Position Position 
        { 
            get => position;
            set
            {
                if (((Point)value-position).x > 1)
                {
                    if (board.TryGetFigure(value + new Point(1, 0), out IFigure targetFigure))
                    {
                        targetFigure.Position = position + new Point(1, 0);
                    }
                }
                if (((Point)value - position).x < -1)
                {
                    if (board.TryGetFigure(value - new Point(2, 0), out IFigure targetFigure))
                    {
                        targetFigure.Position = position - new Point(1, 0);
                    }
                }
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
                tempPosition = Position + new Point(1, 1);
                if (board.IsValidPosition(tempPosition) && (board.IsFreePosition(tempPosition) || !Equals(board[tempPosition].Team, Team))) list.AddLast(tempPosition);
                tempPosition = Position + new Point(1, -1);
                if (board.IsValidPosition(tempPosition) && (board.IsFreePosition(tempPosition) || !Equals(board[tempPosition].Team, Team))) list.AddLast(tempPosition);
                tempPosition = Position + new Point(-1, -1);
                if (board.IsValidPosition(tempPosition) && (board.IsFreePosition(tempPosition) || !Equals(board[tempPosition].Team, Team))) list.AddLast(tempPosition);
                tempPosition = Position + new Point(-1, 1);
                if (board.IsValidPosition(tempPosition) && (board.IsFreePosition(tempPosition) || !Equals(board[tempPosition].Team, Team))) list.AddLast(tempPosition);
                tempPosition = Position + new Point(1, 0);
                if (board.IsValidPosition(tempPosition) && (board.IsFreePosition(tempPosition) || !Equals(board[tempPosition].Team, Team))) list.AddLast(tempPosition);
                tempPosition = Position + new Point(-1, 0);
                if (board.IsValidPosition(tempPosition) && (board.IsFreePosition(tempPosition) || !Equals(board[tempPosition].Team, Team))) list.AddLast(tempPosition);
                tempPosition = Position + new Point(0, 1);
                if (board.IsValidPosition(tempPosition) && (board.IsFreePosition(tempPosition) || !Equals(board[tempPosition].Team, Team))) list.AddLast(tempPosition);
                tempPosition = Position + new Point(0, -1);
                if (board.IsValidPosition(tempPosition) && (board.IsFreePosition(tempPosition) || !Equals(board[tempPosition].Team, Team))) list.AddLast(tempPosition);
                return list;
            }
        }
        public Position[] MovePaths
        {
            get
            {
                var paths = NonSafetyMovePaths;
                Position tempPosition;

                if (LastMovedTurn.Equals(-1) && IsKingSafetyWhenMoveTo(Position))
                {
                    tempPosition = Position + new Point(1, 0);
                    if (board.IsValidPosition(tempPosition) && board.IsFreePosition(tempPosition) && IsKingSafetyWhenMoveTo(tempPosition))
                    {
                        tempPosition = Position + new Point(2, 0);
                        if (board.IsValidPosition(tempPosition) && board.IsFreePosition(tempPosition) && IsKingSafetyWhenMoveTo(tempPosition))
                        {
                            tempPosition = Position + new Point(3, 0);
                            if (board.IsValidPosition(tempPosition) && board.TryGetFigure(tempPosition, out IFigure figure))
                            {
                                if (figure.Type.Equals(FigureType.Rook) && figure.LastMovedTurn.Equals(-1))
                                {
                                    paths.AddLast(Position + new Point(2, 0));
                                }
                            }
                        }
                    }
                    tempPosition = Position + new Point(-1, 0);
                    if (board.IsValidPosition(tempPosition) && board.IsFreePosition(tempPosition) && IsKingSafetyWhenMoveTo(tempPosition))
                    {
                        tempPosition = Position + new Point(-2, 0);
                        if (board.IsValidPosition(tempPosition) && board.IsFreePosition(tempPosition) && IsKingSafetyWhenMoveTo(tempPosition))
                        {
                            tempPosition = Position + new Point(-3, 0);
                            if (board.IsValidPosition(tempPosition) && board.IsFreePosition(tempPosition))
                            {
                                tempPosition = Position + new Point(-4, 0);
                                if (board.IsValidPosition(tempPosition) && board.TryGetFigure(tempPosition, out IFigure figure))
                                {
                                    if (figure.Type.Equals(FigureType.Rook) && figure.LastMovedTurn.Equals(-1))
                                    {
                                        paths.AddLast(Position + new Point(-2, 0));
                                    }
                                }
                            }
                        }
                    }
                }
                return paths.Where(path => IsKingSafetyWhenMoveTo(path)).ToArray();
            }
        }

        private bool IsKingSafetyWhenMoveTo(Position position)
        {
            var targetFigure = board[position];
            var lastPosition = this.position;
            if (!Equals(targetFigure, null) && targetFigure.Team.Equals(Team))
            {
                board[this.position] = targetFigure;
            }
            else
            {
                board[this.position] = null;
            }
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
                            board[this.position] = targetFigure;
                            this.position = lastPosition;
                            board[this.position] = this;
                            return false;
                        }
                    }
                }
            }
            board[this.position] = targetFigure;
            this.position = lastPosition;
            board[this.position] = this;
            return true;
        }

        public KingFigure(Position position, Team team, IBoard board)
        {
            this.team = team;
            this.position = position;
            this.board = board;
            lastMovedTurn = -1;
            board[position] = this;
        }
    }
}