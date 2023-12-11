using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TakidReciveForm.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddImageService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Forms",
                newName: "ImageName");

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Forms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Forms");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Forms",
                newName: "Image");
        }
    }
}
