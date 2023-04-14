using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdaptiveEnglishTrainer.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "16c9c0ce-04ca-43a5-9635-f8d46f80a323", "5a033aaa-6293-413e-9c8e-554988105c2e", "Teacher", "TEACHER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "83cb0625-55e9-4bac-b5c1-dd28d7283cb6", "7ea663a5-f4d9-4da5-91fb-ef4b43334115", "Student", "STUDENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16c9c0ce-04ca-43a5-9635-f8d46f80a323");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83cb0625-55e9-4bac-b5c1-dd28d7283cb6");
        }
    }
}
