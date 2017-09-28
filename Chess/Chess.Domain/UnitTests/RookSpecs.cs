using NUnit.Framework;

namespace Chess.Domain.UnitTests
{
    [TestFixture]
    class When_using_a_black_rook_and
    {
        private ChessBoard _chessBoard;
        private King _blackKing;
        private BlackPawn _blackPawn;
        private Rook _blackRook;
        private Bishop _blackBishop;
        private WhiteKing _whiteKing;
        private Bishop _whiteBishop;
        private Queen _whiteQueen;


        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _blackPawn = new BlackPawn(_chessBoard);
            _blackRook = new Rook(PieceColor.Black, _chessBoard);
            _blackBishop = new Bishop(PieceColor.Black, _chessBoard);
            _whiteBishop = new Bishop(PieceColor.White, _chessBoard);
            _whiteQueen = new Queen(PieceColor.White, _chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);

        }

        [Test]
        public void _01_placing_the_black_rook_on_X_equals_3_and_Y_equals_3_should_place_the_black_rook_on_that_place_on_the_board()
        {
            _chessBoard.Add(_blackRook, 3, 3);
            Assert.That(_blackRook.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackRook.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_the_black_rook_on_X_equals_7_and_Y_eqauls_7_and_moving_to_X_equals_7_and_Y_eqauls_4_should_move_the_rook()
        {
            _chessBoard.Add(_blackRook, 7, 7);
            _blackRook.Move(7, 4);
            Assert.That(_blackRook.XCoordinate, Is.EqualTo(7));
            Assert.That(_blackRook.YCoordinate, Is.EqualTo(4));
            Assert.That(_chessBoard.IsPieceAt(7, 4, _blackRook), Is.True);
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_the_black_rook_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_1_and_Y_eqauls_4_should_not_move_the_rook()
        {
            _chessBoard.Add(_blackRook, 2, 3);
            _blackRook.Move(1, 4);
            Assert.That(_blackRook.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackRook.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(1, 4, _blackRook));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_the_black_rook_on_X_equals_4_and_Y_eqauls_7_and_moving_to_X_equals_4_and_Y_eqauls_6_with_a_pawn_in_the_way_should_not_move_the_rook()
        {
            _chessBoard.Add(_blackRook, 4, 7);
            _chessBoard.Add(_blackPawn, 4, 6);
            _blackRook.Move(4, 6);
            Assert.That(_blackRook.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackRook.YCoordinate, Is.EqualTo(7));
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(6));
            Assert.That(!_chessBoard.IsPieceAt(4, 6, _blackRook));
        }

        [Test]
        public void _05_and_capturing_a_white_bishop()
        {
            _chessBoard.Add(_blackRook, 3, 3);
            _chessBoard.Add(_whiteBishop, 7, 3);
            _blackRook.Move(7, 3);
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whiteBishop));
            Assert.That(_chessBoard.IsPieceAt(7, 3, _blackRook));
            Assert.That(_blackRook.XCoordinate, Is.EqualTo(7));
            Assert.That(_blackRook.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _06_and_is_captured_by_a_white_queen()
        {
            _chessBoard.Add(_blackRook, 3, 0);
            _chessBoard.Add(_whiteQueen, 0, 3);
            _whiteQueen.Move(3, 0);
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackRook));
            Assert.That(_chessBoard.IsPieceAt(3, 0, _whiteQueen));
            Assert.That(_whiteQueen.XCoordinate, Is.EqualTo(3));
            Assert.That(_whiteQueen.YCoordinate, Is.EqualTo(0));
        }

        [Test]
        public void _07_and_puts_the_white_king_in_check()
        {
            Assert.That(!_whiteKing.IsInCheck);
            _chessBoard.Add(_blackRook, 0, 7);
            _blackRook.Move(0, 0);
            Assert.That(_whiteKing.IsInCheck);
        }
    }

    [TestFixture]
    class When_using_a_white_rook_and
    {
        private ChessBoard _chessBoard;
        private King _blackKing;
        private WhitePawn _whitePawn;
        private Rook _whiteRook;
        private Bishop _whiteBishop;
        private WhiteKing _whiteKing;
        private Bishop _blackBishop;
        private Queen _blackQueen;


        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _whitePawn = new WhitePawn(_chessBoard);
            _whiteRook = new Rook(PieceColor.White, _chessBoard);
            _blackBishop = new Bishop(PieceColor.Black, _chessBoard);
            _whiteBishop = new Bishop(PieceColor.White, _chessBoard);
            _blackQueen = new Queen(PieceColor.Black, _chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);

        }

        [Test]
        public void _01_placing_the_black_rook_on_X_equals_3_and_Y_equals_3_should_place_the_black_rook_on_that_place_on_the_board()
        {
            _chessBoard.Add(_whiteRook, 3, 3);
            Assert.That(_whiteRook.XCoordinate, Is.EqualTo(3));
            Assert.That(_whiteRook.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_the_black_rook_on_X_equals_7_and_Y_eqauls_7_and_moving_to_X_equals_7_and_Y_eqauls_4_should_move_the_rook()
        {
            _chessBoard.Add(_whiteRook, 7, 7);
            _whiteRook.Move(7, 4);
            Assert.That(_whiteRook.XCoordinate, Is.EqualTo(7));
            Assert.That(_whiteRook.YCoordinate, Is.EqualTo(4));
            Assert.That(_chessBoard.IsPieceAt(7, 4, _whiteRook), Is.True);
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_the_black_rook_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_1_and_Y_eqauls_4_should_not_move_the_rook()
        {
            _chessBoard.Add(_whiteRook, 2, 3);
            _whiteRook.Move(1, 4);
            Assert.That(_whiteRook.XCoordinate, Is.EqualTo(2));
            Assert.That(_whiteRook.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(1, 4, _whiteRook));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_the_black_rook_on_X_equals_4_and_Y_eqauls_7_and_moving_to_X_equals_4_and_Y_eqauls_6_with_a_pawn_in_the_way_should_not_move_the_rook()
        {
            _chessBoard.Add(_whiteRook, 4, 7);
            _chessBoard.Add(_whitePawn, 4, 6);
            _whiteRook.Move(4, 6);
            Assert.That(_whiteRook.XCoordinate, Is.EqualTo(4));
            Assert.That(_whiteRook.YCoordinate, Is.EqualTo(7));
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(4));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(6));
            Assert.That(!_chessBoard.IsPieceAt(4, 6, _whiteRook));
        }

        [Test]
        public void _05_and_capturing_a_black_bishop()
        {
            _chessBoard.Add(_whiteRook, 3, 3);
            _chessBoard.Add(_blackBishop, 7, 3);
            _whiteRook.Move(7, 3);
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackBishop));
            Assert.That(_chessBoard.IsPieceAt(7, 3, _whiteRook));
            Assert.That(_whiteRook.XCoordinate, Is.EqualTo(7));
            Assert.That(_whiteRook.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _06_and_is_captured_by_a_black_queen()
        {
            _chessBoard.Add(_whiteRook, 3, 0);
            _chessBoard.Add(_blackQueen, 0, 3);
            _blackQueen.Move(3, 0);
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whiteRook));
            Assert.That(_chessBoard.IsPieceAt(3, 0, _blackQueen));
            Assert.That(_blackQueen.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackQueen.YCoordinate, Is.EqualTo(0));
        }

        [Test]
        public void _07_and_puts_the_black_king_in_check()
        {
            Assert.That(!_blackKing.IsInCheck);
            _chessBoard.Add(_whiteRook, 0, 0);
            _whiteRook.Move(0, 7);
            Assert.That(_blackKing.IsInCheck);
        }
    }
}
