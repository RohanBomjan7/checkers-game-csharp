namespace KSU.CIS300.Checkers
{
    /// <summary>
    /// A single cell of a generic linked list.
    /// </summary>
    /// <typeparam name="T">The type of the elements stored in the list.</typeparam>
    public class LinkedListCell<T>
    {
        /// <summary>
        /// Holds the value of the current node
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        ///  Reference to the next node in the list
        /// </summary>
        public LinkedListCell<T> Next { get; set; }

        // Constructor
        public LinkedListCell(T value)
        {
            Value = value;
            Next = null;
        }
    }
}

