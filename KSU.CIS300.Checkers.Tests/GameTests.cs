using System;
using System.Collections.Generic;
using System.Text;
using KSU.CIS300.Checkers;
using NUnit.Framework;

namespace KSU.CIS300.Checkers.Tests
{
    /// <summary>
    /// Tests for the operation of the Game class
    /// </summary>
    public class GameTests
    {
        /// <summary>
        /// The game object to be used in the tests
        /// </summary>
        Game _game;


        /// <summary>
        /// Set up the game object for each test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _game = new Game();
        }

        /// <summary>
        /// Checks that the game is initialized correctly
        /// </summary>
        [Test]
        [Category("AConstructor/CreateBoard")]
        public void GameTestA1_CreateBasicGame()
        {
            Assert.That(_game.RedCount, Is.EqualTo(12));
            Assert.That(_game.BlackCount, Is.EqualTo(12));
            Assert.That(_game.Turn, Is.EqualTo(SquareColor.Black));
            Assert.That(_game.SelectedPiece, Is.Null);
        }

        /// <summary>
        /// Chcks the rows of the board were created correctly
        /// </summary>
        [Test]
        [Category("BGetRow")]
        public void GameTestB1_CheckAllRows()
        {
            for (int row = 1; row <= 8; row++)
            {
                LinkedListCell<BoardSquare> boardRow = _game.GetRow(row);
                Assert.That(boardRow, Is.Not.Null, "Row " + row + " did not exist.");

                int col = 1;
                while (boardRow != null)
                {
                    Assert.That(boardRow.Value, Is.Not.Null, "Board square " + row + ", " + col + " not initialized.");
                    Assert.That(row, Is.EqualTo(boardRow.Value.Row), "Board row in wrong order.");
                    Assert.That(col, Is.EqualTo(boardRow.Value.Column), "Board column in wrong order.");
                    if (row < 4 && col % 2 != row % 2)
                    {
                        Assert.That(boardRow.Value.Color, Is.EqualTo(SquareColor.Red));
                    }
                    else if (row > 5 && col % 2 != row % 2)
                    {
                        Assert.That(boardRow.Value.Color, Is.EqualTo(SquareColor.Black));
                    }
                    else
                    {
                        Assert.That(boardRow.Value.Color, Is.EqualTo(SquareColor.None));
                    }
                    boardRow = boardRow.Next;
                    col++;
                }
            }
        }

        #region SelectSquare

        /// <summary>
        /// Helper method to test selecting a square
        /// </summary>
        /// <param name="row">The row of the square</param>
        /// <param name="col">The column of the square</param>
        /// <param name="color">The expected color</param>
        private void SelectHelper(int row, int col, SquareColor color)
        {
            BoardSquare square = _game.SelectSquare(row, col);
            Assert.That(row, Is.EqualTo(square.Row), "Wrong row selected");
            Assert.That(col, Is.EqualTo(square.Column), "Wrong column selected");
            Assert.That(color, Is.EqualTo(square.Color), "Wrong color selected");
            if (color == SquareColor.None)
            {
                Assert.That(square.Selected, Is.False, "Empty square selected property should be false");
            }
            else
            {
                if (color == _game.Turn)
                {
                    Assert.That(square.Selected, Is.True, "Piece should be selected");
                    Assert.That(_game.SelectedPiece, Is.EqualTo(square));
                }
                else
                {
                    Assert.That(square.Selected, Is.False, "Piece should not be selected (wrong turn)");
                    if (_game.SelectedPiece != null)
                    {
                        Assert.That(_game.SelectedPiece.Selected, Is.False);
                    }
                }
            }
        }

        /// <summary>
        /// Tests selecting the first square on the board
        /// </summary>
        [Test]
        [Category("CSelectSquare")]
        public void GameTestC1_SelectSquareFirstEmpty()
        {
            SelectHelper(1, 1, SquareColor.None);
        }

