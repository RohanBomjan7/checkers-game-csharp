/// <summary>
/// f
/// </summary>
public enum SquareColor
{
    Red,
    Black,
    None
}
/// <summary>
/// v
/// </summary>
public class BoardSquare
{
    /// <summary>
    /// Tracks if piece is a king
    /// </summary>
    public bool King { get; set; } = false;
    /// <summary>
    /// Color of the piece (Red, Black, None)
    /// </summary>
    public SquareColor Color { get; set; }
    /// <summary>
    /// if the piece is selected
    /// </summary>
    public bool Selected { get; set; } = false;
    /// <summary>
    /// Row position
    /// </summary>
    public int Row { get; }
    /// <summary>
    /// Column position
    /// </summary>
    public int Column { get; }  

    // Constructor
    public BoardSquare(int row, int col)
    {
        Row = row;
        Column = col;
        Color = SquareColor.None;
    }
}
