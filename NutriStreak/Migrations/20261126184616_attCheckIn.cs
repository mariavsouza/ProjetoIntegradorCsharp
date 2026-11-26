using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriStreak.Migrations
{
    /// <inheritdoc />
    public partial class attCheckIn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Refeicoes_UsuarioId",
                table: "Refeicoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_UsuarioId",
                table: "CheckIns",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Usuarios_UsuarioId",
                table: "CheckIns",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Refeicoes_Usuarios_UsuarioId",
                table: "Refeicoes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Usuarios_UsuarioId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_Refeicoes_Usuarios_UsuarioId",
                table: "Refeicoes");

            migrationBuilder.DropIndex(
                name: "IX_Refeicoes_UsuarioId",
                table: "Refeicoes");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_UsuarioId",
                table: "CheckIns");
        }
    }
}
