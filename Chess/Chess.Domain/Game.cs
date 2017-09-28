using Chess.Domain.Interfaces;

namespace Chess.Domain
{
    public class Game
    {
        public ChessBoard ChessBoard { get; set; }
        public Player WhitePlayer { get; set; }
        public Player BlackPlayer { get; set; }
        public int TurnCounter { get; set; }

        public Game()
        {
            ChessBoard = new ChessBoard();
            WhitePlayer = new Player();
            BlackPlayer = new Player();
        }

        public void SetUpChessBoard()
        {
            ChessBoard.Add(new BlackPawn(ChessBoard), 0, 6);
            ChessBoard.Add(new BlackPawn(ChessBoard), 1, 6);
            ChessBoard.Add(new BlackPawn(ChessBoard), 2, 6);
            ChessBoard.Add(new BlackPawn(ChessBoard), 3, 6);
            ChessBoard.Add(new BlackPawn(ChessBoard), 4, 6);
            ChessBoard.Add(new BlackPawn(ChessBoard), 5, 6);
            ChessBoard.Add(new BlackPawn(ChessBoard), 6, 6);
            ChessBoard.Add(new BlackPawn(ChessBoard), 7, 6);
            ChessBoard.Add(new Rook(PieceColor.Black, ChessBoard), 0, 7);
            ChessBoard.Add(new Knight(PieceColor.Black, ChessBoard), 1, 7);
            ChessBoard.Add(new Bishop(PieceColor.Black, ChessBoard), 2, 7);
            ChessBoard.Add(new Queen(PieceColor.Black, ChessBoard), 3, 7);
            ChessBoard.Add(new BlackKing(ChessBoard), 4, 7);
            ChessBoard.Add(new Bishop(PieceColor.Black, ChessBoard), 5, 7);
            ChessBoard.Add(new Knight(PieceColor.Black, ChessBoard), 6, 7);
            ChessBoard.Add(new Rook(PieceColor.Black, ChessBoard), 7, 7);
                                                                  
            ChessBoard.Add(new WhitePawn(ChessBoard), 0, 1);
            ChessBoard.Add(new WhitePawn(ChessBoard), 1, 1);
            ChessBoard.Add(new WhitePawn(ChessBoard), 2, 1);
            ChessBoard.Add(new WhitePawn(ChessBoard), 3, 1);
            ChessBoard.Add(new WhitePawn(ChessBoard), 4, 1);
            ChessBoard.Add(new WhitePawn(ChessBoard), 5, 1);
            ChessBoard.Add(new WhitePawn(ChessBoard), 6, 1);
            ChessBoard.Add(new WhitePawn(ChessBoard), 7, 1);
            ChessBoard.Add(new Rook(PieceColor.White, ChessBoard), 0, 0);
            ChessBoard.Add(new Knight(PieceColor.White, ChessBoard), 1, 0);
            ChessBoard.Add(new Bishop(PieceColor.White, ChessBoard), 2, 0);
            ChessBoard.Add(new Queen(PieceColor.White, ChessBoard), 3, 0);
            ChessBoard.Add(new WhiteKing(ChessBoard), 4, 0);
            ChessBoard.Add(new Bishop(PieceColor.White, ChessBoard), 5, 0);
            ChessBoard.Add(new Knight(PieceColor.White, ChessBoard), 6, 0);
            ChessBoard.Add(new Rook(PieceColor.White, ChessBoard), 7, 0);
            
        }

