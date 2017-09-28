using NUnit.Framework;

namespace Chess.Domain.UnitTests
{
    [TestFixture]
    public class When_creating_a_chess_board
    {
        private ChessBoard _chessBoard;

        [SetUp]
        public void SetUp()
        {
            _chessBoard = new ChessBoard();
        }

        [Test]
        public void _01_the_playing_board_should_have_a_Max_Board_Width_of_8()
        {
            Assert.That(ChessBoard.MaxBoardWidth, Is.EqualTo(8));
        }

        [Test]
        public void _02_the_playing_board_should_have_a_Max_Board_Height_of_8()
        {
            Assert.That(ChessBoard.MaxBoardHeight, Is.EqualTo(8));
        }

        [Test]
        public void _03_the_playing_board_should_know_that_X_equals_0_and_Y_equals_0_is_a_valid_board_position()
        {
            var isValidPosition = _chessBoard.IsLegalBoardPosition(0, 0);
            Assert.That(isValidPosition, Is.True);
        }

        [Test]
        public void _04_the_playing_board_should_know_that_X_equals_5_and_Y_equals_5_is_a_valid_board_position()
        {
            var isValidPosition = _chessBoard.IsLegalBoardPosition(5, 5);
            Assert.That(isValidPosition, Is.True);
        }

        [Test]
        public void _05_the_playing_board_should_know_that_X_equals_11_and_Y_equals_5_is_an_invalid_board_position()
        {
            var isValidPosition = _chessBoard.IsLegalBoardPosition(11, 5);
            Assert.That(isValidPosition, Is.False);
        }

        [Test]
        public void _06_the_playing_board_should_know_that_X_equals_0_and_Y_equals_8_is_an_invalid_board_position()
        {
            var isValidPosition = _chessBoard.IsLegalBoardPosition(0, 8);
            Assert.That(isValidPosition, Is.False);
        }

        [Test]
        public void _07_the_playing_board_should_know_that_X_equals_11_and_Y_equals_0_is_an_invalid_board_position()
        {
            var isValidPosition = _chessBoard.IsLegalBoardPosition(11, 0);
            Assert.That(isValidPosition, Is.False);
        }

        [Test]
        public void _08_the_playing_board_should_know_that_X_equals_minus_1_and_Y_equals_5_is_an_invalid_board_position()
        {
            var isValidPosition = _chessBoard.IsLegalBoardPosition(-1, 5);
            Assert.That(isValidPosition, Is.False);
        }

        [Test]
        public void _09_the_playing_board_should_know_that_X_equals_5_and_Y_equals_minus_1_is_an_invalid_board_position()
        {
            var isValidPosition = _chessBoard.IsLegalBoardPosition(5, -1);
            Assert.That(isValidPosition, Is.False);
        }
    }
}
