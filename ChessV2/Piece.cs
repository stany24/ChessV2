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
            for(int i = row+1; i <= 7; i++){AllMoves.Add(new Move(new Square(row, column), new Square(i, column)));}
            for (int i = row-1; i >= 0; i--){AllMoves.Add(new Move(new Square(row, column), new Square(i, column)));}
            for (int i = column+1; i <= 7; i++){AllMoves.Add(new Move(new Square(row, column), new Square(row, i)));}
            for (int i = column-1; i >= 0; i--){AllMoves.Add(new Move(new Square(row, column), new Square(row, i)));}
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
        public override List<Move> GetAllMoves(int row, int column)
        {
            List<Move> AllMoves = new List<Move>();
            for(int i = 1; row+i <=7 && column+i <= 7; i++) { AllMoves.Add(new Move(new Square(row, column), new Square(row+i, column+i))); }
            for(int i = 1; row+i <=7 && column - i >= 0; i++) { AllMoves.Add(new Move(new Square(row, column), new Square(row+i, column-i))); }
            for(int i = 1; row - i >= 0 && column+i <= 7; i++) { AllMoves.Add(new Move(new Square(row, column), new Square(row-i, column+i))); }
            for(int i = 1; row-i >=0 && column-i >= 0; i++) { AllMoves.Add(new Move(new Square(row, column), new Square(row-i, column-i))); }

            return AllMoves;
        }
    }

    public class Pon : Piece
    {
        public override List<Move> GetAllMoves(int row, int column)
        {
            List<Move> AllMoves = new List<Move>();
            if (Color == Color.White)
            {
                AllMoves.Add(new Move(new Square(row,column),new Square(row+1,column)));
                if(column-1 >= 0) { AllMoves.Add(new Move(new Square(row, column), new Square(row + 1, column - 1))); }
                if(column+1 <= 7) { AllMoves.Add(new Move(new Square(row, column), new Square(row + 1, column + 1))); }
            }
            else
            {
                AllMoves.Add(new Move(new Square(row, column), new Square(row - 1, column)));
                if (column - 1 >= 0) { AllMoves.Add(new Move(new Square(row, column), new Square(row - 1, column - 1))); }
                if (column + 1 <= 7) { AllMoves.Add(new Move(new Square(row, column), new Square(row - 1, column + 1))); }
            }
            return AllMoves;
        }
    }
}
