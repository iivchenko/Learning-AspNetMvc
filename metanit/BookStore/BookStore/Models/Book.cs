namespace BookStore.Models
{
    public sealed class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public int Price { get; set; }
    }
}