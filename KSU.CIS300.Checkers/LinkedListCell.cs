/* LinkedListCell.cs
 */

namespace KSU.CIS300.Checkers
{
    /// <summary>
    /// A single cell of a generic linked list.
    /// </summary>
    /// <typeparam name="T">The type of the elements stored in the list.</typeparam>
    public class LinkedListCell<T>
    {
        /// <summary>
        /// Gets or sets the data item stored in the cell.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the next cell in the list.
        /// </summary>
        public LinkedListCell<T> Next { get; set; }
    }
}
