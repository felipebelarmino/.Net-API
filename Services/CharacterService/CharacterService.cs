using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dot_Net_Core_API_with_JWT.Dtos.Character;
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
    private readonly IMapper _mapper;

    public CharacterService(IMapper mapper)
    {
      _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      Character character = _mapper.Map<Character>(newCharacter);
      character.Id = characters.Max(c => c.Id) + 1; //Counts N of id´s and Add plus 1
      characters.Add(character); //Add character
      serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      try
      {
        Character character = characters.First(c => c.Id == id);
        characters.Remove(character);
        serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
      serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();
      serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(c => c.Id == id));
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();

      try
      {
        Character character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
        character.Name = updatedCharacter.Name;
        character.HitPoints = updatedCharacter.HitPoints;
        character.Strength = updatedCharacter.Strength;
        character.Defense = updatedCharacter.Defense;
        character.Intelligence = updatedCharacter.Intelligence;
        character.Class = updatedCharacter.Class;
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }
  }
}