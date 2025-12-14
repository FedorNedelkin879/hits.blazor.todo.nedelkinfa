namespace TodoServerApp.Data.Interfaces
{
	public interface IDataService
	{
		// Товары
		Task<IEnumerable<Product>> GetAllProductsAsync();
		Task<Product> GetProductAsync(int id);
		Task SaveProductAsync(Product product);
		Task DeleteProductAsync(int id);

		// Сотрудники
		Task<IEnumerable<Employee>> GetAllEmployeesAsync();
		Task<Employee> GetEmployeeAsync(int id);
		Task SaveEmployeeAsync(Employee employee);
		Task DeleteEmployeeAsync(int id);

		// Операции
		Task<IEnumerable<Operation>> GetAllOperationsAsync();
		Task<Operation> GetOperationAsync(int id);
		Task SaveOperationAsync(Operation operation);
		Task DeleteOperationAsync(int id);
		Task<IEnumerable<Operation>> GetOperationsByProductAsync(int productId);
		Task<IEnumerable<Operation>> GetOperationsByDateRangeAsync(DateTime startDate, DateTime endDate);

		// Отчёты
		Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate);
		Task<IEnumerable<ProductStockReport>> GetStockReportAsync();
		Task<IEnumerable<SalesReport>> GetSalesReportAsync(DateTime startDate, DateTime endDate);
	}

	// Классы для отчётов
	public class ProductStockReport
	{
		public int ProductId { get; set; }
		public string? ProductName { get; set; }
		public string? Category { get; set; }
		public int CurrentStock { get; set; }
		public decimal Price { get; set; }
		public decimal TotalValue { get; set; }
	}

	public class SalesReport
	{
		public int ProductId { get; set; }
		public string? ProductName { get; set; }
		public int QuantitySold { get; set; }
		public decimal Revenue { get; set; }
	}
}
