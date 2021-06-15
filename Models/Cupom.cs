using System.Text.Json.Serialization;

namespace Dot_Net_Core_API_with_JWT.Models
{
  [JsonConverter(typeof(JsonStringEnumConverter))] 
  public enum Cupom
  {    
    NenhumDesconto = 0,
    ConvenioDesconto10 = 10,
    FuncionarioDesconto20 = 20,
    NovoDesconto30 = 30,
  }
}
