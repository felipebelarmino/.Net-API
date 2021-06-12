using Dot_Net_Core_API_with_JWT.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_Core_API_with_JWT.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CharacterController : ControllerBase //Base class for MVC Controller
  {
    private static Character Knight = new Character(); //Using model Character

    [HttpGet]
    public ActionResult<Character> Get() //Swagger needs => ActionResult<Character> Else Can use IActionResult
    {
      return Ok(Knight);
    }
  }
}