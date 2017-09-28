using Chess.Domain.Interfaces;
using System.Collections.Generic;

namespace Chess.Domain
{
    public class TurnResult
    {
        public bool TurnCompleted { get; set; }
        public IPiece PieceMoved { get; set; }
        public int OldXCoordinate { get; set; }
        public int OldYCoordinate { get; set; }
        public int NewXCoordinate { get; set; }
        public int NewYCoordinate { get; set; }
        public string ReasonForIncompleteTurn { get; set; }
        public List<Flag> Flags { get; set; }

        public TurnResult()
        {
            Flags = new List<Flag>();
        }
    }

    
}
