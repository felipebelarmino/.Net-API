using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Dot_Net_Core_API_with_JWT.Data;
using Dot_Net_Core_API_with_JWT.Dtos.Client;
using Dot_Net_Core_API_with_JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_Core_API_with_JWT.Services.ClientService
{
  public class clientService : IClientService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public clientService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
      _mapper = mapper;
      _context = context;
    }

    private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    private string GetUserRole() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

    public async Task<ServiceResponse<List<GetClientDto>>> AddClient(AddClientDto newClient)
    {
      var serviceResponse = new ServiceResponse<List<GetClientDto>>();

      Client client = _mapper.Map<Client>(newClient);

      client.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

      _context.Clients.Add(client); //Add client
      await _context.SaveChangesAsync();
      serviceResponse.Data = await _context.Clients
      .Where(c => c.User.Id == GetUserId()) //Retorna todos o clientes do usuário logado
      .Select(c => _mapper.Map<GetClientDto>(c)).ToListAsync();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetClientDto>>> DeleteClient(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetClientDto>>();

      try
      {
        Client client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
        if (client != null)
        {
          _context.Clients.Remove(client);
          await _context.SaveChangesAsync();
          serviceResponse.Data = _context.Clients
            .Where(c => c.User.Id == GetUserId())
            .Select(c => _mapper.Map<GetClientDto>(c)).ToList();
        }
        else
        {
          serviceResponse.Success = false;
          serviceResponse.Message = "Cliente não encontrado.";
        }
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetClientDto>>> GetAllClients()
    {
      var serviceResponse = new ServiceResponse<List<GetClientDto>>();

      var dbClients =
      GetUserRole().Equals("Admin") ? await _context.Clients.ToListAsync()
        :
      await _context.Clients
        .Where(c => c.User.Id == GetUserId()).ToListAsync();
      serviceResponse.Data = dbClients.Select(c => _mapper.Map<GetClientDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetClientDto>> GetClientById(int id)
    {
      var serviceResponse = new ServiceResponse<GetClientDto>();
      var dbClient = await _context.Clients
        .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
      serviceResponse.Data = _mapper.Map<GetClientDto>(dbClient);
      return serviceResponse;
    }
    public async Task<ServiceResponse<GetClientDto>> UpdateClient(UpdateClientDto updatedClient)
    {
      var serviceResponse = new ServiceResponse<GetClientDto>();

      try
      {
        Client client = await _context.Clients
          .Include(c => c.User)
          .FirstOrDefaultAsync(c => c.Id == updatedClient.Id);
        if (client.User.Id == GetUserId())
        {
          client.Name = updatedClient.Name;
          client.Adress = updatedClient.Adress;
          client.Class = updatedClient.Class;
          await _context.SaveChangesAsync();
          serviceResponse.Data = _mapper.Map<GetClientDto>(client);
        }
        else
        {
          serviceResponse.Success = false;
          serviceResponse.Message = "Personagem não encontrado.";
        }
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
