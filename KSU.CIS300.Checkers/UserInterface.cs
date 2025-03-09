using System;
using System.Drawing;
using System.Windows.Forms;

namespace KSU.CIS300.Checkers
{
    /// <summary>
    /// s
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// Stores the current game
        /// </summary>
        private Game _game; 
        /// <summary>
        /// v
        /// </summary>
        private Image _red;
        /// <summary>
        /// red
        /// </summary>
        private Image _redKing;
        /// <summary>
        /// b
        /// </summary>
        private Image _black;
        /// <summary>
        /// v
        /// </summary>
        private Image _blackKing;

        /// <summary>
        /// Constructor: Initializes components, loads images, and starts a new game.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();

            // Load images
            _red = Image.FromFile(@"pics\red.png");
            _redKing = Image.FromFile(@"pics\red_king.png");
            _black = Image.FromFile(@"pics\black.png");
            _blackKing = Image.FromFile(@"pics\black_king.png");

            // Start new game
            _game = new Game();
            DrawBoard();
        }

        /// <summary>
        /// Draws the checkerboard and adds labels for each square.
        /// </summary>
        private void DrawBoard()
        {
            boardPanel.Controls.Clear();
            boardPanel.Width = 60 * 8;   // Set board width dynamically
            boardPanel.Height = (60 * 8) + 30; // Set board height

            for (int row = 1; row <= 8; row++)
            {
                LinkedListCell<BoardSquare> boardRow = _game.GetRow(row);
                while (boardRow != null)
                {
                    if (boardRow.Value == null)
                    {
                        boardRow = boardRow.Next;
                        continue;
                    }

                    Label squareLabel = new Label
                    {
                        Width = 60,
                        Height = 60,
                        BackColor = (row + boardRow.Value.Column) % 2 == 0 ? Color.White : Color.Gray,
                        Margin = new Padding(0),
                        Name = $"{row},{boardRow.Value.Column}"
                    };

                    if (squareLabel.BackColor == Color.Gray)
                    {
                        squareLabel.Image = GetSquareImage(boardRow.Value);
                    }

                    squareLabel.Click += new EventHandler(BoardSquare_Click);
                    boardPanel.Controls.Add(squareLabel);

                    boardRow = boardRow.Next; // Move to next column
                }
            }
        }

        /// <summary>
        /// Redraws the board with updated piece positions.
        /// </summary>
        private void RedrawBoard()
        {
            foreach (Control control in boardPanel.Controls)
            {
                if (control is Label squareLabel)
                {
                    string[] pos = squareLabel.Name.Split(',');
                    int row = int.Parse(pos[0]);
                    int col = int.Parse(pos[1]);

                    BoardSquare square = _game.SelectSquare(row, col);

                    if (square == null) continue;  // Prevents null errors

                    squareLabel.Image = GetSquareImage(square);

                    if (square.Selected)
                        squareLabel.BackColor = Color.Aqua;
                    else
                        squareLabel.BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray;
                }
            }

            uxToolStripStatusLabel_Turn.Text = _game.Turn == SquareColor.Black ? "Black's Turn" : "Red's Turn";
        }

        /// <summary>
        /// sh
        /// </summary>
        /// <param name="square">su</param>
        /// <returns>re</returns>
        private Image GetSquareImage(BoardSquare square)
        {
            if (square.Color == SquareColor.Black)
                return square.King ? _blackKing : _black;

            if (square.Color == SquareColor.Red)
                return square.King ? _redKing : _red;

            return null; // No piece
        }
        /// <summary>
        /// ab
        /// </summary>
        /// <param name="sender">s</param>
        /// <param name="e">e</param>
        private void BoardSquare_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            if (clickedLabel == null) return;

            string[] position = clickedLabel.Name.Split(',');
            int row = int.Parse(position[0]);
            int col = int.Parse(position[1]);

            BoardSquare target = _game.SelectSquare(row, col);

            if (target == null) return;  // Prevents null errors

            // **If clicking own piece, update selection instead of moving**
            if (target.Color == _game.Turn)
            {
                _game.SelectedPiece = target;
                RedrawBoard();  // Update UI to show selection
                return;
            }

            // **If trying to move a selected piece**
            if (_game.MakeMove(row, col))
            {
                RedrawBoard(); // Update board with new piece positions

                // Check if the game has been won
                if (_game.RedCount == 0)
                {
                    MessageBox.Show("Black wins!", "Game Over", MessageBoxButtons.OK);
                }
                else if (_game.BlackCount == 0)
                {
                    MessageBox.Show("Red wins!", "Game Over", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Invalid move!", "Warning", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// new
        /// </summary>
        /// <param name="sender">para</param>
        /// <param name="e">w</param>
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _game = new Game();
            DrawBoard();
        }
        /// <summary>
        /// name
        /// </summary>
        /// <param name="sender">pa</param>
        /// <param name="e">e</param>
        private void NewGame_Click(object sender, EventArgs e)
        {
            _game = new Game(); 
            _game.SelectedPiece = null; 
            DrawBoard(); //
            uxToolStripStatusLabel_Turn.Text = "Black's Turn"; // Reset turn indicator
        }
    }
}
