using System.Net.Http.Json;
using BookManagementFrontend.Models;

namespace BookManagementFrontend.Services;

public class BookService
{
	private readonly HttpClient _httpClient;

	public BookService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<List<BookModel>> GetBooksAsync()
	{
		return await _httpClient.GetFromJsonAsync<List<BookModel>>("api/Books") ?? new List<BookModel>();
	}

	public async Task<BookModel?> GetBookByIdAsync(int id)
	{
		return await _httpClient.GetFromJsonAsync<BookModel>($"api/books/{id}");
	}

	public async Task AddBookAsync(BookModel book)
	{
		await _httpClient.PostAsJsonAsync("api/books", book);
	}

	public async Task UpdateBookAsync(BookModel book)
	{
		await _httpClient.PutAsJsonAsync($"api/books/{book.Id}", book);
	}

	public async Task DeleteBookAsync(int id)
	{
		await _httpClient.DeleteAsync($"api/books/{id}");
	}
}