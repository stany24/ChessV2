using System.Drawing;
using System.Windows.Forms;

namespace ChessV2
{
    public partial class Form1 : Form
    {
        private const int SquareSize = 50;
        private const int StartPosY = SquareSize * 7 + offset;
        private const int offset = 30;
        public Form1()
        {
            InitializeComponent();
            CreateBoard();
            PlaceDefaultPieces();
        }

        private void CreateBoard()
        {
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Board.squares[i,j] = new Square(i, j);
                    Square square = new Square(i, j)
                    {
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Size = new Size(SquareSize, SquareSize),
                        Location = new Point(j*SquareSize+offset,StartPosY - i*SquareSize),
                        Text = $"{i}/{j}"
                    };
                    if ((i + j) % 2 == 0) { square.BackColor = Color.White; }
                    else {square.BackColor = Color.Gray; }
                    Controls.Add(square);
                    Board.squares[i,j] = square;
                }
            }
        }

        private void PlaceDefaultPieces()
        {
            //Pon
            for (int i = 0; i < 8; i++) {
                Board.squares[1, i].Piece = new Pon(Color.White); }
            for (int i = 0; i < 8; i++) {
                Board.squares[6, i].Piece = new Pon(Color.Black); }
            //Rock
            Board.squares[0,0].Piece = new Rock(Color.White);
            Board.squares[0,7].Piece = new Rock(Color.White);
            Board.squares[7,0].Piece = new Rock(Color.Black);
            Board.squares[7,7].Piece = new Rock(Color.Black);
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
        }
    }
}
