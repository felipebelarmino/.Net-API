using System.Collections.Generic;

namespace Dot_Net_Core_API_with_JWT.Models
{
  public class Character
  {
    public int Id { get; set; }
    public string Name { get; set; } = "Frodo";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Mage; //Enum
    public User User { get; set; }
    public Weapon Weapon { get; set; } //One to onde relationship
    public List<Skill> Skills{ get; set; } //Many to Many relationship
  }
}
