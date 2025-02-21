using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace KSU.CIS300.Checkers.Tests
{
    /// <summary>
    /// A class to test the BoardSquare class
    /// </summary>
    class BoardSquareTests
    {
        /// <summary>
        /// Test to see if the BoardSquare class has a King property
        /// </summary>
        [Test]
        public void BoardSquareTest_King()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("King"));
        }

        /// <summary>
        /// Test to see if the BoardSquare class has a Color property
        /// </summary>
        [Test]
        public void BoardSquareTest_Color()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("Color"));
        }

        /// <summary>
        /// Test to see if the BoardSquare class has a Selected property
        /// </summary>
        [Test]
        public void BoardSquareTest_Selected()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("Selected"));
        }

        /// <summary>
        /// Test to see if the BoardSquare class has a Row property
        /// </summary>
        [Test]
        public void BoardSquareTest_Row()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("Row"));
        }

        /// <summary>
        /// Test to see if the BoardSquare class has a Column property
        /// </summary>
        [Test]
        public void BoardSquareTest_Column()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq, Has.Property("Column"));
        }

        /// <summary>
        /// Test to see if the BoardSquare class has a constructor that initializes the Row and Column properties
        /// </summary>
        [Test]
        public void BoardSquareTest_Default()
        {
            BoardSquare sq = new BoardSquare(1, 1);
            Assert.That(sq.Row, Is.EqualTo(1));
            Assert.That(sq.Column, Is.EqualTo(1));
            Assert.That(sq.King, Is.False);
            Assert.That(sq.Selected, Is.False);
        }
    }
}
