using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodCalculator.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    BreakFast_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Lunch_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Dinner_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Week_Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "DayTemplates",
                columns: table => new
                {
                    DayNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Breakfast = table.Column<string>(type: "TEXT", nullable: false),
                    Lunch = table.Column<string>(type: "TEXT", nullable: false),
                    Dinner = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayTemplates", x => x.DayNumber);
                });

            migrationBuilder.CreateTable(
                name: "FoodList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Portions = table.Column<int>(type: "INTEGER", nullable: false),
                    Modifier = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FoodType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodList", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FoodTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "MealFillings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MF_Type = table.Column<string>(type: "TEXT", nullable: false),
                    FoodListOrder = table.Column<string>(type: "TEXT", nullable: true),
                    FoodQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Day_Date = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealFillings", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "DayTemplates");

            migrationBuilder.DropTable(
                name: "FoodList");

            migrationBuilder.DropTable(
                name: "FoodTypes");

            migrationBuilder.DropTable(
                name: "MealFillings");
        }
    }
}
