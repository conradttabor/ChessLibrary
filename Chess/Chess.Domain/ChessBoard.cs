using Chess.Domain.Interfaces;
using System;
using System.Collections.Generic;

/*  The chessboard was the final say in all movement decisions
 *  because it had the closest relationship to where the pieces 
 *  are located. It was also used to tell the king if a certain position 
 *  would put him in check, and would put enemy kings in
 *  check after a piece had moved. If one teams king was in check
 *  and a piece's move did not eliminate the threat, the move would
 *  be undone and a negative TurnResult would be returned.
 *  
 *  The chessboard mainly deals with piece management, and fields
 *  queries about the state of the board and its pieces.
 */

namespace Chess.Domain
{
    public class ChessBoard
    {
        public static readonly int MaxBoardWidth = 8;
        public static readonly int MaxBoardHeight = 8;
        private IPiece[,] pieces;
        public List<IPiece> CapturedWhitePieces;
        public List<IPiece> CapturedBlackPieces;

        public ChessBoard ()
        {
            pieces = new IPiece[MaxBoardWidth, MaxBoardHeight];
            CapturedWhitePieces = new List<IPiece>();
            CapturedBlackPieces = new List<IPiece>();
        }

        public void Add(IPiece piece, int xCoordinate, int yCoordinate)
        {
            pieces[xCoordinate, yCoordinate] = piece;
            piece.XCoordinate = xCoordinate;
            piece.YCoordinate = yCoordinate;
        }

        public bool IsLegalBoardPosition(int xCoordinate, int yCoordinate)
        {
            if ((xCoordinate <= 7 && xCoordinate >= 0) && (yCoordinate <= 7 && yCoordinate >= 0))
                return true;

            else
                return false;    
        }

        public bool IsPieceAt(int XCoordinate, int YCoordinate, IPiece piece)
        {
            if (pieces[XCoordinate, YCoordinate] == piece)
                return true;
            else
                return false;
        }

        public bool IsPieceAt(int XCoordinate, int YCoordinate)
        {
            if (pieces[XCoordinate, YCoordinate] != null)
                return true;
            else
                return false;
        }

        public IPiece PieceAt(int xCoordinate, int yCoordinate)
        {
            return pieces[xCoordinate, yCoordinate];
        }

        public TurnResult MovePiece(int oldXCoordinate, int oldYCoordinate, int newXCoordinate, int newYCoordinate, IPiece piece)
        {
            IPiece pontentiallyCapturedPiece = IsPieceAt(newXCoordinate, newYCoordinate) ? PieceAt(newXCoordinate, newYCoordinate) : null;

            var turnResult = new TurnResult()
            {
                OldXCoordinate = oldXCoordinate,
                OldYCoordinate = oldYCoordinate,
                NewXCoordinate = newXCoordinate,
                NewYCoordinate = newYCoordinate,
                PieceMoved = piece
            };

            if (IsPieceAt(newXCoordinate, newYCoordinate))
            {
                if (pieces[newXCoordinate, newYCoordinate] is King)
                {
                    turnResult.TurnCompleted = false;
                    turnResult.ReasonForIncompleteTurn =
                        "You cannot capture a king.";
                    return turnResult;
                }
                else
                    CapturePiece(newXCoordinate, newYCoordinate);
            }

            MakeMove(piece, newXCoordinate, newYCoordinate, oldXCoordinate, oldYCoordinate);

            if (!(piece is King))
            {
                var ownKing = GetOwnKing(piece.PieceColor);
                if (!IsPositionSafeForKing(ownKing, ownKing.XCoordinate, ownKing.YCoordinate))
                {
                    turnResult.TurnCompleted = false;
                    turnResult.ReasonForIncompleteTurn =
                        "Your king is still in check.";

                    UndoMove(piece, pontentiallyCapturedPiece, newXCoordinate, newYCoordinate, oldXCoordinate, oldYCoordinate);

                    return turnResult;
                }
            }

            var otherKing = GetOpponentKing(piece.PieceColor);

            if (!IsPositionSafeForKing(otherKing, otherKing.XCoordinate, otherKing.YCoordinate))
                otherKing.IsInCheck = true;
            if (otherKing.IsInCheckMate())
                turnResult.Flags.Add(Flag.CheckMate);
            if (otherKing.IsInStaleMate() && !IsAnyAlliesLeft(otherKing.PieceColor))
                turnResult.Flags.Add(Flag.StaleMate);
            
            ClearEnemyPawnDoubleSteps(piece.PieceColor);
            piece.HasMoved = true;
            
            turnResult.TurnCompleted = true;
            turnResult.ReasonForIncompleteTurn = "";

            return turnResult;
        }

