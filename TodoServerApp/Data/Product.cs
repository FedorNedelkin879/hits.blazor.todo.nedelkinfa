using System.ComponentModel.DataAnnotations;

namespace TodoServerApp.Data
{
	public class Product
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Название товара обязательно")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "SKU обязателен")]
		public string? SKU { get; set; }

		[Required(ErrorMessage = "Цена обязательна")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше нуля")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Количество обязательно")]
		[Range(0, int.MaxValue)]
		public int Stock { get; set; }

		[Required(ErrorMessage = "Категория обязательна")]
		public string? Category { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;
	}
}
