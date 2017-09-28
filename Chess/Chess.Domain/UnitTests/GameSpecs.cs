using NUnit.Framework;

namespace Chess.Domain.UnitTests
{
    [TestFixture]
    public class When_playing_the_game
    {
        private Game _game;

        [SetUp]
        public void SetUp()
        {
            _game = new Game();
            _game.SetUpChessBoard();
        }

        [Test]
        public void _01_and_checking_if_pieces_are_on_the_board()
        {
            Assert.That(_game.ChessBoard.IsPieceAt(0, 0));
            Assert.That(_game.ChessBoard.IsPieceAt(0 , 7));
            Assert.That(_game.ChessBoard.IsPieceAt(7, 0));
            Assert.That(_game.ChessBoard.IsPieceAt(7, 7));
        }
    }
}
