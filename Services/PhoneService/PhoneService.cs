using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dot_Net_Core_API_with_JWT.Data;
using Dot_Net_Core_API_with_JWT.Dtos.Phone;
using Dot_Net_Core_API_with_JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_Core_API_with_JWT.Services.PhoneService
{
  public class PhoneService : IPhoneService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PhoneService(IMapper mapper, DataContext context)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetPhoneDto>>> AddPhone(AddPhoneDto newPhone)
    {
      var serviceResponse = new ServiceResponse<List<GetPhoneDto>>();

      try
      {
        Phone phone = _mapper.Map<Phone>(newPhone);
        var clientExists = await _context.Clients.FirstOrDefaultAsync(c => c.Id == newPhone.ClientId);

        if (clientExists != null)
        {
          phone.Client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == newPhone.ClientId);
          _context.Phones.Add(phone);
          await _context.SaveChangesAsync();
          serviceResponse.Data = await _context.Phones
          .Where(c => c.Client.Id == newPhone.ClientId)
          .Select(c => _mapper.Map<GetPhoneDto>(c)).ToListAsync();
        }
        else
        {
          serviceResponse.Success = false;
          serviceResponse.Message = "ID do cliente não encontrado.";
        }
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetPhoneDto>>> DeletePhone(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetPhoneDto>>();
      try
      {
        Phone phone = await _context.Phones.FirstAsync(c => c.Id == id);
        _context.Phones.Remove(phone);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _context.Phones.Select(c => _mapper.Map<GetPhoneDto>(c)).ToList();
      }
      catch (Exception ex)
      {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetPhoneDto>>> GetAllPhones()
    {
      var serviceResponse = new ServiceResponse<List<GetPhoneDto>>();
      var dbPhones = await _context.Phones.ToListAsync();
      serviceResponse.Data = dbPhones.Select(c => _mapper.Map<GetPhoneDto>(c)).ToList();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetPhoneDto>> GetPhoneById(int id)
    {
      var serviceResponse = new ServiceResponse<GetPhoneDto>();
      var dbPhones = await _context.Phones.FirstOrDefaultAsync(C => C.Id == id);
      serviceResponse.Data = _mapper.Map<GetPhoneDto>(dbPhones);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetPhoneDto>> UpdatePhone(UpdatePhoneDto updatedPhone)
    {
      var serviceResponse = new ServiceResponse<GetPhoneDto>();
      try
      {
        Phone phone = await _context.Phones.FirstOrDefaultAsync(c => c.Id == updatedPhone.Id);
        phone.PhoneNumber = updatedPhone.PhoneNumber;
        await _context.SaveChangesAsync();
        serviceResponse.Data = _mapper.Map<GetPhoneDto>(phone);
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
