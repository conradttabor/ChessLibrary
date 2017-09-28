using Chess.Domain.Interfaces;
using System;

namespace Chess.Domain
{
    public class Knight : IPiece
    {
        public ChessBoard ChessBoard { get; set; }
        public PieceColor PieceColor { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool HasMoved { get; set; }

        public Knight(PieceColor color, ChessBoard board)
        {
            PieceColor = color;
            ChessBoard = board;
        }

        public bool IsLegalCapture(int newX, int newY)
        {
            throw new NotImplementedException();
        }

        public MovementResult IsLegalMove(int newX, int newY)
        {
            if (!ChessBoard.IsLegalBoardPosition(newX, newY))
                return new MovementResult()
                {
                    WasSuccessful = false,
                    ReasonForFailure = "That board position does not exist."
                };

            if (ChessBoard.IsPieceAt(newX, newY))
                if (ChessBoard.PieceAt(newX, newY).PieceColor == PieceColor)
                    return new MovementResult()
                    {
                        WasSuccessful = false,
                        ReasonForFailure = "There is a piece of the same color already there."
                    };

            if (!((Math.Abs(newX - XCoordinate) == 1 && Math.Abs(newY - YCoordinate) == 2) ^ (Math.Abs(newX - XCoordinate) == 2 && Math.Abs(newY - YCoordinate) == 1)))
                return new MovementResult()
                {
                    WasSuccessful = false,
                    ReasonForFailure = "That is not a valid move for this piece."
                };

            return new MovementResult()
            {
                WasSuccessful = true,
                ReasonForFailure = ""
            };
        }

        public TurnResult Move(int newX, int newY)
        {

            var movementResult = IsLegalMove(newX, newY);

            if (movementResult.WasSuccessful)
            {
                return ChessBoard.MovePiece(XCoordinate, YCoordinate, newX, newY, this);
            }
            else
            {
                return new TurnResult()
                {
                    TurnCompleted = false,
                    OldXCoordinate = XCoordinate,
                    OldYCoordinate = YCoordinate,
                    NewXCoordinate = newX,
                    NewYCoordinate = newY,
                    PieceMoved = this,
                    ReasonForIncompleteTurn = movementResult.ReasonForFailure
                };
            }
        }
    }
}
