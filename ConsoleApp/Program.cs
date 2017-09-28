using Chess.Domain;
using Chess.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            var game = new Game();
            game.SetUpChessBoard();
            var whiteInput = new UserInput();
            var blackInput = new UserInput();
            var userOutput = new UserResponse();
            
            game.BeginGame(whiteInput, blackInput, userOutput);

            

            Console.ReadLine();
        }

        
    }

    public class UserInput : IUserInput
    {
        public int GetUsersMovementSelectionXCoordinate()
        {
            Console.WriteLine("Please enter destination X Coordinate:");
            return GetAndValidateInput();
        }

        public int GetUsersMovementSelectionYCoordinate()
        {
            Console.WriteLine("Please enter destination Y Coordinate:");
            return GetAndValidateInput();
        }

        public int GetUsersPieceSelectionXCoordinate()
        {
            Console.WriteLine("Please enter desired piece's X Coordinate");
            return GetAndValidateInput();
        }

        public int GetUsersPieceSelectionYCoordinate()
        {
            Console.WriteLine("Please enter desired piece's Y Coordinate");
            return GetAndValidateInput();
        }

        private int GetAndValidateInput()
        {
            var input = Console.ReadLine();

            int inputInt;
            var isInt = int.TryParse(input, out inputInt);

            while (!isInt)
            {
                Console.WriteLine("Invalid input, please inter an integer:");
                input = Console.ReadLine();
                isInt = int.TryParse(input, out inputInt);
            }

            return inputInt;
        }
    }

    public class UserResponse : IUserResponse
    {
        public void ReturnMoveInformation(TurnResult turnResult, ChessBoard chessBoard)
        {
            DisplayBoard(chessBoard);
            if (turnResult.Flags.Contains(Flag.GameStart))
                return;
            if (turnResult.Flags.Contains(Flag.CheckMate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"CheckMate! {turnResult.PieceMoved.PieceColor} wins!");
            }
                
            Console.WriteLine($"[{turnResult.OldXCoordinate},{turnResult.OldYCoordinate}] -> [{turnResult.NewXCoordinate},{turnResult.NewYCoordinate}]");
            Console.WriteLine($"Successful Move: {turnResult.TurnCompleted}");
            if (!turnResult.TurnCompleted)
                Console.WriteLine(turnResult.ReasonForIncompleteTurn);         
        }

        public void DisplayBoard(ChessBoard chessBoard)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            for (int y = 7; y >= 0; y--)
            {
                Console.Write(y);
                for (int x = 0; x < 8; x++)
                {                  
                    if (chessBoard.IsPieceAt(x, y))
                    {
                        var piece = chessBoard.PieceAt(x, y);
                        if (piece.PieceColor == PieceColor.Black)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            if (piece is Pawn)
                                Console.Write("[O]");
                            else if (piece is Rook)
                                Console.Write("[|]");
                            else if (piece is Bishop)
                                Console.Write("[^]");
                            else if (piece is Knight)
                                Console.Write("[?]");
                            else if (piece is Queen)
                                Console.Write("[*]");
                            else if (piece is King)
                                Console.Write("[#]");

                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            if (piece is Pawn)
                                Console.Write("[O]");
                            else if (piece is Rook)
                                Console.Write("[|]");
                            else if (piece is Bishop)
                                Console.Write("[^]");
                            else if (piece is Knight)
                                Console.Write("[?]");
                            else if (piece is Queen)
                                Console.Write("[*]");
                            else if (piece is King)
                                Console.Write("[#]");
                        }
                        
                    }
                    else
                        Console.Write("[ ]");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  0  1  2  3  4  5  6  7 ");
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void BadPieceSelection(string reasonForBadSelection)
        {
            Console.WriteLine(reasonForBadSelection);
        }

        public void InformPlayerOfTheirTurn(PieceColor color)
        {
            Console.WriteLine($"{color}'s turn");
        }
    }
}
