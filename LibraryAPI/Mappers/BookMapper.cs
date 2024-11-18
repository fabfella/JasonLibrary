using System.Collections.Generic;
using System.Linq;
using LibraryAPI.DTOs;
using LibraryAPI.Models;
using LibraryAPI.Repositories;

namespace LibraryAPI.Mappers
{
    public static class BookMapper
    {
        // Map a single BookModel and AuthorModel to BookListDTO
        public static BookListDTO ToBookListDTO(BookModel book, AuthorModel author)
        {
            return new BookListDTO
            {
                Title = book.Title,
                Author = author != null ? $"{author.FirstName} {author.LastName}" : "Unknown Author"
            };
        }

        // Map a single BookModel and AuthorModel to BookListDTO
        public static BookListDTO ToBookListDTO(BookModel book, IEnumerable<AuthorModel> authors)
        {
            var author = authors.FirstOrDefault(a => a.Id == book.AuthorID);
            return new BookListDTO
            {
                Title = book.Title,
                Author = author != null ? $"{author.FirstName} {author.LastName}" : "Unknown Author"
            };
        }

        // Map a list of BookModels to a list of BookListDTOs
        public static List<BookListDTO> ToBookListDTOs(IEnumerable<BookModel> books, IEnumerable<AuthorModel> authors)
        {
            return books.Select(book => ToBookListDTO(book, authors)).ToList();
        }

        public static BookEditDTO ToBookEditDTO(BookModel book)
        {
            return new BookEditDTO
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN
            };
        }

        public static void UpdateBookFromDTO(BookModel book, BookEditDTO dto)
        {
            book.Title = dto.Title;
            book.ISBN = dto.ISBN;
        }
    }
}
