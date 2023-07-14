namespace All_Spice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _repo.CreateRecipe(recipeData);
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }

  internal Recipe GetById(int recipeId)
  {
    Recipe recipe = _repo.GetById(recipeId);
    if (recipe == null) throw new Exception($"No recipe here..");
    return recipe;
  }

  internal Recipe UpdateRecipe(Recipe updateData)
  {
    Recipe original = GetById(updateData.Id);
    // if (updateData.CreatorId != userId) throw new Exception("Nacho post");

    original.Title = updateData.Title != null ? updateData.Title : original.Title;
    original.Img = updateData.Img != null ? updateData.Img : original.Img;
    original.Category = updateData.Category != null ? updateData.Category : original.Category;
    original.Instructions = updateData.Instructions != null ? updateData.Instructions : original.Instructions;

    _repo.UpdateRecipe(original);
    return original;
  }

  internal Recipe ArchiveRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetById(recipeId);
    if (recipe.CreatorId != userId) throw new Exception("Nacho recipe");
    recipe.Archived = !recipe.Archived;
    _repo.UpdateRecipe(recipe);
    return recipe;
  }
}