        private void CapturePiece(int newX, int newY)
        {
            var piece = pieces[newX, newY];

            piece.XCoordinate = -1;
            piece.XCoordinate = -1;

            if (piece.PieceColor == PieceColor.White)
                CapturedWhitePieces.Add(piece);
            else if (piece.PieceColor == PieceColor.Black)
                CapturedBlackPieces.Add(piece);

            pieces[newX, newY] = null;
        }

        public bool IsPositionSafeForKing(King king, int newX, int newY)
        {
            pieces[king.XCoordinate, king.YCoordinate] = null; 

            for (int x = 0; x < MaxBoardWidth; x++)
            {
                for (int y = 0; y < MaxBoardHeight; y++)
                {
                    if (IsPieceAt(x, y))
                    {
                        var piece = PieceAt(x, y);
                        if ((piece.PieceColor != king.PieceColor))
                        {
                            if (piece is Pawn)
                            {
                                pieces[king.XCoordinate, king.YCoordinate] = king;
                                if (piece.IsLegalMove(newX, newY).WasSuccessful)
                                    return false;
                            }
                            else
                            {
                                if (piece.IsLegalMove(newX, newY).WasSuccessful)
                                {
                                    pieces[king.XCoordinate, king.YCoordinate] = king;
                                    return false;
                                }
                            }
                            
                        }
                            
                    }
                }
            }
            pieces[king.XCoordinate, king.YCoordinate] = king;
            return true;
        }

        public void EnPassantKill(Pawn pawn)
        {
            var killedPawn = pieces[pawn.XCoordinate, pawn.YCoordinate];

            

            if (killedPawn.PieceColor == PieceColor.White)
                CapturedWhitePieces.Add(killedPawn);
            else if (killedPawn.PieceColor == PieceColor.Black)
                CapturedBlackPieces.Add(killedPawn);

            pieces[pawn.XCoordinate, pawn.YCoordinate] = null;

            killedPawn.XCoordinate = -1;
            killedPawn.YCoordinate = -1;
        }

        private King GetOpponentKing(PieceColor currentColor)
        {
            for (int x = 0; x < MaxBoardWidth; x++)
            {
                for (int y = 0; y < MaxBoardHeight; y++)
                {
                    if (IsPieceAt(x, y))
                    {
                        var piece = PieceAt(x, y);
                        if ((piece.PieceColor != currentColor) && piece is King)
                            return (King)piece;
                    }
                }
            }

            throw new Exception("Oops, there isn't an enemy king on the board!");
        }

        private King GetOwnKing(PieceColor currentColor)
        {
            for (int x = 0; x < MaxBoardWidth; x++)
            {
                for (int y = 0; y < MaxBoardHeight; y++)
                {
                    if (IsPieceAt(x, y))
                    {
                        var piece = PieceAt(x, y);
                        if ((piece.PieceColor == currentColor) && piece is King)
                            return (King)piece;
                    }
                }
            }

            throw new Exception("Oops, there isn't an enemy king on the board!");
        }

        private void ClearEnemyPawnDoubleSteps(PieceColor currentColor)
        {
            for (int x = 0; x < MaxBoardWidth; x++)
            {
                for (int y = 0; y < MaxBoardHeight; y++)
                {
                    if (IsPieceAt(x, y))
                    {
                        var piece = PieceAt(x, y);
                        if ((piece.PieceColor != currentColor) && piece is Pawn)
                        {
                            var pawn = (Pawn)piece;
                            pawn.HasDoubleSteppedLastTurn = false;
                        }
                            
                    }
                }
            }
        }

        private void MakeMove(IPiece piece, int newX, int newY, int oldXCoordinate, int oldYCoordinate)
        {
            pieces[newX, newY] = piece;
            pieces[oldXCoordinate, oldYCoordinate] = null;
            piece.XCoordinate = newX;
            piece.YCoordinate = newY;
        }

        private void UndoMove(IPiece piece, IPiece removedPiece, int newX, int newY, int oldXCoordinate, int oldYCoordinate)
        {
            pieces[newX, newY] = null;
            pieces[oldXCoordinate, oldYCoordinate] = piece;
            piece.XCoordinate = oldXCoordinate;
            piece.YCoordinate = oldYCoordinate;

            if (removedPiece != null)
                pieces[newX, newY] = removedPiece;
            
        }

        private bool IsAnyAlliesLeft(PieceColor color)
        {
            for (int x = 0; x < MaxBoardWidth; x++)
            {
                for (int y = 0; y < MaxBoardHeight; y++)
                {
                    if (IsPieceAt(x, y))
                    {
                        var piece = PieceAt(x, y);
                        if ((piece.PieceColor == color) && !(piece is King))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
