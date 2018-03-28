using System.Data.Entity;

namespace BookStore.Models
{
    public sealed class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
    }
}