using System.Collections.Generic;
using System.Drawing;

namespace ChessV2
{
    public abstract class Piece
    {
        public Color Color { get; set; }

        public virtual Image GetImage() { return null; }

        public virtual List<Move> GetAllMoves(int row, int column) { return new List<Move>(); }
        public List<Move> GetPossibleMoves(List<Move> allmoves)
        {
            for (int i = 0; i < allmoves.Count; i++)
            {
                if (allmoves[i].End.Column < 0 || allmoves[i].End.Row < 0 || allmoves[i].End.Column > 7 || allmoves[i].End.Row > 7) { allmoves.Remove(allmoves[i]); i--; }
            }

            List<Move> PossibleMoves = new List<Move>();
            foreach (var move in allmoves)
            {
                if (Board.squares[move.End.Row, move.End.Column].Piece == null) { PossibleMoves.Add(move); continue; }
                if (Board.squares[move.End.Row, move.End.Column].Piece.Color != Color) { PossibleMoves.Add(move); }
            }
            
            return PossibleMoves;
        }
        public List<Move> GetLegalMoves(int row, int column) { return GetPossibleMoves(GetAllMoves(row, column)); }
    }

    public class SlidingPiece : Piece
    {
        public List<Move> GetAllRockMoves(int row, int column)
        {
            List<Move> AllMoves = new List<Move>();
            Square start = new Square(row, column);
            for (int i = row + 1; i <= 7; i++)
            {
                if (Board.squares[i, column].Piece == null) { AllMoves.Add(new Move(start, new Square(i, column)));continue; }
                if (Board.squares[i, column].Piece.Color != Color) { AllMoves.Add(new Move(start, new Square(i, column))); }
                break;
            }
            for (int i = row - 1; i >= 0; i--)
            {
                if (Board.squares[i, column].Piece == null) { AllMoves.Add(new Move(start, new Square(i, column))); continue; }
                if (Board.squares[i, column].Piece.Color != Color) { AllMoves.Add(new Move(start, new Square(i, column))); }
                break;
            }
            for (int i = column + 1; i <= 7; i++)
            {
                if (Board.squares[i, column].Piece == null) { AllMoves.Add(new Move(start, new Square(row, i))); continue; }
                if (Board.squares[i, column].Piece.Color != Color) { AllMoves.Add(new Move( start, new Square(row, i))); }
                break;
            }
            for (int i = column - 1; i >= 0; i--)
            {
                if (Board.squares[i, column].Piece == null) { AllMoves.Add(new Move(start, new Square(row, i))); continue; }
                if (Board.squares[i, column].Piece.Color != Color) { AllMoves.Add(new Move(start, new Square(row, i))); }
                break;
            }
            return AllMoves;
        }

        public List<Move> GetAllBishopMoves(int row, int column)
        {
            List<Move> AllMoves = new List<Move>();
            Square start = new Square(row, column);

            for (int i = 1; row + i <= 7 && column + i <= 7; i++)
            {
                if (Board.squares[i, column].Piece == null) { AllMoves.Add(new Move(start, new Square(row + i, column + i))); continue; }
                if (Board.squares[i, column].Piece.Color != Color) { AllMoves.Add(new Move(start, new Square(row + i, column + i))); }
                break;
            }
            for (int i = 1; row + i <= 7 && column - i >= 0; i++)
            {
                if (Board.squares[i, column].Piece == null) { AllMoves.Add(new Move(start, new Square(row + i, column - i))); continue; }
                if (Board.squares[i, column].Piece.Color != Color) { AllMoves.Add(new Move(start, new Square(row + i, column - i))); }
                break;
            }
            for (int i = 1; row - i >= 0 && column + i <= 7; i++)
            {
                if (Board.squares[i, column].Piece == null) { AllMoves.Add(new Move(start, new Square(row - i, column + i))); continue; }
                if (Board.squares[i, column].Piece.Color != Color) { AllMoves.Add(new Move(start, new Square(row - i, column + i))); }
                break;
            }
            for (int i = 1; row - i >= 0 && column - i >= 0; i++)
            {
                if (Board.squares[i, column].Piece == null) { AllMoves.Add(new Move(start, new Square(row - i, column - i))); continue; }
                if (Board.squares[i, column].Piece.Color != Color) { AllMoves.Add(new Move(start, new Square(row - i, column - i))); }
                break;
            }
            return AllMoves;
        }
    }

    public class King : Piece
    {
        public King(Color color) { Color = color; }
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

        public override Image GetImage()
        {
            if (Color == Color.White) { return Properties.Resources.WhiteKing; }
            else { return Properties.Resources.BlackKing; }
        }
    }

    public class Queen : SlidingPiece
    {
        public Queen(Color color) { Color = color; }
        public override List<Move> GetAllMoves(int row, int column)
        {
            List<Move> allMoves = GetAllBishopMoves(row, column);
            allMoves.AddRange(GetAllRockMoves(row, column));
            return allMoves;
        }

        public override Image GetImage()
        {
            if (Color == Color.White) { return Properties.Resources.WhiteQueen; }
            else { return Properties.Resources.BlackQueen; }
        }
    }

    public class Rock : SlidingPiece
    {
        public Rock(Color color) { Color = color; }
        public override List<Move> GetAllMoves(int row, int column)
        {
            return GetAllRockMoves(row, column);
        }

        public override Image GetImage()
        {
            if (Color == Color.White) { return Properties.Resources.WhiteRock; }
            else { return Properties.Resources.BlackRock; }
        }
    }

    public class Knight : Piece
    {
        public Knight(Color color) { Color = color; }
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

        public override Image GetImage()
        {
            if (Color == Color.White) { return Properties.Resources.WhiteKnight; }
            else { return Properties.Resources.BlackKnight; }
        }
    }

    public class Bishop : SlidingPiece
    {
        public Bishop(Color color) { Color = color; }
        public override List<Move> GetAllMoves(int row, int column)
        {
            return GetAllBishopMoves(row, column);
        }

        public override Image GetImage()
        {
            if (Color == Color.White) { return Properties.Resources.WhiteBishop; }
            else { return Properties.Resources.BlackBishop; }
        }
    }

    public class Pon : Piece
    {
        public Pon(Color color) { Color = color; }
        public override List<Move> GetAllMoves(int row, int column)
        {
            List<Move> AllMoves = new List<Move>();
            if (Color == Color.White)
            {
                AllMoves.Add(new Move(new Square(row, column), new Square(row + 1, column)));
                if (column - 1 >= 0) { AllMoves.Add(new Move(new Square(row, column), new Square(row + 1, column - 1))); }
                if (column + 1 <= 7) { AllMoves.Add(new Move(new Square(row, column), new Square(row + 1, column + 1))); }
            }
            else
            {
                AllMoves.Add(new Move(new Square(row, column), new Square(row - 1, column)));
                if (column - 1 >= 0) { AllMoves.Add(new Move(new Square(row, column), new Square(row - 1, column - 1))); }
                if (column + 1 <= 7) { AllMoves.Add(new Move(new Square(row, column), new Square(row - 1, column + 1))); }
            }
            return AllMoves;
        }

        public override Image GetImage()
        {
            if (Color == Color.White) { return Properties.Resources.WhitePon; }
            else { return Properties.Resources.BlackPon; }
        }
    }
}
