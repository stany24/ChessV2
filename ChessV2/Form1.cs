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
                        BackColor = Color.White,
                        Size = new Size(SquareSize, SquareSize),
                        Location = new Point(i*SquareSize+offset,StartPosY - j*SquareSize),
                        Text = $"{i}/{j}"
                    };
                    Controls.Add(square);
                    Board.squares[i,j] = square;
                }
            }
        }

        private void PlaceDefaultPieces()
        {

        }
    }
}
