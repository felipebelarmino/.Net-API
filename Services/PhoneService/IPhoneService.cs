using System.Collections.Generic;
using System.Threading.Tasks;
using Dot_Net_Core_API_with_JWT.Dtos.Phone;
using Dot_Net_Core_API_with_JWT.Models;

namespace Dot_Net_Core_API_with_JWT.Services.PhoneService
{
  public interface IPhoneService
  {
    Task<ServiceResponse<List<GetPhoneDto>>> GetAllPhones();

    Task<ServiceResponse<GetPhoneDto>> GetPhoneById(int id);

    Task<ServiceResponse<List<GetPhoneDto>>> AddPhone(AddPhoneDto newPhone);

    Task<ServiceResponse<GetPhoneDto>> UpdatePhone(UpdatePhoneDto updatePhone);

    Task<ServiceResponse<List<GetPhoneDto>>> DeletePhone(int id);
  }
}
