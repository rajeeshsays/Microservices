using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdhiSreeTransportService.Migrations
{
    /// <inheritdoc />
    public partial class destinationgrps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportEntry_DestinationGroups_DestinationGroupId",
                table: "TransportEntry");

            migrationBuilder.DropIndex(
                name: "IX_TransportEntry_DestinationGroupId",
                table: "TransportEntry");

            migrationBuilder.AlterColumn<short>(
                name: "DestinationId",
                table: "DestinationGroups",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<long>(
                name: "DestinationGroupId",
                table: "DestinationGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DestinationGroups_DestinationGroupId",
                table: "DestinationGroups",
                column: "DestinationGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationGroups_DestinationId",
                table: "DestinationGroups",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationGroups_Party_DestinationId",
                table: "DestinationGroups",
                column: "DestinationId",
                principalTable: "Party",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationGroups_TransportEntry_DestinationGroupId",
                table: "DestinationGroups",
                column: "DestinationGroupId",
                principalTable: "TransportEntry",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DestinationGroups_Party_DestinationId",
                table: "DestinationGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_DestinationGroups_TransportEntry_DestinationGroupId",
                table: "DestinationGroups");

            migrationBuilder.DropIndex(
                name: "IX_DestinationGroups_DestinationGroupId",
                table: "DestinationGroups");

            migrationBuilder.DropIndex(
                name: "IX_DestinationGroups_DestinationId",
                table: "DestinationGroups");

            migrationBuilder.DropColumn(
                name: "DestinationGroupId",
                table: "DestinationGroups");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "DestinationGroups",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.CreateIndex(
                name: "IX_TransportEntry_DestinationGroupId",
                table: "TransportEntry",
                column: "DestinationGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportEntry_DestinationGroups_DestinationGroupId",
                table: "TransportEntry",
                column: "DestinationGroupId",
                principalTable: "DestinationGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
