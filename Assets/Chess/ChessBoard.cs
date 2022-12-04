using Chess.Engine;
using Chess.Engine.Figures;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class ChessBoard : MonoBehaviour
    {
        [SerializeField] ChessMatchBehaviour _chessMatchBehaviour;
        public ChessMatchBehaviour ChessMatchBehaviour => _chessMatchBehaviour;

        [SerializeField] RectTransform _marksContent;
        public RectTransform MarksContent => _marksContent;

        [SerializeField] RectTransform _figuresContent;
        public RectTransform FiguresContent => _figuresContent;

        public List<FigureItem> Figures { get; private set; } = new List<FigureItem>();
        public MarkItem CancelMark { get; private set; }
        public List<MarkItem> MoveMarks { get; private set; } = new List<MarkItem>();
        public List<MarkItem> CheckMarks { get; private set; } = new List<MarkItem>();

        public void RenderBoard(IBoard board)
        {
            HideMoveMarks();
            int i = 0;
            foreach(var figure in board)
            {
                if(i < Figures.Count)
                {
                    Figures[i].Init(figure, this);
                }
                else
                {
                    Figures.Add(Instantiate(ChessResources.FigureItem, FiguresContent).Init(figure, this));
                }
                i++;
            }
            for(; i < Figures.Count; i++)
            {
                Figures[i].Hide();
            }
        }

        public void HideMoveMarks()
        {
            if(CancelMark) CancelMark.Hide();
            for (int i = 0; i < MoveMarks.Count; i++)
            {
                MoveMarks[i].Hide();
            }
        }

        public void RenderCancleMark(Position position)
        {
            if (!CancelMark) CancelMark = Instantiate(ChessResources.MarkItem, MarksContent);
            CancelMark.Init(position, ChessResources.SkinPacks[ChessSettings.SkinPack].CancelMarkColor, (p) => HideMoveMarks());
        }

        public void RenderMoveMarks(Position[] positions, Action<Position> onClick = null)
        {
            int i = 0;
            foreach (var position in positions)
            {
                if (i >= MoveMarks.Count)
                {
                    MoveMarks.Add(Instantiate(ChessResources.MarkItem, MarksContent));
                }
                MoveMarks[i++].Init(position, ChessResources.SkinPacks[ChessSettings.SkinPack].MoveMarkColor, onClick);
            }
            for (; i < MoveMarks.Count; i++)
            {
                MoveMarks[i].Hide();
            }
        }
    }
}