using System.Text.Json.Serialization;

namespace Dot_Net_Core_API_with_JWT.Models
{
  public enum RpgClass
  {
    [JsonConverter(typeof(JsonStringEnumConverter))] //Converts to json string for Swagger
    Knight = 1,
    Mage = 2,
    Cleric = 3,
  }
}
