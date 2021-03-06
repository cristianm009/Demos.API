﻿using Demos.API.Application.Contracts;
using Demos.API.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Demos.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
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
            return _nasa.getDONKIInfo();
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public Task<string> GetV2_0()
        {
            return _nasa.getInSightInfo();
        }

        [HttpGet]
        [MapToApiVersion("3.0")]
        public Task<string> GetV3_0([FromServices] IHttpClientFactory factory)
        {
            return _nasa.getInSightInfo(factory);
        }
        [HttpGet]
        [MapToApiVersion("4.0")]
        public Task<string> GetV4_0([FromServices] CustomHttpClient client)
        {
            return client.getInSightInfoTyped();
        }
    }
}
