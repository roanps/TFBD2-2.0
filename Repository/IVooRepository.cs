using VoeMais.Models;

namespace VoeMais.Repository;
public interface IVooRepository
{
    Task<List<Voo>> GetAll();
    Task<Voo?> GetById(int id);
    Task<Voo?> GetByIdWithEscalas(int id);
    Task Create(Voo item);
    Task Update(Voo item);
    Task Delete(Voo item);
}
