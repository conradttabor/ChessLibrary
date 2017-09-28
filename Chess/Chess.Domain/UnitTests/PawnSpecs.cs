using NUnit.Framework;

namespace Chess.Domain.UnitTests
{
    [TestFixture]
    public class When_using_a_black_pawn_and
    {
        private ChessBoard _chessBoard;
        private BlackPawn _blackPawn;
        private BlackKing _blackKing;
        private WhiteKing _whiteKing;
        private Bishop _whiteBishop;
        private WhitePawn _whitePawn;
        private Rook _blackRook;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _whiteBishop = new Bishop(PieceColor.White, _chessBoard);
            _blackRook = new Rook(PieceColor.Black, _chessBoard);
            _whitePawn = new WhitePawn(_chessBoard);
            _blackPawn = new BlackPawn(_chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);
        }

        [Test]
        public void _01_placing_the_black_pawn_on_X_equals_6_and_Y_equals_3_should_place_the_black_pawn_on_that_place_on_the_board()
        {
            _chessBoard.Add(_blackPawn, 6, 3);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(6));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _02_making_an_illegal_move_by_placing_the_black_pawn_on_X_equals_6_and_Y_eqauls_3_and_moving_to_X_equals_7_and_Y_eqauls_3_should_not_move_the_pawn()
        {
            _chessBoard.Add(_blackPawn, 6, 3);
            _blackPawn.Move(7, 3);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(6));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_the_black_pawn_on_X_equals_6_and_Y_eqauls_3_and_moving_to_X_equals_4_and_Y_eqauls_3_should_not_move_the_pawn()
        {
            _chessBoard.Add(_blackPawn, 6, 3);
            _blackPawn.Move(4, 3);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(6));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(3));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_the_black_pawn_on_X_equals_6_and_Y_eqauls_3_and_moving_to_X_equals_6_and_Y_eqauls_2_should_move_the_pawn()
        {
            _chessBoard.Add(_blackPawn, 6, 3);
            _blackPawn.Move(6, 2);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(6));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(2));
            Assert.That(_chessBoard.IsPieceAt(6, 2, _blackPawn), Is.True);
        }

        [Test]
        public void _05_moving_2_spaces_forward_on_its_first_move_moves_the_pawn()
        {
            _chessBoard.Add(_blackPawn, 1, 6);
            _blackPawn.Move(1, 4);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(1));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(4));
            Assert.That(_chessBoard.IsPieceAt(1, 4, _blackPawn));
        }

        [Test]
        public void _06_making_a_diagonal_move_to_capture_a_white_rook_moves_the_pawn_and_captures_the_rook()
        {
            _chessBoard.Add(_blackPawn, 3, 3);
            _chessBoard.Add(_whiteBishop, 4, 2);
            _blackPawn.Move(4, 2);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(2));
            Assert.That(_chessBoard.IsPieceAt(4, 2, _blackPawn), Is.True);
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whiteBishop));
        }

        [Test]
        public void _07_making_a_diagonal_move_without_an_enemy_piece_there_should_not_move_the_pawn()
        {
            _chessBoard.Add(_blackPawn, 3, 3);
            _blackPawn.Move(4, 2);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(3));
            Assert.That(_chessBoard.IsPieceAt(3, 3, _blackPawn), Is.True);
        }

        [Test]
        public void _08_trying_to_capture_an_enemy_bishop_directly_in_front_of_it_should_not_move_the_pawn()
        {
            _chessBoard.Add(_blackPawn, 3, 3);
            _chessBoard.Add(_whiteBishop, 3, 2);
            _blackPawn.Move(3, 2);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(3, 2, _blackPawn));
            Assert.That(!_chessBoard.CapturedWhitePieces.Contains(_whiteBishop));
        }

        [Test]
        public void _09_cant_move_forward_with_own_piece_in_front_of_it()
        {
            _chessBoard.Add(_blackPawn, 3, 3);
            _chessBoard.Add(_blackRook, 3, 2);
            _blackPawn.Move(3, 2);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(3, 2, _blackPawn));

        }

        [Test]
        public void _10_takes_enemy_pawn_with_En_Passant()
        {
            _chessBoard.Add(_blackPawn, 2, 3);
            _chessBoard.Add(_whitePawn, 3, 1);
            _whitePawn.Move(3, 3);
            _blackPawn.Move(3, 2);
            Assert.That(!_chessBoard.IsPieceAt(3, 3, _whitePawn));
            Assert.That(_chessBoard.IsPieceAt(3, 2, _blackPawn));
            Assert.That(_chessBoard.CapturedWhitePieces.Contains(_whitePawn));
        }

        [Test]
        public void _11_tries_to_takes_enemy_pawn_with_En_Passant_but_has_waited_too_long()
        {
            _chessBoard.Add(_blackPawn, 2, 3);
            _chessBoard.Add(_whitePawn, 3, 1);
            _whitePawn.Move(3, 3);
            _blackKing.Move(4, 6);
            _whiteKing.Move(4, 1);
            _blackPawn.Move(3, 2);
            Assert.That(_chessBoard.IsPieceAt(3, 3, _whitePawn));
            Assert.That(!_chessBoard.IsPieceAt(3, 2, _blackPawn));
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.CapturedWhitePieces.Contains(_whitePawn));
        }

        [Test]
        public void _12_tries_to_takes_enemy_pawn_with_En_Passant_but_enemy_pawn_didnt_double_step()
        {
            _chessBoard.Add(_blackPawn, 2, 3);
            _chessBoard.Add(_whitePawn, 3, 1);
            _whitePawn.Move(3, 2);
            _whitePawn.Move(3, 3);
            _blackPawn.Move(3, 2);
            Assert.That(_chessBoard.IsPieceAt(3, 3, _whitePawn));
            Assert.That(!_chessBoard.IsPieceAt(3, 2, _blackPawn));
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.CapturedWhitePieces.Contains(_whitePawn));
        }

        [Test]
        public void _13_tries_to_takes_enemy_pawn_with_En_Passant_but_they_are_on_the_wrong_row()
        {
            _chessBoard.Add(_blackPawn, 2, 5);
            _chessBoard.Add(_whitePawn, 3, 1);
            _whitePawn.Move(3, 3);
            _blackPawn.Move(3, 2);
            Assert.That(_chessBoard.IsPieceAt(3, 3, _whitePawn));
            Assert.That(!_chessBoard.IsPieceAt(3, 2, _blackPawn));
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(2));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(5));
            Assert.That(!_chessBoard.CapturedWhitePieces.Contains(_whitePawn));
        }

        [Test]
        public void _14_makes_it_to_the_other_side_of_the_board_and_is_promoted_to_a_queen()
        {
            _chessBoard.Add(_blackPawn, 1, 1);
            _blackPawn.Move(1, 0);
            Assert.That(_chessBoard.PieceAt(1, 0) is Queen);
        }

        [Test]
        public void _15_is_directly_in_front_of_an_enemy_king_does_not_put_the_king_in_check()
        {
            _chessBoard.Add(_blackPawn, 4, 2);
            _blackPawn.Move(4, 1);
            Assert.That(!_whiteKing.IsInCheck);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(4));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(1));
            Assert.That(_chessBoard.IsPieceAt(4, 0, _whiteKing));
        }

        [Test]
        public void _16_is_diagonal_from_the_enemy_king_should_put_the_enemy_king_in_check()
        {
            _chessBoard.Add(_blackPawn, 3, 2);
            _blackPawn.Move(3, 1);
            Assert.That(_whiteKing.IsInCheck);
            Assert.That(_blackPawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_blackPawn.YCoordinate, Is.EqualTo(1));
            Assert.That(_chessBoard.IsPieceAt(4, 0, _whiteKing));
        }

    }

    [TestFixture]
    public class When_using_a_white_pawn_and
    {
        private ChessBoard _chessBoard;
        private WhitePawn _whitePawn;
        private BlackKing _blackKing;
        private WhiteKing _whiteKing;
        private Bishop _blackBishop;
        private Rook _whiteRook;
        private BlackPawn _blackPawn;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
            _blackBishop = new Bishop(PieceColor.Black, _chessBoard);
            _whiteRook = new Rook(PieceColor.White, _chessBoard);
            _whitePawn = new WhitePawn(_chessBoard);
            _blackKing = new BlackKing(_chessBoard);
            _whiteKing = new WhiteKing(_chessBoard);
            _blackPawn = new BlackPawn(_chessBoard);
            _chessBoard.Add(_blackKing, 4, 7);
            _chessBoard.Add(_whiteKing, 4, 0);
        }

        [Test]
        public void _01_placing_the_white_pawn_on_X_equals_6_and_Y_equals_1_should_place_the_white_pawn_on_that_place_on_the_board()
        {
            _chessBoard.Add(_whitePawn, 6, 1);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(6));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(1));
        }

        [Test]
        public void _02_making_an_illegal_move_by_placing_the_white_pawn_on_X_equals_6_and_Y_eqauls_1_and_moving_to_X_equals_7_and_Y_eqauls_2_should_not_move_the_pawn()
        {
            _chessBoard.Add(_whitePawn, 6, 1);
            _whitePawn.Move(7, 2);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(6));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(1));
        }

        [Test]
        public void _03_making_an_illegal_move_by_placing_the_white_pawn_on_X_equals_6_and_Y_eqauls_1_and_moving_to_X_equals_6_and_Y_eqauls_4_should_not_move_the_pawn()
        {
            _chessBoard.Add(_whitePawn, 6, 1);
            _whitePawn.Move(6, 4);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(6));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(1));
        }

        [Test]
        public void _04_making_a_legal_move_by_placing_the_white_pawn_on_X_equals_6_and_Y_eqauls_1_and_moving_to_X_equals_6_and_Y_eqauls_2_should_move_the_pawn()
        {
            _chessBoard.Add(_whitePawn, 6, 1);
            _whitePawn.Move(6, 2);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(6));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(2));
            Assert.That(_chessBoard.IsPieceAt(6, 2, _whitePawn), Is.True);
        }

        [Test]
        public void _05_moving_2_spaces_forward_on_its_first_move_moves_the_pawn()
        {
            _chessBoard.Add(_whitePawn, 1, 1);
            _whitePawn.Move(1, 3);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(1));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(3));
            Assert.That(_chessBoard.IsPieceAt(1, 3, _whitePawn));
        }

        [Test]
        public void _06_making_a_diagonal_move_to_capture_a_black_bishop_moves_the_pawn_and_captures_the_bishop()
        {
            _chessBoard.Add(_whitePawn, 3, 3);
            _chessBoard.Add(_blackBishop, 4, 4);
            _whitePawn.Move(4, 4);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(4));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(4));
            Assert.That(_chessBoard.IsPieceAt(4, 4, _whitePawn));
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackBishop));
        }

        [Test]
        public void _07_making_a_diagonal_move_without_an_enemy_piece_there_should_not_move_the_pawn()
        {
            _chessBoard.Add(_whitePawn, 3, 3);
            _whitePawn.Move(4, 4);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(3));
            Assert.That(_chessBoard.IsPieceAt(3, 3, _whitePawn), Is.True);
        }

        [Test]
        public void _08_trying_to_capture_an_enemy_bishop_directly_in_front_of_it_should_not_move_the_pawn()
        {
            _chessBoard.Add(_whitePawn, 3, 3);
            _chessBoard.Add(_blackBishop, 3, 4);
            _whitePawn.Move(3, 4);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(3, 4, _whitePawn));
            Assert.That(!_chessBoard.CapturedBlackPieces.Contains(_blackBishop));
        }

        [Test]
        public void _09_cant_move_forward_with_own_piece_in_front_of_it()
        {
            _chessBoard.Add(_whitePawn, 3, 3);
            _chessBoard.Add(_whiteRook, 3, 4);
            _whitePawn.Move(3, 4);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(3));
            Assert.That(!_chessBoard.IsPieceAt(3, 4, _whitePawn));

        }

        [Test]
        public void _10_takes_enemy_pawn_with_En_Passant()
        {
            _chessBoard.Add(_blackPawn, 2, 6);
            _chessBoard.Add(_whitePawn, 3, 4);
            _blackPawn.Move(2, 4);
            _whitePawn.Move(2, 5);           
            Assert.That(_chessBoard.IsPieceAt(2, 5, _whitePawn));
            Assert.That(!_chessBoard.IsPieceAt(2, 4, _blackPawn));
            Assert.That(_chessBoard.CapturedBlackPieces.Contains(_blackPawn));
        }

        [Test]
        public void _11_tries_to_takes_enemy_pawn_with_En_Passant_but_has_waited_too_long()
        {
            _chessBoard.Add(_blackPawn, 2, 6);
            _chessBoard.Add(_whitePawn, 3, 4);
            _blackPawn.Move(2, 4);
            _whiteKing.Move(4, 1);
            _blackKing.Move(5, 7);
            _whitePawn.Move(2, 5);
            Assert.That(!_chessBoard.IsPieceAt(2, 5, _whitePawn));
            Assert.That(_chessBoard.IsPieceAt(2, 4, _blackPawn));
            Assert.That(!_chessBoard.CapturedBlackPieces.Contains(_blackPawn));
        }

        [Test]
        public void _12_tries_to_takes_enemy_pawn_with_En_Passant_but_enemy_pawn_didnt_double_step()
        {
            _chessBoard.Add(_blackPawn, 2, 6);
            _chessBoard.Add(_whitePawn, 3, 4);
            _blackPawn.Move(2, 5);
            _blackPawn.Move(2, 4);
            _whitePawn.Move(2, 5);
            Assert.That(!_chessBoard.IsPieceAt(2, 5, _whitePawn));
            Assert.That(_chessBoard.IsPieceAt(2, 4, _blackPawn));
            Assert.That(!_chessBoard.CapturedBlackPieces.Contains(_blackPawn));
        }

        [Test]
        public void _13_tries_to_takes_enemy_pawn_with_En_Passant_but_they_are_on_the_wrong_row()
        {
            _chessBoard.Add(_blackPawn, 3, 6);
            _chessBoard.Add(_whitePawn, 2, 2);
            _blackPawn.Move(3, 4);
            _whitePawn.Move(3, 5);
            Assert.That(_chessBoard.IsPieceAt(2, 2, _whitePawn));
            Assert.That(_chessBoard.IsPieceAt(3, 4, _blackPawn));
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(2));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(2));
            Assert.That(!_chessBoard.CapturedBlackPieces.Contains(_blackPawn));
        }

        [Test]
        public void _14_makes_it_to_the_other_side_of_the_board_and_is_promoted_to_a_queen()
        {
            _chessBoard.Add(_whitePawn, 1, 6);
            _whitePawn.Move(1, 7);
            Assert.That(_chessBoard.PieceAt(1, 7) is Queen);
        }

        [Test]
        public void _15_is_directly_in_front_of_an_enemy_king_does_not_put_the_king_in_check()
        {
            _chessBoard.Add(_whitePawn, 4, 5);
            _whitePawn.Move(4, 6);
            Assert.That(_blackKing.IsInCheck, Is.False);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(4));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(6));
            Assert.That(_chessBoard.IsPieceAt(4, 7, _blackKing));
        }

        [Test]
        public void _16_is_diagonal_from_the_enemy_king_should_put_the_enemy_king_in_check()
        {
            _chessBoard.Add(_whitePawn, 3, 5);
            _whitePawn.Move(3, 6);
            Assert.That(_blackKing.IsInCheck);
            Assert.That(_whitePawn.XCoordinate, Is.EqualTo(3));
            Assert.That(_whitePawn.YCoordinate, Is.EqualTo(6));
            Assert.That(_chessBoard.IsPieceAt(4, 7, _blackKing));
        }

    }

}
