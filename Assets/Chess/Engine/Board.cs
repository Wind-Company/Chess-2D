using Chess.Engine.Figures;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Chess.Engine 
{
    public class Board : IBoard
    {
        public IMatch Match { get; private set; }
        private const int SIZE = 8;
        private IFigure[] board;
        public IFigure this[Position position] 
        {
            get => board[(int)position];
            set
            {
                board[(int)position] = value;
            }
        }
    
        public Board(IMatch match)
        {
            Match = match;
            board = new IFigure[SIZE * SIZE];
            new RookFigure(Position.A1, Team.Team1, this);
            new KnightFigure(Position.B1, Team.Team1, this);
            new BishopFigure(Position.C1, Team.Team1, this);
            new QueenFigure(Position.D1, Team.Team1, this);
            new KingFigure(Position.E1, Team.Team1, this);
            new BishopFigure(Position.F1, Team.Team1, this);
            new KnightFigure(Position.G1, Team.Team1, this);
            new RookFigure(Position.H1, Team.Team1, this);
            new PawnFigure(Position.A2, Team.Team1, this);
            new PawnFigure(Position.B2, Team.Team1, this);
            new PawnFigure(Position.C2, Team.Team1, this);
            new PawnFigure(Position.D2, Team.Team1, this);
            new PawnFigure(Position.E2, Team.Team1, this);
            new PawnFigure(Position.F2, Team.Team1, this);
            new PawnFigure(Position.G2, Team.Team1, this);
            new PawnFigure(Position.H2, Team.Team1, this);

            new RookFigure(Position.A8, Team.Team2, this);
            new KnightFigure(Position.B8, Team.Team2, this);
            new BishopFigure(Position.C8, Team.Team2, this);
            new QueenFigure(Position.D8, Team.Team2, this);
            new KingFigure(Position.E8, Team.Team2, this);
            new BishopFigure(Position.F8, Team.Team2, this);
            new KnightFigure(Position.G8, Team.Team2, this);
            new RookFigure(Position.H8, Team.Team2, this);
            new PawnFigure(Position.A7, Team.Team2, this);
            new PawnFigure(Position.B7, Team.Team2, this);
            new PawnFigure(Position.C7, Team.Team2, this);
            new PawnFigure(Position.D7, Team.Team2, this);
            new PawnFigure(Position.E7, Team.Team2, this);
            new PawnFigure(Position.F7, Team.Team2, this);
            new PawnFigure(Position.G7, Team.Team2, this);
            new PawnFigure(Position.H7, Team.Team2, this);
        }

        public bool IsValidPosition(Position position)
        {
            return (int)position >= 0 && (int)position < SIZE * SIZE;
        }

        public bool IsFreePosition(Position position)
        {
            if (!IsValidPosition(position)) throw new Exception($"Board Position {position} is not valid!");
            return Equals(board[(int)position], null);
        }

        public bool TryGetFigure(Position position, out IFigure figure)
        {
            figure = null;
            if (IsFreePosition(position)) return false;
            figure = board[(int)position];
            return true;
        }

        public IEnumerator<IFigure> GetEnumerator()
        {
            for(int i = 0; i < SIZE * SIZE; i++)
            {
                if (TryGetFigure((Position)i, out IFigure figure))
                {
                    yield return figure;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}