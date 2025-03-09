using NUnit.Framework;

namespace KSU.CIS300.Checkers.Tests
{
    /// <summary>
    /// A class to test the LinkedListCell class
    /// </summary>
    public class LinkedListCellTests
    {

        /// <summary>
        /// Test to see if the LinkedListCell class has a Next property
        /// </summary>
        [Test]
        public void LinkedListCellTest_Next()
        {
            LinkedListCell<int> cell = new LinkedListCell<int>(0);  // Provide an integer value
            Assert.That(cell, Has.Property("Next"));
        }

        /// <summary>
        /// Test to see if the LinkedListCell class has a Data property
        /// </summary>
        [Test]
        public void LinkedListCellTest_Value()
        {
            LinkedListCell<int> cell = new LinkedListCell<int>(0);  
            Assert.That(cell, Has.Property("Value"));
        }
    }
}