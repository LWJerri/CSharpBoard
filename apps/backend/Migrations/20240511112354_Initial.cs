using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data
{
  /// <inheritdoc />
  public partial class Initial : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterDatabase()
          .Annotation("Npgsql:Enum:priority_enum", "low,normal,high,critical");

      migrationBuilder.CreateTable(
          name: "lists",
          columns: table => new
          {
            id = table.Column<string>(type: "text", nullable: false),
            name = table.Column<string>(type: "text", nullable: false),
            created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_lists", x => x.id);
          });

      migrationBuilder.CreateTable(
          name: "tasks",
          columns: table => new
          {
            id = table.Column<string>(type: "text", nullable: false),
            title = table.Column<string>(type: "text", nullable: false),
            description = table.Column<string>(type: "text", nullable: false),
            due_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            priority = table.Column<int>(type: "integer", nullable: false),
            created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            list_id = table.Column<string>(type: "text", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_tasks", x => x.id);
            table.ForeignKey(
                      name: "FK_tasks_lists_list_id",
                      column: x => x.list_id,
                      principalTable: "lists",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_tasks_list_id",
          table: "tasks",
          column: "list_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "tasks");

      migrationBuilder.DropTable(
          name: "lists");
    }
  }
}