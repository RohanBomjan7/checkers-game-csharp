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
            LinkedListCell<int> cell = new LinkedListCell<int>();
            Assert.That(cell, Has.Property("Next"));
        }

        /// <summary>
        /// Test to see if the LinkedListCell class has a Data property
        /// </summary>
        [Test]
        public void LinkedListCellTest_Data()
        {
            LinkedListCell<int> cell = new LinkedListCell<int>();
            Assert.That(cell, Has.Property("Data"));
        }
    }
}