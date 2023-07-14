namespace All_Spice.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;
  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO recipes
    (title, instructions, img, category, creatorId)
    VALUES
    (@title, @instructions, @img, @category, @creatorId);

    SELECT
    rec.*,
    creator.*
    FROM recipes rec
    JOIN accounts creator ON rec.creatorId = creator.id
    WHERE rec.id = LAST_INSERT_ID();
    ";
    Recipe newRecipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
    {
      recipe.Creator = creator;
      return recipe;
    }, recipeData).FirstOrDefault();
    return newRecipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT
    rec.*,
    creator.* 
    FROM recipes rec
    JOIN accounts creator ON rec.creatorId = creator.id;
    ";
    List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
    {
      recipe.Creator = creator;
      return recipe;
    }).ToList();
    return recipes;
  }


  internal Recipe GetById(int recipeId)
  {
    string sql = @"
    SELECT
    alb.*,
    creator.*
    FROM recipes alb
    JOIN accounts creator ON alb.creatorId = creator.id
    WHERE alb.id = @recipeId;
    ";
    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
    {
      recipe.Creator = creator;
      return recipe;
    }, new { recipeId }).FirstOrDefault();
    return recipe;
  }

  internal void UpdateRecipe(Recipe updateData)
  {
    string sql = @"
    UPDATE recipes SET
    title = @title,
    instructions = @instructions,
    img = @img,
    category = @category,
    WHERE id = @id;";

    _db.Execute(sql, updateData);
  }
}
