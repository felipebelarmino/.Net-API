using System;
using System.Collections.Generic;

namespace Dot_Net_Core_API_with_JWT.Models
{
  public class Client
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }
    public Cupom Class { get; set; } = Cupom.NenhumDesconto;
    public User User { get; set; }
    public List<Phone> Phones { get; set; }
  }
}
