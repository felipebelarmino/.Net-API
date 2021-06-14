using System.Collections.Generic;

namespace Dot_Net_Core_API_with_JWT.Models
{
  public class Client
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; } 
    public Cupom Class { get; set; } = Cupom.Nenhum; //Enum
    public User User { get; set; }
  }
}
