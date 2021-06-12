using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot_Net_Core_API_with_JWT.Models;
using Dot_Net_Core_API_with_JWT.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_Core_API_with_JWT.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CharacterController : ControllerBase //Base class for MVC Controller
  {
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
      _characterService = characterService; // DependencyInjection from DTO Character Service
    }

    [HttpGet("getall")] // Route character/getall
    public async Task<ActionResult<ServiceResponse<List<Character>>>> Get() //Swagger needs => ActionResult<Character> Else Can use IActionResult
    { 
      return Ok(await _characterService.GetAllCharacters()); //Return a list of characters
    }

    [HttpGet("{id}")] // Route character/id
    public async Task<ActionResult<ServiceResponse<Character>>> GetSingle(int id)
    {
      return Ok(await _characterService.GetCharacterById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Character>>>> AddCharacter(Character newCharacter)
    {      
      return Ok(await _characterService.AddCharacter(newCharacter));
    }
  }
}
