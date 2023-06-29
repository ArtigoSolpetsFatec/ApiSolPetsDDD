using ApiSolPetsDDD.Domain.Exceptions;
using ApiSolPetsDDD.Domain.Interfaces;
using ApiSolPetsDDD.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiSolPetsDDD.WebAPI.Controllers
{
    [Produces("application/json")]
    public class PedidoController : Controller
    {
        private readonly IPedidoService pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            this.pedidoService = pedidoService;
        }

        [SwaggerOperation(Summary = "Realiza a busca de um pedido através de seu id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/pedidos/busca-pedido/{idPedido}")]
        [Authorize]
        public async Task<IActionResult> GetPedidosById(int idPedido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.GetPedidosById(idPedido);
                if (result == null)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de todos os pedidos através de um idCliente")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Pedido>))]
        [SwaggerResponse(204, "Pedidos não encontrados!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/pedidos/busca-pedidos-cliente/{idCliente}")]
        [Authorize]
        public async Task<IActionResult> GetPedidosByIdCliente(int idCliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.GetPedidosByIdCliente(idCliente);
                if (result == null || result.Count == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de todos os pedidos do dia corrente")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Pedido>))]
        [SwaggerResponse(204, "Pedidos não encontrados!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/pedidos/busca-pedidos-dia")]
        [Authorize]
        public async Task<IActionResult> GetPedidosDia()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.GetPedidosDia();
                if (result == null || result.Count == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de todos os pedidos do mês corrente")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Pedido>))]
        [SwaggerResponse(204, "Pedidos não encontrados!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/pedidos/busca-pedidos-mes")]
        [Authorize]
        public async Task<IActionResult> GetPedidosMes()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.GetPedidosMes();
                if (result == null || result.Count == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Realiza a busca de todos os pedidos do ano corrente")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(List<Pedido>))]
        [SwaggerResponse(204, "Pedidos não encontrados!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpGet]
        [Route("/pedidos/busca-pedidos-ano")]
        [Authorize]
        public async Task<IActionResult> GetPedidosAno()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.GetPedidosAno();
                if (result == null || result.Count == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Efetua o cadastro de um pedido")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPost]
        [Route("/pedido")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PostPedido(pedido);
                if (result == null || result.IdPedido == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Finaliza um pedido")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPatch]
        [Route("/pedido/finalizar-pedido/status/{statusVenda}/idPedido/{idPedido}/finalizado/{finalizado}")]
        [Authorize]
        public async Task<IActionResult> Patch(char statusVenda, int idPedido, bool finalizado)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PatchFinalizarPedido(statusVenda, idPedido, finalizado);
                if (result == null || result.IdPedido == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Cancela um pedido")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(int))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPatch]
        [Route("/pedido/cancelar-pedido/idPedido/{idPedido}")]
        [Authorize]
        public async Task<IActionResult> Patch(int idPedido, bool finalizado = false, char statusVenda = 'C')
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PatchCancelarPedido(statusVenda, idPedido, finalizado);
                if (result == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Vincula um cliente ao pedido")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPatch]
        [Route("/pedido/vincula-cliente/idPedido/{idPedido}/idCliente/{idCliente}")]
        [Authorize]
        public async Task<IActionResult> Patch(int idPedido, int idCliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PatchVincularClientePedido(idPedido, idCliente);
                if (result == null || result.IdPedido == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Exclui produto de um pedido e atualiza seu estoque")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPatch]
        [Route("/pedido/remove-produto/idPedido/{idPedido}/idProduto/{idProduto}/qtdeProduto/{qtdeProduto}")]
        [Authorize]
        public async Task<IActionResult> Patch(int idPedido, int idProduto, int qtdeProduto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PatchRemoveProdutoPedido(idPedido, idProduto, qtdeProduto);
                if (result == null || result.IdPedido == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Atualiza a quantidade de um produto no pedido")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPatch]
        [Route("/pedido/atualiza-qtde-produto/idPedido/{idPedido}/idProduto/{idProduto}/qtdeProduto/{qtdeProduto}/totalVenda/{totalVenda}")]
        [Authorize]
        public async Task<IActionResult> Patch(int idPedido, int idProduto, int qtdeProduto, double totalVenda)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PatchAtualizaQtdeProdutoPedido(idPedido, idProduto, qtdeProduto, totalVenda);
                if (result == null || result.IdPedido == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Adiciona um produto a um pedido já iniciado")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPatch]
        [Route("/pedido/adiciona-produto")]
        [Authorize]
        public async Task<IActionResult> Patch([FromBody] PedidoProduto pedidoProduto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PatchAdicionarProdutoPedido(pedidoProduto);
                if (result == null || result.IdPedido == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Atualiza o valor total do pedido e o valor de desconto aplicado")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPatch]
        [Route("/pedido/atualiza-total/idPedido/{idPedido}/totalVenda/{totalVenda}/valorDesconto/{valorDesconto}")]
        [Authorize]
        public async Task<IActionResult> Patch(int idPedido, double totalVenda, double valorDesconto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PatchAtualizaDesconTotalVenda(idPedido, totalVenda, valorDesconto);
                if (result == null || result.IdPedido == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Atualiza o valor de produtos especificos e o valor total da venda")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(Pedido))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPatch]
        [Route("/pedido/atualiza-valor-produto/idPedido/{idPedido}/idProduto/{idProduto}/totalVenda/{totalVenda}/valorProduto/{valorProduto}")]
        [Authorize]
        public async Task<IActionResult> Patch(int idPedido, int idProduto, double totalVenda, double valorProduto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PatchAtualizaValorProduto(idPedido, idProduto, totalVenda, valorProduto);
                if (result == null || result.IdPedido == 0)
                    return NoContent();
                return Ok(result);
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Atualiza os dados de um pedido")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(string))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpPut]
        [Route("/pedido/atualizar-pedido")]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] Pedido pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.PutPedido(pedido);
                if (result == 0)
                    return NoContent();
                return Ok("Dados atualizados com sucesso!");
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }

        [SwaggerOperation(Summary = "Exclui os dados de um pedido através de seu id")]
        [SwaggerResponse(200, "Executado com sucesso!", typeof(string))]
        [SwaggerResponse(204, "Pedido não encontrado!")]
        [SwaggerResponse(401, "Requisição requer autenticação")]
        [SwaggerResponse(412, "Exceção de negócio!")]
        [SwaggerResponse(500, "Erro Interno!")]
        [HttpDelete]
        [Route("/pedidos/exclui-pedido/{idPedido}")]
        [Authorize]
        public async Task<IActionResult> Delete(int idPedido)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, ModelState.Values.Select(
                        x => x.Errors.Select(y => y.Exception != null ? y.Exception.Message : y.ErrorMessage)));
                }

                var result = await pedidoService.DeletePedido(idPedido);
                if (result == 0)
                    return NoContent();
                return Ok("Dados excluídos com sucesso!");
            }
            catch (ExcecaoNegocio ex)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro Interno : {ex.Message}");
            }
        }
    }
}
