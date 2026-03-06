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
    public class LivrosController : ApiBaseController
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterLivros()
        {
            var result = await _livroService.ObterTodosAsync();
            return ResultResponse(result);
        }

        [HttpGet("{livroId}")]
        public async Task<IActionResult> ObterGenero([FromRoute] int livroId)
        {
            var result = await _livroService.ObterPorIdAsync(livroId);
            return ResultResponse(result);
        }

        [HttpPut("{livroId}")]
        public async Task<IActionResult> AtualizarGenero([FromBody] CadastroLivroRequest cadastroLivro, [FromRoute] int livroId)
        {
            cadastroLivro.Id = livroId;
            var resultCadastro = await _livroService.AtualizarAsync(cadastroLivro);
            return ResultResponse(resultCadastro);
        }



        [HttpPost]
        public async Task<IActionResult> AdicionarAutor([FromBody] CadastroLivroRequest cadastroLivro)
        {
            var resultCadastro = await _livroService.AdicionarAsync(cadastroLivro);
            return ResultResponse(resultCadastro);
        }

        [HttpDelete("{livroId}")]
        public async Task<IActionResult> RemoverLivro([FromRoute] int livroId)
        {

            var resultCadastro = await _livroService.DeleteAsync(livroId);
            return ResultResponse(resultCadastro);

        }
    }
}
