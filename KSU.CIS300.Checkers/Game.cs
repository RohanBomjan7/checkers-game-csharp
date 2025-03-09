using KSU.CIS300.Checkers;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
/// <summary>
/// g
/// </summary>
public class Game
{

    /// <summary>
    /// pr
    /// </summary>
    private Dictionary<int, LinkedListCell<BoardSquare>> _board;
    /// <summary>
    /// Board representation
    /// </summary>
    public int RedCount { get; private set; } = 12;
    /// <summary>
    /// h
    /// </summary>
    public int BlackCount { get; private set; } = 12;
    /// <summary>
    /// Stores selected piece
    /// </summary>
    public BoardSquare SelectedPiece { get; set; } = null;
    /// <summary>
    /// Black starts first
    /// </summary>
    public SquareColor Turn { get; set; } = SquareColor.Black;

    // Constructor
    public Game()
    {
        _board = new Dictionary<int, LinkedListCell<BoardSquare>>();
        CreateBoard();
    }
    /// <summary>
    /// s
    /// </summary>
    private void CreateBoard()
    {
        _board = new Dictionary<int, LinkedListCell<BoardSquare>>();

        for (int row = 1; row <= 8; row++)
        {
            LinkedListCell<BoardSquare> previous = null;
            for (int col = 1; col <= 8; col++) // Build from left to right
            {
                BoardSquare square = new BoardSquare(row, col);

                if ((row + col) % 2 != 0) // Only dark squares can have pieces
                {
                    if (row < 4) square.Color = SquareColor.Red;
                    else if (row > 5) square.Color = SquareColor.Black;
                    else square.Color = SquareColor.None;
                }
                else
                {
                    square.Color = SquareColor.None;
                }

                LinkedListCell<BoardSquare> cell = new LinkedListCell<BoardSquare>(square);
                cell.Next = previous;
                previous = cell;
            }

            // Reverse the linked list to ensure columns are ordered from 1 to 8
            _board[row] = ReverseLinkedList(previous);
        }
    }
    /// <summary>
    /// ah
    /// </summary>
    /// <param name="head">a</param>
    /// <returns>d</returns>
    private LinkedListCell<BoardSquare> ReverseLinkedList(LinkedListCell<BoardSquare> head)
    {
        LinkedListCell<BoardSquare> prev = null;
        LinkedListCell<BoardSquare> current = head;
        LinkedListCell<BoardSquare> next = null;

        while (current != null)
        {
            next = current.Next;
            current.Next = prev;
            prev = current;
            current = next;
        }

        return prev;
    }

    /// <summary>
    /// PB
    /// </summary>
    public void PrintBoard()
    {
        foreach (var row in _board)
        {
            Console.Write($"Row {row.Key}: ");
            var cell = row.Value;
            while (cell != null)
            {
                Console.Write($"({cell.Value.Row}, {cell.Value.Column}, {cell.Value.Color}) -> ");
                cell = cell.Next;
            }
            Console.WriteLine("null");
        }
    }

    /// <summary>
    /// ag
    /// </summary>
    /// <param name="row">getwor</param>
    /// <returns>we</returns>

    public LinkedListCell<BoardSquare> GetRow(int row)
    {
        return _board.ContainsKey(row) ? _board[row] : null;
    }
    /// <summary>
    /// ya
    /// </summary>
    /// <param name="row">w</param>
    /// <param name="col">q</param>
    /// <returns>we</returns>
    public BoardSquare SelectSquare(int row, int col)
    {
        
        // ✅ Locate the requested square
        BoardSquare selected = GetBoardSquare(row, col);

        if (selected == null)
            return null;  // Square does not exist

        // ✅ Ensure only pieces of the current turn can be selected
        if (selected.Color == Turn)
        {
            // ✅ Deselect any previously selected piece
            if (SelectedPiece != null)
            {
                SelectedPiece.Selected = false;
            }

            // ✅ Mark this piece as selected
            SelectedPiece = selected;
            selected.Selected = true;
        }
        else
        {
            // ✅ Prevent selection of opponent's pieces
            selected.Selected = false;
        }

        return selected;
    }


    /// <summary>
    /// au
    /// </summary>
    /// <param name="cell">cell</param>
    /// <param name="targetCol">targetcol</param>
    /// <param name="targetColor">targetColor</param>
    /// <param name="result">result</param>
    /// <returns>q</returns>

