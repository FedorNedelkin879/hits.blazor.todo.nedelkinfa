using TodoServerApp.Data.Interfaces;

namespace TodoServerApp.Data.Services
{
	public class MemoryDataService : IDataService
	{
		// Заглушки для памяти
		private static List<Product> Products { get; } = [];
		private static List<Employee> Employees { get; } = [];
		private static List<Operation> Operations { get; } = [];

		// ========== ТОВАРЫ ==========
		public Task<IEnumerable<Product>> GetAllProductsAsync()
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task<Product> GetProductAsync(int id)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task SaveProductAsync(Product product)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task DeleteProductAsync(int id)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		// ========== СОТРУДНИКИ ==========
		public Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task<Employee> GetEmployeeAsync(int id)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task SaveEmployeeAsync(Employee employee)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task DeleteEmployeeAsync(int id)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		// ========== ОПЕРАЦИИ ==========
		public Task<IEnumerable<Operation>> GetAllOperationsAsync()
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task<Operation> GetOperationAsync(int id)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task SaveOperationAsync(Operation operation)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task DeleteOperationAsync(int id)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task<IEnumerable<Operation>> GetOperationsByProductAsync(int productId)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task<IEnumerable<Operation>> GetOperationsByDateRangeAsync(DateTime startDate, DateTime endDate)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		// ========== ОТЧЁТЫ ==========
		public Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task<IEnumerable<ProductStockReport>> GetStockReportAsync()
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}

		public Task<IEnumerable<SalesReport>> GetSalesReportAsync(DateTime startDate, DateTime endDate)
		{
			throw new NotImplementedException("Используй MSSQLDataService");
		}
	}
}
