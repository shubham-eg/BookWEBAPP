﻿namespace BookManagementFrontend.Models;

public class BookModel
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Author { get; set; } = string.Empty;
	public int Year { get; set; }
}
