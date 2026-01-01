using Microsoft.AspNetCore.Mvc;
using PecasAntunes.Application.Interfaces;
using PecasAntunes.Application.DTOs;

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
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var lista = await _service.ListarTodasAsync();
        return Ok(lista);
    }
}