using Chess.Domain.Interfaces;
using System;

namespace Chess.Domain
{
    public abstract class King : IPiece
    {
        public ChessBoard ChessBoard { get; set; }
        public PieceColor PieceColor { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool IsInCheck { get; set; }
        public bool HasMoved { get; set; }

        public TurnResult Move(int newX, int newY)
        {
            var movementResult = IsLegalMove(newX, newY);

            if (movementResult.WasSuccessful)
            {
                IsInCheck = false;
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

        public abstract void LongCastle();

        public abstract void ShortCastle();

        /*  IsLegalMove() for the king also calls into the ChessBoard to see if
         *  making that move would put it in check.
         */

        public MovementResult IsLegalMove(int newX, int newY)
        {
            if (!ChessBoard.IsLegalBoardPosition(newX, newY))
                return new MovementResult()
                {
                    WasSuccessful = false,
                    ReasonForFailure = "That board position does not exist."
                };

            if ((Math.Abs(newX - XCoordinate) > 1) || (Math.Abs(newY - YCoordinate) > 1))
                return new MovementResult()
                {
                    WasSuccessful = false,
                    ReasonForFailure = "That is not a valid move for this piece."
                };

            if (!ChessBoard.IsPositionSafeForKing(this, newX, newY))
                return new MovementResult()
                {
                    WasSuccessful = false,
                    ReasonForFailure = "A king cannot move into check."
                };

            if (ChessBoard.IsPieceAt(newX, newY))
                if (ChessBoard.PieceAt(newX, newY).PieceColor == PieceColor)
                    return new MovementResult()
                    {
                        WasSuccessful = false,
                        ReasonForFailure = "There is a piece of the same color already there."
                    };

            return new MovementResult()
            {
                WasSuccessful = true,
                ReasonForFailure = ""
            };
        }

        /*  IsInCheckMate() looks to see if the king is in check and can't make
         *  a legal move.
         *  
         *  IsInStaleMate() confirms that the king is not in check, but he cannot move.
         */
        public bool IsInCheckMate()
        {
            if (!IsInCheck)
                return false;
            else if (IsLegalMove(XCoordinate + 1, YCoordinate + 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate + 1, YCoordinate).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate + 1, YCoordinate - 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate, YCoordinate - 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate - 1, YCoordinate - 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate - 1, YCoordinate).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate - 1, YCoordinate + 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate, YCoordinate + 1).WasSuccessful)
                return false;
            else
                return true;
        }

        public bool IsInStaleMate()
        {
            if (IsInCheck)
                return false;
            else if (IsLegalMove(XCoordinate + 1, YCoordinate + 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate + 1, YCoordinate).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate + 1, YCoordinate - 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate, YCoordinate - 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate - 1, YCoordinate - 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate - 1, YCoordinate).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate - 1, YCoordinate + 1).WasSuccessful)
                return false;
            else if (IsLegalMove(XCoordinate, YCoordinate + 1).WasSuccessful)
                return false;
            else
                return true;
        }
    }
}
