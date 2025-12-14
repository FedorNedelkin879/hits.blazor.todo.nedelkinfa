using Microsoft.EntityFrameworkCore;
using TodoServerApp.Data.Interfaces;

namespace TodoServerApp.Data.Services
{
	public class MSSQLDataService(ApplicationDbContext context) : IDataService
	{
		// ========== ТОВАРЫ ==========
		public async Task<IEnumerable<Product>> GetAllProductsAsync()
		{
			return await context.Products.OrderBy(p => p.Name).ToListAsync();
		}

		public async Task<Product> GetProductAsync(int id)
		{
			return await context.Products.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException($"Товар с ID {id} не найден");
		}

		public async Task SaveProductAsync(Product product)
		{
			if (product.Id == 0)
			{
				product.CreatedDate = DateTime.Now;
				await context.Products.AddAsync(product);
			}
			else
			{
				context.Products.Update(product);
			}
			await context.SaveChangesAsync();
		}

		public async Task DeleteProductAsync(int id)
		{
			var product = await GetProductAsync(id);
			context.Products.Remove(product);
			await context.SaveChangesAsync();
		}

		// ========== СОТРУДНИКИ ==========
		public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
		{
			return await context.Employees.OrderBy(e => e.Name).ToListAsync();
		}

		public async Task<Employee> GetEmployeeAsync(int id)
		{
			return await context.Employees.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException($"Сотрудник с ID {id} не найден");
		}

		public async Task SaveEmployeeAsync(Employee employee)
		{
			if (employee.Id == 0)
			{
				employee.HireDate = DateTime.Now;
				await context.Employees.AddAsync(employee);
			}
			else
			{
				context.Employees.Update(employee);
			}
			await context.SaveChangesAsync();
		}

		public async Task DeleteEmployeeAsync(int id)
		{
			var employee = await GetEmployeeAsync(id);
			context.Employees.Remove(employee);
			await context.SaveChangesAsync();
		}

		// ========== ОПЕРАЦИИ ==========
		public async Task<IEnumerable<Operation>> GetAllOperationsAsync()
		{
			return await context.Operations
				.Include(o => o.Product)
				.Include(o => o.Employee)
				.OrderByDescending(o => o.OperationDate)
				.ToListAsync();
		}

		public async Task<Operation> GetOperationAsync(int id)
		{
			return await context.Operations
				.Include(o => o.Product)
				.Include(o => o.Employee)
				.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException($"Операция с ID {id} не найдена");
		}

		public async Task SaveOperationAsync(Operation operation)
		{
			if (operation.Id == 0)
			{
				operation.OperationDate = DateTime.Now;

				// Обновляем остаток товара
				var product = await GetProductAsync(operation.ProductId);
				if (operation.Type == OperationType.Приход)
				{
					product.Stock += operation.Quantity;
				}
				else if (operation.Type == OperationType.Продажа || operation.Type == OperationType.Списание)
				{
					product.Stock -= operation.Quantity;
				}

				await context.Operations.AddAsync(operation);
				context.Products.Update(product);
			}
			else
			{
				context.Operations.Update(operation);
			}
			await context.SaveChangesAsync();
		}

		public async Task DeleteOperationAsync(int id)
		{
			var operation = await GetOperationAsync(id);
			context.Operations.Remove(operation);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Operation>> GetOperationsByProductAsync(int productId)
		{
			return await context.Operations
				.Include(o => o.Product)
				.Include(o => o.Employee)
				.Where(o => o.ProductId == productId)
				.OrderByDescending(o => o.OperationDate)
				.ToListAsync();
		}

		public async Task<IEnumerable<Operation>> GetOperationsByDateRangeAsync(DateTime startDate, DateTime endDate)
		{
			return await context.Operations
				.Include(o => o.Product)
				.Include(o => o.Employee)
				.Where(o => o.OperationDate >= startDate && o.OperationDate <= endDate)
				.OrderByDescending(o => o.OperationDate)
				.ToListAsync();
		}

		// ========== ОТЧЁТЫ ==========
		public async Task<decimal> GetTotalSalesAsync(DateTime startDate, DateTime endDate)
		{
			return await context.Operations
				.Where(o => o.Type == OperationType.Продажа &&
						   o.OperationDate >= startDate &&
						   o.OperationDate <= endDate)
				.Include(o => o.Product)
				.SumAsync(o => o.Quantity * o.Product!.Price);
		}

		public async Task<IEnumerable<ProductStockReport>> GetStockReportAsync()
		{
			return await context.Products
				.Select(p => new ProductStockReport
				{
					ProductId = p.Id,
					ProductName = p.Name,
					Category = p.Category,
					CurrentStock = p.Stock,
					Price = p.Price,
					TotalValue = p.Stock * p.Price
				})
				.OrderBy(r => r.ProductName)
				.ToListAsync();
		}

		public async Task<IEnumerable<SalesReport>> GetSalesReportAsync(DateTime startDate, DateTime endDate)
		{
			return await context.Operations
				.Where(o => o.Type == OperationType.Продажа &&
						   o.OperationDate >= startDate &&
						   o.OperationDate <= endDate)
				.Include(o => o.Product)
				.GroupBy(o => o.ProductId)
				.Select(g => new SalesReport
				{
					ProductId = g.Key,
					ProductName = g.First().Product!.Name,
					QuantitySold = g.Sum(o => o.Quantity),
					Revenue = g.Sum(o => o.Quantity * o.Product!.Price)
				})
				.OrderByDescending(r => r.Revenue)
				.ToListAsync();
		}
	}
}
