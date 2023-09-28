using FluentValidation;
using GamesGen.Model;
using GamesGen.Service;
using Microsoft.AspNetCore.Mvc;

namespace GamesGen.Controllers
{
    [Route("~/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IValidator<Produto> _produtoValidator;

        public ProdutoController(IProdutoService produtoService, IValidator<Produto> produtoValidator)
        {
            _produtoService = produtoService;
            _produtoValidator = produtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _produtoService.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var resposta = await _produtoService.GetById(id);

            if (resposta == null)
            {
                return NotFound();
            }
            return Ok(resposta);
        }


        [HttpGet("nome/{nome}")]
        public async Task<ActionResult> GetByTitulo(string nome)
        {
            return Ok(await _produtoService.GetByName(nome));
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Produto produto)
        {
            var ValidarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!ValidarProduto.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ValidarProduto);

            }

            var Resposta = await _produtoService.Create(produto);
            if (Resposta is null)
            {
                return BadRequest("Categoria nao encontrado");
            }

            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            var buscaProdutos = await _produtoService.GetById(id);
            if (buscaProdutos is null)
            {
                return NotFound("Postagem não Encontrada");
            }
            await _produtoService.Delete(buscaProdutos);


            return NoContent();
        }

    }

}
