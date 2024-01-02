using Microsoft.EntityFrameworkCore;

namespace KütüphaneOtomasyonu.Models
{
    public class MyDbContext  : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> o) : base(o)
        {

        }
        public Member CurrentUser { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
