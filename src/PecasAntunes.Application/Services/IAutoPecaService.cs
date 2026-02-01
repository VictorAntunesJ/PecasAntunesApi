using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PecasAntunes.Application.DTOs;

namespace PecasAntunes.Application.Interfaces;

public interface IAutoPecaService
{
    Task<AutoPecaResponseDto> CriarAsync(AutoPecaCreateDto dto);
    Task<IEnumerable<AutoPecaResponseDto>> ListarTodasAsync();
    Task<AutoPecaResponseDto> BuscarPorIdAsync(int id);
    Task AtualizarAsync(int id, AutoPecaUpdateDto dto);
}