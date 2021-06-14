using System.Collections.Generic;
using System.Threading.Tasks;
using Dot_Net_Core_API_with_JWT.Dtos.Client;
using Dot_Net_Core_API_with_JWT.Models;

namespace Dot_Net_Core_API_with_JWT.Services.ClientService
{
    public interface IClientService
    {
        Task<ServiceResponse<List<GetClientDto>>> GetAllClients(); //Task is for assync

        Task<ServiceResponse<GetClientDto>> GetClientById(int id);

        Task<ServiceResponse<List<GetClientDto>>> AddClient(AddClientDto newClient);        

        Task<ServiceResponse<GetClientDto>> UpdateClient(UpdateClientDto updateClient);

        Task<ServiceResponse<List<GetClientDto>>> DeleteClient(int id);
    }
}