        /// <summary>
        /// Selects the first black square on the board
        /// </summary>
        [Test]
        [Category("CSelectSquare")]
        public void GameTestC2_SelectSquareFirstBlack()
        {
            SelectHelper(6, 1, SquareColor.Black);
        }

        /// <summary>
        /// Selects the middle empty square on the board
        /// </summary>
        [Test]
        [Category("CSelectSquare")]
        public void GameTestC3_SelectSquareMiddleEmpty()
        {
            SelectHelper(5, 5, SquareColor.None);
        }

        /// <summary>
        /// Selects the last red square on the board
        /// </summary>
        [Test]
        [Category("CSelectSquare")]
        public void GameTestC4_SelectSquareLastRed()
        {
            SelectHelper(3, 8, SquareColor.Red);
        }

        /// <summary>
        /// Tests selecting a square with a piece and then selecting a different square
        /// </summary>
        [Test]
        [Category("CSelectSquare")]
        public void GameTestC5_SelectSquareSwitchPiece()
        {
            SelectHelper(6, 5, SquareColor.Black);
            BoardSquare sq = _game.SelectedPiece;
            SelectHelper(7, 8, SquareColor.Black);
            Assert.That(sq.Selected, Is.False, "Old piece was not de-selected");
        }

        /// <summary>
        /// Tests selecting a square with a piece and then selecting a different square with a different turn
        /// </summary>
        [Test]
        [Category("CSelectSquare")]
        public void GameTestC6_SelectSquareSwitchPieceTurn()
        {
            SelectHelper(6, 5, SquareColor.Black);
            BoardSquare sq = _game.SelectedPiece;
            _game.Turn = SquareColor.Red;
            SelectHelper(3, 4, SquareColor.Red);
            Assert.That(sq.Selected, Is.False, "Old piece was not de-selected");
        }
        #endregion

        #region CheckCapture

        /// <summary>
        /// Tests that a piece that cant be captured returns false
        /// </summary>
        [Test]
        [Category("DCheckCapture")]
        public void GameTestD1_CheckCaptureFalse()
        {
            BoardSquare capturedPiece;
            Assert.That(_game.CheckCapture(_game.GetRow(5), 8, SquareColor.Red, out capturedPiece), Is.False);

        }

        /// <summary>
        /// Tests that a piece that can be captured returns true
        /// </summary>
        [Test]
        [Category("DCheckCapture")]
        public void GameTestD2_CheckCaptureTrue()
        {
            BoardSquare capturedPiece;
            // Ensure there is a red piece at (1, 8) for this test
            _game.SelectSquare(1, 8);
            Assert.That(_game.CheckCapture(_game.GetRow(1), 8, SquareColor.Red, out capturedPiece), Is.True);
            Assert.That(capturedPiece.Color, Is.EqualTo(SquareColor.Red));
        }

        /// <summary>
        /// Tests that a piece that cant be captured returns false and the out parameter is set to the correct value
        /// </summary>
        [Test]
        [Category("DCheckCapture")]
        public void GameTestD3_CheckCaptureOutFalse()
        {
            BoardSquare sq;
            Assert.That(_game.CheckCapture(_game.GetRow(5), 8, SquareColor.Red, out sq), Is.False);
            Assert.That(sq.Color, Is.EqualTo(SquareColor.None));
            Assert.That(sq.Row, Is.EqualTo(5));
            Assert.That(sq.Column, Is.EqualTo(8));
        }

        /// <summary>
        /// Tests that a piece that can be captured returns true and the out parameter is set to the correct value
        /// </summary>
        [Test]
        [Category("DCheckCapture")]
        public void GameTestD4_CheckCaptureOutTrue()
        {
            BoardSquare sq;
            Assert.That(_game.CheckCapture(_game.GetRow(1), 8, SquareColor.Red, out sq), Is.True);
            Assert.That(sq.Color, Is.EqualTo(SquareColor.Red));
            Assert.That(sq.Row, Is.EqualTo(1));
            Assert.That(sq.Column, Is.EqualTo(8));
        }
        #endregion

