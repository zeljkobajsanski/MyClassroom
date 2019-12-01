using Microsoft.EntityFrameworkCore;

namespace Backend.Model
{
    public class ClassroomContext : DbContext
    {
        public ClassroomContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Chapter>();
            modelBuilder.Entity<Question>();
            modelBuilder.Entity<Book>();
        }
    }
}