namespace Chess
{
    public record TemporaryPieceMove(Board Board, Location From, Location To, Piece? OldDestinationPiece) : IDisposable
    {
        public void Undo()
        {
            Board.Set(From, Board.GetPiece(To));
            Board.Set(To, OldDestinationPiece);
        }

        public void Dispose() => Undo();
    }
}