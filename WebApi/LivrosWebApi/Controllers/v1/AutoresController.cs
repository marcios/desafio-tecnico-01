using Asp.Versioning;
using LivrosWebApi.Application.Services;
using LivrosWebApi.Core.Contratcs.Services;
using LivrosWebApi.Core.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LivrosWebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AutoresController : ApiBaseController
    {
        private readonly IAutorService _autorService;

        public AutoresController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterAutores()
        {
            var result = await _autorService.ObterTodosAsync();
            return ResultResponse(result);
        }

        [HttpGet("{autorId}")]
        public async Task<IActionResult> ObterGenero([FromRoute] int autorId)
        {
            var result = await _autorService.ObterPorIdAsync(autorId);
            return ResultResponse(result);
        }


        [HttpPost]
        public async Task<IActionResult> AdicionarAutor([FromBody] CadastroAutorRequest cadastroGenero)
        {
            var resultCadastro = await _autorService.AdicionarAsync(cadastroGenero);
            return ResultResponse(resultCadastro);
        }

        [HttpPut("{autorId}")]
        public async Task<IActionResult> AtualizarAutor([FromBody] CadastroAutorRequest cadastroAutor, [FromRoute] int autorId)
        {
            cadastroAutor.Id = autorId;
            var resultCadastro = await _autorService.AtualizarAsync(cadastroAutor);
            return ResultResponse(resultCadastro);
        }

        [HttpDelete("{autorId}")]
        public async Task<IActionResult> RemoverAutor([FromRoute] int autorId)
        {

            var resultCadastro = await _autorService.DeleteAsync(autorId);
            return ResultResponse(resultCadastro);

        }
    }
}
