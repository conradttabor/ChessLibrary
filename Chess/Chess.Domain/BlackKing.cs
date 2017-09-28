namespace Chess.Domain
{
    public class BlackKing : King
    {
        public BlackKing (ChessBoard board)
        {
            PieceColor = PieceColor.Black;
            ChessBoard = board;
            IsInCheck = false;
        }

        public override void LongCastle()
        {        
            if (HasMoved || IsInCheck)
                return;

            if (!ChessBoard.IsPieceAt(0, 7))
                return;

            var piece = ChessBoard.PieceAt(0, 7);

            if (piece.HasMoved || piece.PieceColor != PieceColor || !(piece is Rook))
                return;

            if (ChessBoard.IsPieceAt(1, 7) || ChessBoard.IsPieceAt(2, 7) || ChessBoard.IsPieceAt(3, 7))
                return;

            if (!ChessBoard.IsPositionSafeForKing(this, 2, 7) || !ChessBoard.IsPositionSafeForKing(this, 3, 7))
                return;

            ChessBoard.MovePiece(XCoordinate, YCoordinate, 2, 7, this);
            ChessBoard.MovePiece(piece.XCoordinate, piece.YCoordinate, 3, 7, piece);        
        }

        public override void ShortCastle()
        {
            if (HasMoved || IsInCheck)
                return;

            if (!ChessBoard.IsPieceAt(7, 7))
                return;

            var piece = ChessBoard.PieceAt(7, 7);

            if (piece.HasMoved || piece.PieceColor != PieceColor || !(piece is Rook))
                return;

            if (ChessBoard.IsPieceAt(5, 7) || ChessBoard.IsPieceAt(6, 7))
                return;

            if (!ChessBoard.IsPositionSafeForKing(this, 5, 7) || !ChessBoard.IsPositionSafeForKing(this, 6, 7))
                return;

            ChessBoard.MovePiece(XCoordinate, YCoordinate, 6, 7, this);
            ChessBoard.MovePiece(piece.XCoordinate, piece.YCoordinate, 5, 7, piece);
        }
    }
}
