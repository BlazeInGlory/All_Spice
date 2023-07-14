using AllSpice.Models;

namespace All_Spice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth;

  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0)
  {
    _ingredientsService = ingredientsService;
    _auth = auth0;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      ingredientData.CreatorId = userInfo.Id;
      Ingredient newIngredient = _ingredientsService.CreateIngredient(ingredientData);
      return Ok(newIngredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("ingredientId")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      _ingredientsService.DeleteIngredient(ingredientId, userInfo.Id);
      return Ok("The ingredient is no more!");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}