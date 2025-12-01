using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IVooPoltronaRepository
    {
        Task<IEnumerable<VooPoltrona>> GetAllAsync();
        Task<VooPoltrona?> GetByIdAsync(int id);
        Task<IEnumerable<VooPoltrona>> GetByVooAsync(int vooId);
        Task AddAsync(VooPoltrona vooPoltrona);
        Task UpdateAsync(VooPoltrona vooPoltrona);
        Task DeleteAsync(int id);
    }
}
