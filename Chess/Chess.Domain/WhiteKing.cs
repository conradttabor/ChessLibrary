namespace Chess.Domain
{
    public class WhiteKing : King
    {
        public WhiteKing(ChessBoard board)
        {
            PieceColor = PieceColor.White;
            ChessBoard = board;
        }

        public override void LongCastle()
        {
            if (HasMoved || IsInCheck)
                return;

            if (!ChessBoard.IsPieceAt(0, 0))
                return;

            var piece = ChessBoard.PieceAt(0, 0);

            if (piece.HasMoved || piece.PieceColor != PieceColor || !(piece is Rook))
                return;

            if (ChessBoard.IsPieceAt(1, 0) || ChessBoard.IsPieceAt(2, 0) || ChessBoard.IsPieceAt(3, 0))
                return;

            if (!ChessBoard.IsPositionSafeForKing(this, 2, 0) || !ChessBoard.IsPositionSafeForKing(this, 3, 0))
                return;

            ChessBoard.MovePiece(XCoordinate, YCoordinate, 2, 0, this);
            ChessBoard.MovePiece(piece.XCoordinate, piece.YCoordinate, 3, 0, piece);
        }

        public override void ShortCastle()
        {
            if (HasMoved || IsInCheck)
                return;

            if (!ChessBoard.IsPieceAt(7, 0))
                return;

            var piece = ChessBoard.PieceAt(7, 0);

            if (piece.HasMoved || piece.PieceColor != PieceColor || !(piece is Rook))
                return;

            if (ChessBoard.IsPieceAt(5, 0) || ChessBoard.IsPieceAt(6, 0))
                return;

            if (!ChessBoard.IsPositionSafeForKing(this, 5, 0) || !ChessBoard.IsPositionSafeForKing(this, 6, 0))
                return;

            ChessBoard.MovePiece(XCoordinate, YCoordinate, 6, 0, this);
            ChessBoard.MovePiece(piece.XCoordinate, piece.YCoordinate, 5, 0, piece);
        }
    }
}
