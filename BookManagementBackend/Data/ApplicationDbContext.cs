using BookManagementBackend.Models;
using Microsoft.EntityFrameworkCore;


namespace BookManagementBackend.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		// DbSet for books
		public DbSet<BookModel> Books { get; set; }
	}
}
