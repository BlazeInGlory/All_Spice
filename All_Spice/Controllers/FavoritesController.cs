namespace All_Spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth;

  public FavoritesController(FavoritesService favoritesService, Auth0Provider auth)
  {
    _favoritesService = favoritesService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Favorite>> CreateFav([FromBody] Favorite favData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      favData.AccountId = userInfo.Id;
      Favorite newFav = _favoritesService.CreateFav(favData);
      return Ok(newFav);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{favId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteFavorite(int favId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      _favoritesService.DeleteFavorite(favId, userInfo.Id);
      return Ok("No Mo Fav");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
