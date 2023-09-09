using System.Windows.Forms;

namespace ChessV2
{
    public static class Board
    {
        public static Square[,] squares = new Square[8,8];
    }

    public class Square :Button
    {
        public Piece Piece { get; set; }
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
}
