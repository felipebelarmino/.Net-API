using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot_Net_Core_API_with_JWT.Models;

namespace Dot_Net_Core_API_with_JWT.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private static List<Character> characters = new List<Character> //Return List using model Character
    {
      new Character(),
      new Character { Id = 1, Name = "Sam" } //Creating a new Character
    };

    public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
    {
      var serviceResponse = new ServiceResponse<List<Character>>();
      characters.Add(newCharacter);
      serviceResponse.Data = characters;
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
    {
      var serviceResponse = new ServiceResponse<List<Character>>();
      serviceResponse.Data = characters;
      return serviceResponse;
    }

    public async Task<ServiceResponse<Character>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<Character>();
      serviceResponse.Data = characters.FirstOrDefault(c => c.Id == id);
      return serviceResponse;
    }
  }
}