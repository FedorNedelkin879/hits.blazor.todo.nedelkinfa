using System.ComponentModel.DataAnnotations;

namespace TodoServerApp.Data
{
	public class Employee
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Имя сотрудника обязательно")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Должность обязательна")]
		public string? Position { get; set; }

		[Required(ErrorMessage = "Отдел обязателен")]
		public string? Department { get; set; }

		public DateTime HireDate { get; set; } = DateTime.Now;
	}
}
