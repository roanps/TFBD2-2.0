using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IAviaoRepository
    {
        Task<IEnumerable<Aviao>> GetAllAsync();
        Task<Aviao> GetByIdAsync(int id);
        Task AddAsync(Aviao aviao);
        Task UpdateAsync(Aviao aviao);
        Task DeleteAsync(int id);
    }
}
