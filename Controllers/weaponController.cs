using System.Threading.Tasks;
using Dot_Net_Core_API_with_JWT.Dtos.Character;
using Dot_Net_Core_API_with_JWT.Dtos.Weapon;
using Dot_Net_Core_API_with_JWT.Models;
using Dot_Net_Core_API_with_JWT.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_Core_API_with_JWT.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class weaponController : ControllerBase
  {
    private readonly IWeaponService _weaponService;
    public weaponController(IWeaponService weaponService)
    {
      _weaponService = weaponService;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto newWeapon)
    {
        return Ok(await _weaponService.AddWeapon(newWeapon));
    }

  }
}