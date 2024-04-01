namespace Chess
{
    public record Piece(PieceType PieceType, PieceColor Color)
    {
        public IEnumerable<Location> GetMoves(Location location, Board board) => 
            PieceType.GetMoves(location, board);

        public override string ToString()
        {
            var c = " " + PieceType;
            return Color == PieceColor.Black ? c.ToLower() : c;
        }
    }

	public static class PieceExtensions
	{
		public static bool Is(this Piece? piece, PieceColor color) =>
			piece != null && piece.Color == color;

		public static bool Is(this Piece? piece, PieceColor color, PieceType pieceType) =>
			Is(piece, color) && piece?.PieceType == pieceType;

	}
}