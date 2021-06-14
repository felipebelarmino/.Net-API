using System.Text.Json.Serialization;

namespace Dot_Net_Core_API_with_JWT.Models
{
  public enum Cupom
  {
    [JsonConverter(typeof(JsonStringEnumConverter))] //Converts to json string for Swagger
    Nenhum = 0,
    Convenio = 10,
    Funcionario = 20,
    Novo = 30,
  }
}
