using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.UnitTests
{
    [TestFixture]
    class When_using_a_black_queen_and
    {
        private ChessBoard _chessBoard;
        private King _blackKing;
        private BlackPawn _blackPawn;
        private Rook _blackRook;
        private Bishop _blackBishop;
        private Knight _blackKnight;
        private Queen _blackQueen;
        private WhiteKing _whiteKing;
        private Knight _whiteKnight;
        private Bishop _whiteBishop;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _blackPawn = new BlackPawn(_chessBoard);
            _blackRook = new Rook(PieceColor.Black, _chessBoard);
            _blackBishop = new Bishop(PieceColor.Black, _chessBoard);
            _blackKnight = new Knight(PieceColor.Black, _chessBoard);
            _blackQueen = new Queen(PieceColor.Black, _chessBoard);
            _whiteKnight = new Knight(PieceColor.White, _chessBoard);
            _whiteBishop = new Bishop(PieceColor.White, _chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);

        }

        [Test]
        public void _01_placing_it_on_X_equals_4_and_Y_equals_5_should_place_it_on_that_place_on_the_board()
        {
            _chessBoard.Add(_blackQueen, 4, 5);
            Assert.That(_blackQueen.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackQueen.YCoordinate, Is.EqualTo(5));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_it_on_X_equals_4_and_Y_eqauls_5_and_moving_to_X_equals_0_and_Y_eqauls_1_should_move_it()
        {
            _chessBoard.Add(_blackQueen, 4, 5);
            _blackQueen.Move(0, 1);
            Assert.That(_blackQueen.XCoordinate, Is.EqualTo(0));
            Assert.That(_blackQueen.YCoordinate, Is.EqualTo(1));
            Assert.That(_chessBoard.IsPieceAt(0, 1, _blackQueen));
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_it_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_4_and_Y_eqauls_2_should_not_move_it()
        {
            _chessBoard.Add(_blackQueen, 2, 3);
            _blackQueen.Move(4, 2);
            Assert.That(_blackQueen.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackQueen.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(4, 2, _blackQueen));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_it_on_X_equals_3_and_Y_eqauls_0_and_moving_to_X_equals_3_and_Y_eqauls_5_with_a_black_pawn_in_the_way_should_not_move_it()
        {
            _chessBoard.Add(_blackQueen, 3, 0);
            _chessBoard.Add(_blackPawn, 3, 1);
            _blackQueen.Move(3, 5);
            Assert.That(_blackQueen.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackQueen.YCoordinate, Is.EqualTo(0));
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(1));
            Assert.That(!_chessBoard.IsPieceAt(3, 5, _blackQueen));
        }

        [Test]
        public void _05_and_capturing_a_white_knight()
        {
            _chessBoard.Add(_blackQueen, 3, 3);
            _chessBoard.Add(_whiteKnight, 7, 3);
            _blackQueen.Move(7, 3);
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whiteKnight));
            Assert.That(_chessBoard.IsPieceAt(7, 3, _blackQueen));
            Assert.That(_blackQueen.XCoordinate, Is.EqualTo(7));
            Assert.That(_blackQueen.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _06_and_is_captured_by_a_white_bishop()
        {
            _chessBoard.Add(_blackQueen, 3, 7);
            _chessBoard.Add(_whiteBishop, 0, 4);
            _whiteBishop.Move(3, 7);
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackQueen));
            Assert.That(_chessBoard.IsPieceAt(3, 7, _whiteBishop));
            Assert.That(_whiteBishop.XCoordinate, Is.EqualTo(3));
            Assert.That(_whiteBishop.YCoordinate, Is.EqualTo(7));
        }

        [Test]
        public void _07_and_puts_the_white_king_in_check()
        {
            Assert.That(!_whiteKing.IsInCheck);
            _chessBoard.Add(_blackQueen, 0, 7);
            _blackQueen.Move(0, 0);
            Assert.That(_whiteKing.IsInCheck);
        }

    }

    [TestFixture]
    class When_using_a_white_queen_and   
    {
        private ChessBoard _chessBoard;
        private King _blackKing;
        private WhitePawn _whitePawn;
        private Rook _whiteRook;
        private Bishop _whiteBishop;
        private Knight _whiteKnight;
        private Queen _whiteQueen;
        private WhiteKing _whiteKing;
        private Knight _blackKnight;
        private Bishop _blackBishop;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _whitePawn = new WhitePawn(_chessBoard);
            _whiteRook = new Rook(PieceColor.White, _chessBoard);
            _blackBishop = new Bishop(PieceColor.Black, _chessBoard);
            _blackKnight = new Knight(PieceColor.Black, _chessBoard);
            _whiteQueen = new Queen(PieceColor.White, _chessBoard);
            _whiteKnight = new Knight(PieceColor.White, _chessBoard);
            _whiteBishop = new Bishop(PieceColor.White, _chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);

        }

        [Test]
        public void _01_placing_it_on_X_equals_4_and_Y_equals_5_should_place_it_on_that_place_on_the_board()
        {
            _chessBoard.Add(_whiteQueen, 4, 5);
            Assert.That(_whiteQueen.XCoordinate, Is.EqualTo(4));
            Assert.That(_whiteQueen.YCoordinate, Is.EqualTo(5));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_it_on_X_equals_4_and_Y_eqauls_5_and_moving_to_X_equals_0_and_Y_eqauls_1_should_move_it()
        {
            _chessBoard.Add(_whiteQueen, 4, 5);
            _whiteQueen.Move(0, 1);
            Assert.That(_whiteQueen.XCoordinate, Is.EqualTo(0));
            Assert.That(_whiteQueen.YCoordinate, Is.EqualTo(1));
            Assert.That(_chessBoard.IsPieceAt(0, 1, _whiteQueen));
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_it_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_4_and_Y_eqauls_2_should_not_move_it()
        {
            _chessBoard.Add(_whiteQueen, 2, 3);
            _whiteQueen.Move(4, 2);
            Assert.That(_whiteQueen.XCoordinate, Is.EqualTo(2));
            Assert.That(_whiteQueen.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(4, 2, _whiteQueen));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_it_on_X_equals_3_and_Y_eqauls_0_and_moving_to_X_equals_3_and_Y_eqauls_5_with_a_black_white_in_the_way_should_not_move_it()
        {
            _chessBoard.Add(_whiteQueen, 3, 0);
            _chessBoard.Add(_whitePawn, 3, 1);
            _whiteQueen.Move(3, 5);
            Assert.That(_whiteQueen.XCoordinate, Is.EqualTo(3));
            Assert.That(_whiteQueen.YCoordinate, Is.EqualTo(0));
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(1));
            Assert.That(!_chessBoard.IsPieceAt(3, 5, _whiteQueen));
        }

        [Test]
        public void _05_and_capturing_a_black_knight()
        {
            _chessBoard.Add(_whiteQueen, 3, 3);
            _chessBoard.Add(_blackKnight, 7, 3);
            _whiteQueen.Move(7, 3);
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackKnight));
            Assert.That(_chessBoard.IsPieceAt(7, 3, _whiteQueen));
            Assert.That(_whiteQueen.XCoordinate, Is.EqualTo(7));
            Assert.That(_whiteQueen.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _06_and_is_captured_by_a_black_bishop()
        {
            _chessBoard.Add(_whiteQueen, 3, 7);
            _chessBoard.Add(_blackBishop, 0, 4);
            _blackBishop.Move(3, 7);
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whiteQueen));
            Assert.That(_chessBoard.IsPieceAt(3, 7, _blackBishop));
            Assert.That(_blackBishop.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackBishop.YCoordinate, Is.EqualTo(7));
        }

        [Test]
        public void _07_and_puts_the_black_king_in_check()
        {
            Assert.That(!_blackKing.IsInCheck);
            _chessBoard.Add(_whiteQueen, 7, 6);
            _whiteQueen.Move(7, 7);
            Assert.That(_blackKing.IsInCheck);
        }

    }
}
