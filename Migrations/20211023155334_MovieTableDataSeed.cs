using Microsoft.EntityFrameworkCore.Migrations;

namespace YourMovies.Migrations
{
    public partial class MovieTableDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "Genre", "ImageUrl", "ReleaseYear", "Title" },
                values: new object[] { 1, "Gore Verbinski", "Adventure", "https://upload.wikimedia.org/wikipedia/en/5/5a/Pirates_AWE_Poster.jpg", 2007, "Pirates of the Caribbean: At world's end" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "Genre", "ImageUrl", "ReleaseYear", "Title" },
                values: new object[] { 2, "Andrew Adamson", "Animated/Adventure/Comedy", "https://upload.wikimedia.org/wikipedia/en/b/b9/Shrek_2_poster.jpg", 2004, "Shrek 2" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Director", "Genre", "ImageUrl", "ReleaseYear", "Title" },
                values: new object[] { 3, "James Cameron", "Romance/Drama", "https://upload.wikimedia.org/wikipedia/en/b/b9/Shrek_2_poster.jpg", 1997, "Titanic" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
