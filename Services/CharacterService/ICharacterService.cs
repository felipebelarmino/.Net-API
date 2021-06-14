using System.Collections.Generic;
using System.Threading.Tasks;
using Dot_Net_Core_API_with_JWT.Dtos.Character;
using Dot_Net_Core_API_with_JWT.Models;

namespace Dot_Net_Core_API_with_JWT.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(); //Task is for assync

        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);

        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);

        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);

        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);

        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
    }
}