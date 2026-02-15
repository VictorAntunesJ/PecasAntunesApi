using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using PecasAntunes.Application.Interfaces;
using PecasAntunes.Domain.Entities;
using PecasAntunes.Infrastructure.Data; 

namespace PecasAntunes.Infrastructure.Repositories;

public class AutoPecaRepository : IAutoPecaRepository
{
    private readonly AppDbContext _context;

    public AutoPecaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AutoPeca> AddAsync(AutoPeca autoPeca) 
    {
        await _context.AutoPecas.AddAsync(autoPeca);
        await _context.SaveChangesAsync();
        return autoPeca; 
    }

    public async Task<IEnumerable<AutoPeca>> GetAllAsync()
    {
        return await _context.AutoPecas.ToListAsync();
    }

    public async Task<AutoPeca?> GetByIdAsync(int id) 
    {
        return await _context.AutoPecas.FindAsync(id);
    }

    public async Task<AutoPeca> UpdateAsync(AutoPeca autoPeca) 
    {
        _context.AutoPecas.Update(autoPeca);
        await _context.SaveChangesAsync();
        return autoPeca;
    }

    public async Task<bool> DeleteAsync(int id) 
    {
        var peca = await GetByIdAsync(id);
        if (peca == null) return false;

        _context.AutoPecas.Remove(peca);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string?> ObterUltimoCodigoInternoAsync()
    {
        return await _context.AutoPecas
        .OrderByDescending(p => p.Id)
        .Select(p => p.CodigoInterno)
        .FirstOrDefaultAsync();
    }
}