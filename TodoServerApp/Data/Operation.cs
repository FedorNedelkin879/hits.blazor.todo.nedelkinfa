using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoServerApp.Data
{
	public class Operation
	{
		public int Id { get; set; }

		[Required]
		public OperationType Type { get; set; }

		[Required]
		public int ProductId { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product? Product { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Количество должно быть больше нуля")]
		public int Quantity { get; set; }

		[Required]
		public int EmployeeId { get; set; }

		[ForeignKey("EmployeeId")]
		public virtual Employee? Employee { get; set; }

		public DateTime OperationDate { get; set; } = DateTime.Now;

		public string? Notes { get; set; }
	}

	public enum OperationType
	{
		Приход = 1,
		Продажа = 2,
		Списание = 3
	}
}
