using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessV2
{
    public partial class Form1 : Form
    {
        private const int SquareSize = 50;
        private const int StartPosY = SquareSize * 7 + offset;
        private const int offset = 30;
        private Color ColorToMove = Color.Black;
        private Square StartSquare = null;
        public Form1()
        {
            InitializeComponent();
            CreateBoard();
            PlaceDefaultPieces();
            //MovePlayer.PlayMove(new Move(new Square(1,0),new Square(3,0)));
            //MovePlayer.PlayMove(new Move(new Square(0,0),new Square(2,0)));
            //MovePlayer.PlayMove(new Move(new Square(2,0),new Square(2,5)));
        }

        private void CreateBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Square square = new Square(i, j)
                    {
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Size = new Size(SquareSize, SquareSize),
                        Location = new Point(j * SquareSize + offset, StartPosY - i * SquareSize),
                        Text = $"{i}/{j}"
                    };
                    square.Click += new EventHandler(OnClick);
                    if ((i + j) % 2 != 0) { square.BackColor = Color.White; }
                    else { square.BackColor = Color.Gray; }
                    Controls.Add(square);
                    Board.squares[i, j] = square;
                }
            }
        }

        public void ResetColor()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 != 0) { Board.squares[i, j].BackColor = Color.White; }
                    else { Board.squares[i, j].BackColor = Color.Gray; }
                }
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            Square ClickedSquare = sender as Square;
            ResetColor();
            //No piece was selected before
            if (StartSquare == null)
            {
                if (ClickedSquare.Piece == null) { return; }
                if (ClickedSquare.Piece.Color != ColorToMove) { return; }
                List<Move> movesToShow = ClickedSquare.Piece.GetLegalMoves(ClickedSquare.Row, ClickedSquare.Column);
                for (int i = 0;i<movesToShow.Count;i++ )
                {
                    Board.squares[movesToShow[i].End.Row, movesToShow[i].End.Column].BackColor = Color.Yellow;
                }
                return;
            }
            //Change the selected piece
            if(ClickedSquare.Piece.Color == ColorToMove)
            {
                StartSquare = null;
                OnClick(sender, e);
                return;
            }
            //Play the move if legal
            List<Move> PossibleMoves = StartSquare.Piece.GetLegalMoves(StartSquare.Row,StartSquare.Column);
            Move WantedMove = new Move(StartSquare, ClickedSquare);
            for(int i = 0; i < PossibleMoves.Count; i++)
            {
                if(WantedMove == PossibleMoves[i]) { MovePlayer.PlayMove(WantedMove);StartSquare = null;return; }
            }

        }

        private void PlaceDefaultPieces()
        {
            //Pon
            for (int i = 0; i < 8; i++){Board.squares[1, i].Piece = new Pon(Color.White);}
            for (int i = 0; i < 8; i++){Board.squares[6, i].Piece = new Pon(Color.Black);}
            //Rock
            Board.squares[0, 0].Piece = new Rock(Color.White);
            Board.squares[0, 7].Piece = new Rock(Color.White);
            Board.squares[7, 0].Piece = new Rock(Color.Black);
            Board.squares[7, 7].Piece = new Rock(Color.Black);
            //Knigth
            Board.squares[0, 1].Piece = new Knight(Color.White);
            Board.squares[0, 6].Piece = new Knight(Color.White);
            Board.squares[7, 1].Piece = new Knight(Color.Black);
            Board.squares[7, 6].Piece = new Knight(Color.Black);
            //Bishop
            Board.squares[0, 2].Piece = new Bishop(Color.White);
            Board.squares[0, 5].Piece = new Bishop(Color.White);
            Board.squares[7, 2].Piece = new Bishop(Color.Black);
            Board.squares[7, 5].Piece = new Bishop(Color.Black);
            //Bishop
            Board.squares[0, 3].Piece = new Queen(Color.White);
            Board.squares[0, 4].Piece = new King(Color.White);
            Board.squares[7, 3].Piece = new Queen(Color.Black);
            Board.squares[7, 4].Piece = new King(Color.Black);
        }
    }
}
