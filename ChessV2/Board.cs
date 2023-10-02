using System.Windows.Forms;

namespace ChessV2
{
    public static class Board
    {
        public static Square[,] squares = new Square[8, 8];
    }

    public class Square : Button
    {
        private Piece piece;
        public Piece Piece
        {
            get { return piece; }
            set
            {
                piece = value;
                if(value == null){
                    BackgroundImage = null;
                    return;}
                BackgroundImage = piece.GetImage();
            }
        }
        public int Row { get; set; }
        public int Column { get; set; }

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }

    public class Move
    {
        public Square Start { get; set; }
        public Square End { get; set; }

        public Move(Square start, Square end)
        {
            Start = start;
            End = end;
        }
    }

    public static class MovePlayer
    {
        public static void PlayMove(Move move)
        {
            Piece MovingPiece = Board.squares[move.Start.Row, move.Start.Column].Piece;
            Board.squares[move.Start.Row, move.Start.Column].Piece = null;
            Board.squares[move.End.Row, move.End.Column].Piece = MovingPiece;
        }

        public static void PlayMove(Square Start, Square End)
        {
            Piece MovingPiece = Board.squares[Start.Row, Start.Column].Piece;
            Board.squares[Start.Row, Start.Column].Piece = null;
            Board.squares[End.Row, End.Column].Piece = MovingPiece;
        }

        public static void PlayMove(int startRow,int startColumn,int endRow,int endColumn)
        {
            Piece MovingPiece = Board.squares[startRow, startColumn].Piece;
            Board.squares[startRow, startColumn].Piece = null;
            Board.squares[endRow, endColumn].Piece = MovingPiece;
        }
    }
}
