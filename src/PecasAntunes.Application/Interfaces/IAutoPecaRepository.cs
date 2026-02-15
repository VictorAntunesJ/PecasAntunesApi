using PecasAntunes.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PecasAntunes.Application.Interfaces
{
    public interface IAutoPecaRepository
    {
        Task<AutoPeca> AddAsync(AutoPeca autoPeca);
        Task<IEnumerable<AutoPeca>> GetAllAsync();
        Task<AutoPeca?> GetByIdAsync(int id);
        Task<AutoPeca> UpdateAsync(AutoPeca autoPeca);
        Task<bool> DeleteAsync(int id);
        Task<string?> ObterUltimoCodigoInternoAsync();
    }
}
