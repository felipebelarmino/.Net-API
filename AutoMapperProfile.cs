using AutoMapper;
using Dot_Net_Core_API_with_JWT.Dtos.Client;
using Dot_Net_Core_API_with_JWT.Dtos.Phone;
using Dot_Net_Core_API_with_JWT.Models;
namespace Dot_Net_Core_API_with_JWT
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Client, GetClientDto>();
      CreateMap<AddClientDto, Client>();

      CreateMap<Phone, GetPhoneDto>();
      CreateMap<AddPhoneDto, Phone>();
    }
  }
}
