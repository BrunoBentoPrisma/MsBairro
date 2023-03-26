using Microsoft.AspNetCore.Mvc;
using MsBairro.Dtos;
using MsBairro.Repositorys.Entidades;
using MsBairro.Services.Interfaces;

namespace MsBairro.Controllers
{
    public class BairroController : Controller
    {
        private readonly IBairroService _bairroService;
        public BairroController(IBairroService serviceBairro)
        {
            _bairroService = serviceBairro;
        }
        [HttpPost("/api/AdicionarBairro")]
        public async Task<IActionResult> AdicionarBairro([FromBody] BairroDto bairroDto)
        {
            try
            {
                var result = await this._bairroService.AddBairro(bairroDto);

                if (!result) return BadRequest("Ocorreu um erro interno ao adicionar o bairro.");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("/api/EditarBairro")]
        public async Task<IActionResult> EditarBairro([FromBody] Bairro bairro)
        {
            try
            {
                var result = await this._bairroService.UpdateBairro(bairro);

                if(!result) return BadRequest("Ocorreu um erro interno ao editar o bairro!");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/RetornaBairroPorId/{id:int}")]
        public async Task<IActionResult> RetornaBairroPorId(int id)
        {
            try
            {
                var bairro = await this._bairroService.GetById(id);

                return Ok(bairro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("/api/ExcluirBairro/{id:int}")]
        public async Task<IActionResult> ExcluirBairro(int id)
        {
            try
            {
                var result = await this._bairroService.DeleteBairro(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/ListaBairro")]
        public async Task<IActionResult> ListaBairro()
        {
            try
            {
                var bairros = await this._bairroService.ListaBairro();

                return Ok(bairros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/api/ListaPaginacaoBairro/{pagina}/{query?}")]
        public async Task<IActionResult> ListaPaginacao(int pagina, string query = "")
        {
            try
            {
                var paginacao = await this._bairroService.GetPaginacao(pagina, query);

                if (paginacao == null) return Json(BadRequest());

                return Ok(paginacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