    public bool CheckCapture(LinkedListCell<BoardSquare> cell, int targetCol, SquareColor targetColor, out BoardSquare result)
    {

        result = null; // Ensure result is initialized

        while (cell != null)
        {
            if (cell.Value.Column == targetCol)
            {
                result = cell.Value;

                if (cell.Value.Color == targetColor)
                {
                    return true;
                }
                break; 
            }
            cell = cell.Next;
        }

        return false; 
    }


    /// <summary>
    /// ds
    /// </summary>
    /// <param name="enemyRow">s</param>
    /// <param name="targetRow">r</param>
    /// <param name="enemyCol">d</param>
    /// <param name="targetCol">g</param>
    /// <param name="enemy">e</param>
    /// <returns>re</returns>
    public bool TestCheckJump(int enemyRow, int targetRow, int enemyCol, int targetCol, SquareColor enemy)
    {
        //BoardSquare dummySquare = new BoardSquare(targetRow, targetCol);
        return CheckJump(enemyRow, targetRow, enemyCol, targetCol, enemy);
    }
    /// <summary>
    /// ad
    /// </summary>
    /// <param name="enemyRow">er</param>
    /// <param name="targetRow">tr</param>
    /// <param name="enemyCol">e</param>
    /// <param name="targetCol">ter</param>
    /// <param name="current">c</param>
    /// <param name="enemy">e</param>
    /// <returns>w</returns>

    private bool CheckJump(int enemyRow, int targetRow, int enemyCol, int targetCol, SquareColor enemy)
    {
        if (!_board.ContainsKey(enemyRow) || !_board.ContainsKey(targetRow))
            return false;

        BoardSquare enemyPiece = SelectSquare(enemyRow, enemyCol);
        BoardSquare targetSquare = SelectSquare(targetRow, targetCol);
        BoardSquare movingPiece = SelectSquare((enemyRow + targetRow) / 2, (enemyCol + targetCol) / 2);

        if (enemyPiece == null || targetSquare == null || movingPiece == null)
            return false;

        // ✅ Ensure direction is valid
        if (!movingPiece.King)
        {
            if (movingPiece.Color == SquareColor.Black && targetRow < movingPiece.Row) return false;
            if (movingPiece.Color == SquareColor.Red && targetRow > movingPiece.Row) return false;
        }

        return enemyPiece.Color == enemy && targetSquare.Color == SquareColor.None;
    }



    /// <summary>
    /// g
    /// </summary>
    /// <param name="target">t</param>
    /// <param name="enemy">e</param>
    /// <param name="row">r</param>
    /// <param name="col">c</param>
    /// <returns>c</returns>
    public bool GetJumpSquare(BoardSquare target, SquareColor enemy, out int row, out int col)
    {
        row = -1;
        col = -1;

        if (SelectedPiece == null)
            return false;

        int rowDiff = target.Row - SelectedPiece.Row;
        int colDiff = target.Column - SelectedPiece.Column;

        if (Math.Abs(rowDiff) != 2 || Math.Abs(colDiff) != 2)
            return false;

        row = (target.Row + SelectedPiece.Row) / 2;
        col = (target.Column + SelectedPiece.Column) / 2;

        return true;
    }
    /// <summary>
    /// h
    /// </summary>
    /// <param name="targetRow">a</param>
    /// <param name="targetCol">s</param>
    /// <returns>s</returns>

    public bool MoveSelectedPiece(int targetRow, int targetCol)
    {
        if (SelectedPiece == null) return false;

        BoardSquare target = SelectSquare(targetRow, targetCol);
        if (target == null || target.Color != SquareColor.None)
            return false;  // Invalid move

        // ✅ Check if THIS PIECE has a jump available
        bool jumpAvailable = CheckAnyJump(SelectedPiece, Turn == SquareColor.Black ? SquareColor.Red : SquareColor.Black);

        // ✅ If a jump is available, enforce it
        if (jumpAvailable && !CheckJump(SelectedPiece.Row, targetRow, SelectedPiece.Column, targetCol,
            (Turn == SquareColor.Black) ? SquareColor.Red : SquareColor.Black))
        {
            Console.WriteLine("Invalid move! A jump is required.");
            return false;
        }

        // ✅ Try to move normally
        bool jumpMore;
        bool moved = CanMove(false, target, Turn == SquareColor.Black ? SquareColor.Red : SquareColor.Black, out jumpMore);

        if (moved)
        {
            MovePiece(SelectedPiece, target);
            SelectedPiece = null;

            // ✅ Only switch turns if no more jumps exist for this piece
            if (!jumpMore)
            {
                Turn = (Turn == SquareColor.Black) ? SquareColor.Red : SquareColor.Black;
            }
        }

        return moved;
    }


