using Chess.Domain.Interfaces;
using System;

namespace Chess.Domain
{
    public class Bishop : IPiece
    {
        public ChessBoard ChessBoard { get; set; }
        public PieceColor PieceColor { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool HasMoved { get; set; }

        public Bishop(PieceColor color, ChessBoard board)
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

            if (ChessBoard.IsPieceAt(newX, newY))
            {
                if (ChessBoard.PieceAt(newX, newY).PieceColor == PieceColor)
                    return new MovementResult()
                    {
                        WasSuccessful = false,
                        ReasonForFailure = "There is a piece of the same color already there."
                    };

            }
                
            var xDirection = newX - XCoordinate;
            var yDirection = newY - YCoordinate;

            if (Math.Abs(xDirection) != Math.Abs(yDirection))
                return new MovementResult()
                {
                    WasSuccessful = false,
                    ReasonForFailure = "That is not a valid move for this piece."
                };
          
            var xSign = xDirection / Math.Abs(xDirection);
            var ySign = yDirection / Math.Abs(yDirection);

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
