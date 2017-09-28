namespace Chess.Domain
{
    public class BlackPawn : Pawn
    {
        public BlackPawn(ChessBoard board)
        {
            PieceColor = PieceColor.Black;
            ChessBoard = board;
            HasMoved = false;
            HasDoubleSteppedLastTurn = false;
        }

        public override MovementResult IsLegalMove(int newX, int newY)
        {
            if (!ChessBoard.IsLegalBoardPosition(newX, newY))
                return new MovementResult()
                {
                    WasSuccessful = false,
                    ReasonForFailure = "That board position does not exist."
                };

            if (ChessBoard.IsPieceAt(newX, newY))
                if (ChessBoard.PieceAt(newX, newY).PieceColor == PieceColor.Black)
                    return new MovementResult()
                    {
                        WasSuccessful = false,
                        ReasonForFailure = "There is a friendly piece at that position."
                    };

            if ((newX == XCoordinate && newY == YCoordinate - 1) && !ChessBoard.IsPieceAt(newX, newY))
            {
                var movementResult =  new MovementResult()
                {
                    WasSuccessful = true,
                    ReasonForFailure = ""
                };

                if (newY == 0)
                    movementResult.Flags.Add(Flag.PawnPromotion);

                return movementResult;
            }
                
            if ((newX == XCoordinate && newY == YCoordinate - 2) 
                && !ChessBoard.IsPieceAt(XCoordinate, YCoordinate - 1) 
                && !ChessBoard.IsPieceAt(XCoordinate, YCoordinate - 2 )
                && !HasMoved)
            {
                HasDoubleSteppedLastTurn = true;
                return new MovementResult()
                {
                    WasSuccessful = true,
                    ReasonForFailure = ""
                };
            }
                
            if ((newX == XCoordinate - 1 || newX == XCoordinate + 1) && newY == YCoordinate - 1)
            {
                if (ChessBoard.IsPieceAt(newX, newY))
                    return new MovementResult()
                    {
                        WasSuccessful = true,
                        ReasonForFailure = ""
                    };
                else if (ChessBoard.IsPieceAt(newX, newY + 1))
                {
                    var piece = ChessBoard.PieceAt(newX, newY + 1);
                    if (piece is WhitePawn)
                    {
                        var pawn = (WhitePawn)piece;
                        if (pawn.HasDoubleSteppedLastTurn)
                        {
                            ChessBoard.EnPassantKill(pawn);
                            return new MovementResult()
                            {
                                WasSuccessful = true,
                                ReasonForFailure = ""
                            };
                        }
                            
                    }
                }
            }

            return new MovementResult()
            {
                WasSuccessful = false,
                ReasonForFailure = "That was not a valid move for this piece."
            };
        }
    }
}
