using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroDetalle2.Migrations
{
    public partial class Migracion_Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    PermisoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    VecesAsignado = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.PermisoId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    esActivo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "RolesDetalle",
                columns: table => new
                {
                    RolDetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RolId = table.Column<int>(type: "INTEGER", nullable: false),
                    PermisoId = table.Column<int>(type: "INTEGER", nullable: false),
                    esAsignado = table.Column<bool>(type: "INTEGER", nullable: false),
                    DescripcionPermiso = table.Column<string>(type: "TEXT", nullable: true),
                    VecesAsignado = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesDetalle", x => x.RolDetalleId);
                    table.ForeignKey(
                        name: "FK_RolesDetalle_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "PermisoId", "Descripcion", "Nombre", "VecesAsignado" },
                values: new object[] { 1, "Permiso para ser administrador del sistema", "Administrador", 0 });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "PermisoId", "Descripcion", "Nombre", "VecesAsignado" },
                values: new object[] { 2, "Permiso para entrar como usuario al sistema", "Usuario", 0 });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "PermisoId", "Descripcion", "Nombre", "VecesAsignado" },
                values: new object[] { 3, "Permiso para entrar como invitado del sistema", "Invitado", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_RolesDetalle_RolId",
                table: "RolesDetalle",
                column: "RolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "RolesDetalle");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
