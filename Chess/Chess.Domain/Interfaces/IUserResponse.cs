using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Interfaces
{
    public interface IUserResponse
    {
        void ReturnMoveInformation(TurnResult turnResult, ChessBoard chessBoard);
        void BadPieceSelection(string reasonForBadSelection);
        void InformPlayerOfTheirTurn(PieceColor color);
    }
}
