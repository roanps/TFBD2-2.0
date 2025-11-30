using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IAeroportoRepository
    {
        Task<IEnumerable<Aeroporto>> GetAllAsync();
        Task<Aeroporto> GetByIdAsync(int id);
        Task AddAsync(Aeroporto aeroporto);
        Task UpdateAsync(Aeroporto aeroporto);
        Task DeleteAsync(int id);
    }
}
