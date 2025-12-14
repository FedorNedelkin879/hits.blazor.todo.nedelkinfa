using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoServerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddInventorySystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "HireDate", "Name", "Position" },
                values: new object[,]
                {
                    { 1, "Продажи", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иван Петров", "Кассир" },
                    { 2, "Управление", new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Мария Сидорова", "Менеджер" },
                    { 3, "Склад", new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петр Иванов", "Кладовщик" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedDate", "Name", "Price", "SKU", "Stock" },
                values: new object[,]
                {
                    { 1, "Молочные продукты", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Молоко", 80m, "SKU001", 50 },
                    { 2, "Хлебопекарня", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Хлеб", 35m, "SKU002", 100 },
                    { 3, "Молочные продукты", new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сыр", 250m, "SKU003", 30 },
                    { 4, "Птицеводство", new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Яйца (дюжина)", 120m, "SKU004", 20 }
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Id", "EmployeeId", "Notes", "OperationDate", "ProductId", "Quantity", "Type" },
                values: new object[,]
                {
                    { 1, 3, "Поступление с поставщика", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 50, 1 },
                    { 2, 1, "Продажа", new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 2 },
                    { 3, 1, "Продажа", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 10, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_EmployeeId",
                table: "Operations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ProductId",
                table: "Operations",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TaskItems",
                columns: new[] { "Id", "CreatedDate", "Description", "FinishDate", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Описание задачи 1", null, "Задача 1" },
                    { 2, new DateTime(2024, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Описание задачи 2", null, "Задача 2" },
                    { 3, new DateTime(2024, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Описание задачи 3", null, "Задача 3" },
                    { 4, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Описание задачи 4", null, "Задача 4" }
                });
        }
    }
}
