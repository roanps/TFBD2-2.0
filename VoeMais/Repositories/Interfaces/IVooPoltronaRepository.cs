using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IVooPoltronaRepository
    {
        Task<VooPoltrona> GetByIdAsync(int id);
        Task<IEnumerable<VooPoltrona>> GetByVooAsync(int vooId);
        Task UpdateAsync(VooPoltrona vooPoltrona);
        Task AddAsync(VooPoltrona vooPoltrona);

    }
}
