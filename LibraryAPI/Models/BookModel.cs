namespace LibraryAPI.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public int AuthorId { get; internal set; }
        public int PublicationYear { get; set; }
        public List<string> Category { get; set; }
        public int AvaliableCopies { get; set; }
    }
}
