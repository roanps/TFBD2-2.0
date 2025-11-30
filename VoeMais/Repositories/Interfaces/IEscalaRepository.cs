using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IEscalaRepository
    {
        Task<IEnumerable<Escala>> GetAllAsync();
        Task<Escala> GetByIdAsync(int id);
        Task AddAsync(Escala escala);
        Task UpdateAsync(Escala escala);
        Task DeleteAsync(int id);
    }
}
