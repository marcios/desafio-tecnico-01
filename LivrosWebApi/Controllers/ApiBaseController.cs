using LivrosWebApi.Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LivrosWebApi.Controllers
{
    
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult ResultResponse(ResultDto resultDto)
        {
            var method = HttpContext.Request.Method;
            return method switch
            {
                "GET" => ResponseGet(resultDto),
                "POST" => ResponsePost(resultDto),
                _ => NotFound()
            };
        }

        private IActionResult ResponsePost(ResultDto dto)
        {
            if(dto.Notificacoes.Any())
                return BadRequest(dto.Notificacoes);


            return new ObjectResult(dto.Data) { StatusCode = StatusCodes.Status201Created };

        }

        private IActionResult ResponseGet(ResultDto dto)
        {
            if(dto.Data == null || dto.Notificacoes.Any())
                return NotFound(dto);  

            return Ok(dto);

        }
    }
}