        #region Check Jumps

        /// <summary>
        /// Tests invalid jump, 0-5 5-5
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE1_CheckJumpBadEnemyRow()
        {
            Assert.That(_game.TestCheckJump(0, 5, 5, 5, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests invalid jump, 9-5 5-5
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE2_CheckJumpBadEnemyRow()
        {
            Assert.That(_game.TestCheckJump(9, 5, 5, 5, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests invalid jump, 5-5 0-5
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE3_CheckJumpBadEnemyCol()
        {
            Assert.That(_game.TestCheckJump(5, 5, 0, 5, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests invalid jump, 5-5 9-5
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE4_CheckJumpBadEnemyCol()
        {
            Assert.That(_game.TestCheckJump(5, 5, 9, 5, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests invalid jump, 1-0 5-5
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE5_CheckJumpBadTargetRow()
        {
            Assert.That(_game.TestCheckJump(1, 0, 5, 5, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests invalid jump, 1-9 5-5
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE6_CheckJumpBadTargetRow()
        {
            Assert.That(_game.TestCheckJump(1, 9, 5, 5, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests invalid jump, 5-5 1-9
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE7_CheckJumpBadTargetCol()
        {
            Assert.That(_game.TestCheckJump(5, 5, 1, 9, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests invalid jump, 5-5 1-9
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE8_CheckJumpBadTargetCol()
        {
            Assert.That(_game.TestCheckJump(5, 5, 1, 9, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests invalid jump, 5-4 6-5
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE9_CheckJumpNoEnemy()
        {
            Assert.That(_game.TestCheckJump(5, 4, 6, 5, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests invalid jump, 3-1 4-2
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestE9_CheckJumpNonEmptyTarget()
        {
            // note that this jump would be illegal anyways since its jumping too many squares
            Assert.That(_game.TestCheckJump(3, 1, 4, 2, SquareColor.Red), Is.False);
        }

        /// <summary>
        /// Tests a valid jump, 2-1 5-5
        /// </summary>
        [Test]
        [Category("ECheckJump")]
        public void GameTestEE10_CheckJump()
        {
            // note that this is a mocked jump (not real)
            Assert.That(_game.TestCheckJump(2, 1, 5, 5, SquareColor.Red), Is.True);
        }

        #endregion

        #region GetJumpSquare


        /// <summary>
        /// Tests getting the jump square for a black piece jumping up left
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF1_GetJumpSquareBlackUpLeft()
        {
            int row, col;
            int tr = 4;
            int tc = 5;

            int sr = 6;
            int sc = 7;
            BoardSquare target = new BoardSquare(tr, tc);
            _game.SelectSquare(sr, sc);


            Assert.That(_game.GetJumpSquare(target, SquareColor.Red, out row, out col),Is.True);
            Assert.That(row, Is.EqualTo(tr + 1), "Wrong Row");
            Assert.That(col, Is.EqualTo(tc + 1), "Wrong Column");
        }


        /// <summary>
        /// Tests getting the jump square for a black piece jumping up right
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF2_GetJumpSquareBlackUpRight()
        {
            int row, col;
            int tr = 4;
            int tc = 7;

            int sr = 6;
            int sc = 5;
            BoardSquare target = new BoardSquare(tr, tc);
            _game.SelectSquare(sr, sc);


            Assert.That(_game.GetJumpSquare(target, SquareColor.Red, out row, out col), Is.True);
            Assert.That(row, Is.EqualTo(tr + 1), "Wrong Row");
            Assert.That(col, Is.EqualTo(tc - 1), "Wrong Column");
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down right
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF3_GetJumpSquareRedDownRight()
        {
            int row, col;
            int tr = 5;
            int tc = 8;

            int sr = 3;
            int sc = 6;
            BoardSquare target = new BoardSquare(tr, tc);
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(sr, sc);


            Assert.That(_game.GetJumpSquare(target, SquareColor.Black, out row, out col), Is.True);
            Assert.That(row, Is.EqualTo(tr - 1), "Wrong Row");
            Assert.That(col, Is.EqualTo(tc - 1), "Wrong Column");
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down left
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF4_GetJumpSquareRedDownLeft()
        {
            int row, col;
            int tr = 5;
            int tc = 4;

            int sr = 3;
            int sc = 6;
            BoardSquare target = new BoardSquare(tr, tc);
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(sr, sc);


            Assert.That(_game.GetJumpSquare(target, SquareColor.Black, out row, out col), Is.True);
            Assert.That(row, Is.EqualTo(tr - 1), "Wrong Row");
            Assert.That(col, Is.EqualTo(tc + 1), "Wrong Column");
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down left
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF5_GetJumpSquareInvalidRow()
        {
            int row, col;
            int tr = 6;
            int tc = 4;

            int sr = 3;
            int sc = 6;
            BoardSquare target = new BoardSquare(tr, tc);
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(sr, sc);


            Assert.That(_game.GetJumpSquare(target, SquareColor.Black, out row, out col), Is.False);
            Assert.That(row, Is.EqualTo(-1));
            Assert.That(col, Is.EqualTo(-1));
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down left
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestF5_GetJumpSquareInvalidCol()
        {
            int row, col;
            int tr = 5;
            int tc = 3;

            int sr = 3;
            int sc = 6;
            BoardSquare target = new BoardSquare(tr, tc);
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(sr, sc);


            Assert.That(_game.GetJumpSquare(target, SquareColor.Black, out row, out col), Is.False);
            Assert.That(row, Is.EqualTo(-1));
            Assert.That(col, Is.EqualTo(-1));
        }

        #endregion

        #region CanMove

        /// <summary>
        /// Tests invalid move, 6-5
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG1_CanMoveLackInvalidRow()
        {
            bool jumpMore;
            _game.SelectSquare(6, 5);
            Assert.That(_game.CanMove(false, new BoardSquare(7,4), SquareColor.Red, out jumpMore), Is.True);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests invalid move, 6-5
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG2_CanMoveBlackInvalidRow()
        {
            bool jumpMore;
            _game.SelectSquare(6, 5); // Select a Black piece at (6, 5)

            // Attempt to move upward (invalid for regular Black pieces)
            Assert.That(_game.CanMove(false, new BoardSquare(5, 6), SquareColor.Red, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests invalid move, 6-5
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG3_CanMoveBlackInvalidCol()
        {
            bool jumpMore;
            _game.SelectSquare(6, 5);
            Assert.That(_game.CanMove(false, new BoardSquare(7, 5), SquareColor.Red, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests invalid move, 6-5
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG4_CanMoveBlackInvalidCol()
        {
            bool jumpMore;
            _game.SelectSquare(6, 5);
            Assert.That(_game.CanMove(false, new BoardSquare(5, 5), SquareColor.Red, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests valid move, 6-5
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG5_CanMoveRedInvalidRow()
        {
            bool jumpMore;
            _game.SelectSquare(3, 4); // Select a Red piece at (3, 4)

            // Ensure the target square is occupied
            BoardSquare targetSquare = _game.SelectSquare(2, 3);
            targetSquare.Color = SquareColor.Black; // Occupy the target square

            Assert.That(_game.CanMove(false, targetSquare, SquareColor.Black, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests invalid move, 3-4
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG6_CanMoveRedInvalidRow()
        {
            bool jumpMore;
            _game.SelectSquare(3, 4); // Select a Red piece at (3, 4)

            // Ensure the target square is occupied
            BoardSquare targetSquare = _game.SelectSquare(2, 5);
            targetSquare.Color = SquareColor.Black; // Occupy the target square

            Assert.That(_game.CanMove(false, targetSquare, SquareColor.Black, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// tests invalid move, 3-4
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG7_CanMoveRedInvalidCol()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            Assert.That(_game.CanMove(false, new BoardSquare(2, 4), SquareColor.Black, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests invalid move, 3-4
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG8_CanMoveRedInvalidCol()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            Assert.That(_game.CanMove(false, new BoardSquare(4, 4), SquareColor.Black, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests invalid King move, 3-4
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestG9_CanMoveKingInvalidCol()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            _game.SelectedPiece.King = true;
            Assert.That(_game.CanMove(false, new BoardSquare(4, 4), SquareColor.Black, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests invalid King move, 3-4
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG10CanMoveKingInvalidCol()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            _game.SelectedPiece.King = true;
            Assert.That(_game.CanMove(false, new BoardSquare(2, 4), SquareColor.Black, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests invalid King move, 3-4
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG11_CanMoveKingInvalidRow()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            _game.SelectedPiece.King = true;
            Assert.That(_game.CanMove(false, new BoardSquare(3, 3), SquareColor.Black, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests invalid King move, 3-4
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG12_CanMoveKingInvalidRow()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            _game.SelectedPiece.King = true;
            Assert.That(_game.CanMove(false, new BoardSquare(3, 5), SquareColor.Black, out jumpMore), Is.False);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests kind jump for a red piece jumping up left
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG13_CanMoveKingUpLeft()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            _game.SelectedPiece.King = true;
            Assert.That(_game.CanMove(false, new BoardSquare(2, 3), SquareColor.Black, out jumpMore), Is.True);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping up right
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG14_CanMoveKingUpRight()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            _game.SelectedPiece.King = true;
            Assert.That(_game.CanMove(false, new BoardSquare(2, 5), SquareColor.Black, out jumpMore), Is.True);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down left
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG15_CanMoveKingDownLeft()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            _game.SelectedPiece.King = true;
            Assert.That(_game.CanMove(false, new BoardSquare(4, 3), SquareColor.Black, out jumpMore), Is.True);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down right
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG16_CanMoveKingDownRight()
        {
            bool jumpMore;
            _game.Turn = SquareColor.Red;
            _game.SelectSquare(3, 4);
            _game.SelectedPiece.King = true;
            Assert.That(_game.CanMove(false, new BoardSquare(4, 5), SquareColor.Black, out jumpMore), Is.True);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down left
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG16_CanMoveRedUpLeft()
        {
            bool jumpMore;
            _game.SelectSquare(3, 4); // Select a Red piece at (3, 4)
            Assert.That(_game.CanMove(false, new BoardSquare(2, 3), SquareColor.Black, out jumpMore), Is.True);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down right
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG17_CanMoveRedUpRight()
        {
            bool jumpMore;
            _game.SelectSquare(3, 4); // Select a Red piece at (3, 4)
            Assert.That(_game.CanMove(false, new BoardSquare(2, 5), SquareColor.Black, out jumpMore), Is.True);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down left
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG18_CanMoveBlackUpLeft()
        {
            bool jumpMore;
            _game.SelectSquare(6, 5); // Select a Black piece at (6, 5)
            _game.SelectedPiece.King = true; // Make the piece a king
            Assert.That(_game.CanMove(false, new BoardSquare(5, 4), SquareColor.Red, out jumpMore), Is.True);
            Assert.That(jumpMore, Is.False);
        }

        /// <summary>
        /// Tests getting the jump square for a red piece jumping down left
        /// </summary>
        [Test]
        [Category("FGetJumpSquare")]
        public void GameTestGG19_CanMoveBlackDownRight()
        {
            bool jumpMore;
            _game.SelectSquare(6, 5); // Select a Black piece at (6, 5)
            Assert.That(_game.CanMove(false, new BoardSquare(7, 6), SquareColor.Red, out jumpMore), Is.True);
            Assert.That(jumpMore, Is.False);
        }

        #endregion

    }
}
