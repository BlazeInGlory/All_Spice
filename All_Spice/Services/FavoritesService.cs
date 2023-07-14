namespace All_Spice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;
  public FavoritesService(FavoritesRepository repo)
  {
    _repo = repo;
  }

  internal Favorite CreateFav(Favorite favData)
  {
    Favorite newFav = _repo.CreateFav(favData);
    return newFav;
  }

  internal void DeleteFavorite(int favId, string userId)
  {
    Favorite fav = GetById(favId);
    if (fav.AccountId != userId) new Exception("Unauthorized to remove from Favs");
    int rows = _repo.DeleteFavorite(favId);
    if (rows > 1) new Exception("Something went wrong");
  }

  internal Favorite GetById(int favId)
  {
    Favorite fav = _repo.GetById(favId);
    if (fav == null) new Exception("Invalid Id");
    return fav;
  }

  // internal List<FavoriteAccount> GetFavoritesByRecipeId(int recipeId)
  // {
  //   List<FavoriteAccount> favs = _repo.GetFavoritesByRecipeId(recipeId);
  //   return favs;
  // }
}