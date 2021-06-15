using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot_Net_Core_API_with_JWT.Dtos.Phone;
using Dot_Net_Core_API_with_JWT.Models;
using Dot_Net_Core_API_with_JWT.Services.PhoneService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_Core_API_with_JWT.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class PhoneController : ControllerBase
  {
    private readonly IPhoneService _phoneService;

    public PhoneController(IPhoneService phoneService)
    {
      _phoneService = phoneService;

    }
    private readonly IPhoneService phoneService;

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetPhoneDto>>>> Get()
    {
      return Ok(await _phoneService.GetAllPhones());
    }

    [HttpGet("GetAllByClientId")]
    public async Task<ActionResult<ServiceResponse<List<GetPhoneDto>>>> GetAllPhonesByClientId(int clientId)
    {
      return Ok(await _phoneService.GetAllPhonesByClientId(clientId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetPhoneDto>>> GetSingle(int id)
    {
      return Ok(await _phoneService.GetPhoneById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetPhoneDto>>> AddPhone(AddPhoneDto newPhone)
    {
      return Ok(await _phoneService.AddPhone(newPhone));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetPhoneDto>>> UpdatePhone(UpdatePhoneDto updatedPhone)
    {
      var response = await _phoneService.UpdatePhone(updatedPhone);
      if (response.Data == null) return NotFound(response);
      return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetPhoneDto>>>> Delete(int id)
    {
      var response = await _phoneService.DeletePhone(id);
      if (response.Data == null) return NotFound(response);
      return Ok(response);
    }
  }
}
