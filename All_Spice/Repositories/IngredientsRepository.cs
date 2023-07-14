using AllSpice.Models;

namespace All_Spice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;
  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients
    (name, quantity, recipeId, creatorId)
    VALUES
    (@name, @quantity, @recipeId, @creatorId);

    SELECT
    ing.*,
    act.*
    FROM ingredients ing
    JOIN accounts act ON act.id = ing.creatorId
    WHERE ing.id = LAST_INSERT_ID()
    ;";

    Ingredient newIngredient = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
    {
      ingredient.Creator = account;
      return ingredient;
    }, ingredientData).FirstOrDefault();
    return newIngredient;
  }

  internal Ingredient GetById(int ingredientId)
  {
    string sql = @"
    SELECT
    ing.*,
    act.*
    FROM ingredients ing
    JOIN accounts act ON act.id = ing.creatorId
    WHERE ing.id = @ingredientId
    ;";
    Ingredient ingredient = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
    {
      ingredient.Creator = account;
      return ingredient;
    }, new { ingredientId }).FirstOrDefault();
    return ingredient;
  }

  internal int DeleteIngredient(int ingredientId)
  {
    string sql = @"
    DELETE FROM ingredients
    WHERE id = @ingredientId
    LIMIT 1
    ;";
    int rows = _db.Execute(sql, new { ingredientId });
    return rows;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    ing.*,
    act.*
    FROM ingredients ing
    JOIN accounts act ON act.id = ing.creatorId
    WHERE ing.recipeId = @recipeId
    ;";
    List<Ingredient> recipeIngredients = _db.Query<Ingredient, Account, Ingredient>(sql, (ingredient, account) =>
    {
      ingredient.Creator = account;
      return ingredient;
    }, new { recipeId }).ToList();
    return recipeIngredients;
  }
}
