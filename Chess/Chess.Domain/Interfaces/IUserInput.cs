using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain.Interfaces
{
    public interface IUserInput
    {
        int GetUsersPieceSelectionXCoordinate();
        int GetUsersPieceSelectionYCoordinate();
        int GetUsersMovementSelectionXCoordinate();
        int GetUsersMovementSelectionYCoordinate();
    }
}
