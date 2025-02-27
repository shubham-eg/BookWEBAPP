using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using BookManagementBackend.Models;
using BookManagementBackend.Data;


namespace BookManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
	private readonly ApplicationDbContext _context;

	// Inject the DbContext into the controller
	public BooksController(ApplicationDbContext context)
	{
		_context = context;
	}

	// GET: api/books
	[HttpGet]
	public async Task<ActionResult<IEnumerable<BookModel>>> GetBooks()
	{
		var books = await _context.Books.ToListAsync();
		return Ok(books); // Return the list of books from the database
	}

	// GET: api/books/{id}
	[HttpGet("{id}")]
	public async Task<ActionResult<BookModel>> GetBook(int id)
	{
		var book = await _context.Books.FindAsync(id);

		if (book == null)
		{
			return NotFound(); // Return 404 if the book is not found
		}

		return Ok(book); // Return the book if found
	}

	// POST: api/books
	[HttpPost]
	public async Task<ActionResult<BookModel>> CreateBook(BookModel book)
	{
		var regex = new Regex(@"^(19|20)\d{2}$");
		if (!regex.IsMatch(book.Year.ToString()) || book.Year < 1900 || book.Year > DateTime.Now.Year)
		{
			return BadRequest(new { message = "Year must be a valid year" });

		}
		// Add the book to the database
		_context.Books.Add(book);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
	}

	// PUT: api/books/{id}
	[HttpPut("{id}")]
	public async Task<ActionResult<BookModel>> EditBook(int id, BookModel updatedBook)
	{
		var book = await _context.Books.FindAsync(id);
		if (book == null)
		{
			return NotFound(); //meow
		}

		// Update the book's properties
		book.Title = updatedBook.Title;
		book.Author = updatedBook.Author;
		book.Year = updatedBook.Year;

		// Save the changes to the database
		_context.Books.Update(book);
		await _context.SaveChangesAsync();

		return Ok(book); // Return the updated book
	}

	// DELETE: api/books/{id}
	[HttpDelete("{id}")]
	public async Task<ActionResult<BookModel>> DeleteBook(int id)
	{
		var book = await _context.Books.FindAsync(id);
		if (book == null)
		{
			return NotFound(); // Return 404 if the book is not found
		}

		// Remove the book from the database
		_context.Books.Remove(book);
		await _context.SaveChangesAsync();

		return Ok(book); // Return the deleted book
	}
}
