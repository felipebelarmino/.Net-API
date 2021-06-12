using AutoMapper;
using Dot_Net_Core_API_with_JWT.Dtos.Character;
using Dot_Net_Core_API_with_JWT.Models;

namespace Dot_Net_Core_API_with_JWT
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
        }
    }
}