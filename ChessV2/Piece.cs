using System.Collections.Generic;
using System.Drawing;

namespace ChessV2
{
    public class Piece
    {
        public Image Image { get; set; }
        public Color Color { get; set; }

        public virtual List<Move> GetAllMoves(int row, int column) { return new List<Move>(); }
        public virtual List<Move> GetPossibleMoves(List<Move> allmoves) { return new List<Move>(); }
        public virtual List<Move> GetLegalMoves() { return new List<Move>(); }
    }

    public class King : Piece
    {
        public override List<Move> GetAllMoves(int row,int column)
        {
            List<Move> AllMoves = new List<Move>();
            AllMoves.Add(new Move(new Square(row,column),new Square(row+1,column+1)));
            AllMoves.Add(new Move(new Square(row,column),new Square(row+1,column-1)));
            AllMoves.Add(new Move(new Square(row,column),new Square(row+1,column)));
            AllMoves.Add(new Move(new Square(row,column),new Square(row-1,column+1)));
            AllMoves.Add(new Move(new Square(row,column),new Square(row-1,column-1)));
            AllMoves.Add(new Move(new Square(row,column),new Square(row-1,column)));
            AllMoves.Add(new Move(new Square(row,column),new Square(row,column+1)));
            AllMoves.Add(new Move(new Square(row,column),new Square(row,column-1)));
            return AllMoves;
        }

        public override List<Move> GetPossibleMoves(List<Move> allmoves)
        {
            List<Move> PossibleMoves = new List<Move>();
            foreach (var move in allmoves)
            {
                if (Board.squares[move.End.Row][move.End.Column].Piece == null) { PossibleMoves.Add(move);continue; }
                if (Board.squares[move.End.Row][move.End.Column].Piece.Color != Color) { PossibleMoves.Add(move);}
            }
            return PossibleMoves;
        }
    }

    public class Queen : Piece
    {

    }

    public class Rock : Piece
    {

    }

    public class Knight : Piece
    {

    }

    public class  Bishop : Piece
    {
        
    }

    public class Pon : Piece
    {

    }
}
