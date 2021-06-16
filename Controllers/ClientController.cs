using System.Collections.Generic;
using System.Threading.Tasks;
using Dot_Net_Core_API_with_JWT.Dtos.Client;
using Dot_Net_Core_API_with_JWT.Models;
using Dot_Net_Core_API_with_JWT.Services.ClientService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_Core_API_with_JWT.Controllers
{
  [Authorize(Roles = "Atendente, Admin")]
  [ApiController]
  [Route("[controller]")]
  public class ClientController : ControllerBase
  {
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
      _clientService = clientService;
    }
 
    [HttpGet("getall")]
    public async Task<ActionResult<ServiceResponse<List<GetClientDto>>>> Get()
    {       
      return Ok(await _clientService.GetAllClients());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetClientDto>>> GetSingle(int id)
    {
      return Ok(await _clientService.GetClientById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetClientDto>>>> AddClient(AddClientDto newClient)
    {      
      return Ok(await _clientService.AddClient(newClient));
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetClientDto>>> UpdateClient(UpdateClientDto updateClient)
    {
      var response = await _clientService.UpdateClient(updateClient);

      if(response.Data == null) return NotFound(response);

      return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetClientDto>>>> DeleteSingle(int id)
    {
      var response = await _clientService.DeleteClient(id);

      if(response.Data == null) return NotFound(response);

      return Ok(response);
    }
  }
}
