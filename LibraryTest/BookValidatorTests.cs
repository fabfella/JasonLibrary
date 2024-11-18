using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryAPI.Validators;

namespace LibraryAPI.Tests
{
    [TestClass]
    public class BookValidatorTests
    {
        public static IEnumerable<object[]> TitleTestData =>
        new List<object[]>
        {
            new object[] { "A Great Book", true }, // Valid title
            new object[] { "", false }, // Empty title
            new object[] { "    ", false }, // Whitespace title
            new object[] { new string('A', 101), false } // Overly long title
        };

        [TestMethod]
        [DataRow("123456789X", true)] // Valid ISBN-10
        [DataRow("9781234567897", true)] // Valid ISBN-13
        [DataRow("123456", false)] // Invalid ISBN
        [DataRow("", false)] // Empty ISBN
        [DataRow(null, false)] // Null ISBN
        public void IsValidISBN_ShouldReturnExpectedResult(string isbn, bool expected)
        {
            // Act
            var result = BookValidator.IsValidISBN(isbn);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DynamicData(nameof(TitleTestData))]
        public void IsValidTitle_ShouldReturnExpectedResult(string title, bool expected)
        {
            // Act
            var result = BookValidator.IsValidTitle(title);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IsValidAuthor_ShouldReturnTrueForValidAuthor()
        {
            // Arrange
            int validAuthorId = 1;

            // Act
            var result = BookValidator.IsValidAuthor(validAuthorId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidAuthor_ShouldReturnFalseForInvalidAuthor()
        {
            // Arrange
            int invalidAuthorId = 0;

            // Act
            var result = BookValidator.IsValidAuthor(invalidAuthorId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsUniqueISBN_ShouldReturnTrueForUniqueISBN()
        {
            // Arrange
            string uniqueISBN = "9781234567897";

            // Act
            var result = BookValidator.IsUniqueISBN(uniqueISBN);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsUniqueISBN_ShouldReturnFalseForInvalidISBN()
        {
            // Arrange
            string invalidISBN = "";

            // Act
            var result = BookValidator.IsUniqueISBN(invalidISBN);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
