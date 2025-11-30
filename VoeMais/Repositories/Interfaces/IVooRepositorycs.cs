using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IVooRepository
    {
        Task<IEnumerable<Voo>> GetAllAsync();
        Task<Voo> GetByIdAsync(int id);
        Task AddAsync(Voo voo);
        Task UpdateAsync(Voo voo);
        Task DeleteAsync(int id);
    }
}
