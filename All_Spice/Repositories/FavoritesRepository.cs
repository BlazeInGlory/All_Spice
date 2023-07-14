namespace All_Spice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;
  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Favorite CreateFav(Favorite favData)
  {
    string sql = @"
    INSERT INTO favorites
    (accountId, recipeId)
    VALUES
    (@accountId, @albumId);
    SELECT LAST_INSERT_ID()
    ;";
    int lastInsertId = _db.ExecuteScalar<int>(sql, favData);
    favData.Id = lastInsertId;
    return favData;
  }

  internal int DeleteFavorite(int favId)
  {
    string sql = @"
    DELETE FROM favorites
    WHERE id = @favId
    LIMIT 1
    ;";
    int rows = _db.Execute(sql, new { favId });
    return rows;
  }

  internal Favorite GetById(int favId)
  {
    string sql = @"
    SELECT
    favs.*
    FROM favorites favs
    WHERE favs.id = @favId
    ;";
    Favorite fav = _db.Query<Favorite>(sql, new { favId }).FirstOrDefault();
    return fav;
  }

  // internal List<FavoriteAccount> GetFavoritesByRecipeId(int recipeId)
  // {
  //   string sql = @"
  //   SELECT
  //   favs.*,
  //   act.*
  //   FROM favorites favs
  //   JOIN accounts act ON act.id = favs.accountId
  //   WHERE favs.recipeId = @recipeId
  //   ;";
  //   List<FavoriteAccount> favs = _db.Query<Favorite, favoriteAccount, favoriteAccount>(sql, (fav, account) =>
  //   {
  //     account.FavoritingId = fav.Id;
  //     return account;
  //   }, new { recipeId }).ToList();
  //   return favs;
  // }
}