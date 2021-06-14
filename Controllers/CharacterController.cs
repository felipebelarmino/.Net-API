using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dot_Net_Core_API_with_JWT.Dtos.Character;
using Dot_Net_Core_API_with_JWT.Models;
using Dot_Net_Core_API_with_JWT.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_Core_API_with_JWT.Controllers
{
  [Authorize(Roles = "Player")]
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
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get() //Swagger needs => ActionResult<Character> Else Can use IActionResult
    { 
      // int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
      return Ok(await _characterService.GetAllCharacters()); //Return a list of characters
    }

    [HttpGet("{id}")] // Route character/id
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
    {
      return Ok(await _characterService.GetCharacterById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
    {      
      return Ok(await _characterService.AddCharacter(newCharacter));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      var response = await _characterService.UpdateCharacter(updatedCharacter);

      if(response.Data == null) return NotFound(response);

      return Ok(response);
    }

    [HttpDelete("{id}")] // Route character/id
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteSingle(int id)
    {
      var response = await _characterService.DeleteCharacter(id);

      if(response.Data == null) return NotFound(response);

      return Ok(response);
    }

    [HttpPost("Skill")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
      return Ok(await _characterService.AddCharacterSkill(newCharacterSkill));
    }
  }
}
