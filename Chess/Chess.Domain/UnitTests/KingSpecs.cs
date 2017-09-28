using NUnit.Framework;
using System;

namespace Chess.Domain.UnitTests
{
    [TestFixture]
    class When_using_a_black_king_and
    {
        private ChessBoard _chessBoard;
        private Bishop _blackBishop;
        private BlackPawn _blackPawn;
        private BlackKing _blackKing;
        private Bishop _whiteBishop;
        private Rook _blackRook;
        private Rook _whiteRook;
        private Queen _whiteQueen;
        private WhiteKing _whiteKing;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _blackBishop = new Bishop(PieceColor.Black, _chessBoard);
            _blackPawn = new BlackPawn(_chessBoard);
            _whiteBishop = new Bishop(PieceColor.White, _chessBoard);
            _blackRook = new Rook(PieceColor.Black, _chessBoard);
            _whiteRook = new Rook(PieceColor.White, _chessBoard);
            _whiteQueen = new Queen(PieceColor.White, _chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_whiteKing, 4, 0);


        }

        [Test]
        public void _01_placing_the_black_king_on_X_equals_3_and_Y_equals_3_should_place_the_black_king_on_that_place_on_the_board()
        {
            _chessBoard.Add(_blackKing, 3, 3);
            Assert.That(_blackKing.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackKing.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_the_black_king_on_X_equals_4_and_Y_eqauls_7_and_moving_to_X_equals_4_and_Y_eqauls_6_should_move_the_king()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _blackKing.Move(4, 6);
            Assert.That(_blackKing.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackKing.YCoordinate, Is.EqualTo(6));
            Assert.That(_chessBoard.IsPieceAt(4, 6, _blackKing), Is.True);
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_the_black_king_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_5_and_Y_eqauls_3_should_not_move_the_king()
        {
            _chessBoard.Add(_blackKing, 2, 3);
            _blackKing.Move(5, 3);
            Assert.That(_blackKing.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackKing.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_the_black_king_on_X_equals_4_and_Y_eqauls_7_and_moving_to_X_equals_4_and_Y_eqauls_6_with_a_pawn_in_the_way_should_not_move_the_king()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackPawn, 4, 6);
            _blackKing.Move(4, 6);
            Assert.That(_blackKing.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackKing.YCoordinate, Is.EqualTo(7));
            Assert.That(_chessBoard.IsPieceAt(4, 6, _blackKing), Is.False);
        }

        [Test]
        public void _05_making_a_legal_move_by_placing_the_black_king_on_X_equals_3_and_Y_equals_3_and_moving_to_X_equals_4_and_Y_equals_4_with_a_white_bishop_putting_the_king_into_check_should_not_move_it()
        {
            _chessBoard.Add(_blackKing, 3, 3);
            _chessBoard.Add(_whiteBishop, 7, 1);
            _blackKing.Move(4, 4);
            Assert.That(_blackKing.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackKing.YCoordinate, Is.EqualTo(3));

        }

        [Test]
        public void _06_making_a_legal_move_by_placing_the_black_king_on_X_equals_3_and_Y_equals_3_and_moving_to_X_equals_4_and_Y_equals_4_with_a_black_bishop_able_to_put_the_king_into_check_should_not_throw_a_moving_into_check_exception_and_move_the_king()
        {
            _chessBoard.Add(_blackKing, 3, 3);
            _chessBoard.Add(_blackBishop, 7, 1);
            _blackKing.Move(4, 4);
            Assert.That(_blackKing.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackKing.YCoordinate, Is.EqualTo(4));
        }

        [Test]
        public void _07_black_king_at_X_equals_3_and_Y_equals_3_moves_to_X_equals_4_and_Y_equals_4_and_HasMoved_is_true()
        {
            _chessBoard.Add(_blackKing, 3, 3);
            _blackKing.Move(4, 4);
            Assert.That(_blackKing.HasMoved);
        }

        [Test]
        public void _08_black_king_at_X_equals_3_and_Y_equals_3_has_not_moved_and_HasMoved_is_false()
        {
            _chessBoard.Add(_blackKing, 3, 3);
            Assert.That(!_blackKing.HasMoved);
        }

        [Test]
        public void _09_black_king_at_X_equals_3_and_Y_equals_3_gets_placed_into_check_by_white_bishop_and_is_in_check()
        {
            _chessBoard.Add(_blackKing, 3, 3);
            _chessBoard.Add(_whiteBishop, 7, 1);
            _whiteBishop.Move(6, 0);
            Assert.That(_blackKing.IsInCheck);
        }

        [Test]
        public void _10_black_king_at_X_equals_4_and_Y_equals_7_long_castles_successfully()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 0, 7);
            _blackKing.LongCastle();
            Assert.That(_chessBoard.IsPieceAt(2, 7, _blackKing));
            Assert.That(_chessBoard.IsPieceAt(3, 7, _blackRook));
        }

