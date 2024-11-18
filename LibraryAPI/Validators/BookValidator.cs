using System.Text.RegularExpressions;

namespace LibraryAPI.Validators
{
    public class BookValidator
    {
        // Method to validate ISBN
        public static bool IsValidISBN(string isbn)
        {
            // ISBN-10 or ISBN-13 format validation
            if (string.IsNullOrEmpty(isbn)) return false;

            // Check if ISBN matches the format for ISBN-10 or ISBN-13
            string isbn10Pattern = @"^\d{9}[\dX]$"; // 10 digits or 9 digits + 'X'
            string isbn13Pattern = @"^\d{13}$";     // 13 digits

            return Regex.IsMatch(isbn, isbn10Pattern) || Regex.IsMatch(isbn, isbn13Pattern);
        }

        // Method to validate Title
        public static bool IsValidTitle(string title, int maxLength = 100)
        {
            if (string.IsNullOrWhiteSpace(title)) return false;

            return title.Length <= maxLength;
        }

        // Method to validate Author
        public static bool IsValidAuthor(int authorId)
        {
            if (authorId == 0) return false;

            bool returnResult = DoesAuthorExits(authorId);

            return returnResult;
        }
        
        public static bool IsUniqueISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn)) return false;

            bool returnResult = DoesISBNExits(isbn);

            return returnResult;
        }

        private static bool DoesAuthorExits(int authorId)
        {
            //Queries a datasource with the authorId

            //If count != 0
            //  return true
            //Else
            //  return false
            return true;
        }

        private static bool DoesISBNExits(string isbn)
        {
            //Queries a datasource with the isbn

            //If count != 0
            //  return true
            //Else
            //  return false

            return true;
        }
    }
}
