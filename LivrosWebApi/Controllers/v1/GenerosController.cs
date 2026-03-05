using Asp.Versioning;
using LivrosWebApi.Core.Contratcs.Services;
using LivrosWebApi.Core.Dtos.Requests.Generos;
using Microsoft.AspNetCore.Mvc;

namespace LivrosWebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
   // [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class GenerosController : ApiBaseController
    {

        private readonly IGeneroService _generoService;

        public GenerosController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterGeneros()
        {
            var result = await _generoService.ObterTodosAsync();
            return ResultResponse(result);
        }

        [HttpGet("{generoId}")]
        public async Task<IActionResult> ObterGenero([FromRoute]int generoId)
        {
            var result = await _generoService.ObterTodosAsync();
            return ResultResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarGenero([FromBody] CadastroGeneroRequest cadastroGenero)
        {
            var resultCadastro = await _generoService.AdicionarAsync(cadastroGenero);
            return ResultResponse(resultCadastro);
        }

        [HttpPut("{generoId}")]
        public async Task<IActionResult> AtualizarGenero([FromBody] CadastroGeneroRequest cadastroGenero, [FromRoute] int generoId)
        {
            cadastroGenero.Id = generoId;
            var resultCadastro = await _generoService.AtualizarAsync(cadastroGenero);
            return ResultResponse(resultCadastro);
        }

    }
}
