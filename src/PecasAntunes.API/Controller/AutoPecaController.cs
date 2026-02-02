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

[ApiController]
[Route("api/[controller]")]
public class AutoPecasController : ControllerBase
{
    private readonly IAutoPecaService _service;
    public AutoPecasController(IAutoPecaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(AutoPecaCreateDto dto)
    {
        var result = await _service.CriarAsync(dto);

        var response = ApiResponse<AutoPecaResponseDto>
            .CreateSuccessResponse(result);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var lista = await _service.ListarTodasAsync();

        return Ok(ApiResponse<IEnumerable<AutoPecaResponseDto>>
        .CreateSuccessResponse(lista));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var autoPeca = await _service.BuscarPorIdAsync(id);

        var response = ApiResponse<AutoPecaResponseDto>
            .CreateSuccessResponse(autoPeca);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, AutoPecaUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID da rota diferente do body");

        await _service.AtualizarAsync(id, dto);

        return Ok(new
        {
            success = true,
            message = "Peça atualizada com sucesso"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        await _service.DeletarAsync(id);

        return Ok(new
        {
            success = true,
            message = "Peça removida com sucesso"
        });
    }



    [HttpGet("{erro-teste}")]
    public IActionResult TestErro()
    {
        throw new Exception("Erro de teste");
    }
}