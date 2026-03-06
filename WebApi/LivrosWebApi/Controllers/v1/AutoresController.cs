using Asp.Versioning;
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


        [HttpPost]
        public async Task<IActionResult> AdicionarGenero([FromBody] CadastroAutorRequest cadastroGenero)
        {
            var resultCadastro = await _autorService.AdicionarAsync(cadastroGenero);
            return ResultResponse(resultCadastro);
        }
    }
}
