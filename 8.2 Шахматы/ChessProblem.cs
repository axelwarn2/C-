using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public class ChessProblem
	{
		private readonly Board board;

		public ChessProblem(Board board)
		{
			this.board = board;
		}

		// Определяет мат, шах или пат белым.
		public ChessStatus CalculateChessStatus()
		{
			var isCheck = IsCheckForWhite();
			var hasMoves = false;
			foreach (var locFrom in board.GetPieces(PieceColor.White))
			{
				foreach (var locTo in board.GetPiece(locFrom)!.GetMoves(locFrom, board))
				{
					var old = board.GetPiece(locTo);
					board.Set(locTo, board.GetPiece(locFrom));
					board.Set(locFrom, null);
					if (!IsCheckForWhite())
						hasMoves = true;
					board.Set(locFrom, board.GetPiece(locTo));
					board.Set(locTo, old);
				}
			}
			if (isCheck)
				return hasMoves ? ChessStatus.Check : ChessStatus.Mate;
			else
				return hasMoves ? ChessStatus.Ok : ChessStatus.Stalemate;
		}

		// Шах ли для белых? (check — это шах)
		private bool IsCheckForWhite()
		{
			var isCheck = false;
			foreach (var loc in board.GetPieces(PieceColor.Black))
			{
				var piece = board.GetPiece(loc)!;
				var moves = piece.GetMoves(loc, board);
				foreach (var destination in moves)
				{
					if (board.GetPiece(destination).Is(PieceColor.White, PieceType.King))
						isCheck = true;
				}
			}
			if (isCheck) return true;
			return false;
		}
	}

	public static class ChessExtensions
	{


	}
}