        public void BeginGame(IUserInput whiteInput, IUserInput blackInput, IUserResponse userResponse)
        {
            SetUpChessBoard();
            var isGameComplete = false;

            var beginingTurn = new TurnResult();
            beginingTurn.Flags.Add(Flag.GameStart);
            userResponse.ReturnMoveInformation(beginingTurn, ChessBoard);

            while (!isGameComplete)
            {

                isGameComplete = WhiteTurn(whiteInput, userResponse);
                TurnCounter++;
                if (isGameComplete)
                    break;

                isGameComplete = BlackTurn(blackInput, userResponse);
                TurnCounter++;
                if (isGameComplete)
                    break;

                if (TurnCounter == 50 
                    && ChessBoard.CapturedBlackPieces.Count == 0 
                    && ChessBoard.CapturedWhitePieces.Count == 0)
                {
                    isGameComplete = true;
                    var result = new TurnResult();
                    result.Flags.Add(Flag.GameDraw);
                    userResponse.ReturnMoveInformation(result, ChessBoard);
                    break;
                }                   
            }
        }

        private bool WhiteTurn(IUserInput whiteInput, IUserResponse userResponse)
        {
            var isGameOver = false;

            userResponse.InformPlayerOfTheirTurn(PieceColor.White);

            var whitesDesiredMove = GetMoveFromPlayer(whiteInput, userResponse, WhitePlayer, PieceColor.White);

            var turnResult = TakeTurn(whitesDesiredMove, whiteInput, userResponse, WhitePlayer, PieceColor.White);

            if (turnResult.Flags.Contains(Flag.CheckMate))
                isGameOver = true;

            return isGameOver;
        }

        private bool BlackTurn(IUserInput blackInput, IUserResponse userResponse)
        {
            var isGameOver = false;

            userResponse.InformPlayerOfTheirTurn(PieceColor.Black);

            var blacksDesiredMove = GetMoveFromPlayer(blackInput, userResponse, BlackPlayer, PieceColor.Black);

            var turnResult = TakeTurn(blacksDesiredMove, blackInput, userResponse, BlackPlayer, PieceColor.Black);

            if (turnResult.Flags.Contains(Flag.CheckMate))
                isGameOver = true;

            return isGameOver;
        }

        private PlayerMove GetMoveFromPlayer(IUserInput input, IUserResponse userResponse, Player player, PieceColor currentColor)
        {
            bool isValidSelection = false;
            var move = player.MakeMove(input);
            while (!isValidSelection)
            {
                if (ChessBoard.IsLegalBoardPosition(move.PieceSelectionXCoordinate, move.PieceSelectionYCoordinate)
                && ChessBoard.IsPieceAt(move.PieceSelectionXCoordinate, move.PieceSelectionYCoordinate))
                {
                    if (ChessBoard.PieceAt(move.PieceSelectionXCoordinate, move.PieceSelectionYCoordinate).PieceColor == currentColor)
                        isValidSelection = true;
                    else
                    {
                        userResponse.BadPieceSelection("You must select your own piece to move.");
                        move = WhitePlayer.MakeMove(input);
                    }
                }
                else
                {
                    userResponse.BadPieceSelection("You must select a piece at a valid location.");
                    move = WhitePlayer.MakeMove(input);
                }

            }

            return move;
        }

        private TurnResult TakeTurn(PlayerMove playerMove, IUserInput input, IUserResponse userResponse, Player player, PieceColor pieceColor)
        {
            var turnResult = ChessBoard
                    .PieceAt(playerMove.PieceSelectionXCoordinate, playerMove.PieceSelectionYCoordinate)
                    .Move(playerMove.PieceDestinationXCoordinate, playerMove.PieceDestinationYCoordinate);

            userResponse.ReturnMoveInformation(turnResult, ChessBoard);

            while (!turnResult.TurnCompleted)
            {
                playerMove = GetMoveFromPlayer(input, userResponse, player, pieceColor);
                turnResult = ChessBoard
                    .PieceAt(playerMove.PieceSelectionXCoordinate, playerMove.PieceSelectionYCoordinate)
                    .Move(playerMove.PieceDestinationXCoordinate, playerMove.PieceDestinationYCoordinate);
                userResponse.ReturnMoveInformation(turnResult, ChessBoard);
            }

            return turnResult;
        }
    }
}
