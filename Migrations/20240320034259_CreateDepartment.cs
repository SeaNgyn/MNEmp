using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFormL1.Migrations
{
    /// <inheritdoc />
    public partial class CreateDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 1,
                column: "DepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 2,
                column: "DepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 3,
                column: "DepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 4,
                column: "DepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 5,
                column: "DepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 6,
                column: "DepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 7,
                column: "DepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 8,
                column: "DepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 9,
                column: "DepartmentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "iD",
                keyValue: 10,
                column: "DepartmentId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Degrees",
                newName: "Id");
        }
    }
}