        [Test]
        public void _11_black_king_at_X_equals_4_and_Y_equals_7_long_castles_but_he_has_already_moved()
        {
            _chessBoard.Add(_blackKing, 4, 6);
            _chessBoard.Add(_blackRook, 0, 7);
            _blackKing.Move(4, 7);
            _blackKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 7, _blackRook));
        }

        [Test]
        public void _12_black_king_at_X_equals_4_and_Y_equals_7_long_castles_but_the_rook_has_moved()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 0, 6);
            _blackRook.Move(0, 7);
            _blackKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 7, _blackRook));
        }

        [Test]
        public void _13_black_king_at_X_equals_4_and_Y_equals_7_long_castles_but_there_is_a_piece_in_the_way()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 0, 7);
            _chessBoard.Add(_blackPawn, 2, 7);
            _blackKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 7, _blackRook));
        }

        [Test]
        public void _14_black_king_at_X_equals_4_and_Y_equals_7_long_castles_but_he_is_in_check()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 0, 7);
            _chessBoard.Add(_whiteBishop, 3, 0);
            _whiteBishop.Move(7, 4);
            Assert.That(_blackKing.IsInCheck);
            _blackKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 7, _blackRook));
        }

        [Test]
        public void _15_black_king_at_X_equals_4_and_Y_equals_7_long_castles_but_one_of_the_spaces_is_under_attack()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 0, 7);
            _chessBoard.Add(_whiteRook, 2, 2);
            _blackKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 7, _blackRook));
        }

        [Test]
        public void _16_black_king_at_X_equals_4_and_Y_equals_7_long_castles_and_the_final_space_is_under_attack()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 0, 7);
            _chessBoard.Add(_whiteRook, 0, 2);
            _blackKing.LongCastle();
            Assert.That(_chessBoard.IsPieceAt(2, 7, _blackKing));
            Assert.That(_chessBoard.IsPieceAt(3, 7, _blackRook));
        }

        [Test]
        public void _17_black_king_at_X_equals_4_and_Y_equals_7_short_castles_successfully()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 7, 7);
            _blackKing.ShortCastle();
            Assert.That(_chessBoard.IsPieceAt(6, 7, _blackKing));
            Assert.That(_chessBoard.IsPieceAt(5, 7, _blackRook));
        }

        [Test]
        public void _18_black_king_at_X_equals_4_and_Y_equals_7_short_castles_but_he_has_moved()
        {
            _chessBoard.Add(_blackKing, 4, 6);
            _chessBoard.Add(_blackRook, 7, 7);
            _blackKing.Move(4, 7);
            _blackKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 7, _blackRook));
        }

        [Test]
        public void _19_black_king_at_X_equals_4_and_Y_equals_7_short_castles_but_the_rook_has_moved()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 7, 6);
            _blackRook.Move(7, 7);
            _blackKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 7, _blackRook));
        }

        [Test]
        public void _20_black_king_at_X_equals_4_and_Y_equals_7_short_castles_but_there_is_a_piece_in_the_way()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 7, 7);
            _chessBoard.Add(_blackPawn, 6, 7);
            _blackKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 7, _blackRook));
        }

        [Test]
        public void _21_black_king_at_X_equals_4_and_Y_equals_7_short_castles_but_he_is_in_check()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 7, 7);
            _chessBoard.Add(_whiteBishop, 3, 0);
            _whiteBishop.Move(7, 4);
            Assert.That(_blackKing.IsInCheck);
            _blackKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 7, _blackRook));
        }

        [Test]
        public void _22_black_king_at_X_equals_4_and_Y_equals_7_short_castles_but_one_of_the_spaces_is_under_attack()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 7, 7);
            _chessBoard.Add(_whiteRook, 6, 2);
            _blackKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 7, _blackKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 7, _blackRook));
        }

        [Test]
        public void _23_black_king_at_X_equals_4_and_Y_equals_7_short_castles_and_the_final_space_is_under_attack()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_blackRook, 7, 7);
            _chessBoard.Add(_whiteRook, 7, 2);
            _blackKing.ShortCastle();
            Assert.That(_chessBoard.IsPieceAt(6, 7, _blackKing));
            Assert.That(_chessBoard.IsPieceAt(5, 7, _blackRook));
        }

        [Test]
        public void _24_black_king_at_X_equals_4_and_Y_equals_7_is_in_check_but_he_moves_out()
        {
            _chessBoard.Add(_blackKing, 3, 3);
            _chessBoard.Add(_whiteBishop, 7, 1);
            _whiteBishop.Move(6, 0);
            Assert.That(_blackKing.IsInCheck, Is.EqualTo(true));
            _blackKing.Move(3, 2);
            Assert.That(_blackKing.IsInCheck, Is.EqualTo(false));
        }

        [Test]
        public void _25_black_king_at_X_equals_4_and_Y_equals_7_is_put_into_check_and_cant_move_resulting_in_a_checkmate()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteRook, 0, 6);
            _chessBoard.Add(_whiteQueen, 1, 0);
            var moveSummary = _whiteQueen.Move(1, 7);
            Assert.That(moveSummary.Flags.Contains(Flag.CheckMate));
        }

        [Test]
        public void _26_it_is_at_X_equals_4_and_Y_equals_7_and_a_piece_tries_to_capture_it_should_not_move_either_piece()
        {
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteRook, 4, 4);
            _whiteRook.Move(4, 7);
            Assert.That(_chessBoard.IsPieceAt(4, 7, _blackKing));
            Assert.That(_chessBoard.IsPieceAt(4, 4, _whiteRook));
        }

    }

    [TestFixture]
    class When_using_a_white_king_and
    {
        private ChessBoard _chessBoard;
        private Bishop _whiteBishop;
        private WhitePawn _whitePawn;
        private WhiteKing _whiteKing;
        private Bishop _blackBishop;
        private Rook _whiteRook;
        private Rook _blackRook;
        private BlackKing _blackKing;
        private Queen _blackQueen;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _whiteBishop = new Bishop(PieceColor.White, _chessBoard);
            _whitePawn = new WhitePawn(_chessBoard);
            // _whiteKing = new WhiteKing(_chessBoard);
            _blackBishop = new Bishop(PieceColor.Black, _chessBoard);
            _whiteRook = new Rook(PieceColor.White, _chessBoard);
            _blackRook = new Rook(PieceColor.Black, _chessBoard);
            _blackQueen = new Queen(PieceColor.Black, _chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);


        }

        [Test]
        public void _01_placing_the_white_king_on_X_equals_3_and_Y_equals_3_should_place_the_white_king_on_that_place_on_the_board()
        {
            _chessBoard.Add(_whiteKing, 3, 3);
            Assert.That(_whiteKing.XCoordinate, Is.EqualTo(3));
            Assert.That(_whiteKing.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _02_making_a_legal_move_by_placing_the_white_king_on_X_equals_4_and_Y_eqauls_0_and_moving_to_X_equals_4_and_Y_eqauls_1_should_move_the_king()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _whiteKing.Move(4, 1);
            Assert.That(_whiteKing.XCoordinate, Is.EqualTo(4));
            Assert.That(_whiteKing.YCoordinate, Is.EqualTo(1));
            Assert.That(_chessBoard.IsPieceAt(4, 1, _whiteKing), Is.True);
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_the_white_king_on_X_equals_2_and_Y_eqauls_3_and_moving_to_X_equals_5_and_Y_eqauls_3_should_not_move_the_king()
        {
            _chessBoard.Add(_whiteKing, 2, 3);
            _whiteKing.Move(5, 3);
            Assert.That(_whiteKing.XCoordinate, Is.EqualTo(2));
            Assert.That(_whiteKing.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_the_white_king_on_X_equals_4_and_Y_eqauls_7_and_moving_to_X_equals_4_and_Y_eqauls_6_with_a_pawn_in_the_way_should_not_move_the_king()
        {
            _chessBoard.Add(_whiteKing, 4, 7);
            _chessBoard.Add(_whitePawn, 4, 6);
            _whiteKing.Move(4, 6);
            Assert.That(_whiteKing.XCoordinate, Is.EqualTo(4));
            Assert.That(_whiteKing.YCoordinate, Is.EqualTo(7));
            Assert.That(_chessBoard.IsPieceAt(4, 6, _whiteKing), Is.False);
        }

        [Test]
        public void _05_making_a_legal_move_by_placing_the_white_king_on_X_equals_3_and_Y_equals_3_and_moving_to_X_equals_4_and_Y_equals_4_with_a_black_bishop_putting_the_king_into_check_should_not_move_it()
        {
            _chessBoard.Add(_whiteKing, 3, 3);
            _chessBoard.Add(_blackBishop, 7, 1);
            _whiteKing.Move(4, 4);
            Assert.That(_whiteKing.XCoordinate, Is.EqualTo(3));
            Assert.That(_whiteKing.YCoordinate, Is.EqualTo(3));

        }

        [Test]
        public void _06_making_a_legal_move_by_placing_the_black_king_on_X_equals_3_and_Y_equals_3_and_moving_to_X_equals_4_and_Y_equals_4_with_a_black_bishop_able_to_put_the_king_into_check_should_not_throw_a_moving_into_check_exception_and_move_the_king()
        {
            _chessBoard.Add(_whiteKing, 3, 3);
            _chessBoard.Add(_whiteBishop, 7, 1);
            _whiteKing.Move(4, 4);
            Assert.That(_whiteKing.XCoordinate, Is.EqualTo(4));
            Assert.That(_whiteKing.YCoordinate, Is.EqualTo(4));
        }

        [Test]
        public void _07_is_at_X_equals_3_and_Y_equals_3_moves_to_X_equals_4_and_Y_equals_4_and_HasMoved_is_true()
        {
            _chessBoard.Add(_whiteKing, 3, 3);
            _whiteKing.Move(4, 4);
            Assert.That(_whiteKing.HasMoved);
        }

        [Test]
        public void _08_is_at_X_equals_3_and_Y_equals_3_has_not_moved_and_HasMoved_is_false()
        {
            _chessBoard.Add(_whiteKing, 3, 3);
            Assert.That(!_whiteKing.HasMoved);
        }

        [Test]
        public void _09_is_at_X_equals_3_and_Y_equals_3_gets_placed_into_check_by_white_bishop_and_is_in_check()
        {
            _chessBoard.Add(_whiteKing, 3, 3);
            _chessBoard.Add(_blackBishop, 7, 1);
            _blackBishop.Move(6, 0);
            Assert.That(_whiteKing.IsInCheck);
        }

        [Test]
        public void _10_is_at_X_equals_4_and_Y_equals_0_long_castles_successfully()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 0, 0);
            _whiteKing.LongCastle();
            Assert.That(_chessBoard.IsPieceAt(2, 0, _whiteKing));
            Assert.That(_chessBoard.IsPieceAt(3, 0, _whiteRook));
        }

        [Test]
        public void _11_is_at_X_equals_4_and_Y_equals_0_long_castles_but_he_has_already_moved()
        {
            _chessBoard.Add(_whiteKing, 4, 1);
            _chessBoard.Add(_whiteRook, 0, 0);
            _whiteKing.Move(4, 0);
            _whiteKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 0, _whiteRook));
        }

        [Test]
        public void _12_is_at_X_equals_4_and_Y_equals_0_long_castles_but_the_rook_has_moved()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 0, 1);
            _whiteRook.Move(0, 0);
            _whiteKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 0, _whiteRook));
        }

        [Test]
        public void _13_is_at_X_equals_4_and_Y_equals_0_long_castles_but_there_is_a_piece_in_the_way()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 0, 0);
            _chessBoard.Add(_whitePawn, 2, 0);
            _whiteKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 0, _whiteRook));
        }

        [Test]
        public void _14_is_at_X_equals_4_and_Y_equals_0_long_castles_but_he_is_in_check()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 0, 0);
            _chessBoard.Add(_blackBishop, 5, 5);
            _blackBishop.Move(7, 3);
            Assert.That(_whiteKing.IsInCheck);
            _whiteKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 0, _whiteRook));
        }

        [Test]
        public void _15_is_at_X_equals_4_and_Y_equals_0_long_castles_but_one_of_the_spaces_is_under_attack()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 0, 0);
            _chessBoard.Add(_blackRook, 2, 2);
            _whiteKing.LongCastle();
            Assert.That(!_chessBoard.IsPieceAt(2, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(3, 0, _whiteRook));
        }

        [Test]
        public void _16_is_at_X_equals_4_and_Y_equals_0_long_castles_and_the_final_space_is_under_attack()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 0, 0);
            _chessBoard.Add(_blackRook, 0, 2);
            _whiteKing.LongCastle();
            Assert.That(_chessBoard.IsPieceAt(2, 0, _whiteKing));
            Assert.That(_chessBoard.IsPieceAt(3, 0, _whiteRook));
        }

        [Test]
        public void _17_is_at_X_equals_4_and_Y_equals_0_short_castles_successfully()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 7, 0);
            _whiteKing.ShortCastle();
            Assert.That(_chessBoard.IsPieceAt(6, 0, _whiteKing));
            Assert.That(_chessBoard.IsPieceAt(5, 0, _whiteRook));
        }

        [Test]
        public void _18_is_at_X_equals_4_and_Y_equals_0_short_castles_but_he_has_moved()
        {
            _chessBoard.Add(_whiteKing, 4, 1);
            _chessBoard.Add(_whiteRook, 7, 0);
            _whiteKing.Move(4, 0);
            _whiteKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 0, _whiteRook));
        }

        [Test]
        public void _19_is_at_X_equals_4_and_Y_equals_0_short_castles_but_the_rook_has_moved()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 7, 1);
            _whiteRook.Move(7, 0);
            _whiteKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 0, _whiteRook));
        }

        [Test]
        public void _20_is_at_X_equals_4_and_Y_equals_0_short_castles_but_there_is_a_piece_in_the_way()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 7, 0);
            _chessBoard.Add(_whitePawn, 6, 0);
            _whiteKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 0, _whiteRook));
        }

        [Test]
        public void _21_is_at_X_equals_4_and_Y_equals_0_short_castles_but_he_is_in_check()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 7, 0);
            _chessBoard.Add(_blackBishop, 5, 5);
            _blackBishop.Move(7, 3);
            Assert.That(_whiteKing.IsInCheck);
            _whiteKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 0, _whiteRook));
        }

        [Test]
        public void _22_is_at_X_equals_4_and_Y_equals_0_short_castles_but_one_of_the_spaces_is_under_attack()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 7, 0);
            _chessBoard.Add(_blackRook, 6, 2);
            _whiteKing.ShortCastle();
            Assert.That(!_chessBoard.IsPieceAt(6, 0, _whiteKing));
            Assert.That(!_chessBoard.IsPieceAt(5, 0, _whiteRook));
        }

        [Test]
        public void _23_is_X_equals_4_and_Y_equals_0_short_castles_and_the_final_space_is_under_attack()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_whiteRook, 7, 0);
            _chessBoard.Add(_whiteRook, 7, 2);
            _whiteKing.ShortCastle();
            Assert.That(_chessBoard.IsPieceAt(6, 0, _whiteKing));
            Assert.That(_chessBoard.IsPieceAt(5, 0, _whiteRook));
        }

        [Test]
        public void _24_is_at_X_equals_4_and_Y_equals_0_is_in_check_but_he_moves_out()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_blackBishop, 5, 5);
            _blackBishop.Move(7, 3);
            Assert.That(_whiteKing.IsInCheck);
            _whiteKing.Move(3, 1);
            Assert.That(!_whiteKing.IsInCheck);
        }

        [Test]
        public void _25_is_at_X_equals_4_and_Y_equals_0_is_put_into_check_and_cant_move_resulting_in_a_checkmate()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_blackRook, 0, 1);
            _chessBoard.Add(_blackQueen, 1, 2);
            var moveSummary = _blackQueen.Move(1, 0);
            Assert.That(moveSummary.Flags.Contains(Flag.CheckMate));
        }

        [Test]
        public void _26_it_is_at_X_equals_4_and_Y_equals_0_and_a_piece_tries_to_capture_it_should_not_move_either_piece()
        {
            _chessBoard.Add(_whiteKing, 4, 0);
            _chessBoard.Add(_blackRook, 4, 4);
            _blackRook.Move(4, 0);
            Assert.That(_chessBoard.IsPieceAt(4, 0, _whiteKing));
            Assert.That(_chessBoard.IsPieceAt(4, 4, _blackRook));
        }

    }
}
