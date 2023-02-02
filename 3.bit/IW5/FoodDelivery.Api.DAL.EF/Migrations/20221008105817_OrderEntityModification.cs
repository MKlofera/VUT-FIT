using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Api.DAL.EF.Migrations
{
    public partial class OrderEntityModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Foods",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("0315248e-7dc9-4a06-9702-e01002eb25ec"),
                column: "Price",
                value: 21.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("11cf3324-14b1-4c5e-8547-b5d0319ad2f1"),
                column: "Price",
                value: 12.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("1ce5ac47-9247-4e4f-b51a-dadba6df5ad6"),
                column: "Price",
                value: 298.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("65712099-0f16-46e8-a747-ddd07255c6ad"),
                column: "Price",
                value: 298.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("8c7c72ea-03b6-4754-9924-f311db160aea"),
                column: "Price",
                value: 22.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"),
                column: "Price",
                value: 28.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("997e6b62-9e2a-4e80-ae84-67094ed58a14"),
                column: "Price",
                value: 98.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("9e1b5d5b-d581-475b-9b7e-527cb16b9abf"),
                column: "Price",
                value: 22.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("c6c88ffc-249e-4b56-a26e-4f6f674fddad"),
                column: "Price",
                value: 37.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("cfdbb65e-eaff-4be3-b42b-18724ef8c938"),
                column: "Price",
                value: 48.90m);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("ef6f07bb-63c0-4260-8264-447c97e4e2ca"),
                column: "Price",
                value: 8.90m);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("39ec26de-182e-4678-8c24-7c448be05a36"),
                columns: new[] { "CreatedDate", "DeliveryTime" },
                values: new object[] { new DateTime(2022, 10, 8, 12, 58, 16, 715, DateTimeKind.Local).AddTicks(8146), new DateTime(2022, 10, 8, 12, 58, 16, 718, DateTimeKind.Local).AddTicks(2467) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab"),
                columns: new[] { "CreatedDate", "DeliveryTime" },
                values: new object[] { new DateTime(2022, 10, 8, 12, 58, 16, 718, DateTimeKind.Local).AddTicks(8711), new DateTime(2022, 10, 8, 12, 58, 16, 718, DateTimeKind.Local).AddTicks(8728) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Orders");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Foods",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("0315248e-7dc9-4a06-9702-e01002eb25ec"),
                column: "Price",
                value: 21.899999999999999);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("11cf3324-14b1-4c5e-8547-b5d0319ad2f1"),
                column: "Price",
                value: 12.9);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("1ce5ac47-9247-4e4f-b51a-dadba6df5ad6"),
                column: "Price",
                value: 298.89999999999998);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("65712099-0f16-46e8-a747-ddd07255c6ad"),
                column: "Price",
                value: 298.89999999999998);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("8c7c72ea-03b6-4754-9924-f311db160aea"),
                column: "Price",
                value: 22.899999999999999);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"),
                column: "Price",
                value: 28.899999999999999);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("997e6b62-9e2a-4e80-ae84-67094ed58a14"),
                column: "Price",
                value: 98.900000000000006);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("9e1b5d5b-d581-475b-9b7e-527cb16b9abf"),
                column: "Price",
                value: 22.899999999999999);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("c6c88ffc-249e-4b56-a26e-4f6f674fddad"),
                column: "Price",
                value: 37.899999999999999);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("cfdbb65e-eaff-4be3-b42b-18724ef8c938"),
                column: "Price",
                value: 48.899999999999999);

            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("ef6f07bb-63c0-4260-8264-447c97e4e2ca"),
                column: "Price",
                value: 8.9000000000000004);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("39ec26de-182e-4678-8c24-7c448be05a36"),
                column: "DeliveryTime",
                value: new DateTime(2022, 9, 28, 17, 5, 52, 988, DateTimeKind.Local).AddTicks(2141));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab"),
                column: "DeliveryTime",
                value: new DateTime(2022, 9, 28, 17, 5, 52, 992, DateTimeKind.Local).AddTicks(2532));
        }
    }
}
