using Dot_Net_Core_API_with_JWT.Dtos.Weapon;
using Dot_Net_Core_API_with_JWT.Models;

namespace Dot_Net_Core_API_with_JWT.Dtos.Character
{
  public class GetCharacterDto
  {
    public int Id { get; set; }
    public string Name { get; set; } = "Frodo";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Knight; //Enum 
    public GetWeaponDto Weapon { get; set; }
  }
}