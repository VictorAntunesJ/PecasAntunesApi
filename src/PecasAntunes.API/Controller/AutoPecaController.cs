using Microsoft.AspNetCore.Mvc;
using PecasAntunes.Application.Interfaces;
using PecasAntunes.Application.DTOs;
using PecasAntunes.API.Helpers;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices.Marshalling;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Reflection.Metadata;
using System.Reflection;
using PecasAntunes.Domain.Entities;
using System.Data.SqlTypes;

namespace PecasAntunes.Api.Controllers;

/// <summary>
/// Gerencia operações relacionadas às autopeças
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
public class AutoPecasController : ControllerBase
{
    private readonly IAutoPecaService _service;
    public AutoPecasController(IAutoPecaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cadastra uma nova autopeça
    /// </summary>
    /// <remarks>
    /// O código interno é gerado automaticamente pelo sistema.
    /// </remarks>
    /// <response code="200">Peça cadastrada com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] AutoPecaCreateDto dto)
    {
        var result = await _service.CriarAsync(dto);

        return Ok(ApiResponse<AutoPecaResponseDto>
            .CreateSuccessResponse(result));
    }

    /// <summary>
    /// Lista todas as autopeças cadastradas
    /// </summary>
    ///<remarks>
    /// Retorna uma coleção com todas as peças registradas no sistema.
    ///</remarks>
    /// <response code="200">Lista de peças retornada com sucesso</response>
    /// <response code="404">Nenhuma peça encontrada</response>
    /// <response code="400">Dados inválidos</response>
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var lista = await _service.ListarTodasAsync();

        return Ok(ApiResponse<IEnumerable<AutoPecaResponseDto>>
        .CreateSuccessResponse(lista));
    }

    /// <summary>
    /// Busca uma autopeça pelo ID
    /// </summary>
    /// <param name="id">Identificador da peça</param>
    /// <remarks>
    /// Retorna os dados completos da peça caso ela exista.
    /// </remarks>
    /// <response code="200">Peça encontrada com sucesso.</response>
    /// <response code="404">Peça não encontrada</response>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var autoPeca = await _service.BuscarPorIdAsync(id);

        var response = ApiResponse<AutoPecaResponseDto>
            .CreateSuccessResponse(autoPeca);

        return Ok(response);
    }

    /// <summary>
    /// Atualiza uma autopeça existente
    /// </summary>
    /// <param name="id">Identificador da peça</param>
    /// <param name="dto">Dados atualizados da peça</param>
    /// <response code="200">Peça atualizada com sucesso</response>
    /// <response code="400">Dados inválidos.</response>
    /// <response code="404">Peça não encontrada.</response>

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AutoPecaUpdateDto dto)
    {
        await _service.AtualizarAsync(id, dto);

        return Ok(new
        {
            success = true,
            message = "Peça atualizada com sucesso"
        });
    }
    /// <summary>
    /// Remove uma autopeça do sistema
    /// </summary>
    /// <param name="id">Identificador da peça</param>
    /// <response code="204">Peça removida com sucesso</response>
    /// <response code="404">Peça não encontrada</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(int id)
    {
        await _service.DeletarAsync(id);

        return Ok(new
        {
            success = true,
            message = "Peça removida com sucesso"
        });
    }



#if DEBUG
    [HttpGet("erro-teste")]
    public IActionResult TestErro()
    {
        throw new Exception("Erro de teste");
    }
#endif

}