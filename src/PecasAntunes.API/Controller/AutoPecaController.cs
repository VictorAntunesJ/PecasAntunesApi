using Microsoft.AspNetCore.Mvc;
using PecasAntunes.Application.Interfaces;
using PecasAntunes.Application.DTOs;
using PecasAntunes.API.Helpers;

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

    [HttpGet("{error}")]
    public IActionResult TestErro()
    {
        throw new Exception("Erro de teste");
    }
}