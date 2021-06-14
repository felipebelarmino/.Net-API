using Dot_Net_Core_API_with_JWT.Models;

namespace Dot_Net_Core_API_with_JWT.Dtos.Client
{
  public class AddClientDto
  {
    public string Name { get; set; }
    public string Adress { get; set; }
    public Cupom Class { get; set; } = Cupom.Novo; //Enum
  }
}