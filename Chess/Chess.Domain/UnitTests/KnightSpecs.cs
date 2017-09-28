using NUnit.Framework;

namespace Chess.Domain.UnitTests
{
    [TestFixture]
    class When_using_a_black_knight_and
    {
        private ChessBoard _chessBoard;
        private King _blackKing;
        private BlackPawn _blackPawn;
        private Rook _blackRook;
        private Bishop _blackBishop;
        private Knight _blackKnight;
        private WhiteKing _whiteKing;
        private Rook _whiteRook;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _blackPawn = new BlackPawn(_chessBoard);
            _blackRook = new Rook(PieceColor.Black, _chessBoard);
            _blackBishop = new Bishop(PieceColor.Black, _chessBoard);
            _blackKnight = new Knight(PieceColor.Black, _chessBoard);
            _whiteRook = new Rook(PieceColor.White, _chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);

        }

        [Test]
        public void _01_placing_it_on_X_equals_4_and_Y_equals_5_should_place_it_on_that_place_on_the_board()
        {
            _chessBoard.Add(_blackKnight, 4, 5);
            Assert.That(_blackKnight.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackKnight.YCoordinate, Is.EqualTo(5));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_it_on_X_equals_4_and_Y_eqauls_5_and_moving_to_X_equals_6_and_Y_eqauls_4_should_move_it()
        {
            _chessBoard.Add(_blackKnight, 4, 5);
            _blackKnight.Move(6, 4);
            Assert.That(_blackKnight.XCoordinate, Is.EqualTo(6));
            Assert.That(_blackKnight.YCoordinate, Is.EqualTo(4));
            Assert.That(_chessBoard.IsPieceAt(6, 4, _blackKnight));
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_it_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_1_and_Y_eqauls_4_should_not_move_it()
        {
            _chessBoard.Add(_blackKnight, 2, 3);
            _blackKnight.Move(1, 4);
            Assert.That(_blackKnight.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackKnight.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(1, 4, _blackKnight));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_it_on_X_equals_1_and_Y_eqauls_0_and_moving_to_X_equals_2_and_Y_eqauls_2_with_a_black_pawn_in_the_way_should_not_move_it()
        {
            _chessBoard.Add(_blackKnight, 1, 0);
            _chessBoard.Add(_blackPawn, 2, 2);
            _blackKnight.Move(2, 2);
            Assert.That(_blackKnight.XCoordinate, Is.EqualTo(1));
            Assert.That(_blackKnight.YCoordinate, Is.EqualTo(0));
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(2));
            Assert.That(!_chessBoard.IsPieceAt(2, 2, _blackKnight));
        }

        [Test]
        public void _05_and_capturing_a_white_rook()
        {
            _chessBoard.Add(_blackKnight, 3, 3);
            _chessBoard.Add(_whiteRook, 4, 5);
            _blackKnight.Move(4, 5);
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whiteRook));
            Assert.That(_chessBoard.IsPieceAt(4, 5, _blackKnight));
            Assert.That(_blackKnight.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackKnight.YCoordinate, Is.EqualTo(5));
        }

