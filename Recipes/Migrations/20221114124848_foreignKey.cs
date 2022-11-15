using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recipes.Migrations
{
    public partial class foreignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_RecipeID",
                table: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "RecipeID",
                table: "Ingredient",
                newName: "FKIngredientRecipeID");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_RecipeID",
                table: "Ingredient",
                newName: "IX_Ingredient_FKIngredientRecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_FKIngredientRecipeID",
                table: "Ingredient",
                column: "FKIngredientRecipeID",
                principalTable: "Recipe",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Recipe_FKIngredientRecipeID",
                table: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "FKIngredientRecipeID",
                table: "Ingredient",
                newName: "RecipeID");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_FKIngredientRecipeID",
                table: "Ingredient",
                newName: "IX_Ingredient_RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_RecipeID",
                table: "Ingredient",
                column: "RecipeID",
                principalTable: "Recipe",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
