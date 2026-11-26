using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriStreak.Migrations
{
    /// <inheritdoc />
    public partial class attPlanejamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Planejamentos_UsuarioId",
                table: "Planejamentos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planejamentos_Usuarios_UsuarioId",
                table: "Planejamentos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planejamentos_Usuarios_UsuarioId",
                table: "Planejamentos");

            migrationBuilder.DropIndex(
                name: "IX_Planejamentos_UsuarioId",
                table: "Planejamentos");
        }
    }
}
