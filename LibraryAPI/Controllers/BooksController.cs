using System.Linq;
using LibraryAPI.DTOs;
using LibraryAPI.Mappers;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryRepository _repository = LibraryRepository.Instance;

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _repository.GetBooks();
            var authors = _repository.GetAuthors();

            var dtos = books.Select(book =>
            {
                var author = authors.FirstOrDefault(a => a.Id == book.AuthorId);
                // Handle null author
                var authorName = author != null ? $"{author.FirstName} {author.LastName}" : "Unknown Author";
                return new BookListDTO
                {
                    Title = book.Title,
                    Author = authorName
                };
            });

            return Ok(dtos);
        }


        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _repository.GetBookById(id);
            if (book == null) return NotFound();

            var author = _repository.GetAuthors().FirstOrDefault(a => a.Id == book.AuthorId);
            return Ok(BookMapper.ToBookListDTO(book, author));
        }


        [HttpPost]
        public IActionResult AddBook([FromBody] BookEditDTO dto)
        {
            if (!_repository.GetAuthors().Any(a => a.Id == dto.Id))
                return BadRequest("Invalid author.");

            if (_repository.GetBooks().Any(b => b.ISBN == dto.ISBN))
                return Conflict("ISBN already exists.");

            var book = new Models.BookModel
            {
                Title = dto.Title,
                ISBN = dto.ISBN,
                AuthorId = dto.Id // Assuming the DTO contains the author ID
            };

            _repository.AddBook(book); // The repository will assign the ID
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, dto);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookEditDTO dto)
        {
            var book = _repository.GetBookById(id);
            if (book == null) return NotFound();

            BookMapper.UpdateBookFromDTO(book, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _repository.GetBookById(id);
            if (book == null) return NotFound();

            _repository.RemoveBook(id);
            return NoContent();
        }
    }

}
