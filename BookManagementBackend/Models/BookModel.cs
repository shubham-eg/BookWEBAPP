using System.ComponentModel.DataAnnotations;

namespace BookManagementBackend.Models
{
	public class BookModel
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;

		[RegularExpression(@"^(19|20)\d{2}$", ErrorMessage = "Year must be a valid year")]
		public int Year { get; set; }
	}
}