        [Test]
        public void _06_and_is_captured_by_a_white_rook()
        {
            _chessBoard.Add(_blackKnight, 3, 7);
            _chessBoard.Add(_whiteRook, 3, 4);
            _whiteRook.Move(3, 7);
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackKnight));
            Assert.That(_chessBoard.IsPieceAt(3, 7, _whiteRook));
            Assert.That(_whiteRook.XCoordinate, Is.EqualTo(3));
            Assert.That(_whiteRook.YCoordinate, Is.EqualTo(7));
        }

        [Test]
        public void _07_and_puts_the_white_king_in_check()
        {
            Assert.That(!_whiteKing.IsInCheck);
            _chessBoard.Add(_blackKnight, 1, 3);
            _blackKnight.Move(3, 2);
            Assert.That(_whiteKing.IsInCheck);
        }

    }

    [TestFixture]
    class When_using_a_white_knight_and
    {
        private ChessBoard _chessBoard;
        private King _blackKing;
        private WhitePawn _whitePawn;
        private Rook _whiteRook;
        private Bishop _whiteBishop;
        private Knight _whiteKnight;
        private WhiteKing _whiteKing;
        private Rook _blackRook;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _whitePawn = new WhitePawn(_chessBoard);
            _blackRook = new Rook(PieceColor.Black, _chessBoard);
            _whiteBishop = new Bishop(PieceColor.White, _chessBoard);
            _whiteKnight = new Knight(PieceColor.White, _chessBoard);
            _whiteRook = new Rook(PieceColor.White, _chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);

        }

        [Test]
        public void _01_placing_it_on_X_equals_4_and_Y_equals_5_should_place_it_on_that_place_on_the_board()
        {
            _chessBoard.Add(_whiteKnight, 4, 5);
            Assert.That(_whiteKnight.XCoordinate, Is.EqualTo(4));
            Assert.That(_whiteKnight.YCoordinate, Is.EqualTo(5));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_it_on_X_equals_4_and_Y_eqauls_5_and_moving_to_X_equals_6_and_Y_eqauls_4_should_move_it()
        {
            _chessBoard.Add(_whiteKnight, 4, 5);
            _whiteKnight.Move(6, 4);
            Assert.That(_whiteKnight.XCoordinate, Is.EqualTo(6));
            Assert.That(_whiteKnight.YCoordinate, Is.EqualTo(4));
            Assert.That(_chessBoard.IsPieceAt(6, 4, _whiteKnight));
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_it_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_1_and_Y_eqauls_4_should_not_move_it()
        {
            _chessBoard.Add(_whiteKnight, 2, 3);
            _whiteKnight.Move(1, 4);
            Assert.That(_whiteKnight.XCoordinate, Is.EqualTo(2));
            Assert.That(_whiteKnight.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(1, 4, _whiteKnight));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_it_on_X_equals_1_and_Y_eqauls_0_and_moving_to_X_equals_2_and_Y_eqauls_2_with_a_black_pawn_in_the_way_should_not_move_it()
        {
            _chessBoard.Add(_whiteKnight, 1, 0);
            _chessBoard.Add(_whitePawn, 2, 2);
            _whiteKnight.Move(2, 2);
            Assert.That(_whiteKnight.XCoordinate, Is.EqualTo(1));
            Assert.That(_whiteKnight.YCoordinate, Is.EqualTo(0));
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(2));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(2));
            Assert.That(!_chessBoard.IsPieceAt(2, 2, _whiteKnight));
        }

        [Test]
        public void _05_and_capturing_a_black_rook()
        {
            _chessBoard.Add(_whiteKnight, 3, 3);
            _chessBoard.Add(_blackRook, 4, 5);
            _whiteKnight.Move(4, 5);
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackRook));
            Assert.That(_chessBoard.IsPieceAt(4, 5, _whiteKnight));
            Assert.That(_whiteKnight.XCoordinate, Is.EqualTo(4));
            Assert.That(_whiteKnight.YCoordinate, Is.EqualTo(5));
        }

        [Test]
        public void _06_and_is_captured_by_a_black_rook()
        {
            _chessBoard.Add(_whiteKnight, 3, 7);
            _chessBoard.Add(_blackRook, 3, 4);
            _blackRook.Move(3, 7);
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whiteKnight));
            Assert.That(_chessBoard.IsPieceAt(3, 7, _blackRook));
            Assert.That(_blackRook.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackRook.YCoordinate, Is.EqualTo(7));
        }

        [Test]
        public void _07_and_puts_the_black_king_in_check()
        {
            Assert.That(!_blackKing.IsInCheck);
            _chessBoard.Add(_whiteKnight, 4, 3);
            _whiteKnight.Move(5, 5);
            Assert.That(_blackKing.IsInCheck);
        }

    }
}
