using AllSpice.Models;

namespace All_Spice.Repositories;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;

  public IngredientsService(IngredientsRepository repo)
  {
    _repo = repo;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient newIngredient = _repo.CreateIngredient(ingredientData);
    return newIngredient;
  }

  internal Ingredient GetById(int ingredientId)
  {
    Ingredient ingredient = _repo.GetById(ingredientId);
    if (ingredient == null) new Exception("Invalid Id");
    return ingredient;
  }

  internal void DeleteIngredient(int ingredientId, string userId)
  {
    Ingredient ingredient = GetById(ingredientId);
    if (ingredient.CreatorId != userId) new Exception("Not your ingredient to delete pal");
    int rows = _repo.DeleteIngredient(ingredientId);
    if (rows > 1) new Exception("Uh oh..We deleted more than 1");
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }
}