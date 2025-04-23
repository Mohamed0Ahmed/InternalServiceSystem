using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Guests_PhoneNumber",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Guests_PhoneNumber",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PointsSettings_Stores_StoreId",
                table: "PointsSettings");

            migrationBuilder.DropTable(
                name: "GuestPoints");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_BranchId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_PointsSettings_StoreId",
                table: "PointsSettings");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PhoneNumber",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_PhoneNumber",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PointsSettings");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "HelpRequests");

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Rooms",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GuestId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPriceAtOrderTime",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "HelpRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GuestId",
                table: "HelpRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Guests",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Guests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Guests",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredPoints = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rewards_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPoints_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerPoints_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BranchId_RoomName",
                table: "Rooms",
                columns: new[] { "BranchId", "RoomName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_GuestId",
                table: "Orders",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_CustomerId",
                table: "HelpRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_GuestId",
                table: "HelpRequests",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_Username",
                table: "Guests",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPoints_BranchId",
                table: "CustomerPoints",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPoints_CreatedOn",
                table: "CustomerPoints",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPoints_CustomerId_BranchId",
                table: "CustomerPoints",
                columns: new[] { "CustomerId", "BranchId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPoints_LastModifiedOn",
                table: "CustomerPoints",
                column: "LastModifiedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_StoreId",
                table: "Customers",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_BranchId",
                table: "Rewards",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_CreatedOn",
                table: "Rewards",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_LastModifiedOn",
                table: "Rewards",
                column: "LastModifiedOn");

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Customers_CustomerId",
                table: "HelpRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Guests_GuestId",
                table: "HelpRequests",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Customers_CustomerId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpRequests_Guests_GuestId",
                table: "HelpRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Guests_GuestId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "CustomerPoints");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_BranchId_RoomName",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_GuestId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_CustomerId",
                table: "HelpRequests");

            migrationBuilder.DropIndex(
                name: "IX_HelpRequests_GuestId",
                table: "HelpRequests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_Username",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPriceAtOrderTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "HelpRequests");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Guests");

            migrationBuilder.AlterColumn<string>(
                name: "RoomName",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "PointsSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Orders",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "HelpRequests",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Guests",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "GuestPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 20, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuestPoints_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuestPoints_Guests_PhoneNumber",
                        column: x => x.PhoneNumber,
                        principalTable: "Guests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BranchId",
                table: "Rooms",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_PointsSettings_StoreId",
                table: "PointsSettings",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PhoneNumber",
                table: "Orders",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_PhoneNumber",
                table: "HelpRequests",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_GuestPoints_BranchId",
                table: "GuestPoints",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_GuestPoints_CreatedOn",
                table: "GuestPoints",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_GuestPoints_LastModifiedOn",
                table: "GuestPoints",
                column: "LastModifiedOn");

            migrationBuilder.CreateIndex(
                name: "IX_GuestPoints_PhoneNumber_BranchId",
                table: "GuestPoints",
                columns: new[] { "PhoneNumber", "BranchId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpRequests_Guests_PhoneNumber",
                table: "HelpRequests",
                column: "PhoneNumber",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Guests_PhoneNumber",
                table: "Orders",
                column: "PhoneNumber",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PointsSettings_Stores_StoreId",
                table: "PointsSettings",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
