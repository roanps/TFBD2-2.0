using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IPoltronaRepository
    {
        Task<IEnumerable<Poltrona>> GetAllAsync();
        Task<Poltrona> GetByIdAsync(int id);
        Task AddAsync(Poltrona poltrona);
        Task UpdateAsync(Poltrona poltrona);
        Task DeleteAsync(int id);
    }
}
