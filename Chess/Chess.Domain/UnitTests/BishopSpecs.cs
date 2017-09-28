using NUnit.Framework;

namespace Chess.Domain.UnitTests
{
    [TestFixture]
    class When_using_a_black_bishop_and
    {
        private ChessBoard _chessBoard;
        private Bishop _blackBishop;
        private BlackPawn _blackPawn;
        private BlackKing _blackKing;
        private WhiteKing _whiteKing;
        private Queen _whiteQueen;
        private Knight _whiteKnight;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _blackBishop = new Bishop(PieceColor.Black, _chessBoard);
            _whiteQueen = new Queen(PieceColor.White, _chessBoard);
            _whiteKnight = new Knight(PieceColor.White, _chessBoard);
            _blackPawn = new BlackPawn(_chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);
        }

        [Test]
        public void _01_placing_the_black_bishop_on_X_equals_3_and_Y_equals_3_should_place_the_black_bishop_on_that_place_on_the_board()
        {
            _chessBoard.Add(_blackBishop, 3, 3);
            Assert.That(_blackBishop.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackBishop.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_the_black_bishop_on_X_equals_2_and_Y_eqauls_7_and_moving_to_X_equals_7_and_Y_eqauls_2_should_move_the_bishop()
        {
            _chessBoard.Add(_blackBishop, 2, 7);
            _blackBishop.Move(7, 2);
            Assert.That(_blackBishop.XCoordinate, Is.EqualTo(7));
            Assert.That(_blackBishop.YCoordinate, Is.EqualTo(2));
            Assert.That(_chessBoard.IsPieceAt(7, 2, _blackBishop), Is.True);
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_the_black_bishop_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_6_and_Y_eqauls_3_should_not_move_the_bishop()
        {
            _chessBoard.Add(_blackBishop, 2, 3);
            _blackBishop.Move(6, 3);
            Assert.That(_blackBishop.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackBishop.YCoordinate, Is.EqualTo(3));
            Assert.That(_chessBoard.IsPieceAt(6, 3, _blackBishop), Is.False);
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_the_black_bishop_on_X_equals_0_and_Y_eqauls_0_and_moving_to_X_equals_4_and_Y_eqauls_4_with_a_pawn_in_the_way_should_not_move_the_bishop()
        {
            _chessBoard.Add(_blackBishop, 0, 0);
            _chessBoard.Add(_blackPawn, 2, 2);
            _blackBishop.Move(4, 4);
            Assert.That(_blackBishop.XCoordinate, Is.EqualTo(0));
            Assert.That(_blackBishop.YCoordinate, Is.EqualTo(0));
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(2));
            Assert.That(_chessBoard.IsPieceAt(4, 4, _blackBishop), Is.False);
        }

        [Test]
        public void _05_and_capturing_a_white_queen()
        {
            _chessBoard.Add(_blackBishop, 7, 4);
            _chessBoard.Add(_whiteQueen, 3, 0);
            _blackBishop.Move(3, 0);
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whiteQueen));
            Assert.That(_chessBoard.IsPieceAt(3, 0, _blackBishop));
            Assert.That(_blackBishop.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackBishop.YCoordinate, Is.EqualTo(0));
        }

        [Test]
        public void _06_and_is_captured_by_a_white_knight()
        {
            _chessBoard.Add(_blackBishop, 5, 5);
            _chessBoard.Add(_whiteKnight, 6, 3);
            _whiteKnight.Move(5, 5);
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackBishop));
            Assert.That(_chessBoard.IsPieceAt(5, 5, _whiteKnight));
            Assert.That(_whiteKnight.XCoordinate, Is.EqualTo(5));
            Assert.That(_whiteKnight.YCoordinate, Is.EqualTo(5));

        }

        [Test]
        public void _07_and_puts_the_white_king_in_check()
        {
            Assert.That(!_whiteKing.IsInCheck);
            _chessBoard.Add(_blackBishop, 1, 5);
            _blackBishop.Move(0, 4);
            Assert.That(_whiteKing.IsInCheck);
        }

        
    }

    [TestFixture]
    class When_using_a_white_bishop_and
    {
        private ChessBoard _chessBoard;
        private Bishop _whiteBishop;
        private WhitePawn _whitePawn;
        private BlackKing _blackKing;
        private WhiteKing _whiteKing;
        private Queen _blackQueen;
        private Knight _blackKnight;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _whiteBishop = new Bishop(PieceColor.White, _chessBoard);
            _blackQueen = new Queen(PieceColor.Black, _chessBoard);
            _blackKnight = new Knight(PieceColor.Black, _chessBoard);
            _whitePawn = new WhitePawn(_chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);
        }

        [Test]
        public void _01_placing_it_on_X_equals_3_and_Y_equals_3_should_place_the_bishop_on_that_place_on_the_board()
        {
            _chessBoard.Add(_whiteBishop, 3, 3);
            Assert.That(_whiteBishop.XCoordinate, Is.EqualTo(3));
            Assert.That(_whiteBishop.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_it_on_X_equals_2_and_Y_eqauls_7_and_moving_to_X_equals_7_and_Y_eqauls_2_should_move_the_bishop()
        {
            _chessBoard.Add(_whiteBishop, 2, 7);
            _whiteBishop.Move(7, 2);
            Assert.That(_whiteBishop.XCoordinate, Is.EqualTo(7));
            Assert.That(_whiteBishop.YCoordinate, Is.EqualTo(2));
            Assert.That(_chessBoard.IsPieceAt(7, 2, _whiteBishop), Is.True);
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_it_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_6_and_Y_eqauls_3_should_not_move_the_bishop()
        {
            _chessBoard.Add(_whiteBishop, 2, 3);
            _whiteBishop.Move(6, 3);
            Assert.That(_whiteBishop.XCoordinate, Is.EqualTo(2));
            Assert.That(_whiteBishop.YCoordinate, Is.EqualTo(3));
            Assert.That(_chessBoard.IsPieceAt(6, 3, _whiteBishop), Is.False);
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_it_on_X_equals_0_and_Y_eqauls_0_and_moving_to_X_equals_4_and_Y_eqauls_4_with_an_ally_pawn_in_the_way_should_not_move_the_bishop()
        {
            _chessBoard.Add(_whiteBishop, 0, 0);
            _chessBoard.Add(_whitePawn, 2, 2);
            _whiteBishop.Move(4, 4);
            Assert.That(_whiteBishop.XCoordinate, Is.EqualTo(0));
            Assert.That(_whiteBishop.YCoordinate, Is.EqualTo(0));
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(2));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(2));
            Assert.That(_chessBoard.IsPieceAt(4, 4, _whiteBishop), Is.False);
        }

        [Test]
        public void _05_and_capturing_a_black_queen()
        {
            _chessBoard.Add(_whiteBishop, 7, 4);
            _chessBoard.Add(_blackQueen, 3, 0);
            _whiteBishop.Move(3, 0);
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackQueen));
            Assert.That(_chessBoard.IsPieceAt(3, 0, _whiteBishop));
            Assert.That(_whiteBishop.XCoordinate, Is.EqualTo(3));
            Assert.That(_whiteBishop.YCoordinate, Is.EqualTo(0));
        }

        [Test]
        public void _06_and_is_captured_by_a_black_knight()
        {
            _chessBoard.Add(_whiteBishop, 5, 5);
            _chessBoard.Add(_blackKnight, 6, 3);
            _blackKnight.Move(5, 5);
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whiteBishop));
            Assert.That(_chessBoard.IsPieceAt(5, 5, _blackKnight));
            Assert.That(_blackKnight.XCoordinate, Is.EqualTo(5));
            Assert.That(_blackKnight.YCoordinate, Is.EqualTo(5));

        }

        [Test]
        public void _07_and_puts_the_black_king_in_check()
        {
            Assert.That(!_blackKing.IsInCheck);
            _chessBoard.Add(_whiteBishop, 3, 4);
            _whiteBishop.Move(2, 5);
            Assert.That(_blackKing.IsInCheck);
        }


    }
}
