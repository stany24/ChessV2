using System.Collections.Generic;
using System.Drawing;

namespace ChessV2
{
    public class Piece
    {
        public Image Image { get; set; }
        public Color Color { get; set; }

        public virtual List<Move> GetAllMoves(int row, int column) { return new List<Move>(); }
        public List<Move> GetPossibleMoves(List<Move> allmoves)
        {
            List<Move> PossibleMoves = new List<Move>();
            foreach (var move in allmoves)
            {
                if (Board.squares[move.End.Row][move.End.Column].Piece == null) { PossibleMoves.Add(move); continue; }
                if (Board.squares[move.End.Row][move.End.Column].Piece.Color != Color) { PossibleMoves.Add(move); }
            }
            return PossibleMoves;
        }
        public List<Move> GetLegalMoves() { return new List<Move>(); }
    }

    public class King : Piece
    {
        public override List<Move> GetAllMoves(int row, int column)
        {
            List<Move> AllMoves = new List<Move>
            {
                new Move(new Square(row, column), new Square(row + 1, column + 1)),
                new Move(new Square(row, column), new Square(row + 1, column - 1)),
                new Move(new Square(row, column), new Square(row + 1, column)),
                new Move(new Square(row, column), new Square(row - 1, column + 1)),
                new Move(new Square(row, column), new Square(row - 1, column - 1)),
                new Move(new Square(row, column), new Square(row - 1, column)),
                new Move(new Square(row, column), new Square(row, column + 1)),
                new Move(new Square(row, column), new Square(row, column - 1))
            };
            return AllMoves;
        }
    }

    public class Queen : Piece
    {

    }

    public class Rock : Piece
    {
        public override List<Move> GetAllMoves(int row, int column)
        {
            List<Move> AllMoves = new List<Move>();
            for(int i = row; i <= 7; i++){AllMoves.Add(new Move(new Square(row, column), new Square(i, column)));}
            for (int i = row; i >= 0; i--){AllMoves.Add(new Move(new Square(row, column), new Square(i, column)));}
            for (int i = column; i <= 7; i++){AllMoves.Add(new Move(new Square(row, column), new Square(row, i)));}
            for (int i = column; i >= 0; i--){AllMoves.Add(new Move(new Square(row, column), new Square(row, i)));}
            return AllMoves;
        }
    }

    public class Knight : Piece
    {
        public override List<Move> GetAllMoves(int row, int column)
        {
            List<Move> AllMoves = new List<Move>
            {
                new Move(new Square(row, column), new Square(row+2, column-1)),
                new Move(new Square(row, column), new Square(row+2, column+1)),
                new Move(new Square(row, column), new Square(row-2, column-1)),
                new Move(new Square(row, column), new Square(row-2, column+1)),
                new Move(new Square(row, column), new Square(row-1, column+2)),
                new Move(new Square(row, column), new Square(row+1, column+2)),
                new Move(new Square(row, column), new Square(row-1, column-2)),
                new Move(new Square(row, column), new Square(row+1, column-2))
            };
            return AllMoves;
        }
    }

    public class Bishop : Piece
    {

    }

    public class Pon : Piece
    {

    }
}
