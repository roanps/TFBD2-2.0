using VoeMais.Models;

namespace VoeMais.Repository;
public interface IAeroportoRepository
{
    Task<List<Aeroporto>> GetAll();
    Task<Aeroporto?> GetById(int id);
    Task Create(Aeroporto item);
    Task Update(Aeroporto item);
    Task Delete(Aeroporto item);
}
