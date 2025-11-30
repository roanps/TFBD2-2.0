using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IEmpresaAereaRepository
    {
        Task<IEnumerable<EmpresaAerea>> GetAllAsync();
        Task<EmpresaAerea> GetByIdAsync(int id);
        Task AddAsync(EmpresaAerea empresa);
        Task UpdateAsync(EmpresaAerea empresa);
        Task DeleteAsync(int id);
    }
}
