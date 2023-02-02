using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Api.DAL.EF.Migrations
{
    public partial class Seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Description", "Latitude", "Logo", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("274d6393-6e49-4fcd-a2d0-ca79be190ef5"), "Údolní 4 Brno-město", "OCHUTNEJTE PRAVOU ITALSKOU KUCHYNI", 12.32123, null, 41.112341999999998, "Pizzerie La Famiglia Brno" },
                    { new Guid("4f27d553-adbe-4daf-9c12-9d9ee4c3fd2d"), "1234 Main St, Anytown, USA", "Eminem's restaurant Mom's Spaghetti to get Super Bowl pop-up Today.", -6.9857230000000001, null, 139.345, "Mom's spaghetti" },
                    { new Guid("a03f7047-9c07-4be9-841f-319054699811"), "Jackson street", "THE TRADITIONAL BRITISH KITCHEN", -56.514123400000003, null, 111.34542, "Borgo Agnese" },
                    { new Guid("dec40972-ec8b-432e-8b66-d8577b45b2fc"), "Neo's street 14", "Sushi, které se rozplývá na jazyku", 46.357230000000001, null, 12.345420000000001, "Koishi fish & sushi Restaurant" },
                    { new Guid("ee3205ef-f3f3-4693-a74f-6e62a95587db"), "Butcher street 4", "Itallian pizza and burgers", 16.223310000000001, null, 89.231399999999994, "Amici Pizza & Burgers" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Allergens", "Description", "Name", "Photo", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { new Guid("0315248e-7dc9-4a06-9702-e01002eb25ec"), "Eggs,Milk,Peanuts", "tomato sauce, mozzarella", "Margherita", "https://carusopizza.cz/630-home_default/margherita.jpg", 21.899999999999999, new Guid("ee3205ef-f3f3-4693-a74f-6e62a95587db") },
                    { new Guid("11cf3324-14b1-4c5e-8547-b5d0319ad2f1"), "Eggs,Milk,Peanuts", "Spaghetti with meatballs, just like mom used to make.", "Spaghetti With Balls", "https://www.gannett-cdn.com/presto/2021/10/01/PDTN/6ecbea6f-940c-419c-9981-fe1eff36d7ae-moms_spaghetti_review_2.jpg", 12.9, new Guid("4f27d553-adbe-4daf-9c12-9d9ee4c3fd2d") },
                    { new Guid("1ce5ac47-9247-4e4f-b51a-dadba6df5ad6"), "", "according to our daily offer", "Wagyu", "https://pavillonsteakhouse.cz/wp-content/uploads/2022/03/Pavillon-08.jpg", 298.89999999999998, new Guid("a03f7047-9c07-4be9-841f-319054699811") },
                    { new Guid("65712099-0f16-46e8-a747-ddd07255c6ad"), "Eggs,Milk", "tomato sauce, mozzarella, ham, mushrooms", "Prosciutto Funghi", "https://carusopizza.cz/670-large_default/prosciutto-funghi.jpg", 298.89999999999998, new Guid("274d6393-6e49-4fcd-a2d0-ca79be190ef5") },
                    { new Guid("8c7c72ea-03b6-4754-9924-f311db160aea"), "Eggs,Milk,Peanuts,Crustaceans", "tomato sauce, mozzarella, italian salami napoli, italian salami spianata", "Salami", "https://carusopizza.cz/641-home_default/salami.jpg", 22.899999999999999, new Guid("274d6393-6e49-4fcd-a2d0-ca79be190ef5") },
                    { new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "Sulphur,Lupin", "cream, mozzarella, gorgonzola, camembert,  smoked cheese", "quattro formaggi", "https://carusopizza.cz/635-large_default/4-formaggi.jpg", 28.899999999999999, new Guid("ee3205ef-f3f3-4693-a74f-6e62a95587db") },
                    { new Guid("997e6b62-9e2a-4e80-ae84-67094ed58a14"), "Molluscs,Milk,Lupin,Sulphur,Mustard", "the most tender cut of meat from the carcass. the meat stays exceptionally tender.", "TunaSteak", "https://pavillonsteakhouse.cz/wp-content/uploads/2020/12/Pavilon-002.jpg", 98.900000000000006, new Guid("a03f7047-9c07-4be9-841f-319054699811") },
                    { new Guid("9e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "Sulphur,Lupin", "tomato sauce, mozzarella, ham, bacon, pepper salami", "Golosona", "https://carusopizza.cz/637-home_default/golosona.jpg", 22.899999999999999, new Guid("ee3205ef-f3f3-4693-a74f-6e62a95587db") },
                    { new Guid("c6c88ffc-249e-4b56-a26e-4f6f674fddad"), "Soybeans,Molluscs,Fish,Nuts,Celery", "tomato sauce, mozzarella, ham, bacon, pepper salami", "MAKI", "https://www.hatsurestaurant.cz/wp-content/uploads/2022/05/maki-min-1-1024x726.jpeg", 37.899999999999999, new Guid("dec40972-ec8b-432e-8b66-d8577b45b2fc") },
                    { new Guid("cfdbb65e-eaff-4be3-b42b-18724ef8c938"), "Sesame,Nuts,Celery,Molluscs", "Technically, sashimi is not a type of sushi because it does not contain any rice. ", "SASHIMI", "https://www.hatsurestaurant.cz/wp-content/uploads/2022/05/maki-min-1-1024x726.jpeg", 48.899999999999999, new Guid("dec40972-ec8b-432e-8b66-d8577b45b2fc") },
                    { new Guid("ef6f07bb-63c0-4260-8264-447c97e4e2ca"), "Gluten,Crustaceans,Fish", "Bolognese Spaghetti, just like mom used to make.", "Bolognese Spaghetti", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRWfBr1XVoKa0ItKifkeIPLM_QtSKopCQitFA&usqp=CAU", 8.9000000000000004, new Guid("4f27d553-adbe-4daf-9c12-9d9ee4c3fd2d") }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Address", "DeliveryTime", "Description", "Name", "OrderState", "RestaurantId" },
                values: new object[,]
                {
                    { new Guid("39ec26de-182e-4678-8c24-7c448be05a36"), "1234 Main st. China town", new DateTime(2022, 9, 28, 17, 5, 52, 988, DateTimeKind.Local).AddTicks(2141), "ASAP please, I'm hungry", "Marek Klofera", 1, new Guid("ee3205ef-f3f3-4693-a74f-6e62a95587db") },
                    { new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab"), "1234 Main st. Metrix", new DateTime(2022, 9, 28, 17, 5, 52, 992, DateTimeKind.Local).AddTicks(2532), "Metrix is cool", "Mr. Anderson", 2, new Guid("ee3205ef-f3f3-4693-a74f-6e62a95587db") }
                });

            migrationBuilder.InsertData(
                table: "FoodOrderNotes",
                columns: new[] { "Id", "FoodId", "Note", "OrderId" },
                values: new object[,]
                {
                    { new Guid("0751cbb9-f7f5-4e56-a0c0-c19b5ee3e450"), new Guid("9e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "No onions please", new Guid("39ec26de-182e-4678-8c24-7c448be05a36") },
                    { new Guid("0f2c0750-4de7-4944-8581-8f038726203e"), new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "No cheese please", new Guid("39ec26de-182e-4678-8c24-7c448be05a36") },
                    { new Guid("58cb9ade-2ee2-428b-b208-100fc981d29c"), new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "", new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab") },
                    { new Guid("6ccac066-8ea4-4be3-a9ea-2301d6df84a6"), new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"), "Double Cheese", new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab") },
                    { new Guid("b03b189f-f4f0-49bd-bb0b-7f56d4548b68"), new Guid("65712099-0f16-46e8-a747-ddd07255c6ad"), "No ham please, extra corn", new Guid("39ec26de-182e-4678-8c24-7c448be05a36") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("0751cbb9-f7f5-4e56-a0c0-c19b5ee3e450"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("0f2c0750-4de7-4944-8581-8f038726203e"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("58cb9ade-2ee2-428b-b208-100fc981d29c"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("6ccac066-8ea4-4be3-a9ea-2301d6df84a6"));

            migrationBuilder.DeleteData(
                table: "FoodOrderNotes",
                keyColumn: "Id",
                keyValue: new Guid("b03b189f-f4f0-49bd-bb0b-7f56d4548b68"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("0315248e-7dc9-4a06-9702-e01002eb25ec"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("11cf3324-14b1-4c5e-8547-b5d0319ad2f1"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("1ce5ac47-9247-4e4f-b51a-dadba6df5ad6"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("8c7c72ea-03b6-4754-9924-f311db160aea"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("997e6b62-9e2a-4e80-ae84-67094ed58a14"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("c6c88ffc-249e-4b56-a26e-4f6f674fddad"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("cfdbb65e-eaff-4be3-b42b-18724ef8c938"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("ef6f07bb-63c0-4260-8264-447c97e4e2ca"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("65712099-0f16-46e8-a747-ddd07255c6ad"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"));

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: new Guid("9e1b5d5b-d581-475b-9b7e-527cb16b9abf"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("39ec26de-182e-4678-8c24-7c448be05a36"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("7573e0e4-7da0-4aa9-860b-395610bd0eab"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("4f27d553-adbe-4daf-9c12-9d9ee4c3fd2d"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("a03f7047-9c07-4be9-841f-319054699811"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("dec40972-ec8b-432e-8b66-d8577b45b2fc"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("274d6393-6e49-4fcd-a2d0-ca79be190ef5"));

            migrationBuilder.DeleteData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: new Guid("ee3205ef-f3f3-4693-a74f-6e62a95587db"));
        }
    }
}