    /// <summary>
    /// gh
    /// </summary>
    /// <param name="forceJump">ca</param>
    /// <param name="targetSquare">ts</param>
    /// <param name="enemy">e</param>
    /// <param name="jumpMore">jm</param>
    /// <returns>re</returns>

    public bool CanMove(bool forceJump, BoardSquare targetSquare, SquareColor enemy, out bool jumpMore)
    {
        jumpMore = false;

        if (SelectedPiece == null || targetSquare == null)
        {
            Console.WriteLine("CanMove() failed: No piece selected or target square is null.");
            return false;  // No piece selected or target square invalid
        }

        int rowDiff = targetSquare.Row - SelectedPiece.Row;
        int colDiff = Math.Abs(targetSquare.Column - SelectedPiece.Column);

        Console.WriteLine($"CanMove() Debug: rowDiff={rowDiff}, colDiff={colDiff}, SelectedPiece.Row={SelectedPiece.Row}, TargetSquare.Row={targetSquare.Row}, SelectedPiece.Color={SelectedPiece.Color}, TargetSquare.Color={targetSquare.Color}");

        // Ensure the move is diagonal
        if (Math.Abs(rowDiff) != 1 || colDiff != 1)
        {
            Console.WriteLine("CanMove() failed: Not a diagonal move.");
            return false;
        }

        // If jump enforcement is active, only allow jumps
        if (forceJump)
        {
            bool validJump = Jump(SelectedPiece, targetSquare, enemy, out jumpMore);
            return validJump;
        }

        // Allow normal diagonal moves (1 step)
        if (targetSquare.Color == SquareColor.None)
        {
            // Kings can move in any direction
            if (SelectedPiece.King)
                return true;

            // Black pieces can only move downward (row increases)
            if (SelectedPiece.Color == SquareColor.Black && rowDiff == 1)
                return true;

            // Red pieces can only move upward (row decreases)
            if (SelectedPiece.Color == SquareColor.Red && rowDiff == -1)
                return true;
        }

        Console.WriteLine("CanMove() failed: Move direction not allowed or target square is occupied.");
        return false; // Otherwise, invalid move
    }
    /// <summary>
    /// jh
    /// </summary>
    /// <param name="current">w</param>
    /// <param name="target">e</param>
    /// <param name="enemy">d</param>
    /// <param name="jumpMore">jm</param>
    /// <returns>re</returns>
    private bool Jump(BoardSquare current, BoardSquare target, SquareColor enemy, out bool jumpMore)
    {
        jumpMore = false;

        int enemyRow = (current.Row + target.Row) / 2;
        int enemyCol = (current.Column + target.Column) / 2;

        BoardSquare enemyPiece = SelectSquare(enemyRow, enemyCol);

        if (enemyPiece == null || enemyPiece.Color != enemy)
            return false;  // No enemy piece to jump over

        // **Remove captured piece**
        enemyPiece.Color = SquareColor.None;
        if (enemy == SquareColor.Red) RedCount--;
        if (enemy == SquareColor.Black) BlackCount--;

        // Update piece position
        target.Color = current.Color;
        current.Color = SquareColor.None;

        // Check if more jumps are possible
        jumpMore = CheckAnyJump(target, enemy);
        return true;
    }

