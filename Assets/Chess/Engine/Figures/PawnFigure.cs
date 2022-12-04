using System.Collections.Generic;
using System.Linq;

namespace Chess.Engine.Figures 
{
    public class PawnFigure : IFigure
    {
        private Team team;
        public Team Team => team;

        public FigureType Type => FigureType.Pawn;

        private Position position;
        public Position Position 
        { 
            get => position;
            set
            {
                if (board.IsFreePosition(value))
                {
                    var dirX = ((Point)value - position).x;
                    if (!dirX.Equals(0) && !board.IsFreePosition(position + new Point(dirX, 0)))
                    {
                        board[this.position + new Point(dirX, 0)] = null;
                    }
                }
                board[position] = null;
                doubleMoved = System.Math.Abs(((Point)position - value).y) > 1;
                position = value;
                lastMovedTurn = board.Match.Turn;
                board[position] = this;
            }
        }
        private IBoard board;
        private int lastMovedTurn;
        public int LastMovedTurn => lastMovedTurn;
        public bool doubleMoved = false;

        public LinkedList<Position> NonSafetyMovePaths
        {
            get
            {
                IFigure figure;
                Position tempPosition;
                LinkedList<Position> list = new LinkedList<Position>();
                int dirY = Equals(Team, Team.Team1) ? 1 : -1;
                tempPosition = Position + new Point(0, dirY);
                if (board.IsValidPosition(tempPosition) && board.IsFreePosition(tempPosition))
                {
                    list.AddLast(tempPosition);
                    tempPosition = position + new Point(0, dirY * 2);
                    if (LastMovedTurn.Equals(-1) && board.IsValidPosition(tempPosition) && board.IsFreePosition(tempPosition))
                    {
                        list.AddLast(tempPosition);
                    }
                }
                tempPosition = Position + new Point(1, dirY);
                if (board.IsValidPosition(tempPosition))
                {
                    if (board.TryGetFigure(tempPosition, out figure))
                    {
                        if (!Equals(figure.Team, Team)) list.AddLast(tempPosition);
                    }
                    else
                    {
                        tempPosition = Position + new Point(1, 0);
                        if (board.TryGetFigure(tempPosition, out figure))
                        {
                            var pawn = figure as PawnFigure;
                            if(!Equals(pawn, null) && pawn.doubleMoved && pawn.LastMovedTurn.Equals(board.Match.Turn - 1))
                                list.AddLast(Position + new Point(1, dirY));
                        }
                    }
                }
                tempPosition = Position + new Point(-1, dirY);
                if (board.IsValidPosition(tempPosition))
                {
                    if (board.TryGetFigure(tempPosition, out figure))
                    {
                        if (!Equals(figure.Team, Team)) list.AddLast(tempPosition);
                    }
                    else
                    {
                        tempPosition = Position + new Point(-1, 0);
                        if (board.TryGetFigure(tempPosition, out figure))
                        {
                            var pawn = figure as PawnFigure;
                            if (!Equals(pawn, null) && pawn.doubleMoved && pawn.LastMovedTurn.Equals(board.Match.Turn - 1))
                                list.AddLast(Position + new Point(-1, dirY));
                        }
                    }
                }

                return list;
            }
        }
        public Position[] MovePaths => NonSafetyMovePaths.Where(path => IsKingSafetyWhenMoveTo(path)).ToArray();

        private bool IsKingSafetyWhenMoveTo(Position position)
        {
            var hostileFigure = board[position];
            if(Equals(hostileFigure, null))
            {
                var dirX = ((Point)position - this.position).x;
                if (!dirX.Equals(0) && board.TryGetFigure(this.position + new Point(dirX, 0), out hostileFigure))
                {
                    board[this.position + new Point(dirX, 0)] = null;
                }
            }
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
            board[this.position] = null;
            this.position = lastPosition;
            board[this.position] = this;
            if (!Equals(hostileFigure, null))
            {
                board[hostileFigure.Position] = hostileFigure;
            }
            return true;
        }

        public PawnFigure(Position position, Team team, IBoard board)
        {
            this.team = team;
            this.position = position;
            this.board = board;
            lastMovedTurn = -1;
            doubleMoved = false;
            board[position] = this;
        }
    }
}