using Chess.Domain.Interfaces;
using System;

namespace Chess.Domain
{
    public class Rook : IPiece
    {
        public ChessBoard ChessBoard { get; set; }
        public PieceColor PieceColor { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool HasMoved { get; set; }

        public Rook(PieceColor color, ChessBoard board)
        {
            PieceColor = color;
            ChessBoard = board;
            HasMoved = false;
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

        public MovementResult IsLegalMove(int newX, int newY)
        {          
            if (!ChessBoard.IsLegalBoardPosition(newX, newY))
                return new MovementResult()
                {
                    WasSuccessful = false,
                    ReasonForFailure = "That board position does not exist."
                };

            var xDirection = newX - XCoordinate;
            var yDirection = newY - YCoordinate;
          
            if (!((xDirection == 0) ^ (yDirection == 0)))
                return new MovementResult()
                {
                    WasSuccessful = false,
                    ReasonForFailure = "That is not a valid move for this piece."
                };

            if (ChessBoard.IsPieceAt(newX, newY))
                if (ChessBoard.PieceAt(newX, newY).PieceColor == PieceColor)
                    return new MovementResult()
                    {
                        WasSuccessful = false,
                        ReasonForFailure = "There is a piece of the same color already there."
                    };

            int xSign = 0, ySign = 0;

            if (xDirection == 0)
                ySign = yDirection / Math.Abs(yDirection);
            else if(yDirection == 0)
                xSign = xDirection / Math.Abs(xDirection);

            int x = XCoordinate + xSign, y = YCoordinate + ySign;

            while ((x != newX) && (y != newY))
            {
                if (ChessBoard.IsPieceAt(x, y))
                    return new MovementResult()
                    {
                        WasSuccessful = false,
                        ReasonForFailure = "There is a piece in the way of that movement."
                    };

                x += xSign;
                y += ySign;
            }

            return new MovementResult()
            {
                WasSuccessful = true,
                ReasonForFailure = ""
            };
        }
    }
}
