using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoServerApp.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: IdentityDbContext<ApplicationUser>(options)
	{
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Operation> Operations { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Seed данные для продуктов
			builder.Entity<Product>().HasData([
				new() { Id = 1, Name = "Молоко", SKU = "SKU001", Price = 80, Stock = 50, Category = "Молочные продукты", CreatedDate = new DateTime(2024, 12, 1) },
				new() { Id = 2, Name = "Хлеб", SKU = "SKU002", Price = 35, Stock = 100, Category = "Хлебопекарня", CreatedDate = new DateTime(2024, 12, 1) },
				new() { Id = 3, Name = "Сыр", SKU = "SKU003", Price = 250, Stock = 30, Category = "Молочные продукты", CreatedDate = new DateTime(2024, 12, 2) },
				new() { Id = 4, Name = "Яйца (дюжина)", SKU = "SKU004", Price = 120, Stock = 20, Category = "Птицеводство", CreatedDate = new DateTime(2024, 12, 2) }
			]);

			// Seed данные для сотрудников
			builder.Entity<Employee>().HasData([
				new() { Id = 1, Name = "Иван Петров", Position = "Кассир", Department = "Продажи", HireDate = new DateTime(2023, 1, 15) },
				new() { Id = 2, Name = "Мария Сидорова", Position = "Менеджер", Department = "Управление", HireDate = new DateTime(2022, 6, 1) },
				new() { Id = 3, Name = "Петр Иванов", Position = "Кладовщик", Department = "Склад", HireDate = new DateTime(2023, 8, 20) }
			]);

			// Seed данные для операций
			builder.Entity<Operation>().HasData([
				new() { Id = 1, Type = OperationType.Приход, ProductId = 1, Quantity = 50, EmployeeId = 3, OperationDate = new DateTime(2024, 12, 10), Notes = "Поступление с поставщика" },
				new() { Id = 2, Type = OperationType.Продажа, ProductId = 1, Quantity = 5, EmployeeId = 1, OperationDate = new DateTime(2024, 12, 11), Notes = "Продажа" },
				new() { Id = 3, Type = OperationType.Продажа, ProductId = 2, Quantity = 10, EmployeeId = 1, OperationDate = new DateTime(2024, 12, 12), Notes = "Продажа" }
			]);

			// Связи
			builder.Entity<Operation>()
				.HasOne(o => o.Product)
				.WithMany()
				.HasForeignKey(o => o.ProductId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Operation>()
				.HasOne(o => o.Employee)
				.WithMany()
				.HasForeignKey(o => o.EmployeeId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
