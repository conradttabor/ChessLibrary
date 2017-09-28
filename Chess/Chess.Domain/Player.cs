using Chess.Domain.Interfaces;

namespace Chess.Domain
{
    public class Player
    {
        public PlayerMove MakeMove(IUserInput input)
        {
            return new PlayerMove()
            {
                PieceSelectionXCoordinate = input.GetUsersPieceSelectionXCoordinate(),
                PieceSelectionYCoordinate = input.GetUsersPieceSelectionYCoordinate(),
                PieceDestinationXCoordinate = input.GetUsersMovementSelectionXCoordinate(),
                PieceDestinationYCoordinate = input.GetUsersMovementSelectionYCoordinate()
            };
        }
    }
}