    /// <summary>
    /// ah
    /// </summary>
    /// <param name="current">current</param>
    /// <param name="enemy">ev</param>
    /// <returns>re</returns>
    private bool CheckAnyJump(BoardSquare current, SquareColor enemy)
    {
        if (current == null || current.Color == SquareColor.None)
            return false;

        int row = current.Row;
        int col = current.Column;

        // 🔹 Allow kings to jump in all four directions
        int[] rowOffsets = current.King ? new int[] { -2, 2 } :
                            (current.Color == SquareColor.Red ? new int[] { -2 } : new int[] { 2 });
        int[] colOffsets = new int[] { -2, 2 };

        foreach (int rowOffset in rowOffsets)
        {
            foreach (int colOffset in colOffsets)
            {
                int enemyRow = row + rowOffset / 2;
                int enemyCol = col + colOffset / 2;
                int targetRow = row + rowOffset;
                int targetCol = col + colOffset;

                // 🔹 Only check jumps if the enemy piece is in the correct position
                if (SelectSquare(enemyRow, enemyCol)?.Color == enemy &&
                    SelectSquare(targetRow, targetCol)?.Color == SquareColor.None)
                {
                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Get
    /// </summary>
    /// <param name="row">ro</param>
    /// <param name="col">col</param>
    /// <returns>re</returns>
    private BoardSquare GetBoardSquare(int row, int col)
    {
        LinkedListCell<BoardSquare> boardRow = GetRow(row);

        // Check if the row exists
        if (boardRow == null)
        {
            Console.WriteLine($"Row {row} not found in board.");
            return null;
        }

        while (boardRow != null)
        {
            if (boardRow.Value != null && boardRow.Value.Column == col)
            {
                return boardRow.Value;
            }
            boardRow = boardRow.Next;
        }

        Console.WriteLine($"Column {col} not found in row {row}.");
        return null;
    }

    /// <summary>
    /// sy
    /// </summary>
    /// <param name="from">fro</param>
    /// <param name="to">to</param>
    private void MovePiece(BoardSquare from, BoardSquare to)
    {
        to.Color = from.Color;
        to.King = from.King;
        from.Color = SquareColor.None;
        from.King = false;

        // **Promote to king if reached the last row**
        if (to.Color == SquareColor.Black && to.Row == 8)
            to.King = true;
        if (to.Color == SquareColor.Red && to.Row == 1)
            to.King = true;

        // **Check for win condition**
        if (RedCount == 0)
            Console.WriteLine("Black Wins!");
        if (BlackCount == 0)
            Console.WriteLine("Red Wins!");
    }


    /// <summary>
    /// h
    /// </summary>
    /// <param name="row">row</param>
    /// <param name="col">col</param>
    /// <returns>re</returns>

    public bool MakeMove(int row, int col)
    {
        Console.WriteLine($"Attempting to move to ({row}, {col})");

        if (SelectedPiece == null)
        {
            Console.WriteLine("No piece selected!");
            return false;
        }

        BoardSquare target = GetBoardSquare(row, col);
        if (target == null)
        {
            Console.WriteLine("Target square does not exist.");
            return false;
        }

        if (target.Color != SquareColor.None)
        {
            Console.WriteLine($"Target square ({row}, {col}) is not empty. It contains {target.Color}.");
            return false;
        }

        Console.WriteLine($"Selected piece is at ({SelectedPiece.Row}, {SelectedPiece.Column})");

        // 🔹 Check if any jump is required
        bool jumpAvailable = CheckAnyJump(SelectedPiece, (Turn == SquareColor.Black) ? SquareColor.Red : SquareColor.Black);

        if (jumpAvailable)
        {
            Console.WriteLine("A jump is available, enforcing jump rule...");

            if (!CheckJump(SelectedPiece.Row, row, SelectedPiece.Column, col,
                (Turn == SquareColor.Black) ? SquareColor.Red : SquareColor.Black))
            {
                Console.WriteLine("Invalid move! A jump is required but was not taken.");
                return false;
            }
        }

        // 🔹 Attempt normal movement
        bool jumpMore;
        bool moved = CanMove(false, target, (Turn == SquareColor.Black) ? SquareColor.Red : SquareColor.Black, out jumpMore);

        if (!moved)
        {
            Console.WriteLine("Move was rejected by CanMove().");
            return false;
        }

        MovePiece(SelectedPiece, target);
        SelectedPiece = null;

        // 🔹 Switch turns only if no more jumps are available
        if (!jumpMore)
        {
            Turn = (Turn == SquareColor.Black) ? SquareColor.Red : SquareColor.Black;
        }

        Console.WriteLine($"Move successful! New turn: {Turn}");
        return true;
    }

}

