using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DientesLimpios.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class RenombrarTablaConsultorios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Consultorioes",
                table: "Consultorioes");

            migrationBuilder.RenameTable(
                name: "Consultorioes",
                newName: "Consultorios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consultorios",
                table: "Consultorios",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Consultorios",
                table: "Consultorios");

            migrationBuilder.RenameTable(
                name: "Consultorios",
                newName: "Consultorioes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consultorioes",
                table: "Consultorioes",
                column: "Id");
        }
    }
}
