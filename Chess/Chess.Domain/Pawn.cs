using Chess.Domain.Interfaces;

/*  Pawns had to be put into abstract classes because their
 *  movements were very different based on piece color. Black
 *  pawns had to move down the board, and whites up the board.
 *  Pawns were also a challenge because unlike other pieces, 
 *  their attacks come from diagonal, and they cannot move forward 
 *  no matter what the piece in front of them is.
 */

namespace Chess.Domain
{
    public abstract class Pawn : IPiece
    {
        public ChessBoard ChessBoard { get; set; }
        public PieceColor PieceColor { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public bool HasMoved { get; set; }
        public bool HasDoubleSteppedLastTurn { get; set; }

        public TurnResult Move(int newX, int newY)
        {

            var movementResult = IsLegalMove(newX, newY);

            if (movementResult.WasSuccessful)
            {
                if (movementResult.Flags.Contains(Flag.PawnPromotion))
                {
                    var queen = new Queen(PieceColor, ChessBoard)
                    {
                        HasMoved = true,
                        XCoordinate = XCoordinate,
                        YCoordinate = YCoordinate
                    };
                    return ChessBoard.MovePiece(XCoordinate, YCoordinate, newX, newY, queen);
                    
                }
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

        public abstract MovementResult IsLegalMove(int newX, int newY);

    }
}
