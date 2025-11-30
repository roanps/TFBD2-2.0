using VoeMais.Models;

namespace VoeMais.Repositories.Interfaces
{
    public interface IPassagemRepository
    {
        Task<Passagem> GetByIdAsync(int id);
        Task AddAsync(Passagem passagem);
        Task<IEnumerable<Passagem>> GetByClienteAsync(int clienteId);
    }
}
