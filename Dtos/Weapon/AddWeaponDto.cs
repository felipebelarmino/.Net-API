namespace Dot_Net_Core_API_with_JWT.Dtos.Weapon
{
    public class AddWeaponDto
    {
        public string Name { get; set; }
        public int Damage { get; set;}
        public int CharacterId { get; set; }
    }
}