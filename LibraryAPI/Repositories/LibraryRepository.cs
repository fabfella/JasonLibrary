using System.Collections.Generic;
using System.Linq;
using LibraryAPI.Models;

namespace LibraryAPI.Repositories
{
    public class LibraryRepository
    {
        // Singleton Instance
        private static readonly LibraryRepository _instance = new LibraryRepository();

        private int _nextBookId = 1; // Start with 1 for auto-incrementing book IDs
        private int _nextAuthorId = 1; // Similarly for authors if needed

        public static LibraryRepository Instance => _instance;

        // Private constructor to prevent instantiation
        private LibraryRepository()
        {
            Books = new List<BookModel>();
            Authors = new List<AuthorModel>();
        }

        // In-memory data
        private List<BookModel> Books { get; }
        private List<AuthorModel> Authors { get; }

        // Book Management Methods

        public void AddBook(BookModel book)
        {
            book.Id = _nextBookId++; // Assign and increment ID
            Books.Add(book);
        }

        public IEnumerable<BookModel> GetBooks() => Books;

        public BookModel GetBookById(int id) => Books.FirstOrDefault(b => b.Id == id);

        public void RemoveBook(int id) => Books.RemoveAll(b => b.Id == id);

        // Author Management Methods
        public void AddAuthor(AuthorModel author)
        {
            author.Id = _nextAuthorId++; // Assign and increment ID
            Authors.Add(author);
        }

        public IEnumerable<AuthorModel> GetAuthors() => Authors;

        public AuthorModel GetAuthorById(int id) => Authors.FirstOrDefault(a => a.Id == id);

        public void RemoveAuthor(int id) => Authors.RemoveAll(a => a.Id == id);
    }
}
