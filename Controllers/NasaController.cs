using Demos.API.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Demos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NasaController : Controller
    {

        private readonly ILogger<NasaController> _logger;
        private readonly INasa _nasa;

        public NasaController(ILogger<NasaController> logger, INasa nasa)
        {
            _logger = logger;
            _nasa = nasa;
        }

        [HttpGet]
        public Task<string> Get()
        {
            return _nasa.getNasaInfo();
        }
    }
}
