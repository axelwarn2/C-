namespace Chess
{
	public class PieceType
	{
		public static readonly PieceType Rook = new('R', true, (1, 0), (0, 1));
		public static readonly PieceType King = new('K', false, (1, 1), (1, 0), (0, 1));
		public static readonly PieceType Queen = new('Q', true, (1, 1), (1, 0), (0, 1));
		public static readonly PieceType Bishop = new('B', true, (1, 1));
		public static readonly PieceType Knight = new('N', false, (2, 1), (1, 2));

		private readonly (int X, int Y)[] directions;
		private readonly bool longMoves;
		private readonly char symbol;

		private PieceType(char symbol, bool longMoves, params (int X, int Y)[] directions)
		{
			this.longMoves = longMoves;
			this.symbol = symbol;
			this.directions = directions
				.Union(directions.Select(d => (-d.X, d.Y)))
				.Union(directions.Select(d => (d.X, -d.Y)))
				.Union(directions.Select(d => (-d.X, -d.Y)))
				.ToArray();
		}

		public override string ToString() => symbol.ToString();

		public IEnumerable<Location> GetMoves(Location location, Board board) =>
			directions.SelectMany(d => MovesInOneDirection(location, board, d, longMoves));

		private static IEnumerable<Location> MovesInOneDirection(Location from, Board board, (int X, int Y) dir,
			bool longMoves)
		{
			var piece = board.GetPiece(from)!;
			var distance = longMoves ? int.MaxValue : 2;
			for (var i = 1; i < distance; i++)
			{
				var to = new Location(from.X + dir.X * i, from.Y + dir.Y * i);
				if (!board.Contains(to)) break;
				var destinationPiece = board.GetPiece(to);
				if (destinationPiece == null)
					yield return to;
				else
				{
					if (destinationPiece.Color != piece.Color)
						yield return to;
					yield break;
				}
			}
		}
	}
}