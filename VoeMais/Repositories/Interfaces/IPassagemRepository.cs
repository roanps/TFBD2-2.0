using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IPassagemRepository
    {
        Task<IEnumerable<Passagem>> GetAllAsync();
        Task<Passagem?> GetByIdAsync(int id);
        Task<IEnumerable<Passagem>> GetByClienteAsync(int clienteId);
        Task AddAsync(Passagem passagem);
        Task UpdateAsync(Passagem passagem);
        Task DeleteAsync(int id);
    }
}
