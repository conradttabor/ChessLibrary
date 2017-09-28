using Chess.Domain.Interfaces;
using System;

    /*  The queen is a combination of the rook and the bishop.
     *  Instead of lumping all of the logic to check if the requested
     *  move is legal, I separated the horizontal and vertical checks 
     *  into their own private methods.
    */

namespace Chess.Domain
{
    public class Queen : IPiece
    {
        public ChessBoard ChessBoard { get; set; }
        public PieceColor PieceColor { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool HasMoved { get; set; }

        public Queen(PieceColor color, ChessBoard board)
        {
            PieceColor = color;
            ChessBoard = board;
        }

        /*  If it is an availible move to the piece, it continues on to
         *  the ChessBoard where several more checks will be made that 
         *  the move is legitimate before finalizing it.
         *  
         *  If the move is not available, the piece returns a turn result 
         *  with a negative response and a reason that the turn was not successful.
         */

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

        /*  Every piece, for the most part, goes through a similar set of
         *  steps to determine if it can make a particular move.
         *  
         *  1. Is the position even a space on the board?
         *  2. Is there a friendly piece there?
         *  3. Does it follow the movement rules of the piece?
         *  
         *  If any of those conditions are unfavorable, an unsuccessful
         *  movement result is sent back to the caller with a reason on
         *  why that move could not be made.
         */

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

            if (!(IsLegalDiagonal(newX, newY) ^ IsLegalPerpendicular(newX, newY)))
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

        private bool IsLegalDiagonal(int newX, int newY)
        {
            var xDirection = newX - XCoordinate;
            var yDirection = newY - YCoordinate;

            if (Math.Abs(xDirection) != Math.Abs(yDirection))
                return false;
           
            var xSign = xDirection / Math.Abs(xDirection);
            var ySign = yDirection / Math.Abs(yDirection);

            int x = XCoordinate + xSign, y = YCoordinate + ySign;

            while ((x != newX) && (y != newY))
            {
                if (ChessBoard.IsPieceAt(x, y))
                    return false;

                x += xSign;
                y += ySign;
            }

            return true;
        }

        private bool IsLegalPerpendicular(int newX, int newY)
        {
            var xDirection = newX - XCoordinate;
            var yDirection = newY - YCoordinate;

            if (!((xDirection == 0) ^ (yDirection == 0)))
                return false;

            int xSign = 0, ySign = 0;

            if (xDirection == 0)
                ySign = yDirection / Math.Abs(yDirection);
            else if (yDirection == 0)
                xSign = xDirection / Math.Abs(xDirection);

            int x = XCoordinate + xSign, y = YCoordinate + ySign;

            while ((x != newX) || (y != newY))
            {
                if (ChessBoard.IsPieceAt(x, y))
                    return false;

                x += xSign;
                y += ySign;
            }

            return true;
        }
    }
}
