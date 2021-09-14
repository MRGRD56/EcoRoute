using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EcoRoute.MapBox.Interop;
using Microsoft.AspNetCore.Mvc;

namespace EcoRoute.Controllers
{
    [Route("api/[controller]")]
    public class DirectionsController : ControllerBase
    {
        private readonly IMapBoxClient _mapBoxClient;

        public DirectionsController(IMapBoxClient mapBoxClient)
        {
            _mapBoxClient = mapBoxClient;
        }
        
        public async Task<IActionResult> Get(
            [Required] string profile, 
            [Required] string coordinates)
        {
            return Ok(await _mapBoxClient.GetDirectionsAsync(profile, coordinates));
        }
    }
}