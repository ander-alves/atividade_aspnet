using FluentValidation;
using GamesGen.Model;
using GamesGen.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesGen.Controllers
{
    [Authorize]
    [Route("~/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IValidator<Categoria> _categoriaValidator;

        public CategoriaController(ICategoriaService categoriaService, IValidator<Categoria> categoriaValidator)
        {
            _categoriaService = categoriaService;
            _categoriaValidator = categoriaValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoriaService.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var resposta = await _categoriaService.GetById(id);

            if (resposta == null)
            {
                return NotFound();
            }
            return Ok(resposta);
        }


        [HttpGet("nome/{nome}")]
        public async Task<ActionResult> GetByTitulo(string nome)
        {
            return Ok(await _categoriaService.GetByName(nome));
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Categoria categoria)
        {
            var validarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!validarCategoria.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validarCategoria);
            }

            await _categoriaService.Create(categoria);

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            var buscaCategoria = await _categoriaService.GetById(id);
            if (buscaCategoria is null)
            {
                return NotFound("Postagem não Encontrada");
            }
            await _categoriaService.Delete(buscaCategoria);


            return NoContent();
        }

    }

}
