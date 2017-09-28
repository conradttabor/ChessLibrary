/*  I decided to give the HasMoved property to every
 *  implementation of piece because it is important to
 *  rooks, pawns, and kings, and when querying a piece, 
 *  it made it easier to ask if the piece had moved instead 
 *  of trying to cast it into a certain class.
 */

namespace Chess.Domain.Interfaces
{
    public interface IPiece
    {
        ChessBoard ChessBoard { get; set; }
        PieceColor PieceColor { get; set; }
        int XCoordinate { get; set; }
        int YCoordinate { get; set; }
        bool HasMoved { get; set; }

        TurnResult Move(int newX, int newY);
        MovementResult IsLegalMove(int newX, int newY);
    }
}
