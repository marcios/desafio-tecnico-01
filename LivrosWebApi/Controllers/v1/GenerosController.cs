using Asp.Versioning;
using LivrosWebApi.Core.Contratcs.Services;
using Microsoft.AspNetCore.Mvc;

namespace LivrosWebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
   // [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class GenerosController : ControllerBase
    {

        private readonly IGeneroService _generoService;

        public GenerosController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterGeneros()
        {
            var generos = await _generoService.ObterTodosAsync();
            return Ok(generos);
        }

    }
}
